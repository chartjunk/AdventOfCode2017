open System
open RecTowers
open ClipboardUtils
open StringUtils
open FunctionalUtils
open CollectionUtils
open System.Collections.Generic

let rec getUnbalancedChildren graph (wghts:IDictionary<string,int>) n =
    function
    | None -> 
        let selfWeight = item wghts n
        let childWeights = item graph n |> List.map (flip (getUnbalancedChildren graph wghts) None)
        n, (selfWeight + (childWeights |> List.sumBy snd3)), match childWeights with

        // Unbalanced level already found.
        | _ when childWeights |> List.exists (thd >> Option.isSome) -> childWeights |> List.find (thd >> Option.isSome) |> thd

        // This is the unbalanced level.
        | _ when childWeights |> List.distinctBy snd3 |> List.length > 1 -> Some(childWeights |> List.map (both fst3 snd3))

        // Unbalanced level nout yet found.
        | _ -> None
    | u -> n, 0, u

[<EntryPoint;STAThread>]
let main argv = 
    splitCleanList newline 
    >> List.map extractRow
    >> both 
        (List.map(both fst3 (snd3 >> int)) >> dict) 
        (List.map(both fst3 thd) >> dict)
    >> (fun (wghts, graph) ->
                        
        getUnbalancedChildren graph wghts (getRoot graph) None
    ) 
    >> string
    |> rotateClipboard
    0

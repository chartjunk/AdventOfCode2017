open System
open RecTowers
open ClipboardUtils
open StringUtils
open FunctionalUtils
open CollectionUtils

let rec getUnbalancedTotalWeights graph wghts n =
    function
    | None -> 
        let weight = item wghts n
        let children, cWeights = 
            item graph n 
            |> List.map (flip (getUnbalancedTotalWeights graph wghts) None)
            |> fw (List.map snd3)

        n, weight + (cWeights |> Seq.sum),
        match cWeights, children |> Seq.tryPick thd with
        // This is the unbalanced level.
        | _::x::y::_, None when x<>y -> children |> Seq.map(both fst3 snd3) |> Some
        | _, u -> u
    // The unbalanced level already found in a cousin branch.
    | u -> n, 0, u

[<EntryPoint;STAThread>]
let main argv = 
    splitCleanList newline 
    >> List.map extractRow
    >> both 
        (List.map(both fst3 (snd3 >> int)) >> dict) 
        (List.map(both fst3 thd) >> dict)
    >> (fun (wghts, graph) ->
                        
        match getUnbalancedTotalWeights graph wghts (getRoot graph) None |> thd with
        | Some(u) -> 
            let (unbW, _), (bW, _) = u |> Seq.countBy snd |> both (Seq.minBy snd) (Seq.maxBy snd)
            let unbName = u |> Seq.find (snd >> (=) unbW) |> fst
            bW - unbW + item wghts unbName |> Some
        | None -> None
    ) 
    >> string
    |> rotateClipboard
    0

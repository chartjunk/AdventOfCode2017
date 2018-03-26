open System
open ClipboardUtils
open FunctionalUtils
open StringUtils

let rec cyclic src = seq { yield! src; yield! cyclic src }

let rec getCheckSum src skip offset =
    function
    | take::ls ->
        let srcLen = src |> List.length
        let skip = ((skip % srcLen) + srcLen) % srcLen
        let csrc = cyclic src        
        let part = 
            csrc 
            |> Seq.skip skip 
            |> Seq.take take 
            |> Seq.toList
            |> List.rev
            |> List.indexed
            |> List.map(both (fst>>(+)skip>>(flip(%)srcLen)) snd)
            |> Map.ofList
        let src2 = 
            src
            |> List.indexed
            |> List.map (
                function
                | (ix, _) when part |> Map.containsKey ix -> part.Item ix
                | (_, s) -> s)
        getCheckSum src2 (skip+take+offset) (offset+1) ls

    | [] -> 
        [0;1]
        |> List.map (flip Seq.item src)
        |> List.reduce (*)

[<EntryPoint;STAThread>]
let main argv =
    let src = seq{0..255} |> List.ofSeq
    splitCleanList ","
    >> List.map int
    >> getCheckSum src 0 0
    >> string
    |> rotateClipboard
    0

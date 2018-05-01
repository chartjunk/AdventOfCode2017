open ClipboardUtils
open System
open Pipe
open FunctionalUtils

[<EntryPoint;STAThread>]
let main _ =
    stringToList
    >> fw Map.ofList
    >> fun (l, m) -> 
        l
        |> Seq.map fst
        |> Seq.fold (flip (fun id -> 
            function       
            | (union, c) when union |> Set.contains id |> not -> union |> Set.union (getAreaForId id m), c+1
            | s -> s)) (set[], 0)
    >> snd
    >> string
    |> rotateClipboard
    0

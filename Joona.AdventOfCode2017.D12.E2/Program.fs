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
        |> Seq.fold (fun s id -> 
            match s with
            | (union,_) when union |> Set.contains id -> s
            | (union,c) -> union |> Set.union (getAreaForId id m), c+1) (set[], 0)
    >> snd
    >> string
    |> rotateClipboard
    0

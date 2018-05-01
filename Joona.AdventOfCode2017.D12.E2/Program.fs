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
            | (union, c) when not(Set.contains id union) -> Set.union (getAreaForId m id) union, c+1
            | s -> s)) (set[], 0)
    >> snd
    >> string
    |> rotateClipboard
    0

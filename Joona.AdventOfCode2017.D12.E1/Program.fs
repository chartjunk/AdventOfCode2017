open ClipboardUtils
open System
open Pipe
open FunctionalUtils

[<EntryPoint;STAThread>]
let main _ =
    stringToList
    >> Map.ofList
    >> flip getAreaForId 0
    >> Set.count
    >> string
    |> rotateClipboard
    0

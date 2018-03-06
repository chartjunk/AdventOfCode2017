open System
open ClipboardUtils
open Garbage

[<EntryPoint;STAThread>]
let main argv =
    Seq.toList
    >> preprocess
    >> cancel 
    >> piece >> fst
    >> Seq.collect id
    >> Seq.length
    >> string
    |> rotateClipboard
    0

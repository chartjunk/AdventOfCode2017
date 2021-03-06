﻿open System
open ClipboardUtils
open Garbage

[<EntryPoint;STAThread>]
let main argv =
    Seq.toList
    >> preprocess
    >> cancel 
    >> piece >> snd
    >> List.filter((<>)',')
    >> group 0
    >> string
    |> rotateClipboard
    0

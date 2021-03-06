﻿open ClipboardUtils
open System
open HexGrid

[<EntryPoint;STAThread>]
let main _ =
    stringToCoordinates
    >> Seq.reduce (List.map2 (+))
    >> coordinateToDistance
    >> string
    |> rotateClipboard
    0

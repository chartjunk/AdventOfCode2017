open ClipboardUtils
open System
open HexGrid

[<EntryPoint;STAThread>]
let main _ =
    stringToCoordinates
    >> Seq.scan (List.map2 (+)) [0;0;0]
    >> Seq.map coordinateToDistance
    >> Seq.max
    >> string
    |> rotateClipboard
    0

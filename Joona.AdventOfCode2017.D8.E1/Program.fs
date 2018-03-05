open System
open ClipboardUtils
open Register

[<EntryPoint;STAThread>]
let main argv =
    toCommands
    >> Seq.fold execCommand Map.empty
    >> Map.toSeq
    >> Seq.maxBy snd >> snd
    >> string
    |> rotateClipboard
    0

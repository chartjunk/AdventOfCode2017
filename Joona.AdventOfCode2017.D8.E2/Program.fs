open System
open ClipboardUtils
open FunctionalUtils
open Register

[<EntryPoint;STAThread>]
let main argv =
    toCommands
    >> Seq.mapFold (fwFst execCommand) Map.empty
    >> fst
    >> Seq.collect Map.toSeq
    >> Seq.maxBy snd >> snd
    >> string
    |> rotateClipboard
    0

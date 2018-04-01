open System
open ClipboardUtils
open StringUtils
open KnotHash

[<EntryPoint;STAThread>]
let main argv =
    let src = seq{0..255} |> List.ofSeq
    splitCleanList ","
    >> List.map int
    >> getKnotHash src
    >> Seq.take 2 
    >> Seq.reduce (*)
    >> string
    |> rotateClipboard
    0

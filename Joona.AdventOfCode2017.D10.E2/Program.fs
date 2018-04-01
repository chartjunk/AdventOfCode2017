open System
open ClipboardUtils
open FunctionalUtils
open KnotHash

[<EntryPoint;STAThread>]
let main _ =
    let src = seq{0..255} |> List.ofSeq

    Seq.skipWhile ((=)' ')
    >> Seq.map int
    >> List.ofSeq
    >> flip (@) [17;31;73;47;23]
    >> getKnotHashForRounds 64 src
    >> Seq.chunkBySize 16
    >> Seq.map (Seq.reduce (^^^) >> sprintf "%02x")
    >> Seq.reduce (+)
    >> string
    |> rotateClipboard
    0

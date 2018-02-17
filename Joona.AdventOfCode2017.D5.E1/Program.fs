open System
open ClipboardUtils
open Jump

[<EntryPoint; STAThread>]
let main argv =
    (+)1 |> runJumpApp |> rotateClipboard
    0
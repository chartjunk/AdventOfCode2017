open System
open Jump
open ClipboardUtils

[<EntryPoint; STAThread>]
let main argv =
    function
    | v when v > 2 -> v-1
    | v -> v+1
    |> runJumpApp |> rotateClipboard
    0

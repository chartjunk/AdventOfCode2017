open System
open ClipboardUtils
open StringUtils
open RecTowers

let inputToNameDict =
    splitClean newline
    >> Array.map (extractRow >> (fun (n,w,cs) -> n,cs))
    >> dict

[<EntryPoint; STAThread>]
let main argv = 
    inputToNameDict >> getRoot |> rotateClipboard
    0

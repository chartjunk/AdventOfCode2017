open System
open System.Windows.Forms
open Checksum
open ClipboardUtils

[<EntryPoint; STAThread>]
let main argv =
    sheetToChecksum seqToChecksum >> string |> rotateClipboard 
    0

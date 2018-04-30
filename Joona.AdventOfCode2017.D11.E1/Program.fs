open StringUtils
open ClipboardUtils
open System
open FunctionalUtils

[<EntryPoint;STAThread>]
let main _ =
    trim >> splitClean ","
    >> Seq.map (function
    | "n"  -> [0;-1;1]
    | "s"  -> [0;1;-1]
    | "nw" -> [1;-1;0]
    | "se" -> [-1;1;0]
    | "ne" -> [-1;0;1]
    | "sw" -> [1;0;-1])
    >> Seq.reduce (List.map2 (+))
    >> Seq.sumBy abs >> flip (/)2
    >> string
    |> rotateClipboard
    0

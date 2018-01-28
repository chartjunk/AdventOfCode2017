open System
open ClipboardUtils;
open Checksum

let cartesian xs = 
    xs |> Seq.collect(fun y -> xs |> Seq.map (fun x -> x, y))

let divideRow = 
    Seq.indexed
    >> cartesian
    >> Seq.pick(function
        | (i, a), (j, b) when i<>j && b<>0 && a%b=0 -> Some (a/b)
        | _ -> None)

[<EntryPoint; STAThread>]
let main argv =
    sheetToChecksum divideRow >> string |> rotateClipboard
    0

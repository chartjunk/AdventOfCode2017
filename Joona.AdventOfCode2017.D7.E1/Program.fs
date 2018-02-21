open System
open ClipboardUtils
open StringUtils

let flip f a b = f b a
let getOwnName = splitClean space >> Array.head
let getUpperNames = 
    splitCleanList " -> "
    >> (function
    | _::x::_ -> x |> splitCleanList ", "
    | _ -> List.Empty)

[<EntryPoint; STAThread>]
let main argv =
    splitCleanList newline
    >> (fun lines -> 
        let upperNames = lines |> List.map getUpperNames |> List.collect id |> set
        lines |> Seq.map getOwnName |> Seq.find (flip Set.contains upperNames >> not))
    |> rotateClipboard
    0

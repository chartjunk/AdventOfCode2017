open System
open ClipboardUtils
open FunctionalUtils

let preprocess =
    List.filter((<>)'\r')
    >> List.filter((<>)'\n')

let cancel =
    let rec cancelAcc acc =
        function
        | [] -> acc
        | '!'::_::cs -> cancelAcc acc cs
        | c::cs -> cancelAcc (acc@[c]) cs
    cancelAcc []

let piece =
    let rec pieceAcc acc =
        function
        | [] -> acc
        | '<'::cs -> cs |> List.skipWhile((<>)'>') |> List.skip 1 |> pieceAcc acc
        | c::cs -> pieceAcc (acc@[c]) cs
    pieceAcc []

let group =
    let rec groupAcc acc level =
        function
        | [] -> acc
        | '{'::cs -> groupAcc (acc+level+1) (level+1) cs
        | '}'::cs -> groupAcc acc (level-1) cs
    flip groupAcc 0

[<EntryPoint;STAThread>]
let main argv =
    Seq.toList
    >> preprocess
    >> cancel 
    >> piece 
    >> List.filter((<>)',')
    >> group 0
    >> string
    |> rotateClipboard
    0

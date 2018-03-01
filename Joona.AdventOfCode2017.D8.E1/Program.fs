open System
open ClipboardUtils
open StringUtils
open FunctionalUtils

let toCommands =    
    splitCleanList newline
    >> List.map (splitCleanList space)

let findOrZero k = 
    Map.tryFind k >> function
    | Some v -> v |> int, true
    | None -> 0, false

let rec toRegistry (map:Map<string, 'a>) =
    function
    | [] -> map
    | c::cs -> 
        match c with
        | tr::op::opval::_::condr::cond::condvalStr::[] -> 
            let trval, isFound = map |> findOrZero tr
            let condrval, _ = map |> findOrZero condr
            let condval = condvalStr |> int
            let condfun = 
                match cond with
                | "!=" -> (<>)
                | ">" -> (>)
                | "<" -> (<)
                | ">=" -> (>=)
                | "<=" -> (<=)
                | "==" -> (=)
            trval + (opval |> int) * match op with | "dec" -> -1 | "inc" -> 1
            |> match condfun condrval condval, isFound with
            | false, _ -> fun _ -> map
            | true, false -> flip (Map.add tr) map
            | true, true -> map |> Map.remove tr |> flip (Map.add tr)
            |> flip toRegistry cs

[<EntryPoint;STAThread>]
let main argv =
    toCommands
    >> toRegistry Map.empty
    >> Map.toList
    >> List.maxBy snd >> snd
    >> string
    |> rotateClipboard
    0

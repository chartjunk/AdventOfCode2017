open System
open DubsSumming

let rec foldDubs = function
    | [h::t; i::u] -> foldDubs [t;u] + (
        match h,i with | a,b when a=b -> 2*a | _ -> 0) 
    | _ -> 0

[<EntryPoint>]
let main argv =
    argv.[0] 
    |> explodeToInt 
    |> List.splitInto 2
    |> foldDubs
    |> printfn "%i"
    Console.ReadKey() |> ignore
    0

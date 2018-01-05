open System
open DubsSumming

let rec foldDubs = function
    | [h::t; i::u] when h=i -> 2*h + foldDubs [t;u]
    | [_::[]; _] -> 0
    | [_::t; _::u] -> foldDubs [t;u]

[<EntryPoint>]
let main argv =
    argv.[0] 
    |> explodeToInt 
    |> List.splitInto 2
    |> foldDubs
    |> printfn "%i"
    Console.ReadKey() |> ignore
    0

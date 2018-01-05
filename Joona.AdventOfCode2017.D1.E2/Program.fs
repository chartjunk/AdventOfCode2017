open System
open DubsSumming

[<EntryPoint>]
let main argv =
    argv.[0] 
    |> explodeToInt 
    |> List.splitInto 2
    |> fun [a; b;] -> List.zip a b
    |> List.map (function (a,b) when a=b -> 2*a | _ -> 0)
    |> List.sum
    |> printfn "%i"
    Console.ReadKey() |> ignore
    0

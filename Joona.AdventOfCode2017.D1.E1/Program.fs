open System

[<EntryPoint>] 
let main argv = 
    argv.[0] 
    |> DubsSumming.sumCircStrDubs 
    |> printfn "%i"
    Console.ReadKey() |> ignore
    0
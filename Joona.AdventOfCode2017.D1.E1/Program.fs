open System

let rec sumDubs (l:List<Char>) = 
    match l with 
    | _::[] -> 0 
    | h::t when h = t.Head -> sumDubs t + int h - int '0' 
    | _::t -> sumDubs t

[<EntryPoint>] 
let main argv = 
    (argv.[0] |> Seq.last)::[for c in argv.[0] -> c] 
    |> sumDubs 
    |> printfn "%i"
    Console.ReadKey() |> ignore
    0
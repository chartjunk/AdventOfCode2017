module DubsSumming
    
    open System

    let rec sumDubs l = 
        match l with 
        | _::[] -> 0 
        | h::t when h = t.Head -> h + sumDubs t
        | _::t -> sumDubs t
    
    let explodeToInt s = [for c in s -> c] |> List.map (int >> (+) (-int '0'))
    let circulate s = (s |> Seq.last)::s
    let sumCircStrDubs (s:String) = s |> explodeToInt |> circulate |> sumDubs
open System
open ClipboardUtils
open StringUtils

let flip f a b = f b a

let distributeUntilCollision initialMemory =
    
    let bCountf = initialMemory |> Array.length |> float    
    
    let distribute memory =
        let max = memory |> Array.max
        let maxIx = memory |> Array.findIndex ((=)max)
        let distributionForBank b = 
            let maxIxf = float maxIx
            let maxf = float max
            let bf = float b
            floor((maxf + bCountf - (bCountf + (bf-maxIxf-1.)) % bCountf - 1.)/bCountf) |> int
        memory |> Array.mapi(fun ix v -> 
            match distributionForBank ix with
            | d when ix = maxIx -> d
            | d -> d + v)

    let rec cycle (history : Set<Array>) =
        function
        | memory when memory |> history.Contains -> history.Count
        | memory -> memory |> history.Add |> flip cycle (distribute memory)
    
    cycle Set.empty initialMemory

[<EntryPoint; STAThread>]
let main argv =
    splitClean tab >> Array.map int >> distributeUntilCollision >> string |> rotateClipboard
    0

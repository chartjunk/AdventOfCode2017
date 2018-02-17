open System
open ClipboardUtils
open StringUtils
open MemoryDistribution

[<EntryPoint; STAThread>]
let main argv =    
    splitClean tab >> Array.map int
    
    // Get colliding memory pattern
    >> distributeUntilCollision >> fst
    // Get loop size
    >> distributeUntilCollision >> snd 

    >> string |> rotateClipboard
    0

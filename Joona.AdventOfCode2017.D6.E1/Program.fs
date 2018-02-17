open System
open ClipboardUtils
open StringUtils
open MemoryDistribution

[<EntryPoint; STAThread>]
let main argv =
    splitClean tab >> Array.map int >> distributeUntilCollision >> snd >> string |> rotateClipboard
    0

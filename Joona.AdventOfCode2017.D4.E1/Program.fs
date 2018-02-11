open System
open PassphraseUtils
    
[<EntryPoint; STAThread>]
let main argv = 
    both Seq.length (Seq.distinct >> Seq.length) >> (<||)(=) |> checkWith
    0

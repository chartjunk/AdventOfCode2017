open System
open PassphraseUtils

[<EntryPoint; STAThread>]
let main argv =
    Seq.map (string >> Seq.sort >> String.Concat) >> Seq.sort >> Seq.pairwise >> Seq.exists((<||)(=)) >> not |> checkWith
    0

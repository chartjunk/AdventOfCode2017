open System
open ClipboardUtils

let getLevel (point:float) = ((sqrt point)-1.)/2. |> ceil
let getLevelCenters l = seq { for c in [0..3] |> List.map float -> (l*2.+1.)**2.-(l+(2.*c*l)) }
let getClosestFrom y = Seq.minBy(fun x -> abs(x-y))
let getManhattanDistance point =
    let level = getLevel point
    let closest = getClosestFrom point (getLevelCenters level)
    level + abs(point - closest)

[<EntryPoint; STAThread>]
let main argv =
    (float >> getManhattanDistance >> string) |> rotateClipboard
    0

open System
open ClipboardUtils
open StringUtils

let between a b i = i >= a && i < b

let jumpWith values =

    let valuesCount = values |> Array.length
    let isInside = between 0 valuesCount

    let jump ix =
        let newIx = ix + values.[ix]
        values.[ix] <- values.[ix] + 1
        newIx
    
    let rec jumpUntilOutside jumpCount = 
        jump >> function
        | ix when isInside ix -> jumpUntilOutside (jumpCount+1) ix
        | _ -> jumpCount+1
        
    jumpUntilOutside 0 0

[<EntryPoint; STAThread>]
let main argv =
    splitClean newline >> Array.map int >> jumpWith >> string |> rotateClipboard
    0
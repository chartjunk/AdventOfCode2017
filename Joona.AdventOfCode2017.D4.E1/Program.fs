open System
open ClipboardUtils
open StringUtils

let both f g a = f a, g a
let isValid = splitClean " " >> both Array.length (Array.distinct >> Array.length) >> (<||)(=)
    
[<EntryPoint; STAThread>]
let main argv = 
    splitClean newline >> Array.where isValid >> Array.length >> string |> rotateClipboard
    0

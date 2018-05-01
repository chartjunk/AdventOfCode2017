open ClipboardUtils
open System
open StringUtils
open FunctionalUtils

[<EntryPoint;STAThread>]
let main _ =
    flip (List.fold clear) [","; "<-> "]
    >> splitCleanList newline    
    >> List.map (
        splitCleanList " " 
        >> List.map int
        >> fun (id::conn) -> id, conn)
    >> Map.ofList
    >> fun m ->        
        let rec area (passed:Set<int>) =
            function
            | id when passed.Contains id -> passed
            | id -> m.[id] |> List.fold area (Set.add id passed)
        area Set.empty 0 |> Set.count
    >> string
    |> rotateClipboard
    0

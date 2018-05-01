module Pipe
    open StringUtils
    open FunctionalUtils

    let stringToList =
        flip (List.fold clear) [","; "<-> "]
        >> splitCleanList newline    
        >> List.map (
            splitCleanList " " 
            >> List.map int
            >> fun (id::conn) -> id, conn)

    let getAreaForId m =
        let rec area passed =
            function
            | id when not(Set.contains id passed) -> Map.find id m |> List.fold area (Set.add id passed)
            | _ -> passed
        area Set.empty
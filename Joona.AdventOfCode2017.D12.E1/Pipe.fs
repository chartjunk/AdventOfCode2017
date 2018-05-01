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

    let getAreaForId id (m:Map<int,List<int>>) =
        let rec area (passed:Set<int>) =
            function
            | id when passed.Contains id -> passed
            | id -> m.[id] |> List.fold area (Set.add id passed)
        area Set.empty id
module RecTowers
    open StringUtils
    open FunctionalUtils
    open System.Collections.Generic

    let extractRow = 
        ["(";")";"-> ";","] |> flip (List.fold clear)
        >> splitCleanList space
        >> function | n::w::cs -> (n, w, cs)
        
    let getRoot (d:IDictionary<string, string list>) =
        let values = d.Values |> Seq.collect id |> set
        d.Keys |> Seq.find (values.Contains >> not)

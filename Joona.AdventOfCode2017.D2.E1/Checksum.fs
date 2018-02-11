module Checksum
    open System
    open StringUtils
    let sheetToSeqs =
        split Environment.NewLine StringSplitOptions.None 
        >> (Seq.map (split "\t" StringSplitOptions.RemoveEmptyEntries >> Seq.map int))
    let seqToChecksum s = Seq.max s - Seq.min s
    let sheetToChecksum (rowToInt:seq<int>->int) = sheetToSeqs >> Seq.map rowToInt >> Seq.sum



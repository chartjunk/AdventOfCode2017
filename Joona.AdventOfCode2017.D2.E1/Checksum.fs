module Checksum
    open System
    let split (splitter:string) (options:StringSplitOptions) (text:string) = text.Split([|splitter|], options)
    let sheetToSeqs =
        split Environment.NewLine StringSplitOptions.None 
        >> (Seq.map (split "\t" StringSplitOptions.RemoveEmptyEntries >> Seq.map int))
    let seqToChecksum s = Seq.max s - Seq.min s
    let sheetToChecksum (rowToInt:seq<int>->int) = sheetToSeqs >> Seq.map rowToInt >> Seq.sum



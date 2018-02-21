module StringUtils
    open System
    let newline = Environment.NewLine
    let tab = '\t' |> string
    let space = " "
    let split (splitter:string) (options:StringSplitOptions) (text:string) = text.Split([|splitter|], options)
    let splitClean s = split s StringSplitOptions.RemoveEmptyEntries
    let splitCleanList s = splitClean s >> List.ofArray

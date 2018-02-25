module StringUtils
    open System
    let newline = Environment.NewLine
    let tab = '\t' |> string
    let space = " "
    let split (splitter:string) (options:StringSplitOptions) (text:string) = text.Split([|splitter|], options)
    let splitClean s = split s StringSplitOptions.RemoveEmptyEntries
    let splitCleanList s = splitClean s >> List.ofArray
    let clear (s:String) (c:String) = s.Replace(c, "")
﻿module StringUtils
    open System
    let newline = Environment.NewLine
    let split (splitter:string) (options:StringSplitOptions) (text:string) = text.Split([|splitter|], options)
    let splitClean s = split s StringSplitOptions.RemoveEmptyEntries

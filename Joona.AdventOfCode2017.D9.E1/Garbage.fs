module Garbage
    open FunctionalUtils

    let preprocess =
        List.filter((<>)'\r')
        >> List.filter((<>)'\n')

    let cancel =
        let rec cancelAcc acc =
            function
            | [] -> acc
            | '!'::_::cs -> cancelAcc acc cs
            | c::cs -> cancelAcc (acc@[c]) cs
        cancelAcc []

    let piece =
        let rec pieceAcc acc =
            let log, stream = acc
            function
            | [] -> acc
            | '<'::cs -> 
                let eofGarb = cs |> List.findIndex((=)'>')
                pieceAcc (log@[cs.[0..eofGarb-1]], stream) cs.[eofGarb+1..]
            | c::cs -> pieceAcc (log, stream@[c]) cs
        pieceAcc ([],[])

    let group =
        let rec groupAcc acc level =
            function
            | [] -> acc
            | '{'::cs -> groupAcc (acc+level+1) (level+1) cs
            | '}'::cs -> groupAcc acc (level-1) cs
        flip groupAcc 0
module KnotHash
    open FunctionalUtils
    let rec cyclic src = seq { yield! src; yield! cyclic src }

    let getKnotHashForRounds rounds (src: int list) len =
        let srcLen = src |> List.length
        let rec getKnotHashAcc cRounds cSrc cSkip cOffset =
            function
            | take::ls ->
                let skip = ((cSkip % srcLen) + srcLen) % srcLen
                let cyclicSrc = cyclic cSrc        
                let part = 
                    cyclicSrc 
                    |> Seq.skip skip 
                    |> Seq.take take 
                    |> Seq.toList
                    |> List.rev
                    |> List.indexed
                    |> List.map(both (fst>>(+)skip>>(flip(%)srcLen)) snd)
                    |> Map.ofList
                let nSrc = 
                    cSrc
                    |> List.indexed
                    |> List.map (
                        function
                        | (ix, _) when part |> Map.containsKey ix -> part.Item ix
                        | (_, s) -> s)
                getKnotHashAcc cRounds nSrc (cSkip+take+cOffset) (cOffset+1) ls
            | [] when cRounds = 1 -> cSrc
            | _ -> getKnotHashAcc (cRounds-1) cSrc cSkip cOffset len
        getKnotHashAcc rounds src 0 0 len

    let getKnotHash = getKnotHashForRounds 1
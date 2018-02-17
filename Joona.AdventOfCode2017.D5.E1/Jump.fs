module Jump
    open StringUtils

    let between a b i = i >= a && i < b

    let jumpWith setOff values =

        let valuesCount = values |> Array.length
        let isInside = between 0 valuesCount

        let jump ix =
            let value = values.[ix]
            let newIx = ix + value
            values.[ix] <- setOff value
            newIx
    
        let rec jumpUntilOutside jumpCount = 
            jump >> function
            | ix when isInside ix -> jumpUntilOutside (jumpCount+1) ix
            | _ -> jumpCount+1
        
        jumpUntilOutside 0 0

    let runJumpApp setOff = splitClean newline >> Array.map int >> jumpWith setOff >> string



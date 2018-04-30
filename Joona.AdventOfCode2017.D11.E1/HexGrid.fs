module HexGrid
    open StringUtils
    open FunctionalUtils

    let stringToCoordinates = 
        trim >> splitClean ","
        >> Array.map (function
        | "n"  -> [0;-1;1]
        | "s"  -> [0;1;-1]
        | "nw" -> [1;-1;0]
        | "se" -> [-1;1;0]
        | "ne" -> [-1;0;1]
        | "sw" -> [1;0;-1])

    let coordinateToDistance =
        List.sumBy abs >> flip (/)2


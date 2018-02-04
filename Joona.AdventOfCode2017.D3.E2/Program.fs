open System
open ClipboardUtils

let fw f p = p, f p
let rotateInt f = float >> f >> int
let getLast i s = Seq.item (-1*i + Seq.length s) s

let getLevelMax l = 4.*l**2.+2.*l+1.
let getLevelMin = (+)(-1.) >> getLevelMax >> (+)1.

type Point = {ix:int; pn:seq<int>; v:int; takeM2:bool}

let getLevelPoints l =

    let min = rotateInt getLevelMin l
    let max = rotateInt getLevelMax l
    let norm i = i-min+1
    let side1 = l*2-1
    let side2 = l*2
    let prevSide1 = side1-2
    let prevSide2 = side2-2
    let isCorner = 
        norm >> (fun n ->
            n<>0 && ((n<=side1*2 && n%side1=0) 
            || (n>=side1*2+side2 && (n+2)%side2=0)))
    
    // TODO: Get rid of this wall.
    let getPrevNeighbor i =
        i-1 - (norm i
        |> function
        | np when np < side1 -> prevSide2*2+prevSide1*2
        | np when np = side1 -> prevSide2*2+prevSide1*2+1
        | np when np < side1*2 -> side1+prevSide2*2+prevSide1
        | np when np = side1*2 -> side1+prevSide2*2+prevSide1+1
        | np when np < side1*2+side2 -> side1*2+prevSide2*2
        | np when np = side1*2+side2 -> side1*2+prevSide2*2+1
        | np when np < side1*2+side2*2 -> side2+side1*2+prevSide2
        | np when np = side1*2+side2*2 -> side2+side1*2+prevSide2+1)

    seq{min..max}
    |> Seq.map (fw getPrevNeighbor)
    |> Seq.map (fun (i,n) -> (i,n,{ix=i;pn=[];v=0;takeM2=false}))
    |> Seq.map (
        function
        | i,n,p when i=min || isCorner(i-1) -> {p with pn=[n;n+1]; takeM2=true}
        | i,n,p when isCorner i             -> {p with pn=[n]}
        | i,n,p when isCorner(i+1)          -> {p with pn=[n-1;n]}
        | i,n,p                             -> {p with pn=[n-1;n;n+1]})

let getFirstGreaterThan comparisonValue =

    let rec values levelm1 levelm2 prevLevel =
        seq {
            // Include last item from m2 sequence if any
            let prev = Seq.append (
                match levelm2 |> Seq.tryLast with
                | Some i -> [i]
                | None -> []) levelm1 
            let l = prevLevel+1
            let next = 
                match l with
                // Treat first level as a special case
                | 1 -> ([1..7], [1;1;2;4;5;10;11]) ||> Seq.map2(fun ix v -> {ix=ix;pn=[];v=v;takeM2=false})
                | _ -> 
                    getLevelPoints
                    // Sum prev level neighbors
                    >> Seq.map(fun p -> p, prev |> Seq.filter(fun pv -> p.pn |> Seq.contains pv.ix) |> Seq.sumBy(fun pv -> pv.v))
                    // Accumulate same level neighbor = previous point
                    >> Seq.mapFold(fun (m2, m1) (p, pnsum) ->                
                        let nsum = pnsum + m1 + (m2 * match p.takeM2 with true -> 1 | _ -> 0) // TODO: Check if there is a better way to do this
                        let vn = nsum in ({p with v=vn},(m1,vn))
                    ) (prev |> getLast 2 |> (fun p -> p.v), prev |> getLast 1 |> (fun p -> p.v))
                    >> fst <| l

            yield! next
            yield! values next levelm1 l
        }  
        
    values [][] 0 |> Seq.pick (function | p when p.v > comparisonValue -> Some(p.v) | _ -> None)
    
[<EntryPoint; STAThread>]
let main argv =
    (int >> getFirstGreaterThan >> string) |> rotateClipboard
    0

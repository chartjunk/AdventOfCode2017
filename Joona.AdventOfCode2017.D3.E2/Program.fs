open System
open ClipboardUtils

let fw f p = p, f p
let rotateInt f = float >> f >> int

let getLevelMax l = 4.*l**2.+2.*l+1.
let getLevelMin = (+)(-1.) >> getLevelMax >> (+)1.

type Point = {ix:int; pn:seq<int>; v:int; m2coef:int}

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

    let getPrevNeighbor i = 
        i-1 - ([((=),1);((<),0)] |> List.pick(fun (op, off) -> match norm i with
        | np when op np side1             -> Some (prevSide2*2+prevSide1*2+off)
        | np when op np (side1*2)         -> Some (side1+prevSide2*2+prevSide1+off)
        | np when op np (side1*2+side2)   -> Some (side1*2+prevSide2*2+off)
        | np when op np (side1*2+side2*2) -> Some (side2+side1*2+prevSide2+off)
        | _ -> None))

    seq{min..max}
    |> Seq.map (fw getPrevNeighbor)
    |> Seq.map (fun (i,n) -> (i,n,{ix=i;pn=[];v=0;m2coef=0}))
    |> Seq.map (function
        | i,n,p when i=min || isCorner(i-1) -> {p with pn=[n;n+1]; m2coef=1}
        | i,n,p when isCorner i             -> {p with pn=[n]}
        | i,n,p when isCorner(i+1)          -> {p with pn=[n-1;n]}
        | _,n,p                             -> {p with pn=[n-1;n;n+1]})

let getFirst condition =

    let rec values prev prevPrev prevNum =
        seq {
            let l = prevNum+1
            let pvs = prevPrev @ prev        
            let next = 
                match l with
                // Treat first level as a special case
                | 1 -> ([1..7], [1;1;2;4;5;10;11]) ||> Seq.map2(fun ix v -> {ix=ix;pn=[];v=v;m2coef=0})
                | _ -> 
                    getLevelPoints
                    // Sum prev level neighbors
                    >> Seq.map(fun p -> p, pvs |> Seq.filter(fun pv -> p.pn |> Seq.contains pv.ix) |> Seq.sumBy(fun pv -> pv.v))
                    // Accumulate same level neighbor = previous point
                    >> Seq.mapFold(fun (m2, m1) (p, pnsum) -> let vn = pnsum+m1+m2*p.m2coef in ({p with v=vn},(m1,vn))) (
                        // Init accumulation with the last two items from prev
                        let v = prev |> List.map (fun p -> p.v) |> (@)[0;0] |> List.rev in (v.[0], v.[1]))
                    >> fst <| l

            yield! next
            yield! values (next |> List.ofSeq) prev l
        }  
        
    values [][] 0 |> Seq.pick (function | p when p.v |> condition -> Some(p.v) | _ -> None)
    
[<EntryPoint; STAThread>]
let main argv =
    (int >> (<=) >> getFirst >> string) |> rotateClipboard
    0

module FunctionalUtils
    let flip f a b = f b a
    let flip3 f a b c = f b a b
    let both f g a = f a, g a     
    let fst3 (a,_,_) = a
    let snd3 (_,b,_) = b
    let thd (_,_,c) = c
    let fw f a = a, f a
    let fwFst f a b = a, f a b
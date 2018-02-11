module PassphraseUtils
    open ClipboardUtils
    open StringUtils
    let both f g a = f a, g a
    let checkWith f = splitClean newline >> Array.where (splitClean " " >> f) >> Seq.ofArray >> Seq.length >> string |> rotateClipboard

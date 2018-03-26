module ClipboardUtils
    open System.Windows.Forms
    let setClipboard = Clipboard.SetText
    let rotateClipboard f = Clipboard.GetText() |> f |> setClipboard

module ClipboardUtils
    open System.Windows.Forms
    let rotateClipboard f = Clipboard.GetText() |> f |> Clipboard.SetText

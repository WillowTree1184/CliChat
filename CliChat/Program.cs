using Terminal.Gui;

ColorScheme TransparentScheme = new ColorScheme()
{
    Normal = Terminal.Gui.Attribute.Make(Color.White, Color.Black),
    Focus = Terminal.Gui.Attribute.Make(Color.White, Color.Black),
    HotNormal = Terminal.Gui.Attribute.Make(Color.White, Color.Black),
    HotFocus = Terminal.Gui.Attribute.Make(Color.White, Color.Black)
};

Application.Init();

View window = new View("CliChat")
{
    Height = Dim.Fill() - 1,
    Width = Dim.Fill() - 1,
    X = 1,
    Y = 1,
    ColorScheme = TransparentScheme
};

Window messageWindow = new Window("CliChat")
{
    X = 0,
    Y = 0,
    Width = Dim.Fill(),
    Height = Dim.Fill() - 2,
    ColorScheme = TransparentScheme
};

TextView messageView = new TextView
{
    X = 0,
    Y = 0,
    Width = Dim.Fill(),
    Height = Dim.Fill(),
    ReadOnly = true,
    WordWrap = true
};

messageWindow.Add(messageView);

Label inputPointer = new Label("|")
{
    X = 0,
    Y = Pos.Bottom(messageWindow) + 1
};

TextField inputField = new TextField("")
{
    X = 2,
    Y = Pos.Bottom(messageWindow) + 1,
    Width = Dim.Fill(),
    Height = 1
};

inputField.KeyPress += (e) =>
{
    if (e.KeyEvent.Key == Key.Enter)
    {
        // Handle the enter key press event
        string? message = inputField.Text.ToString();
        if (!string.IsNullOrWhiteSpace(message))
        {
            messageView.Text += $"[{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}] Willow Tree 1184:\n\t{message}\n\n";
            inputField.Text = "";

            messageView.GetCurrentHeight(out int height);
            int newLine = Math.Max(0, messageView.Lines - height);

            messageView.ScrollTo(newLine);
        }
    }
};

window.Add(messageWindow, inputPointer, inputField);

Application.Top.Add(window);

messageView.KeyPress += (e) =>
{
    if (e.KeyEvent.IsCtrl && (e.KeyEvent.Key != Key.V))
    {
        return;
    }
    inputField.SetFocus();
};

inputField.SetFocus();

Application.Run();

Application.QuitKey = Key.CtrlMask | Key.Q;
using System;
using FontStashSharp;
using Microsoft.Xna.Framework.Input;
using Myra.Graphics2D.UI;

namespace P2Game;

public class Decision : UiElement
{
    private Window window;
    private TextButton yesButton;
    private TextButton noButton;

    Panel panel;
    private Label bodyText;

    public Decision() : base()
    {
        window = new Window
        {
            Title = "Window"
        };

        bodyText = new Label()
        {
            Text = "filler\nhttps://www.undergraduate.study.cam.ac.uk/applying/entrance-requirements\n ",
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        panel = new Panel();

        yesButton = new TextButton
        {
            Text = "yes",
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Bottom
        };

        noButton = new TextButton
        {
            Text = "no",
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Bottom
        };

        panel.AddChild(bodyText);
        panel.AddChild(yesButton);
        panel.AddChild(noButton);
        
        window.Content = panel;
        window.CloseButton.Enabled = false;
        window.CloseKey = Keys.None;
        
        

        window.Closed += (s, a) => { Console.WriteLine("Window Closed"); };

        Main.widgets.Add(yesButton);
        Main.widgets.Add(noButton);
    }

    public override void Update()
    {
       
    }

    public void Enable(Desktop desktop)
    {
        window.ShowModal(desktop);
    }

    public void SetFont(DynamicSpriteFont spriteFont)
    {
        window.TitleFont = spriteFont;
        yesButton.Font = spriteFont;
        noButton.Font = spriteFont;
    }
}
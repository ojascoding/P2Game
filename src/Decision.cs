using System;
using System.IO;
using FontStashSharp;
using Microsoft.Xna.Framework.Input;
using Myra.Graphics2D.UI;


namespace P2Game;

public class Decision : UiElement
{
    private Window window;
    private TextButton yesButton;
    private TextButton noButton;
    
    //Panel to store all of the UI Elements
    Panel panel;
    private Label bodyText;
    private EventPicker eventPicker;
    private Event randomEvent;
    private bool hasRun = false;
    private Desktop desktop;

    public Decision(Desktop _desktop)
    {
        eventPicker = new EventPicker();
        window = new Window
        {
            Title = "Window"
        };
        desktop = _desktop;
        panel = new Panel();

        bodyText = new Label()
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };


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

        IntervalTimer.CountdownEnded += ReturnDecision;
    }

    public override void Update()
    {
        
        yesButton.TouchDown += (s, a) => 
        {
            Main.pollutionBar.SetPercent(Main.pollutionBar.GetPercent() + randomEvent.pollutionCost);
            Main.popularityBar.SetPercent(Main.popularityBar.GetPercent() + randomEvent.popularityCost);
            Main.moneyValue += randomEvent.cost;
            
            window.Close();
        };


    }

    public void Enable(bool val)
    {
        if (val)
        {
            window.ShowModal(desktop);
        }
        else
        {
            window.Close();
        }
    }
    
    //Sets up the decision variable at the beginning of the game
    public void SetDecisionOnce(DynamicSpriteFont hwygothFont, DynamicSpriteFont robotoFont)
    {
        //Sets the fonts to each type of text
        window.TitleFont = hwygothFont;
        bodyText.Font = robotoFont;
        yesButton.Font = robotoFont;
        noButton.Font = robotoFont;

        randomEvent = eventPicker.GenerateEvent();
        window.Title = randomEvent.name;
        window.TitleGrid.HorizontalAlignment = HorizontalAlignment.Center;
        bodyText.Text = randomEvent.description;
    }

    private void ReturnDecision()
    {
        hasRun = false;
        randomEvent = eventPicker.GenerateEvent();
        window.Show(desktop);
        window.Title = randomEvent.name;
        bodyText.Text = randomEvent.description;
    }
}
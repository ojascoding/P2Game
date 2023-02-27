using System;
using System.IO;
using FontStashSharp;
using Microsoft.Xna.Framework;
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
    private Desktop desktop;

    private int cyclesLeft;
    private Random random;

    private int eventIndex = 0;
    private Label popupText;

    private int decisionPicked;

    private byte[] hwygoth;
    private FontSystem fontSystem;
    
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

        popupText = new Label();
        popupText.Visible = false;

        panel.AddChild(bodyText);
        panel.AddChild(yesButton);
        panel.AddChild(noButton);
        
        window.Content = panel;
        window.CloseButton.Enabled = false;
        window.CloseKey = Keys.None;
        
        hwygoth = File.ReadAllBytes("Content/fonts/HWYGOTH.ttf");
        fontSystem = new FontSystem();

        fontSystem.AddFont(hwygoth);
        
        
        window.Closed += (s, a) => { Console.WriteLine("Window Closed"); };

        Main.widgets.Add(yesButton);
        Main.widgets.Add(noButton);
        Main.widgets.Add(popupText);


        IntervalTimer.CountdownEnded += ReturnDecision;


        random = new Random();
        cyclesLeft = 1/*random.Next(2, 5)*/;
    }

    public override void Update()
    {
        if (Main.paused)
        {
            // Console.WriteLine(cyclesLeft);
            if (yesButton.IsPressed && randomEvent.cost[0] < Main.moneyValue)
            {
                //Set the bar to the bar + whatever amount is in the event
                // * 0 is the yes value in the Event class
                Main.pollutionBar.SetPercent(Main.pollutionBar.GetPercent() + randomEvent.pollutionCost[0]); 
                Main.popularityBar.SetPercent(Main.popularityBar.GetPercent() + randomEvent.popularityCost[0]);
                Main.moneyValue += randomEvent.cost[0];

                yesButton.IsPressed = false; // * Toggles the button off afterwards as that generates bug if not done
                Main.paused = false;

                decisionPicked = 0;
                
                GeneratePopup();
                
                window.Close();
            }

            if (noButton.IsPressed&& randomEvent.cost[1] < Main.moneyValue)
            {
                // * 1 is the no value in the Event class
                Main.pollutionBar.SetPercent(Main.pollutionBar.GetPercent() + randomEvent.pollutionCost[1]); 
                Main.popularityBar.SetPercent(Main.popularityBar.GetPercent() + randomEvent.popularityCost[1]); 
                Main.moneyValue += randomEvent.cost[1];

                noButton.IsPressed = false;
                Main.paused = false;

                decisionPicked = 1;
                
                GeneratePopup();

                window.Close();
            }
        }

        // if (popupText.Visible)
        // {
        //     if (popupText.Font.FontSize == 0)
        //     {
        //         popupText.Visible = false;
        //     }
        //     
        //     else if (popupText.Font.FontSize < 56)
        //     {
        //         popupText.Font = fontSystem.GetFont(popupText.Font.FontSize + 1);
        //     }
        //
        //     else
        //     {
        //         popupText.Font = fontSystem.GetFont(popupText.Font.FontSize - 1);
        //     }
        //     
        // }
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

        randomEvent = eventPicker.GenerateEvent(eventIndex++);
        window.Title = randomEvent.name;
        window.TitleGrid.HorizontalAlignment = HorizontalAlignment.Center;
        bodyText.Text = randomEvent.description;
    }

    private void ReturnDecision()
    {
        cyclesLeft--;
        
        if (cyclesLeft == 0)
        {
            // randomEvent = eventPicker.GenerateEvent();

            randomEvent = eventPicker.GenerateEvent(eventIndex++);
            
            window.Show(desktop);
            window.Title = randomEvent.name;
            bodyText.Text = randomEvent.description;
            Main.paused = true;

            cyclesLeft = 1/*random.Next(2, 5)*/;
        }
    }

    void GeneratePopup()
    {
        // popupText.Visible = true;
        popupText.HorizontalAlignment = HorizontalAlignment.Center;
        popupText.VerticalAlignment = VerticalAlignment.Center;
        popupText.Font = fontSystem.GetFont(48);
        popupText.TextColor = Color.Black;

        if (randomEvent.pollutionCost[0] >= 0)
        {
            popupText.Text = "Pollution went up by " + randomEvent.pollutionCost[decisionPicked];
        }

        else
        {
            popupText.Text = "Pollution went down by " + randomEvent.pollutionCost[decisionPicked];
        }
    }
    
    
}
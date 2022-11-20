using System;
using System.Globalization;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Myra;
using Myra.Graphics2D.UI;

namespace P2Game;

public class IntervalTimer : UiElement
{
    private float currentTime;
    private bool timeUp;
    public static Action CountdownEnded;

    public IntervalTimer() : base()
    {
        float intervalTime = 15f;
        currentTime = intervalTime;

        Text = new Label()
        {
            Id = "label",
            Text = intervalTime.ToString(CultureInfo.InvariantCulture),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Top
        };
        
        Main.widgets.Add(Text);
    }

    public void Countdown(GameTime gameTime, Desktop desktop)
    {

        currentTime -= (float) gameTime.ElapsedGameTime.TotalSeconds;
        double i =  Math.Round(currentTime, 0, MidpointRounding.AwayFromZero);
        
        Text.Text = i.ToString(CultureInfo.CurrentCulture);

        if (currentTime <= 0)
        {
            timeUp = true;
            currentTime = 15f;
            CountdownEnded?.Invoke();
        }

        else
        {
            timeUp = false;
        }
    }

    public bool GetTimeUp()
    {
        return timeUp;
    }
    
    public void SetFont(DynamicSpriteFont font)
    {
        Text.Font = font;
    }
}
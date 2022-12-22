using System;
using System.Globalization;
using FontStashSharp;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using Myra;
using Myra.Graphics2D.UI;

namespace P2Game;

public class IntervalTimer : UiElement
{
    private float currentTime;
    private bool timeUp;
    private int maxTime = 5;
    public static Action CountdownEnded; //An event that pulses whenever the countdown runs out

    public IntervalTimer() : base()
    {
        float intervalTime = 5;
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
        if (!Main.paused)
        {
            currentTime -= (float) gameTime.ElapsedGameTime.TotalSeconds;
            double i =  Math.Round(currentTime, 0, MidpointRounding.AwayFromZero);
        
            Text.Text = i.ToString(CultureInfo.CurrentCulture);

            if (currentTime <= 0)
            {
                Text.Visible = false;
                timeUp = true;
                currentTime = maxTime;
                CountdownEnded?.Invoke();
            }

            else
            {
                Text.Visible = true;
                timeUp = false;
            }

        }

        else
        {
            Text.Text = maxTime.ToString();
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
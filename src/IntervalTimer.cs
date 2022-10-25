using System;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Myra;
using Myra.Graphics2D.UI;

namespace P2Game;

public class IntervalTimer : UiElement
{
    private float currentTime;
    private bool timeUp;

    public IntervalTimer() : base(Main.game)
    {
        float intervalTime = 15f;
        currentTime = intervalTime;

        text = new Label()
        {
            Id = "label",
            Text = intervalTime.ToString(),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Top
        };
        
        Main.widgets.Add(text);
    }

    public void Countdown(GameTime gameTime)
    {
        currentTime -= (float) gameTime.ElapsedGameTime.TotalSeconds;
        double i =  Math.Round(currentTime, 0, MidpointRounding.AwayFromZero);
        text.Text = i.ToString();

        if (currentTime <= 0)
        {
            timeUp = true;
            currentTime = 15f;
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
        text.Font = font;
    }
}
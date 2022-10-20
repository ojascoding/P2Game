using System;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Myra;
using Myra.Graphics2D.UI;

namespace P2Game;

public class IntervalTimer
{
    private float currentTime;
    public Label text;

    public IntervalTimer(Microsoft.Xna.Framework.Game game)
    {
        float intervalTime = 15f;
        currentTime = intervalTime;
        MyraEnvironment.Game = game;

        text = new Label()
        {
            Id = "label",
            Text = intervalTime.ToString(),
            Top = 30,
            Left = 356
        };
    }

    public void Countdown(GameTime gameTime)
    {
        currentTime -= (float) gameTime.ElapsedGameTime.TotalSeconds;
        double i =  Math.Round(currentTime, 0, MidpointRounding.AwayFromZero);
        text.Text = i.ToString();

        currentTime = (currentTime <= 0) ? 15 : currentTime;
    }

    public void SetFont(DynamicSpriteFont font)
    {
        text.Font = font;
    }
    
}
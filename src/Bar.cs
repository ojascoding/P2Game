using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.VectorDraw;
using Myra.Graphics2D.UI;

namespace P2Game;

public class Bar : UiElement
{
    private readonly float scale;
    private readonly Texture2D background;
    private readonly Texture2D icon;

    private readonly Vector2 position;
    private int currentValue;
    private Rectangle part;
    private Color barColor;

    public Bar(Texture2D mainTex, Texture2D backgroundTex, int x, int y, Color color)
    {
        currentValue = 50;
        scale = 2;

        icon = mainTex;
        background = backgroundTex;
        barColor = color;

        position = new Vector2(x, y);
        part = new Rectangle(0, 0, (int) (currentValue * scale + 8), (int) (icon.Height * scale));

        // Text = new Label()
        // {
        //     Left = 20,
        //     Top = 384,
        //     Text = currentValue.ToString()
        // };
        //
        // Main.widgets.Add(Text);
    }

    public void Update(int value)
    {
        // if (!Main.paused)
        currentValue = value;
        if (currentValue > 100) {currentValue = 100;}
        if (currentValue < 0) {currentValue = 0;}
            part.Width = (int) (currentValue * scale + 8);
        // Console.WriteLine(part.Width);
    }

    public void Render(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
    {
        Console.WriteLine(currentValue);
        spriteBatch.FillRectangle(position.X, position.Y, currentValue * scale,
            background.Height * scale, barColor, 0.5f);
        spriteBatch.Draw(background, new Rectangle((int) position.X, (int) position.Y, (int) (background.Width * scale),
            (int) (background.Height * scale)), Color.White);
        spriteBatch.Draw(icon, new Rectangle((int) position.X, (int) position.Y, (int) (icon.Width * scale), (int) (icon.Height * scale)), Color.White);
    }

    public void SetPercent(int amt)
    {
        currentValue = amt;
    }

    public int GetPercent()
    {
        return currentValue;
    }
}
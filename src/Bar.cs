using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.VectorDraw;

namespace P2Game;

public class Bar : UiElement
{
    public float scale;
    private Texture2D background;
    private Texture2D bin;

    private Vector2 position;
    private float maxValue;
    private int currentValue;
    private Rectangle part;

    public Bar(Texture2D trashBinTexture, Texture2D barBackgroundTexture, int x, int y)
    {
        currentValue = 50;
        maxValue = 100;
        scale = 2;

        bin = trashBinTexture;
        background = barBackgroundTexture;

        position = new Vector2(x, y);
        part = new Rectangle(0, 0, (int) (currentValue * scale + 8), (int) (bin.Height * scale));
    }

    public void Update(int value)
    {
        // if (!Main.paused)
        currentValue = value;
        part.Width = (int) (currentValue * scale + 8);
        // Console.WriteLine(part.Width);
    }

    public void Render(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
    {
        Console.WriteLine(currentValue);
        spriteBatch.FillRectangle(position.X, position.Y, currentValue * scale,
            background.Height * scale, new Color(109, 86, 34), 0.5f);
        spriteBatch.Draw(background, new Rectangle((int) position.X, (int) position.Y, (int) (background.Width * scale),
            (int) (background.Height * scale)), Color.White);
        spriteBatch.Draw(bin, new Rectangle((int) position.X, (int) position.Y, (int) (bin.Width * scale), (int) (bin.Height * scale)), Color.White);
        
        //Kinda like spritebatch but for shapes
        // PrimitiveBatch primitiveBatch = new PrimitiveBatch(graphicsDevice);
        // //The actual thing that does the drawing
        // PrimitiveDrawing primitiveDrawing = new PrimitiveDrawing(primitiveBatch);
        // //Temporary junk to set up the shape spritebatch
        // Matrix projection = Matrix.CreateOrthographicOffCenter(0f, 800, 480, 0f, 0f, 1f);
        // Matrix other = Matrix.Identity;
        // primitiveBatch.Begin(ref projection, ref other);
        // primitiveDrawing.DrawSolidRectangle(new Vector2(position.X, position.Y), currentValue * scale, background.Height, new Color(109, 86, 34)); //Actual Bar
        // primitiveBatch.End();
        
        
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
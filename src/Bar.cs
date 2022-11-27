using System;
using System.Net.Mime;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Shapes;
using MonoGame.Extended.VectorDraw;
using Myra.Graphics2D.UI;

namespace P2Game;

public class Bar : UiElement
{
    private int percent;
    public Vector2 scale;
    private Texture2D barTexture;
    private Texture2D barBackgroundTexture;
    
    private Rectangle spriteRect;
    private Rectangle backgroundSpriteRect;
    private Rectangle newBounds;
    private int oldPercent;
    private GraphicsDevice graphicsDevice;

    private Color color;

    public Bar(GraphicsDevice _graphicsDevice, int amt, Texture2D _barTexture, Texture2D _barBackgroundTexture, int x,
        int y, Color _color, String text, int left)
    {
        percent = amt;
        scale = new Vector2(2, 2);

        barTexture = _barTexture;
        barBackgroundTexture = _barBackgroundTexture;
        
        spriteRect = new Rectangle(x, y, Convert.ToInt32(108 * scale.X), Convert.ToInt32(24 * scale.X));
        backgroundSpriteRect = new Rectangle(x, y, Convert.ToInt32(108 * scale.X), Convert.ToInt32(24 * scale.X));

        graphicsDevice = _graphicsDevice;
        
        oldPercent = percent;
        newBounds = spriteRect;
        color = _color;

        Text = new Label
        {
            Text = text,
            Top = 396,
            Left = left
        };
        Main.widgets.Add(Text);
    }

    public override void Update()
    {
        if (!Main.paused)
        {
            newBounds.Width += percent - oldPercent; //Since the width of the 
            // Console.WriteLine("Percent is " + percent + "\nOldPercent is " + oldPercent + "\nNewbounds width is " + newBounds.Width +"\nSpriterect width is " + spriteRect.Width);
        }
    }

    public void Render(SpriteBatch spriteBatch)
    {
        // spriteBatch.Draw(barTexture,  spriteRect, spriteRect, Color.White);
        // spriteBatch.Draw(barBackgroundTexture, backgroundSpriteRect, Color.White);
        
        //Kinda like spritebatch but for shapes
        PrimitiveBatch primitiveBatch = new PrimitiveBatch(graphicsDevice);
        //The actual thing that does the drawing
        PrimitiveDrawing primitiveDrawing = new PrimitiveDrawing(primitiveBatch);
        //Temporary junk to set up the shape spritebatch
        Matrix projection = Matrix.CreateOrthographicOffCenter(0f, 800, 480, 0f, 0f, 1f);
        Matrix other = Matrix.Identity;
        primitiveBatch.Begin(ref projection, ref other);
        primitiveDrawing.DrawSolidRectangle(new Vector2(spriteRect.X, spriteRect.Y), percent * 2, 48, color); //Actual Bar
        primitiveDrawing.DrawRectangle(new Vector2(spriteRect.X, spriteRect.Y), 200, 48, Color.Black); //Border around it
        primitiveBatch.End();
    }

    public void SetPercent(int amt)
    {
        oldPercent = percent;
        percent = amt;
    }

    public int GetPercent()
    {
        return percent;
    }
    
    public void SetFont(DynamicSpriteFont font)
    {
        Text.Font = font;
    }
}
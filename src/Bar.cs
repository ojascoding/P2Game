using System.ComponentModel.DataAnnotations;
using System.IO;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D.UI;
using XNAssets;

namespace P2Game;

public class Bar : UiElement
{
    private int percent;
    private Texture2D sprite;
    private Rectangle spriteRect;

    public Bar(int amt, Stream stream, GraphicsDevice graphicsDevice, int x, int y) : base()
    {
        percent = amt;
        sprite = Texture2D.FromStream(graphicsDevice, stream);

        spriteRect = new Rectangle(x, y, 100, 24);
    }

    public override void Update()
    {
        spriteRect.Width = percent;
    }

    public void Render(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(sprite, new Vector2(0, 0), spriteRect, Color.White);
    }
    
    public void SetPercent(int amt)
    {
        percent = amt;
    }

    public int GetPercent()
    {
        return percent;
    }

    public void SetFont(DynamicSpriteFont font)
    {
        base.Text.Font = font;
    }
    
}
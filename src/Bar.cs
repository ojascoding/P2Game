using System.ComponentModel.DataAnnotations;
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

    public Bar(int amt, AssetManager assetManager, string barSprite, int x, int y) : base()
    {
        percent = amt;
        sprite = assetManager.Load<Texture2D>(barSprite);

        spriteRect = new Rectangle(x, y, amt, 24);
    }

    public override void Update()
    {
        base.Text.Text = percent.ToString();
    }

    public void Render(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(sprite, spriteRect, Color.White);
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
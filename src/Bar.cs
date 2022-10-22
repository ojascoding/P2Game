using System.ComponentModel.DataAnnotations;
using FontStashSharp;
using Myra.Graphics2D.UI;

namespace P2Game;

public class Bar : UiElement
{
    private int percent;

    public Bar(int amt) : base(Main.game)
    {
        percent = amt;

        base.text = new Label()
        {
            Id = "label",
            Text = percent.ToString()
        };
        
        Main.widgets.Add(text);
    }

    public override void Update()
    {
        base.text.Text = percent.ToString();
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
        base.text.Font = font;
    }
    
}
using System.ComponentModel.DataAnnotations;
using FontStashSharp;
using Myra.Graphics2D.UI;

namespace P2Game;

public class Bar : UiElement
{
    private int percent;

    public Bar(int amt) : base()
    {
        percent = amt;

        base.Text = new Label()
        {
            Id = "label",
            Text = percent.ToString()
        };
        
        Main.widgets.Add(Text);
    }

    public override void Update()
    {
        base.Text.Text = percent.ToString();
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
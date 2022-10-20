using System.ComponentModel.DataAnnotations;
using FontStashSharp;
using Myra.Graphics2D.UI;

namespace P2Game;

public class Bar
{
    public Label text;
    private int percent;
    

    public Bar(int amt)
    {
        percent = amt;
    }

    void Update()
    {
        text.Text = percent.ToString();
    }
    
    void SetPercent(int amt)
    {
        percent = amt;
    }

    int GetPercent()
    {
        return percent;
    }

    void SetFont(DynamicSpriteFont font)
    {
        text.Font = font;
    }
}
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Myra;
using Myra.Graphics2D.UI;

namespace P2Game;

public class UiElement
{
    protected Label Text;

    public UiElement()
    {
        MyraEnvironment.Game = Main.Game;
    }
    
    public Label GetText()
    {
        return Text;
    }
        
    public void SetPosition(int left, int top)
    {
        Text.Left = left;
        Text.Top = top;
    }

    public Vector2 GetPosition()
    {
        return new Vector2(Text.Left, Text.Top);
    }

    public void SetAlignment(HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
    {
        Text.HorizontalAlignment = horizontalAlignment;
        Text.VerticalAlignment = verticalAlignment;
    }
    
    public virtual void Update()
    {
        
    }
}
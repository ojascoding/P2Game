using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Myra;
using Myra.Graphics2D.UI;

namespace P2Game;

public class UiElement
{
    protected Label text;

    public UiElement(Microsoft.Xna.Framework.Game game)
    {
        MyraEnvironment.Game = game;
    }
    
    public Label GetText()
    {
        return text;
    }
        
    public void SetPosition(int left, int top)
    {
        text.Left = left;
        text.Top = top;
    }

    public Vector2 GetPosition()
    {
        return new Vector2(text.Left, text.Top);
    }

    public void SetAlignment(HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
    {
        text.HorizontalAlignment = horizontalAlignment;
        text.VerticalAlignment = verticalAlignment;
    }
    
    public virtual void Update()
    {
        
    }
}
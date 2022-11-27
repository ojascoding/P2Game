using System.Collections.Generic;
using System.IO;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D.UI;
using XNAssets;

namespace P2Game;

public class Main : Game
{
    // ReSharper disable once NotAccessedField.Local
    //Boilerplate Variables
    private GraphicsDeviceManager graphics; // A necessary boilerplate code to identify the graphics driver
    private SpriteBatch spriteBatch; // A tool to load all sprites
    private AssetManager assetManager; // A tool to load all content without MGCB Editor
    public static Game Game;
    
    // UI
    private IntervalTimer intervalTimer; // A class I made myself to store the interval timer at the top
    private Desktop desktop; // A necessary Myra variable to render all UI elements
    private Panel panelUi;
    FontSystem hwygothFontSystem = new FontSystem();
    FontSystem robotoFontSystem = new FontSystem();



    // ReSharper disable once InconsistentNaming
    public static readonly List<Widget> widgets = new List<Widget>();
    public static Bar popularityBar;
    public static Bar pollutionBar;
    private static Bar tourismBar;
    public static int moneyValue;
    private Label money;

    public static bool paused;

    private Decision decision;
    
    public Main()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        Directory.SetCurrentDirectory("../../../"); //Sets the working directory to the home directory
        Game = this;
        SetupUi();
        decision = new Decision(desktop);
        decision.Enable(true);

        moneyValue = 500;

        
        base.Initialize();
        SetupFonts();

    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice); //Creates the device needed to load sprites and art
        var trashBarPath = "Sprites/Trash-Bar";
        var trashBackgroundBarPath = "Sprites/Trash-Bar-Grid";
        var trashBarTexture = Content.Load<Texture2D>(trashBarPath);
        var trashBackgroundBarTexture = Content.Load<Texture2D>(trashBackgroundBarPath);

        pollutionBar = new Bar(graphics.GraphicsDevice ,50, trashBarTexture, trashBackgroundBarTexture,0, graphics.PreferredBackBufferHeight - 48, new Color(138, 111, 48), "Pollution", 0);
        popularityBar = new Bar( graphics.GraphicsDevice, 50,trashBarTexture, trashBackgroundBarTexture, 600, graphics.PreferredBackBufferHeight - 48, Color.Yellow, "Popularity", 700);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (Decision.isGameOver)
        {
            GameOver();
        }
        
        intervalTimer.Countdown(gameTime, desktop);
        
        //TODO: Cleanup the Main File and make the update function more useful
        pollutionBar.Update(); //A method which simply updates the text on the bar 
        popularityBar.Update();
        decision.Update();
        UpdateBars(); //A method in the main file itself that updates all 3 bars

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        
        spriteBatch.Begin();
        desktop.Render();
        pollutionBar.Render(spriteBatch);
        popularityBar.Render(spriteBatch);
        spriteBatch.End();

        base.Draw(gameTime);
    }

    void UpdateBars()
    {
        int barFactor = 4;
        bool runOnce = false;
        
        if (intervalTimer.GetTimeUp() && runOnce == false)
        {
            pollutionBar.SetPercent(pollutionBar.GetPercent() + barFactor);
            popularityBar.SetPercent(popularityBar.GetPercent() - barFactor);
            // tourismBar.SetPercent(tourismBar.GetPercent() + (popularityBar.GetPercent() - pollutionBar.GetPercent()));
            runOnce = true;
        }
        
        
        else {runOnce = false;}
        
        //Keep the money value up-to-date
        money.Text = "$" + moneyValue;
    }

    void SetupUi()
    {
        intervalTimer = new IntervalTimer(); //Creates a new interval timer (Class made by Me)
        
        // Setting up the UI Desktop
        panelUi = new Panel();
        desktop = new Desktop();
        //Sets the tourism bar to the bottom

        money = new Label
        {
            Text = "$" + moneyValue,
            TextColor = new Color(17, 140, 79),
            HorizontalAlignment = HorizontalAlignment.Right
        };

        money.Text = "$" + moneyValue;

        widgets.Add(money);

        foreach (var widget in widgets)
        {
            panelUi.Widgets.Add(widget);
        }
        
        desktop.Root = panelUi;
    }
    
    void SetupFonts()
    {
        byte[] hwygoth = File.ReadAllBytes("Content/fonts/HWYGOTH.ttf");
        byte[] roboto = File.ReadAllBytes("Content/fonts/Roboto-Regular.ttf");
        hwygothFontSystem.AddFont(hwygoth);
        robotoFontSystem.AddFont(roboto);

        money.Font = hwygothFontSystem.GetFont(48);

        intervalTimer.SetFont(hwygothFontSystem.GetFont(40));
        
        pollutionBar.SetFont(hwygothFontSystem.GetFont(36));
        popularityBar.SetFont(hwygothFontSystem.GetFont(36));

        decision.SetDecisionOnce(hwygothFontSystem.GetFont(36), robotoFontSystem.GetFont(24));
    }

    void GameOver()
    {
        if (pollutionBar.GetPercent() > 50 || popularityBar.GetPercent() < 75)
        {
            Lose();
        }
        else
        {
            Win();
        }
    }

    void Lose()
    {
        Label loseText = new Label
        {
            Text = "You Lose!",
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Font = hwygothFontSystem.GetFont(48)
        };
        
        panelUi.Widgets.Add(loseText);
        desktop.Root = panelUi;
        paused = true;
    }

    void Win()
    {
        Label winText = new Label
        {
            Text = "You Win!",
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Font = hwygothFontSystem.GetFont(48)
        };
        
        panelUi.Widgets.Add(winText);
        desktop.Root = panelUi;
        paused = true;
    }
}
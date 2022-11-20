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
    

    // ReSharper disable once InconsistentNaming
    public static readonly List<Widget> widgets = new List<Widget>();
    static public Bar popularityBar;
    static public Bar pollutionBar;
    static public Bar tourismBar;
    static public int moneyValue;
    private Label money;

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
        SetupUI();
        decision = new Decision(desktop);
        decision.Enable(true);

        moneyValue = 500;

        TitleContainerAssetResolver assetResolver = new TitleContainerAssetResolver("../../../../Content");
        assetManager = new AssetManager(GraphicsDevice, assetResolver); //The thing needed to bypass the MGCB Editor to load sprites
        
        SetupArt();
        SetupFonts();
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice); //Creates the device needed to load sprites and art
        
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        intervalTimer.Countdown(gameTime, desktop);
        
        //TODO: Cleanup the Main File and make the update function more useful
        pollutionBar.Update(); //A method which simply updates the text on the bar 
        popularityBar.Update();
        // tourismBar.Update();
        decision.Update();
        UpdateBars(); //A method in the main file itself that updates all 3 bars

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        
        spriteBatch.Begin();
        desktop.Render();
        spriteBatch.End();

        base.Draw(gameTime);
    }

    void UpdateBars()
    {
        int barFactor = 4;
        bool runOnce = false;
        // ReSharper disable ConditionIsAlwaysTrueOrFalse
        
        if (intervalTimer.GetTimeUp() && runOnce == false)
        {
            //ReSharper enable ConditionIsAlwaysTrueOrFalse
            pollutionBar.SetPercent(pollutionBar.GetPercent() + barFactor);
            popularityBar.SetPercent(popularityBar.GetPercent() - barFactor);
            tourismBar.SetPercent(tourismBar.GetPercent() + (popularityBar.GetPercent() - pollutionBar.GetPercent()));
            runOnce = true;
        }
        
        
        else {runOnce = false;}
        
        //Keep the money value up-to-date
        money.Text = "$" + moneyValue;
    }

    // ReSharper disable once InconsistentNaming
    void SetupUI()
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

    void SetupArt()
    {
        pollutionBar = new Bar(50, assetManager, "Sprites/Trash-Bar.png", 0, 380);
        popularityBar = new Bar(50, assetManager, "Sprites/Trash-Bar.png", 700, 380);
        // tourismBar = new Bar();
        
        pollutionBar.SetAlignment(HorizontalAlignment.Left, VerticalAlignment.Bottom); //Sets the pollution bar to the bottom left
        popularityBar.SetAlignment(HorizontalAlignment.Right, VerticalAlignment.Bottom); //Sets the popularity bar to the bottom right
        tourismBar.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Bottom);
    }
    
    void SetupFonts()
    {
        byte[] hwygoth = File.ReadAllBytes("Content/fonts/HWYGOTH.ttf");
        byte[] roboto = File.ReadAllBytes("Content/fonts/Roboto-Regular.ttf");
        FontSystem hwygothFontSystem = new FontSystem();
        hwygothFontSystem.AddFont(hwygoth);
        FontSystem robotoFontSystem = new FontSystem();
        robotoFontSystem.AddFont(roboto);

        money.Font = hwygothFontSystem.GetFont(48);

        intervalTimer.SetFont(hwygothFontSystem.GetFont(40));

        pollutionBar.SetFont(hwygothFontSystem.GetFont(36));
        popularityBar.SetFont(hwygothFontSystem.GetFont(36));
        tourismBar.SetFont(hwygothFontSystem.GetFont(24));
        
        decision.SetDecisionOnce(hwygothFontSystem.GetFont(36), robotoFontSystem.GetFont(24));
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra.Graphics2D.UI;
using XNAssets;

namespace P2Game;

public class Main : Game
{
    //Boilerplate Variables
    private GraphicsDeviceManager graphics; // A neccesary boilerplate code to identify the graphics driver
    private SpriteBatch spriteBatch; // A tool to load all sprites
    private AssetManager assetManager; // A tool to load all content without MGCB Editor
    private FontSystem ordinaryFontSystem; // A tool to load in all fonts
    public static Microsoft.Xna.Framework.Game game;
    
    // UI
    private IntervalTimer intervalTimer; // A class I made myself to store the interval timer at the top
    private Desktop desktop; // A necessary Myra variable to render all UI elements
    private Panel panelUi;

    public static List<Widget> widgets = new List<Widget>();
    private Bar pollutionBar;
    private Bar popularityBar;
    private Bar tourismBar;

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
        game = this;
        
        SetupUI();

        TitleContainerAssetResolver assetResolver = new TitleContainerAssetResolver("Content");
        assetManager = new AssetManager(GraphicsDevice, assetResolver);
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);

        byte[] font = File.ReadAllBytes("Content/fonts/HWYGOTH.ttf");
        ordinaryFontSystem = new FontSystem();
        ordinaryFontSystem.AddFont(font);
        
        intervalTimer.SetFont(ordinaryFontSystem.GetFont(48));
        
        pollutionBar.SetFont(ordinaryFontSystem.GetFont(36));
        popularityBar.SetFont(ordinaryFontSystem.GetFont(36));
        tourismBar.SetFont(ordinaryFontSystem.GetFont(36));

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        intervalTimer.Countdown(gameTime);
        
        pollutionBar.Update();
        popularityBar.Update();
        tourismBar.Update();

        // TODO: Add your update logic here

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


    // ReSharper disable once InconsistentNaming
    void SetupUI()
    {
        intervalTimer = new IntervalTimer(); //Creates a new interval timer (Class made by Me)
        
        // Setting up the UI Desktop
        panelUi = new Panel();
        desktop = new Desktop();

        pollutionBar = new Bar(50);
        popularityBar = new Bar(50);
        tourismBar = new Bar(50);
        
        pollutionBar.SetAlignment(HorizontalAlignment.Left, VerticalAlignment.Bottom);
        popularityBar.SetAlignment(HorizontalAlignment.Right, VerticalAlignment.Bottom);
        tourismBar.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Bottom);

        foreach (var widget in widgets)
        {
            Console.WriteLine("1");
            panelUi.Widgets.Add(widget);
        }
        
        
        desktop.Root = panelUi;
    }
}
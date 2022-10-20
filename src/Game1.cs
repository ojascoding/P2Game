using System;
using System.IO;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra.Graphics2D.UI;
using XNAssets;

namespace P2Game;

public class Game1 : Game
{
    //Boilerplate Variables
    private GraphicsDeviceManager graphics; // A neccesary boilerplate code to identify the graphics driver
    private SpriteBatch spriteBatch; // A tool to load all sprites
    private AssetManager assetManager; // A tool to load all content without MGCB Editor
    private FontSystem ordinaryFontSystem; // A tool to load in all fonts

    // UI
    private IntervalTimer intervalTimer; // A class I made myself to store the interval timer at the top
    private Desktop desktop; // A necessary Myra variable to render all UI elements
    private Grid grid;
    public Bar bar;

    public Game1()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        Directory.SetCurrentDirectory("../../../"); //Sets the working directory to the home directory
        intervalTimer = new IntervalTimer(this); //Creates a new interval timer (Class made by Me)
        
        // Setting up the UI Desktop
        grid = new Grid() {Width = 8, Height = 8};
        desktop = new Desktop();
        desktop.Root = grid;

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

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        intervalTimer.Countdown(gameTime);

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        spriteBatch.Begin();
        desktop.Render();
        spriteBatch.End();
        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
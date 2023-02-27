using System.Collections.Generic;
using System.IO;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;

namespace P2Game;

public class Main : Game
{
    // ReSharper disable once NotAccessedField.Local
    //Boilerplate Variables
    private GraphicsDeviceManager graphics; // A necessary boilerplate code to identify the graphics driver
    private SpriteBatch spriteBatch; // A tool to load all sprites
    public static Game game;
    
    // UI
    private IntervalTimer intervalTimer; // A class I made myself to store the interval timer at the top
    private Desktop mainDesktop; // A necessary Myra variable to render all UI elements
    private Panel panelUi;
    FontSystem hwygothFontSystem = new FontSystem();
    FontSystem robotoFontSystem = new FontSystem();
    
    private Texture2D backgroundSpritewText;
    private Texture2D backgroundSprite;
    
    public static readonly List<Widget> widgets = new List<Widget>();
    public static Bar popularityBar;
    public static Bar pollutionBar;
    public static int moneyValue;
    private Label money;

    public static bool paused = true;

    private Decision decision;

    private GameState currentState;

    private Desktop menuDesktop;
    private Panel menuPanel;

    private TextButton startButton;
    private TextButton quitButton;
    private Label title;
    
    enum GameState
    {
        MainMenu,
        MainState
    }
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

        currentState = GameState.MainMenu;

        SetupUi();
        decision = new Decision(mainDesktop);
        decision.Enable(true);
        moneyValue = 500;
        SetupFonts();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice); //Creates the device needed to load sprites and art
        backgroundSpritewText = Content.Load<Texture2D>("Sprites/BeachBackgroundwithText");
        backgroundSprite = Content.Load<Texture2D>("Sprites/BeachBackground");

        var trashBinTexture = Content.Load<Texture2D>("Sprites/Trash-Bin");
        var trashBackgroundBarTexture = Content.Load<Texture2D>("Sprites/Trash-Bar-Grid");
        var popularityTexture = Content.Load<Texture2D>("Sprites/Popularity-Icon");

        pollutionBar = new Bar(trashBinTexture, trashBackgroundBarTexture,0, graphics.PreferredBackBufferHeight - 48, new Color(109, 86, 34));
        popularityBar = new Bar( popularityTexture, trashBackgroundBarTexture, 600, graphics.PreferredBackBufferHeight - 48, new Color(99, 155, 255));
    }
    
    protected override void Update(GameTime gameTime)
    {
        switch (currentState)
        {
            case GameState.MainMenu:
                MainMenuUpdate();
                break;
            
            case GameState.MainState:
                MainStateUpdate(gameTime);
                break;
        }
    }

    private void MainMenuUpdate()
    {
        if (startButton.IsPressed)
        {
            currentState = GameState.MainState;
        }

        if (quitButton.IsPressed)
        {
            Exit();
        }
    }

    private void MainStateUpdate(GameTime gameTime)
    {
        intervalTimer.Countdown(gameTime, mainDesktop);
            
        //TODO: Cleanup the Main File and make the update function more useful 
        pollutionBar.Update(); //A method which simply updates the text on the bar 
        popularityBar.Update();
        decision.Update();
        UpdateBars(); //A method in the main file itself that updates all 3 bars

        base.Update(gameTime);

        if (pollutionBar.GetPercent() <= 0)
        {
            Win();
        }
        
        else if (popularityBar.GetPercent() >= 100)
        {
            Win();
        }
        
        else if (pollutionBar.GetPercent() >= 100)
        {
            Lose();
        }

        
        
        else if (popularityBar.GetPercent() <= 0)
        {
            Lose();
        }
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Transparent);

        switch (currentState)
        {
            case GameState.MainMenu:
                MainMenuDraw();
                break;
            
            case GameState.MainState:
                MainStateDraw();
                break;
        }

        base.Draw(gameTime);
    }

    private void MainMenuDraw()
    {
        spriteBatch.Begin();
        spriteBatch.Draw(backgroundSprite, Vector2.Zero, Color.White);
        spriteBatch.End();
        menuDesktop.Render();
    }

    private void MainStateDraw()
    {
        spriteBatch.Begin();
        spriteBatch.Draw(backgroundSpritewText, new Vector2(0, 0), Color.White);
        pollutionBar.Render(graphics.GraphicsDevice, spriteBatch);
        popularityBar.Render(graphics.GraphicsDevice, spriteBatch);
        spriteBatch.End();
        
        mainDesktop.Render();
    }
    
    void UpdateBars()
    {
        // if (intervalTimer.GetTimeUp())
        // {
        //     pollutionBar.Update(pollutionBar.GetPercent() + barFactor);
        //     popularityBar.Update(popularityBar.GetPercent() - barFactor);
        //     // tourismBar.SetPercent(tourismBar.GetPercent() + (popularityBar.GetPercent() - pollutionBar.GetPercent()));
        // }
        
        //Keep the money value up-to-date
        money.Text = "$" + moneyValue;
    }

    void SetupUi()
    {
        intervalTimer = new IntervalTimer(); //Creates a new interval timer (Class made by Me)
        
        // Setting up the UI Desktop
        panelUi = new Panel();
        mainDesktop = new Desktop();
        //Sets the tourism bar to the bottom
        

        money = new Label
        {
            Text = "$" + moneyValue,
            TextColor = new Color(17, 140, 79),
            HorizontalAlignment = HorizontalAlignment.Right
        };

        money.Text = "$" + moneyValue;


        foreach (var widget in widgets)
        {
            panelUi.Widgets.Add(widget);
        }
        
        mainDesktop.Root = panelUi;

        menuDesktop = new Desktop();
        menuPanel = new Panel();

        startButton = new TextButton
        {
            Text = "Start",
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
            Top = 10,
            TextColor = Color.White,
            Background = new SolidBrush(Color.Transparent),
            OverBackground = new SolidBrush(Color.Transparent),
            Border = new SolidBrush(Color.White),
            BorderThickness = new Thickness(2)
        };

        quitButton = new TextButton
        {
            Text = "Quit",
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
            Top = 80,
            TextColor = Color.White,
            Background = new SolidBrush(Color.Transparent),
            OverBackground = new SolidBrush(Color.Transparent),
            Border = new SolidBrush(Color.White),
            BorderThickness = new Thickness(2)
        };
        

        title = new Label
        {
            Text = "Beach Dilemma",
            HorizontalAlignment = HorizontalAlignment.Center,
            Top = 80
        };
        
        menuPanel.Widgets.Add(startButton);
        menuPanel.Widgets.Add(quitButton);
        menuPanel.Widgets.Add(title);
        menuDesktop.Root = menuPanel;
    }
    
    void SetupFonts()
    {
        byte[] hwygoth = File.ReadAllBytes("Content/fonts/HWYGOTH.ttf");
        byte[] roboto = File.ReadAllBytes("Content/fonts/Roboto-Regular.ttf");
        hwygothFontSystem.AddFont(hwygoth);
        robotoFontSystem.AddFont(roboto);

        money.Font = hwygothFontSystem.GetFont(48);

        intervalTimer.SetFont(hwygothFontSystem.GetFont(40));

        startButton.Font = robotoFontSystem.GetFont(48);
        quitButton.Font = robotoFontSystem.GetFont(48);


        title.Font = hwygothFontSystem.GetFont(96);

        decision.SetDecisionOnce(hwygothFontSystem.GetFont(36), robotoFontSystem.GetFont(24));
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
        mainDesktop.Root = panelUi;
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
        mainDesktop.Root = panelUi;
        paused = true;
    }
}
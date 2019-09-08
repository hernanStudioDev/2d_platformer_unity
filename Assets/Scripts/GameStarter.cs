using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// This is the main type for your game
/// </summary>
public class Game1 : MonoBehaviour
{
    public const int SCREEN_WIDTH = 800;
    public const int SCREEN_HEIGHT = 600;
    public static Random Random;

    static bool exitGame;

    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    SpriteBatch spriteBatchHUD;

    public static SpriteFont FontSmall;
    public static SpriteFont FontLarge;

    public static Texture2D SprSinglePixel;
    public static GameCamera2D cam = new GameCamera2D();
    public static float _zoom;

    public static BloomComponent bloom;
    public static int bloomSettingsIndex = 0;
    public static float bloomIntensityIndex = 1f;


    #region LevelTextures

    // Level 01
    public Texture2D SprStage1BGMain;
    public Texture2D SprStage1BGMainB;
    public Texture2D SprStage1BGBack;
    public Texture2D SprStage1BGBackB;
    public Texture2D SprStage1FGTreeBlur;
    public Texture2D SprStage1FGDoorEntry01;

    // Level 02
    public Texture2D SprStage2BGMain;
    public Texture2D SprStage2BGBack;


    #endregion

    #region Character Textures

    public Texture2D SprCharacterWalkIdle;
    public Texture2D SprCharacterReact;
    public Texture2D SprCharacterAttacks;
    public Texture2D SprCharacterAttacks02;
    public Texture2D SprCharacterShadow;

    // CutScenes
    public Texture2D SprCutSceneCody01;
    public Texture2D SprCutSceneCody02;
    public Texture2D SprCutSceneAdon01;
    public Texture2D SprCutSceneAdon02;
    public Texture2D SprCutSceneGirl01;
    public Texture2D SprCutSceneDeejay01;
    public Texture2D SprCutSceneDeejay02;
    public Texture2D SprCutSceneDeejay03;

    // Rolento Enemy Ranged
    public Texture2D SprRolentoWalkIdle;
    public Texture2D SprRolentoAttacks;

    // Deejay Enemy Close
    public Texture2D SprDeejayWalkIdle;
    public Texture2D SprDeejayAttacks;

    // Adon Enemy Close
    public Texture2D SprAdonWalkIdle;
    public Texture2D SprAdonAttacks;

    #endregion

    #region Game Item Textures

    public Texture2D SprRocks;
    public Texture2D SprTrashCanNormal;
    public Texture2D SprTrashCanHit;
    public Texture2D SprHealthPack;

    #endregion

    #region HUD Textures

    public Texture2D SprPlayerHUD;
    public Texture2D SprPlayerBarHUD;

    #endregion

    #region Game Menu Textures

    public Texture2D SprTitleScreenBackground;
    public Texture2D SprTitleScreenCharOn;
    public Texture2D SprTitleScreenCharOff;
    public Texture2D SprHowToPlay;

    #endregion

    #region Special Effects Textures

    public Texture2D SprSmallSpark;

    #endregion

    #region Music

    public Song MusicTitleScreen;
    public Song MusicGame01;
    public Song MusicGame02;
    public Song MusicGame03;

    #endregion

    private void Awake()
    {
      Content.RootDirectory = "Content";

      graphics = new GraphicsDeviceManager(this);

      // Set fullscreen
      graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
      graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
      graphics.MinimumPixelShaderProfile = ShaderProfile.PS_3_0;
      //graphics.PreferMultiSampling = true;
      graphics.IsFullScreen = false;

      bloom = new BloomComponent(this);
      Components.Add(bloom);
    }

    /// <summary>
    /// Allows the game to perform any initialization it needs to before starting to run.
    /// This is where it can query for any required services and load any non-graphic
    /// related content.  Calling base.Initialize will enumerate through any components
    /// and initialize them as well.
    /// </summary>
    protected override void Initialize()
    {
        exitGame = false;
        IsMouseVisible = false;

        base.Initialize();
    }

    /// <summary>
    /// LoadContent will be called once per game and is the place to load
    /// all of your content.
    /// </summary>
    protected void LoadContent()
    {
        // Create a new SpriteBatch, which can be used to draw textures.
        spriteBatch = new SpriteBatch(GraphicsDevice);
        spriteBatchHUD = new SpriteBatch(GraphicsDevice);
        Random = new Random();

        FontLarge = Content.Load<SpriteFont>(@"Textures\FontLarge");
        FontSmall = Content.Load<SpriteFont>(@"Textures\FontSmall");
        SprSinglePixel = Content.Load<Texture2D>(@"Textures\singlePixel");

        // Load Level 01
        SprStage1BGBack = Content.Load<Texture2D>(@"Textures\Level\Level01-bg");
        SprStage1BGBackB = Content.Load<Texture2D>(@"Textures\Level\Level01-bg2");
        SprStage1BGMain = Content.Load<Texture2D>(@"Textures\Level\Level01");
        SprStage1BGMainB = Content.Load<Texture2D>(@"Textures\Level\Level01-b");
        SprStage1FGTreeBlur = Content.Load<Texture2D>(@"Textures\Level\TreeBlur");
        SprStage1FGDoorEntry01 = Content.Load<Texture2D>(@"Textures\BG_Objects\DoorEntry");

        // Load Level 02
        SprStage2BGBack = Content.Load<Texture2D>(@"Textures\Level\Level02-bg");
        SprStage2BGMain = Content.Load<Texture2D>(@"Textures\Level\Level02");

        // Cody Animations
        SprCharacterWalkIdle = Content.Load<Texture2D>(@"Textures\Characters\CodySpriteWalkIdleSheet");
        SprCharacterAttacks = Content.Load<Texture2D>(@"Textures\Characters\CodySpriteAttackSheet");
        SprCharacterAttacks02 = Content.Load<Texture2D>(@"Textures\Characters\CodySpriteAttack02Sheet");
        SprCharacterReact = Content.Load<Texture2D>(@"Textures\Characters\CodySpriteReactSheet");
        SprCharacterShadow = Content.Load<Texture2D>(@"Textures\Characters\CodySpriteShadow");

        // Rolento Animations
        SprRolentoWalkIdle = Content.Load<Texture2D>(@"Textures\Characters\Rolento\RolentoWalkIdleSpriteSheet");
        SprRolentoAttacks = Content.Load<Texture2D>(@"Textures\Characters\Rolento\RolentoAttackSpriteSheet");

        // Deejay Animations
        SprDeejayWalkIdle = Content.Load<Texture2D>(@"Textures\Characters\Deejay\DeejayWalkIdleSpriteSheet");
        SprDeejayAttacks = Content.Load<Texture2D>(@"Textures\Characters\Deejay\DeejayAttackSpriteSheet");

        // Adon Animations
        SprAdonWalkIdle = Content.Load<Texture2D>(@"Textures\Characters\Adon\AdonWalkIdleSpriteSheet");
        SprAdonAttacks = Content.Load<Texture2D>(@"Textures\Characters\Adon\AdonAttackSpriteSheet");

        // Game Items ////
        SprRocks = Content.Load<Texture2D>(@"Textures\GameItems\Rocks");
        SprTrashCanNormal = Content.Load<Texture2D>(@"Textures\GameItems\TrashCanNormal");
        SprTrashCanHit = Content.Load<Texture2D>(@"Textures\GameItems\TrashCanHit");
        SprHealthPack = Content.Load<Texture2D>(@"Textures\GameItems\healthpack");

        SprPlayerHUD = Content.Load<Texture2D>(@"Textures\HUD\PlayerOneCodyHUD");
        SprPlayerBarHUD = Content.Load<Texture2D>(@"Textures\HUD\PlayerBar");

        // Cutscene Images //////
        SprCutSceneCody01 = Content.Load<Texture2D>(@"Textures\Cutscenes\cody01");
        SprCutSceneCody02 = Content.Load<Texture2D>(@"Textures\Cutscenes\cody02");
        SprCutSceneAdon01 = Content.Load<Texture2D>(@"Textures\Cutscenes\adon01");
        SprCutSceneAdon02 = Content.Load<Texture2D>(@"Textures\Cutscenes\adon02");
        SprCutSceneGirl01 = Content.Load<Texture2D>(@"Textures\Cutscenes\girl01");
        SprCutSceneDeejay01 = Content.Load<Texture2D>(@"Textures\Cutscenes\deejay01");
        SprCutSceneDeejay02 = Content.Load<Texture2D>(@"Textures\Cutscenes\deejay02");
        SprCutSceneDeejay03 = Content.Load<Texture2D>(@"Textures\Cutscenes\deejay03");

        // Title Screen
        SprTitleScreenBackground = Content.Load<Texture2D>(@"Textures\title_screen");
        SprTitleScreenCharOff = Content.Load<Texture2D>(@"Textures\title_screenCharOff");
        SprTitleScreenCharOn = Content.Load<Texture2D>(@"Textures\title_screenCharOn");
        SprHowToPlay = Content.Load<Texture2D>(@"Textures\HowToPlay");

        // Special Effects
        SprSmallSpark = Content.Load<Texture2D>(@"Textures\Effects\small-spark");

        MusicTitleScreen = Content.Load<Song>(@"Music\music02");
        MusicGame01 = Content.Load<Song>(@"Music\music01");
        MusicGame02 = Content.Load<Song>(@"Music\music02");
        MusicGame03 = Content.Load<Song>(@"Music\music03");

        MenuManager.CreateMenuItems();

        SoundManager.Initialize();

    }

    /// <summary>
    /// UnloadContent will be called once per game and is the place to unload
    /// all content.
    /// </summary>
    protected override void UnloadContent()
    {
        // TODO: Unload any non ContentManager content here
    }

    /// <summary>
    /// Allows the game to run logic such as updating the world,
    /// checking for collisions, gathering input, and playing audio.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected void Update(GameTime gameTime)
    {
        // Allows the game to exit
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
            || exitGame)
            this.Exit();

        if (GamePad.GetState(PlayerIndex.One).Triggers.Right > 0.00001f)
            _zoom = 1.5f;

        if (GamePad.GetState(PlayerIndex.One).Triggers.Left > 0.00001f)
            _zoom = 1f;

        cam.UpdateCameraZoom(_zoom, gameTime);
        bloom.UpdateBloom(bloomIntensityIndex, gameTime);
        InputHelper.UpdateStates();
        GameManager.Update(gameTime);
        SoundManager.Update();
        MusicManager.Update();

        base.Update(gameTime);
    }

    /// <summary>
    /// This is called when the game should draw itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.BackToFront, SaveStateMode.None, cam.get_transformation(GraphicsDevice));
        GraphicsDevice.SamplerStates[0].MagFilter = TextureFilter.Point;

        spriteBatchHUD.Begin();
        GameManager.Draw(spriteBatch, spriteBatchHUD);
        spriteBatch.End();
        spriteBatchHUD.End();

        base.Draw(gameTime);
    }

    public static void ExitGame()
    {
        exitGame = true;
    }

    public void SetWindowSize(int x, int y)
    {
        graphics.PreferredBackBufferWidth = x;
        graphics.PreferredBackBufferHeight = y;
        graphics.ApplyChanges();
    }

}

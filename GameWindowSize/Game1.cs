using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameWindowSize
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager mGraphics;
        SpriteBatch mSpriteBatch;

        TexturedPrimitive mUWBLogo;
        SoccerBall mBall;
        Vector2 mSoccerPosition = new Vector2(50, 50);
        float mSoccerBallRadius = 3f;

        static public SpriteBatch sSpriteBatch;     // Suporte para desenho.
        static public ContentManager sContent;      // Carregando texturas.
        static public GraphicsDeviceManager sGraphics;  // Tamanho corrente da exibição.
        static public System.Random sRan;                  // Para gerar números randomícos.

        // Tamanho da tela preferido.
        const int kWindowWidth = 1000;
        const int kWindowHeight = 1000;

        const int kNumObjects = 4;

        // Trabalha com a classe TexturedPrimitive.
        TexturedPrimitive[] mGraphicsObjects;   // Arranjo de objetos.
        int mCurrentIndex = 0;

        
        public Game1()
        {
            //mGraphics = new GraphicsDeviceManager(this);            
            Content.RootDirectory = "Content";
            Game1.sContent = Content;
            Game1.sGraphics = new GraphicsDeviceManager(this);
            Game1.sGraphics.PreferredBackBufferWidth = kWindowWidth;
            Game1.sGraphics.PreferredBackBufferHeight = kWindowHeight;

            Game1.sRan = new System.Random();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            //mSpriteBatch = new SpriteBatch(GraphicsDevice);

            // Cria um novo SpriteBatch, que pode ser usado para desenhar texturas.
            Game1.sSpriteBatch = new SpriteBatch(GraphicsDevice);
            //Game1.sContent = this.Content;

            // Define os limites da Camera.
            Camera.SetCameraWindow(new Vector2(10f, 20f), 100f);
           

            /* Cria as primitias.
            mGraphicsObjects = new TexturedPrimitive[kNumObjects];
            mGraphicsObjects[0] = new TexturedPrimitive("UWB-JPG",
                new Vector2(15f, 25f),
                new Vector2(10, 10));
            mGraphicsObjects[1] = new TexturedPrimitive("UWB-JPG",
                new Vector2(35f, 60f),
                new Vector2(35f, 35f));
            mGraphicsObjects[2] = new TexturedPrimitive("UWB-PNG",
                new Vector2(105f, 25f),
                new Vector2(10f, 10f));
            mGraphicsObjects[3] = new TexturedPrimitive("UWB-PNG",
                new Vector2(90f, 60f),
                new Vector2(35f, 35f));
                */


            /**
            mGraphicsObjects[0] = new TexturedPrimitive("UWB-JPG",
                new Vector2(15f, 25f),
                new Vector2(10, 10));
            mGraphicsObjects[1] = new TexturedPrimitive("UWB-JPG",
                new Vector2(35f, 60f),
                new Vector2(50f, 50f));
            mGraphicsObjects[2] = new TexturedPrimitive("UWB-PNG",
                new Vector2(105f, 25f),
                new Vector2(10f, 10f));
            mGraphicsObjects[3] = new TexturedPrimitive("UWB-PNG",
                new Vector2(90f, 60f),
                new Vector2(35f, 35f));
    */

            mUWBLogo = new TexturedPrimitive("UWB-PNG", new Vector2(30, 30), new Vector2(20, 20));
            mBall = new SoccerBall(mSoccerPosition, mSoccerBallRadius * 2f);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mUWBLogo.Update(InputWrapper.ThumbSticks.Left, Vector2.Zero);


            mBall.Update();
            mBall.Update(Vector2.Zero, InputWrapper.ThumbSticks.Right);

            if(InputWrapper.Buttons.A == ButtonState.Pressed)
            {
                mBall = new SoccerBall(mSoccerPosition, mSoccerBallRadius * 2f);
            }


            /*

            // TODO: Add your update logic here
            if(InputWrapper.Buttons.A == ButtonState.Pressed)
            {
                if (!mGraphics.IsFullScreen)
                {
                    mGraphics.IsFullScreen = true;
                    mGraphics.ApplyChanges();
                }
            }

            // "B" retorna ao modo enjanelado.
            if(InputWrapper.Buttons.B == ButtonState.Pressed)
            {
                if (mGraphics.IsFullScreen)
                {
                    mGraphics.IsFullScreen = false;
                    mGraphics.ApplyChanges();
                }
            }

            // Botão "X" para selecionar o próximo objeto com que trabalhar.
            if(InputWrapper.Buttons.X == ButtonState.Pressed)
            {
                mCurrentIndex = (mCurrentIndex + 1) % kNumObjects;
            }

            // Atualia o objeto de trabalho atual com os sticks thubms.
            mGraphicsObjects[mCurrentIndex].Update(InputWrapper.ThumbSticks.Left, InputWrapper.ThumbSticks.Right);
            */

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Game1.sSpriteBatch.Begin();     // Inicializa o suporte do desenho.

            // Faça um loop e desenha cada primitiva.
            /**
            foreach(TexturedPrimitive p in mGraphicsObjects)
            {
                p.Draw();
            }
    */

            mUWBLogo.Draw();
            mBall.Draw();

            // Imprimi a mensagem de texto para ecoar o status.
            FontSupport.PrintStatus("Ball Position: " + mBall.Position, null);
            FontSupport.PrintStatusAt(mUWBLogo.Position,
                mUWBLogo.Position.ToString(), Color.White);
            FontSupport.PrintStatusAt(mBall.Position, "Radius " + mBall.Radius, Color.Red);


            // Imprimi a mensagem de texto para ecoar status.
            /**
            FontSupport.PrintStatus("Selected objet is: " + mCurrentIndex + " Location= " +
                mGraphicsObjects[mCurrentIndex].Position, null);

            FontSupport.PrintStatusAt(mGraphicsObjects[mCurrentIndex].Position, "Selected", Color.Red);
    **/

            Game1.sSpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWindowSize
{
    public class TexturedPrimitive
    {
        protected Texture2D mImage;     // A imagem UWB-JPG.jpg a ser carregada
        protected Vector2 mPosition;    // Posição central da imagem.
        protected Vector2 mSize;        // Tamanho da imagem a ser desenhada.

        // accessors
        public Vector2 Position { get { return mPosition; } set { mPosition = value; } }
        public Vector2 Size { get { return mSize; } set { mSize = value; } }

        public Vector2 MinBound { get { return mPosition - (0.5f * mSize); }} 
        public Vector2 MaxBound { get { return mPosition + (0.5f * mSize); }}

        public TexturedPrimitive(String imageName, Vector2 position, Vector2 size)
        {
            mImage = Game1.sContent.Load<Texture2D>(imageName);
            mPosition = position;
            mSize = size;
        }

        public void Update(Vector2 deltaTranslate, Vector2 deltaScale)
        {
            mPosition += deltaTranslate;
            mSize += deltaScale;
        }

        public void Draw()
        {
            // Define onde e o tamanho da textura a exibir.            
            Rectangle destRect = Camera.ComputePixelRectangle(mPosition, mSize);

            Game1.sSpriteBatch.Draw(mImage, destRect, Color.White);
        }
    }
}

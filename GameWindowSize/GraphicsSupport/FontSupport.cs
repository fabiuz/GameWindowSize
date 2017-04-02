using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWindowSize
{
    class FontSupport
    {
        static private SpriteFont sTheFont = null;
        static private Color sDefaultDrawColor = Color.Black;
        static private Vector2 sStatusLocation = new Vector2(5, 5);

        static private void LoadFont()
        {
            // Para propósito de demonstração, carrega a Arial.spriteFont
            if(null == sTheFont)
            {
                sTheFont = Game1.sContent.Load<SpriteFont>("Arial");
            }
        }

        static private Color ColorToUse(Nullable<Color> c)
        {
            return (null == c) ? sDefaultDrawColor : (Color)c;
        }

        static public void PrintStatus(String msg, Nullable<Color> drawColor)
        {
            LoadFont();
            Color useColor = ColorToUse(drawColor);

            // Calcula o canto superior esquerdo como a referência para o status de saída.
            Game1.sSpriteBatch.DrawString(sTheFont, msg, sStatusLocation, useColor);
        }

        static public void PrintStatusAt(Vector2 pos, String msg, Nullable<Color> drawColor)
        {
            LoadFont();
            Color useColor = ColorToUse(drawColor);
            int pixelX, pixelY;

            Camera.ComputePixelPosition(pos, out pixelX, out pixelY);
            Game1.sSpriteBatch.DrawString(sTheFont, msg, new Vector2(pixelX, pixelY), useColor);
        }

    }
}

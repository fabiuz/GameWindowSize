using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWindowSize
{
    static public class Camera
    {
        static private Vector2 sOrigin = Vector2.Zero;      // Origem do mundo.
        static private float sWidth = 100;                 // Largura do mundo.
        static private float sRatio = -1f;                  // Razão entre a janela da câmera e o pixel.
        static private float sHeight = -1f;

        // Suporta colisão com o limite da câmera.
        public enum CameraWindowCollisionStatus
        {
            CollideTop = 0,
            CollideBottom = 1,
            CollideLeft = 2,
            CollideRight = 3,
            InsideWindow = 4
        }

        static public CameraWindowCollisionStatus CollideWithCameraWindow(TexturedPrimitive prim)
        {
            Vector2 min = CameraWindowLowerLeftPosition;
            Vector2 max = CameraWindowUpperRightPosition;

            if (prim.MaxBound.Y > max.Y)
                return CameraWindowCollisionStatus.CollideTop;
            if (prim.MinBound.X < min.Y)
                return CameraWindowCollisionStatus.CollideLeft;
            if (prim.MaxBound.X > max.X)
                return CameraWindowCollisionStatus.CollideRight;
            if (prim.MinBound.Y < min.Y)
                return CameraWindowCollisionStatus.CollideBottom;

            return CameraWindowCollisionStatus.InsideWindow;
        }

        /// Accessors to the camera window bounds
        static public Vector2 CameraWindowLowerLeftPosition
        { get { return sOrigin; } }
        static public Vector2 CameraWindowUpperRightPosition
        { get { return sOrigin + new Vector2(sWidth, sHeight); } }

        static private float cameraWindowToPixelRatio()
        {
            if(sRatio < 0f)
            {
                sRatio = (float)Game1.sGraphics.PreferredBackBufferWidth / sWidth;
                sHeight = sWidth * (float)Game1.sGraphics.PreferredBackBufferHeight / (float)Game1.sGraphics.PreferredBackBufferWidth;
            }

            return sRatio;
        }

        static public void SetCameraWindow(Vector2 origin, float width)
        {
            sOrigin = origin;
            sWidth = width;
            cameraWindowToPixelRatio();
        }

        static public void ComputePixelPosition(Vector2 cameraPosition, out int x, out int y)
        {
            float ratio = cameraWindowToPixelRatio();
            

            // Converte a posição para o espaço pixel.
            x = (int)(((cameraPosition.X - sOrigin.X) * ratio) + 0.5f);
            y = (int)(((cameraPosition.Y - sOrigin.Y) * ratio) + 0.5f);

            y = Game1.sGraphics.PreferredBackBufferHeight - y;
        }

        static public Rectangle ComputePixelRectangle(Vector2 position, Vector2 size)
        {
            float ratio = cameraWindowToPixelRatio();

            // Converte tamanho do espaço da câmera para espaço do pixel.
            int width = (int)((size.X * ratio) + 0.5f);
            int height = (int)((size.Y * ratio) + 0.5f);

            // Converte a posição para espaço do pixel.
            int x, y;
            ComputePixelPosition(position, out x, out y);

            // Reference a posição é o centro.
            y -= height / 2;
            x -= width / 2;

            return new Rectangle(x, y, width, height);
        }

        /// <summary>
        /// Computes a random position inside the current camera window
        /// </summary>
        /// <returns></returns>
        static public Vector2 RandomPosition()
        {
            float rangeX = 0.8f * sWidth;
            float offsetX = 0.1f * sWidth;
            float rangeY = 0.8f * sHeight;
            float offsetY = 0.1f * sHeight;

            float x = (float)(Game1.sRan.NextDouble()) * rangeX + offsetX + sOrigin.X;
            float y = (float)(Game1.sRan.NextDouble()) * rangeY + offsetY + sOrigin.Y;

            return new Vector2(x, y);
        }

    }
}

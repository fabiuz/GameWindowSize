using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWindowSize
{
    public class BasketBall: TexturedPrimitive
    {
        private const float kIncreaseRate = 1.001f;
        private Vector2 kInitSize = new Vector2(5, 5);
        private const float kFinalSize = 15f;

        public BasketBall() : base("BasketBall")
        {
            mPosition = Camera.RandomPosition();
            mSize = kInitSize;
        }

        public bool UpdateAndExplode()
        {
            mSize *= kIncreaseRate;
            return mSize.X > kFinalSize;
        }
    }
}

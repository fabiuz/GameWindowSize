using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWindowSize
{
    class MyGame
    {
        TexturedPrimitive mHero;
        Vector2 kHeroSize = new Vector2(15, 15);
        Vector2 kHeroPosition = Vector2.Zero;

        List<BasketBall> mBBallList;
        TimeSpan mCreationTimeStamp;
        int mTotalBBallCreated = 0;

        // Isto é 0.5 segundos.
        const int kBballMSecInterval = 500;

        // GameState
        int mScore = 0;
        int mBBallMissed = 0, mBBallHit = 0;
        const int kBballTouchScore = 1;
        const int kBballMissedScore = -2;
        const int kWinScore = 10;
        const int kLossScore = -10;
        TexturedPrimitive mFinal = null;

        public MyGame()
        {
            // Herói.
            mHero = new TexturedPrimitive("Me", kHeroPosition, kHeroSize);

            // Basketballs
            mCreationTimeStamp = new TimeSpan(0);
            mBBallList = new List<BasketBall>();

        }

        public void UpdateGame(GameTime gameTime)
        {
            if(null != mFinal)
            {
                return;
            }

            mHero.Update(InputWrapper.ThumbSticks.Right);

            // Basketball
            for(int b = mBBallList.Count - 1; b >= 0; b--)
            {
                if (mBBallList[b].UpdateAndExplode())
                {
                    mBBallList.RemoveAt(b);
                    mBBallMissed++;
                    mScore += kBballMissedScore;
                }
            }

            for(int b = mBBallList.Count - 1; b >= 0; b--)
            {
                if (mHero.PrimitivesTouches(mBBallList[b]))
                {
                    mBBallList.RemoveAt(b);
                    mBBallHit++;
                    mScore += kBballTouchScore;
                }
            }

            // Verifica para nova condição de basketball.
            TimeSpan timePassed = gameTime.TotalGameTime;
            timePassed = timePassed.Subtract(mCreationTimeStamp);

            if(timePassed.TotalMilliseconds > kBballMSecInterval)
            {
                mCreationTimeStamp = gameTime.TotalGameTime;
                BasketBall b = new BasketBall();
                mTotalBBallCreated++;
                mBBallList.Add(b);
            }

            // Verifica por condição de vitória.
            if (mScore > kWinScore)
                mFinal = new TexturedPrimitive("Winner",
                    new Vector2(75, 50), new Vector2(30, 20));
            else if (mScore < kLossScore)
                mFinal = new TexturedPrimitive("Looser",
                    new Vector2(75, 90), new Vector2(30, 20));
        }

        public void DrawGame()
        {
            mHero.Draw();

            foreach(BasketBall b in mBBallList)
            {
                b.Draw();
            }

            if(null != mFinal)
            {
                mFinal.Draw();
            }

            // Desenha último para sempre exibir no topo.
            FontSupport.PrintStatus("Status: " +
                "Score = " + mScore +
                " Basketball: Generated( " + mTotalBBallCreated +
                ") Collected(" + mBBallHit + ") Missed(" + mBBallMissed + ")",
                null);
        }
    }
}

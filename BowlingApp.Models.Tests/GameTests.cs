using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;

namespace BowlingApp.Models.Tests
{
    public class GameTests
    {
        public GameTests()
        {
            game = new Game();
        }

        private Game game;

        private void repeatRolls(int rolls, int pins)
        {
            for (int i = 0; i < rolls; i++)
            {
                game.Roll(pins);
            }
        }


        #region GetScore
        [Fact]
        public void TestGetScore_FirstFrame()
        {
            game.RollMany(new int[] { 1, 5, 3, 7, 3, 6, 10, 5, 2, 6, 4, 10, 10, 9, 1, 10, 10, 10 });
            Assert.Equal(6, game.GetScore(0));
        }

        [Fact]
        public void TestGetScore_FifthFrame()
        {
            game.RollMany(new int[] { 1, 5, 3, 7, 3, 6, 10, 5, 2, 6, 4, 10, 10, 9, 1, 10, 10, 10 });
            Assert.Equal(52, game.GetScore(4));
        }

        [Fact]
        public void TestGetScore_PerfectGame()
        {
            repeatRolls(12, 10);
            Assert.Equal(300, game.GetScore());
        }

        [Fact]
        public void TestGetScore_Random_ExtraRolls()
        {
            game.RollMany(new int[] { 1, 5, 3, 7, 3, 6, 10, 5, 2, 6, 4, 10, 10, 9, 1, 10, 10, 10 });
            Assert.Equal(171, game.GetScore());
        }

        [Fact]
        public void TestGetScore_Random_NoExtraRolls()
        {
            game.RollMany(new int[] { 4, 5, 3, 4, 7, 3, 7, 3, 9, 0, 10, 3, 5, 10, 10, 7, 2 });
            Assert.Equal(142, game.GetScore());
        }

        [Fact]
        public void TestGetScore_Gutterballs()
        {
            repeatRolls(20, 0);
            Assert.Equal(0, game.GetScore());
        }

        [Fact]
        public void TestGetScore_AllFours()
        {
            repeatRolls(20, 4);
            Assert.Equal(80, game.GetScore());
        }
        #endregion
        #region GetFrameIndex
        [Fact]
        public void TestGetFrameIndex_NoStrikesNoSpares1()
        {
            repeatRolls(5, 4);
            Assert.Equal(2, game.GetFrameIndex(game.CurrentRollIndex));
        }

        [Fact]
        public void TestGetFrameIndex_NoStrikesNoSpares2()
        {
            repeatRolls(6, 4);
            Assert.Equal(3, game.GetFrameIndex(game.CurrentRollIndex));
        }

        [Fact]
        public void TestGetFrameIndex_WithSpares1()
        {
            game.RollMany(new int[] { 1, 2, 4, 6, 5, 2, 2, 2 });
            Assert.Equal(4, game.GetFrameIndex(game.CurrentRollIndex));
        }

        [Fact]
        public void TestGetFrameIndex_WithSpares2()
        {
            game.RollMany(new int[] { 9, 1, 9, 1, 5, 2, 2, 2, 9 });
            Assert.Equal(4, game.GetFrameIndex(game.CurrentRollIndex));
        }

        [Fact]
        public void TestGetFrameIndex_WithStrikes1()
        {
            game.RollMany(new int[] { 10, 3, 3, 3, 3, 2, 2, 2 });
            Assert.Equal(4, game.GetFrameIndex(game.CurrentRollIndex));
        }

        [Fact]
        public void TestGetFrameIndex_WithStrikes2()
        {
            game.RollMany(new int[] { 10, 10, 10, 3, 3, 2, 2, 2 });
            Assert.Equal(5, game.GetFrameIndex(game.CurrentRollIndex));
        }

        [Fact]
        public void TestGetFrameIndex_MaxRolls()
        {
            repeatRolls(12, 10);
            Assert.Equal(9, game.GetFrameIndex(game.CurrentRollIndex));
        }
        #endregion
        #region GetFrame
        [Fact]
        public void TestGetFrame_FirstFrame()
        {          
            repeatRolls(12, 10);

            Frame frameTest = new Frame() { Rolls = new int[] { 10}, Score = 30 };
            Frame frame = game.GetFrame(0);

            string frameTestJson = JsonConvert.SerializeObject(frameTest);
            string frameJson = JsonConvert.SerializeObject(frame);
            Assert.Equal(frameTestJson, frameJson);
        }

        [Fact]
        public void TestGetFrame_FifthFrame()
        {
            repeatRolls(12, 10);

            Frame frameTest = new Frame() { Rolls = new int[] { 10 }, Score = 150 };
            Frame frame = game.GetFrame(4);

            string frameTestJson = JsonConvert.SerializeObject(frameTest);
            string frameJson = JsonConvert.SerializeObject(frame);
            Assert.Equal(frameTestJson, frameJson);
        }

        [Fact]
        public void TestGetFrame_LastFrame()
        {
            repeatRolls(12, 10);

            Frame frameTest = new Frame() { Rolls = new int[] { 10, 10, 10 }, Score = 300 };
            Frame frame = game.GetFrame(9);

            string frameTestJson = JsonConvert.SerializeObject(frameTest);
            string frameJson = JsonConvert.SerializeObject(frame);
            Assert.Equal(frameTestJson, frameJson);
        }
        #endregion
        #region GetIsGameOver
        [Fact]
        public void GetIsGameOver_RandomGame()
        {
            game.RollMany(new int[] { 1, 5, 3, 7, 3, 6, 10, 5, 2, 6, 4, 10, 10, 9, 1, 9, 0});
            Assert.True(game.GetIsGameOver());
        }

        [Fact]
        public void GetIsGameOver_PerfectGame()
        {
            repeatRolls(12, 10);
            Assert.True(game.GetIsGameOver());
        }

        [Fact]
        public void GetIsGameOver_NotOver()
        {
            repeatRolls(4, 10);
            Assert.False(game.GetIsGameOver());
        }

        [Fact]
        public void IsGameOver_GutterBalls()
        {
            repeatRolls(20, 0);
            Assert.True(game.GetIsGameOver());
        }
        #endregion







    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingApp.Models
{
    public class Game
    {
        #region Properties
        public int[] Rolls { get; set; } = new int[21];
        public Frame[] Frames = new Frame[10].Select(f => new Frame()).ToArray();


        private int _CurrentRollIndex = 0;
        public int CurrentRollIndex {
            get
            {
                return _CurrentRollIndex;
            }
            set
            {
                _CurrentRollIndex = value;
                OnCurrentRollIndexChanged(CurrentRollIndex);
            }
        }

        private int _CurrentFrameIndex = 0;
        public int CurrentFrameIndex
        {
            get
            {
                return _CurrentFrameIndex;
            }
            set
            {
                _CurrentFrameIndex = value;
                OnCurrentFrameIndexChanged(CurrentFrameIndex);
            }
        }

        private int _Score = 0;
        public int Score
        {
            get
            {
                return _Score;
            }
            set
            {
                _Score = value;
                OnScoreChanged(Score);
            }
        }

        private bool _IsGameOver = false;
        public bool IsGameOver
        {
            get
            {
                return _IsGameOver;
            }
            set
            {
                _IsGameOver = value;
                OnIsGameOverChanged(IsGameOver);
            }
        }

        #endregion

        #region Functions
        public void Reset()
        {
            Rolls = new int[21];
            Frames = new Frame[10].Select(f => new Frame()).ToArray();
            CurrentRollIndex = 0;
            CurrentFrameIndex = 0;
            Score = 0;
            IsGameOver = false;
        }

        public void Roll()
        {
            Random rnd = new Random();

            Frame currentFrame = Frames[CurrentFrameIndex];

            if (CurrentFrameIndex == 9)
            {
                if (currentFrame.RollIndexes.Count() == 0)
                {
                    int pins = rnd.Next(0, 11);
                    Roll(pins);
                }
                else if (currentFrame.RollIndexes.Count() == 1)
                {
                    int firstRoll = Rolls[currentFrame.RollIndexes[0]];
                    if (firstRoll == 10)
                    {
                        int pins = rnd.Next(0, 11);
                        Roll(pins);
                    }
                    else
                    {
                        int pins = rnd.Next(0, 11 - firstRoll);
                        Roll(pins);
                    }
                }
                else
                {
                    int secondRoll = Rolls[currentFrame.RollIndexes[1]];
                    if (secondRoll == 10)
                    {
                        int pins = rnd.Next(0, 11);
                        Roll(pins);
                    }
                    else
                    {
                        int pins = rnd.Next(0, 11 - secondRoll);
                        Roll(pins);
                    }
                }
            }
            else
            {
                if (currentFrame.RollIndexes.Count() == 0)
                {
                    int pins = rnd.Next(0, 11);
                    Roll(pins);
                }
                else
                {
                    int firstRoll = Rolls[currentFrame.RollIndexes[0]];
                    int pins = rnd.Next(0, 11 - firstRoll);
                    Roll(pins);
                }
            }
        }

        public bool GetIsGameOver()
        {
            if (CurrentFrameIndex == 9)
            {
                Frame lastFrame = Frames[CurrentFrameIndex];
                if (lastFrame.RollIndexes.Count() == 2)
                {
                    int firstRoll = Rolls[lastFrame.RollIndexes[0]];
                    int secondRoll = Rolls[lastFrame.RollIndexes[1]];
                    if (firstRoll == 10 || (firstRoll + secondRoll == 10))
                    {
                        return false;
                    }
                    return true;
                }
                else if (lastFrame.RollIndexes.Count() == 3)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsSpare(int rollIndex)
        {
            return Rolls[rollIndex] + Rolls[rollIndex + 1] == 10;
        }

        public bool IsStrike(int rollIndex)
        {
            return Rolls[rollIndex] == 10;
        }

        public void Roll(int pins)
        {
            if (IsGameOver == false)
            {
                CurrentFrameIndex = GetFrameIndex(CurrentRollIndex);
                Frames[CurrentFrameIndex].RollIndexes.Add(CurrentRollIndex);
                Rolls[CurrentRollIndex] = pins;
                CurrentRollIndex++;
                CurrentFrameIndex = GetFrameIndex(CurrentRollIndex);
                Score = GetScore();
                IsGameOver = GetIsGameOver();
            }
        }

        public void RollMany(int[] pinArray)
        {
            for (int i = 0; i < pinArray.Length; i++)
            {
                Roll(pinArray[i]);
            }
        }

        public int GetFrameIndex(int rollIndex)
        {
            int frameIndex = 0;
            bool isSecondRoll = false;
            for (int i = 0; i < rollIndex; i++)
            {
                if (frameIndex != 9)
                {
                    if (isSecondRoll)
                    {
                        frameIndex++;
                        isSecondRoll = false;
                    }
                    else
                    {
                        if (Rolls[i] == 10)
                        {
                            frameIndex++;
                        }
                        else
                        {
                            isSecondRoll = true;
                        }
                    }
                }
            }
            return frameIndex;
        }

        //public Frame GetFrame(int frameIndex, bool includeScore = true)
        //{
        //    Frame frame = new Frame();
        //    List<int> frameRolls = new List<int>();
        //    for (int rollIndex = 0; rollIndex < CurrentRollIndex; rollIndex++)
        //    {
        //        if (GetFrameIndex(rollIndex) == frameIndex)
        //        {
        //            frameRolls.Add(Rolls[rollIndex]);
        //        }
        //    }

        //    frame.Rolls = frameRolls.ToArray();
        //    if (includeScore)
        //    {
        //        frame.Score = GetScore(frameIndex);
        //    }


        //    return frame;
        //}

        public int GetScore(int frameTarget = 9)
        {
            int score = 0;
            int rollIndex = 0;
            for (int frame = 0; frame < frameTarget + 1; frame++)
            {
                if (IsSpare(rollIndex))
                {
                    score += 10 + Rolls[rollIndex + 2];
                    rollIndex += 2;
                }
                else if (IsStrike(rollIndex))
                {
                    score += 10 + Rolls[rollIndex + 1] + Rolls[rollIndex + 2];
                    rollIndex++;
                }
                else
                {
                    score += Rolls[rollIndex] + Rolls[rollIndex + 1];
                    rollIndex += 2;
                }

            }

            return score;
        }


        protected void OnCurrentRollIndexChanged(int currentRollIndex)
        {
            CurrentRollIndexChanged?.Invoke(this, currentRollIndex);
        }

        protected void OnCurrentFrameIndexChanged(int currentFrameIndex)
        {
            CurrentFrameIndexChanged?.Invoke(this, currentFrameIndex);
        }

        protected void OnScoreChanged(int score)
        {
            ScoreChanged?.Invoke(this, score);
        }

        protected void OnIsGameOverChanged(bool isGameOver)
        {
            IsGameOverChanged?.Invoke(this, isGameOver);
        }

        public void EditFrame(int frameIndex, Frame frame)
        {

        }

        #endregion

        #region Events
        public event EventHandler<int> CurrentRollIndexChanged;
        public event EventHandler<int> CurrentFrameIndexChanged;
        public event EventHandler<int> ScoreChanged;
        public event EventHandler<bool> IsGameOverChanged;
        #endregion
    }
}

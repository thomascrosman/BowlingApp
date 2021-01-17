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

        public int NextRollMaximum
        {
            get
            {
                int currentFrameIndex = GetFrameIndex(CurrentRollIndex);
                Frame currentFrame = Frames[currentFrameIndex];

                if (currentFrameIndex == 9)
                {
                    if (currentFrame.RollIndexes.Count() == 0)
                    {
                        return 10;
                    }
                    else if (currentFrame.RollIndexes.Count() == 1)
                    {
                        int firstRoll = Rolls[currentFrame.RollIndexes[0]];
                        if (firstRoll == 10)
                        {
                            return 10;
                        }
                        else
                        {
                            return 10 - firstRoll;
                        }
                    }
                    else
                    {
                        int secondRoll = Rolls[currentFrame.RollIndexes[1]];
                        if (secondRoll == 10)
                        {
                            return 10;
                        }
                        else
                        {
                            return 10 - secondRoll;
                        }
                    }
                }
                else
                {
                    if (currentFrame.RollIndexes.Count() == 0)
                    {
                        return 10;
                    }
                    else
                    {
                        int firstRoll = Rolls[currentFrame.RollIndexes[0]];
                        return 10 - firstRoll;
                    }
                }
            }
        }

        #endregion

        #region Functions
        public void Reset()
        {
            Rolls = new int[21];
            Frames.ToList().ForEach(frame => frame.RollIndexes.Clear());
            CurrentRollIndex = 0;
            Score = 0;
            IsGameOver = false;
        }

        public void Roll(int pins)
        {
            if (IsGameOver == false)
            {
                int currentFrameIndex = GetFrameIndex(CurrentRollIndex);
                Frames[currentFrameIndex].RollIndexes.Add(CurrentRollIndex);
                Rolls[CurrentRollIndex] = pins;
                CurrentRollIndex++;
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



        public bool GetIsGameOver()
        {
            int currentFrameIndex = GetFrameIndex(CurrentRollIndex);
            if (currentFrameIndex == 9)
            {
                Frame lastFrame = Frames[currentFrameIndex];
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

        protected void OnScoreChanged(int score)
        {
            ScoreChanged?.Invoke(this, score);
        }

        protected void OnIsGameOverChanged(bool isGameOver)
        {
            IsGameOverChanged?.Invoke(this, isGameOver);
        }


        #endregion

        #region Events
        public event EventHandler<int> CurrentRollIndexChanged;
        public event EventHandler<int> ScoreChanged;
        public event EventHandler<bool> IsGameOverChanged;
        #endregion
    }
}

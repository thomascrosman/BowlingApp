using BowlingApp.Models;
using GalaSoft.MvvmLight;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace BowlingApp
{
    public class GameVM : ViewModelBase
    {
        public GameVM()
        {
            game = new Game();

            FrameVMs = new BindingList<FrameVM>();

            foreach (Frame frame in game.Frames)
            {
                FrameVMs.Add(new FrameVM(this, frame));
            }

            game.CurrentRollIndexChanged += OnCurrentRollIndexChanged;
            game.CurrentFrameIndexChanged += OnCurrentFrameIndexChanged;
            game.ScoreChanged += OnScoreChanged;
            game.IsGameOverChanged += OnIsGameOverChanged;

        }

        private Game game;

        #region Commands
        private ICommand _RollCommand;
        public ICommand RollCommand
        {
            get
            {
                return _RollCommand ?? (_RollCommand = new CommandHandler(() => Roll(), () => !IsGameOver));
            }
        }

        private ICommand _ResetCommand;
        public ICommand ResetCommand
        {
            get
            {
                return _ResetCommand ?? (_ResetCommand = new CommandHandler(() => Reset(), () => true));
            }
        }
        #endregion


        public void LoadRolls(int[] rolls)
        {
            game.Reset();
            game.RollMany(rolls);
        }

        public void Reset()
        {
            game.Reset();
        }


        public void Roll()
        {
            game.Roll();
        }

        public void Roll(int roll)
        {
            game.Roll(roll);
        }

        public int GetScoreByFrameIndex(int frameIndex)
        {
            return game.GetScore(frameIndex);
        }

        public int CurrentRollIndex
        {
            get
            {
                return game.CurrentRollIndex;
            }
        }

        public int CurrentFrameIndex
        {
            get
            {
                return game.CurrentFrameIndex;
            }
        }

        public int Score
        {
            get
            {
                return game.Score;
            }
        }

        public int[] Rolls
        {
            get
            {
                return game.Rolls;
            }
        }

        public Frame[] Frames
        {
            get
            {
                return game.Frames;
            }
        }

        public bool IsGameOver
        {
            get
            {
                return game.IsGameOver;
            }
        }


        public void OnCurrentFrameIndexChanged(object sender, int currentFrameIndex)
        {
            RaisePropertyChanged(() => CurrentFrameIndex);
            CurrentRollIndexChanged?.Invoke(this, currentFrameIndex);
        }

        private void OnCurrentRollIndexChanged(object sender, int currentRollIndex)
        {
            RaisePropertyChanged(() => CurrentRollIndex);
            CurrentFrameIndexChanged?.Invoke(this, currentRollIndex);
        }

        private void OnScoreChanged(object sender, int score)
        {
            RaisePropertyChanged(() => Score);
            //for (int frameIndex = 0; frameIndex < CurrentFrameIndex + 1; frameIndex++)
            //{
            //    FrameVM frameVM = FrameVMs[frameIndex];
            //    frameVM.Rolls.Clear();
            //    Frame frame = game.Frames[frameIndex];
            //    foreach(int rollIndex in frame.RollIndexes)
            //    {
            //        int roll = game.Rolls[rollIndex];
            //        frameVM.Rolls.Add(roll);
            //    }

            //    int frameScore = game.GetScore(frameIndex);

            //    frameVM.ScoreSlotTotal = frameScore.ToString();               
            //}
        }

        private void OnIsGameOverChanged(object sender, bool isGameOver)
        {
            RaisePropertyChanged(() => IsGameOver);
        }



        public BindingList<FrameVM> FrameVMs { get; set; }


        public event EventHandler<int> CurrentRollIndexChanged;
        public event EventHandler<int> CurrentFrameIndexChanged;

    }
}

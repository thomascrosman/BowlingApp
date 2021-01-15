using BowlingApp.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Frame = BowlingApp.Models.Frame;

namespace BowlingApp
{
    public class GameVM : ViewModelBase
    {
        public GameVM()
        {
            FrameVMs = new BindingList<FrameVM>
            {
                new FrameVM(0),
                new FrameVM(1),
                new FrameVM(2),
                new FrameVM(3),
                new FrameVM(4),
                new FrameVM(5),
                new FrameVM(6),
                new FrameVM(7),
                new FrameVM(8),
                new FrameVM(9),
            };

            game = new Game();
            game.CurrentRollIndexChanged += OnCurrentRollIndexChanged;
            game.CurrentFrameIndexChanged += OnCurrentFrameIndexChanged;
            game.ScoreChanged += OnScoreChanged;
            game.IsGameOverChanged += OnIsGameOverChanged;
        }

        #region Commands
        private ICommand _RollCommand;
        public ICommand RollCommand
        {
            get
            {
                return _RollCommand ?? (_RollCommand = new CommandHandler(() => Roll(), () => !IsGameOver));
            }
        }

        private ICommand _LoadRollsCommand;
        public ICommand LoadRollsCommand
        {
            get
            {
                return _LoadRollsCommand ?? (_LoadRollsCommand = new CommandHandler(() => LoadRolls(), () => true));
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


        public void LoadRolls()
        {
            MessageBox.Show("Loading Rolls");
            game.Reset();
            game.RollMany(new int[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 });
        }

        public void Reset()
        {
            game.Reset();
            foreach (FrameVM frameVM in FrameVMs)
            {
                frameVM.Reset();
            }
        }



        private Game game;

        public void Roll()
        {
            game.Roll();
        }

        public void Roll(int roll)
        {
            game.Roll(roll);
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

        public bool IsGameOver
        {
            get
            {
                return game.IsGameOver;
            }
        }


        private void OnCurrentFrameIndexChanged(object sender, int currentFrameIndex)
        {
            RaisePropertyChanged(() => CurrentFrameIndex);
        }

        private void OnCurrentRollIndexChanged(object sender, int currentRollIndex)
        {
            RaisePropertyChanged(() => CurrentRollIndex);
        }

        private void OnScoreChanged(object sender, int score)
        {
            RaisePropertyChanged(() => Score);
            for (int frameIndex = 0; frameIndex < CurrentFrameIndex + 1; frameIndex++)
            {
                FrameVM frameVM = FrameVMs[frameIndex];
                frameVM.Rolls.Clear();
                Frame frame = game.Frames[frameIndex];
                foreach(int rollIndex in frame.RollIndexes)
                {
                    int roll = game.Rolls[rollIndex];
                    frameVM.Rolls.Add(roll);
                }

                int frameScore = game.GetScore(frameIndex);

                frameVM.ScoreSlotTotal = frameScore.ToString();               
            }
        }

        private void OnIsGameOverChanged(object sender, bool isGameOver)
        {
            RaisePropertyChanged(() => IsGameOver);
        }



        public BindingList<FrameVM> FrameVMs { get; set; }
       
    }
}

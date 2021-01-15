using BowlingApp.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private ICommand _ResetCommand;
        public ICommand ResetCommand
        {
            get
            {
                return _ResetCommand ?? (_ResetCommand = new CommandHandler(() => Reset(), () => true));
            }
        }
        #endregion



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
                Frame frame = game.GetFrame(frameIndex);
                foreach(int roll in frame.Rolls)
                {
                    frameVM.Rolls.Add(roll);
                }
                frameVM.ScoreSlotTotal = frame.Score.ToString();               
            }
        }

        private void OnIsGameOverChanged(object sender, bool isGameOver)
        {
            RaisePropertyChanged(() => IsGameOver);
        }



        public BindingList<FrameVM> FrameVMs { get; set; }
       
    }
}

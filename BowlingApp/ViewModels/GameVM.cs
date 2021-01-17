using BowlingApp.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
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
            game.ScoreChanged += OnScoreChanged;
            game.IsGameOverChanged += OnIsGameOverChanged;

        }

        #region Properties and Variables
        private Game game;

        public BindingList<FrameVM> FrameVMs { get; set; }

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
                return game.GetFrameIndex(CurrentRollIndex);
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

        public Frame CurrentFrame
        {
            get
            {
                return game.Frames[CurrentFrameIndex];
            }
        }
        #endregion

        #region Functions
        public void Roll(int? arg = null)
        {
            if(arg == null)
            {
                Random rnd = new Random();
                game.Roll(rnd.Next(0, game.NextRollMaximum + 1));
            }
            else
            {
                game.Roll(arg.Value);
            }
        }

        public int GetScoreByFrameIndex(int frameIndex)
        {
            return game.GetScore(frameIndex);
        }

        public void Reset()
        {
            game.Reset();
        }

        public void LoadRolls(int[] rolls)
        {
            game.Reset();
            game.RollMany(rolls);
        }

        public void OnCurrentFrameIndexChanged(object sender, int currentFrameIndex)
        {
            RaisePropertyChanged(() => CurrentFrameIndex);
            CurrentFrameIndexChanged?.Invoke(this, currentFrameIndex);
            RaisePropertyChanged(() => CurrentFrame);
        }

        private void OnCurrentRollIndexChanged(object sender, int currentRollIndex)
        {
            RaisePropertyChanged(() => CurrentRollIndex);
            CurrentRollIndexChanged?.Invoke(this, currentRollIndex);
            CurrentFrameIndexChanged?.Invoke(this, game.GetFrameIndex(currentRollIndex));
        }

        private void OnScoreChanged(object sender, int score)
        {
            RaisePropertyChanged(() => Score);
        }

        private void OnIsGameOverChanged(object sender, bool isGameOver)
        {
            RaisePropertyChanged(() => IsGameOver);
        }
        #endregion

        #region Events
        public event EventHandler<int> CurrentRollIndexChanged;
        public event EventHandler<int> CurrentFrameIndexChanged;
        #endregion

        #region Commands
        private ICommand _RollRandomizeCommand;
        public ICommand RollRandomizeCommand
        {
            get
            {
                return _RollRandomizeCommand ?? (_RollRandomizeCommand = new CommandHandler(() => Roll(), () => !IsGameOver));
            }
        }

        private ICommand _Roll0Command;
        public ICommand Roll0Command
        {
            get
            {
                return _Roll0Command ?? (_Roll0Command = new CommandHandler(() => Roll(0), () => !IsGameOver));
            }
        }

        private ICommand _Roll1Command;
        public ICommand Roll1Command
        {
            get
            {
                return _Roll1Command ?? (_Roll1Command = new CommandHandler(() => Roll(1), () => (!IsGameOver && game.NextRollMaximum >= 1)));
            }
        }

        private ICommand _Roll2Command;
        public ICommand Roll2Command
        {
            get
            {
                return _Roll2Command ?? (_Roll2Command = new CommandHandler(() => Roll(2), () => (!IsGameOver && game.NextRollMaximum >= 2)));
            }
        }

        private ICommand _Roll3Command;
        public ICommand Roll3Command
        {
            get
            {
                return _Roll3Command ?? (_Roll3Command = new CommandHandler(() => Roll(3), () => (!IsGameOver && game.NextRollMaximum >= 3)));
            }
        }

        private ICommand _Roll4Command;
        public ICommand Roll4Command
        {
            get
            {
                return _Roll4Command ?? (_Roll4Command = new CommandHandler(() => Roll(4), () => (!IsGameOver && game.NextRollMaximum >= 4)));
            }
        }

        private ICommand _Roll5Command;
        public ICommand Roll5Command
        {
            get
            {
                return _Roll5Command ?? (_Roll5Command = new CommandHandler(() => Roll(5), () => (!IsGameOver && game.NextRollMaximum >= 5)));
            }
        }

        private ICommand _Roll6Command;
        public ICommand Roll6Command
        {
            get
            {
                return _Roll6Command ?? (_Roll6Command = new CommandHandler(() => Roll(6), () => (!IsGameOver && game.NextRollMaximum >= 6)));
            }
        }

        private ICommand _Roll7Command;
        public ICommand Roll7Command
        {
            get
            {
                return _Roll7Command ?? (_Roll7Command = new CommandHandler(() => Roll(7), () => (!IsGameOver && game.NextRollMaximum >= 7)));
            }
        }

        private ICommand _Roll8Command;
        public ICommand Roll8Command
        {
            get
            {
                return _Roll8Command ?? (_Roll8Command = new CommandHandler(() => Roll(8), () => (!IsGameOver && game.NextRollMaximum >= 8)));
            }
        }

        private ICommand _Roll9Command;
        public ICommand Roll9Command
        {
            get
            {
                return _Roll9Command ?? (_Roll9Command = new CommandHandler(() => Roll(9), () => (!IsGameOver && game.NextRollMaximum >= 9)));
            }
        }

        private ICommand _Roll10Command;
        public ICommand Roll10Command
        {
            get
            {
                return _Roll10Command ?? (_Roll10Command = new CommandHandler(() => Roll(10), () => (!IsGameOver && game.NextRollMaximum >= 10)));
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
    }
}

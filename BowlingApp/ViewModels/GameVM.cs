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

        public ObservableCollection<int> PossibleNextThrowValues
        {
            get
            {
                ObservableCollection<int> possibleNextThrowValues = new ObservableCollection<int>();
                possibleNextThrowValues.Add(0);
                possibleNextThrowValues.Add(1);
                possibleNextThrowValues.Add(2);
                possibleNextThrowValues.Add(3);

                return possibleNextThrowValues;
            }
        }

        private int _NextThrowValue;
        public int NextThrowValue
        {
            get
            {
                return _NextThrowValue;
            }
            set
            {
                _NextThrowValue = value;
                RaisePropertyChanged(() => NextThrowValue);
            }
        }
        #endregion

        #region Functions
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

        #region Events
        public event EventHandler<int> CurrentRollIndexChanged;
        public event EventHandler<int> CurrentFrameIndexChanged;
        #endregion
    }
}

using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BowlingApp
{
    public class FrameVM : ViewModelBase
    {
        public FrameVM(int frameIndex)
        {
            FrameIndex = frameIndex;
            Rolls = new ObservableCollection<int>();
            Rolls.CollectionChanged += OnRollsChanged;
        }

        private ICommand _SetRollCommand;
        public ICommand SetRollCommand
        {
            get
            {
                return _SetRollCommand ?? (_SetRollCommand = new CommandHandler(() => SetRoll(), () => IsActiveFrame));
            }
        }


        public void SetRoll()
        {

        }

        private bool _IsActiveFrame;
        public bool IsActiveFrame
        {
            get
            {
                return _IsActiveFrame;
            }
            set
            {
                _IsActiveFrame = value;
                RaisePropertyChanged(() => IsActiveFrame);
            }
        }



        public int FrameNumber
        {
            get
            {
                return FrameIndex + 1;
            }
        }

        public ObservableCollection<int> Rolls;
        private int _FrameIndex;
        public int FrameIndex
        {
            get
            {
                return _FrameIndex;
            }
            set
            {
                _FrameIndex = value;
                
            }
        }


        private string _ScoreSlot1;
        public string ScoreSlot1
        {
            get
            {
                return _ScoreSlot1;
            }
            set
            {
                _ScoreSlot1 = value;
                RaisePropertyChanged(() => ScoreSlot1);
            }
        }

        private string _ScoreSlot2;
        public string ScoreSlot2
        {
            get
            {
                return _ScoreSlot2;
            }
            set
            {
                _ScoreSlot2 = value;
                RaisePropertyChanged(() => ScoreSlot2);
            }
        }

        private string _ScoreSlot3;
        public string ScoreSlot3
        {
            get
            {
                return _ScoreSlot3;
            }
            set
            {
                _ScoreSlot3 = value;
                RaisePropertyChanged(() => ScoreSlot3);
            }
        }

        private string _ScoreSlotTotal;
        public string ScoreSlotTotal
        {
            get
            {
                return _ScoreSlotTotal;
            }
            set
            {
                _ScoreSlotTotal = value;
                RaisePropertyChanged(() => ScoreSlotTotal);
            }
        }

        private void OnRollsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
                if (FrameIndex == 9)
                {
                    if (Rolls.Count == 1)
                    {
                        if (Rolls[0] == 10)
                        {
                            ScoreSlot1 = "X";
                        }
                        else
                        {
                            ScoreSlot1 = Rolls[0].ToString();
                        }
                    }
                    else if (Rolls.Count == 2)
                    {
                        if (Rolls[0] == 10)
                        {
                            if (Rolls[1] == 10)
                            {
                                ScoreSlot2 = "X";
                            }
                            else
                            {
                                ScoreSlot2 = Rolls[1].ToString();
                            }
                        }
                        else
                        {
                            if (Rolls[0] + Rolls[1] == 10)
                            {
                                ScoreSlot2 = "/";
                            }
                            else
                            {
                                ScoreSlot2 = Rolls[1].ToString();
                            }
                        }
                    }
                    else if (Rolls.Count == 3)
                    {
                        if (Rolls[2] == 10)
                        {
                            ScoreSlot3 = "X";
                        }
                        else
                        {
                            ScoreSlot3 = Rolls[2].ToString();
                        }
                    }
                }
                else
                {
                    if (Rolls.Count == 1)
                    {
                        if (Rolls[0] == 10)
                        {
                            ScoreSlot2 = "X";
                        }
                        else
                        {
                            ScoreSlot1 = Rolls[0].ToString();
                        }
                    }
                    else if (Rolls.Count == 2)
                    {
                        ScoreSlot1 = Rolls[0].ToString();
                        if (Rolls[0] + Rolls[1] == 10)
                        {
                            ScoreSlot2 = "/";
                        }
                        else
                        {
                            ScoreSlot2 = Rolls[1].ToString();
                        }
                    }
            }            
        }

        public void Reset()
        {
            ScoreSlot1 = "";
            ScoreSlot2 = "";

            if (FrameIndex == 9)
            {
                ScoreSlot3 = "";
            }

            ScoreSlotTotal = "";
        }
    }
}

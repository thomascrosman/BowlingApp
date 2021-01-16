﻿using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BowlingApp.Models;

namespace BowlingApp
{
    public class FrameVM : ViewModelBase
    {
        private GameVM GameVM { get; set; }
        public Frame Frame { get; set; }

        public FrameVM(GameVM gameVM, Frame frame)
        {
            GameVM = gameVM;
            Frame = frame;
            GameVM.CurrentRollIndexChanged += OnCurrentRollIndexChanged;
            GameVM.CurrentFrameIndexChanged += OnCurrentFrameIndexChanged;
        }


        private bool _ScoreSlot1Active;
        public bool ScoreSlot1Active
        {
            get
            {
                return _ScoreSlot1Active;
            }
            set
            {
                _ScoreSlot1Active = value;
                RaisePropertyChanged(() => ScoreSlot1Active);
            }
        }

        private bool _ScoreSlot2Active;
        public bool ScoreSlot2Active
        {
            get
            {
                return _ScoreSlot2Active;
            }
            set
            {
                _ScoreSlot2Active = value;
                RaisePropertyChanged(() => ScoreSlot2Active);
            }
        }

        private bool _ScoreSlot3Active;
        public bool ScoreSlot3Active
        {
            get
            {
                return _ScoreSlot3Active;
            }
            set
            {
                _ScoreSlot3Active = value;
                RaisePropertyChanged(() => ScoreSlot3Active);
            }
        }

        private void OnCurrentRollIndexChanged(object sender, int currentRollIndexChanged)
        {
            if (GameVM.CurrentFrameIndex >= FrameIndex)
            {
                int rollCount = Frame.RollIndexes.Count;

                if (rollCount == 0)
                {
                    ScoreSlot1Active = true;
                    ScoreSlot2Active = false;
                    ScoreSlot3Active = false;

                    ScoreSlot1 = "";
                    ScoreSlot2 = "";
                    ScoreSlot3 = "";


                    ScoreSlotTotal = "";
                }
                else
                {
                    if (rollCount == 1)
                    {
                        ScoreSlot1Active = false;
                        ScoreSlot2Active = true;
                        ScoreSlot3Active = false;
                    }

                    if (rollCount == 2)
                    {
                        ScoreSlot1Active = false;
                        ScoreSlot2Active = false;
                        ScoreSlot3Active = true;
                    }


                    int firstRoll = 0;
                    int secondRoll = 0;
                    int thirdRoll = 0;

                    if (rollCount > 0)
                    {
                        firstRoll = GameVM.Rolls[Frame.RollIndexes[0]];
                    }                 

                    if (rollCount > 1)
                    {
                        secondRoll = GameVM.Rolls[Frame.RollIndexes[1]];
                    }

                    if (rollCount > 2)
                    {
                        thirdRoll = GameVM.Rolls[Frame.RollIndexes[2]];
                    }

                    if (FrameIndex == 9)
                    {
                        if (rollCount == 1)
                        {
                            if (firstRoll == 10)
                            {
                                ScoreSlot1 = "X";
                            }
                            else if (firstRoll == 0)
                            {
                                ScoreSlot1 = "-";
                            }
                            else
                            {
                                ScoreSlot1 = firstRoll.ToString();
                            }
                        }
                        else if (rollCount == 2)
                        {
                            if (firstRoll == 10)
                            {
                                if (secondRoll == 10)
                                {
                                    ScoreSlot2 = "X";
                                }
                                else if (secondRoll == 0)
                                {
                                    ScoreSlot2 = "-";
                                }
                                else
                                {
                                    ScoreSlot2 = secondRoll.ToString();
                                }
                            }
                            else
                            {
                                if (firstRoll + secondRoll == 10)
                                {
                                    ScoreSlot2 = "/";
                                }
                                else if (secondRoll == 0)
                                {
                                    ScoreSlot2 = "-";
                                }
                                else
                                {
                                    ScoreSlot2 = secondRoll.ToString();
                                }
                            }
                        }
                        else if (rollCount == 3)
                        {
                            if (thirdRoll == 10)
                            {
                                ScoreSlot3 = "X";
                            }
                            else if (thirdRoll == 0)
                            {
                                ScoreSlot3 = "-";
                            }
                            else
                            {
                                ScoreSlot3 = thirdRoll.ToString();
                            }
                        }
                    }
                    else
                    {
                        if (rollCount == 1)
                        {
                            if (firstRoll == 10)
                            {
                                ScoreSlot2 = "X";
                            }
                            else if (firstRoll == 0)
                            {
                                ScoreSlot1 = "-";
                            }
                            else
                            {
                                ScoreSlot1 = firstRoll.ToString();
                            }
                        }
                        else if (rollCount == 2)
                        {
                            if (firstRoll + secondRoll == 10)
                            {
                                ScoreSlot2 = "/";
                            }
                            else if (secondRoll == 0)
                            {
                                ScoreSlot2 = "-";
                            }
                            else
                            {
                                ScoreSlot2 = secondRoll.ToString();
                            }
                        }
                    }
                }
            }
            else
            {
                ScoreSlot1 = "";
                ScoreSlot2 = "";
                ScoreSlot3 = "";
            }

            if(GameVM.CurrentFrameIndex >= FrameIndex)
            {
                ScoreSlotTotal = GameVM.GetScoreByFrameIndex(FrameIndex).ToString();
            }
            else
            {
                ScoreSlotTotal = "";
            }
        }


        private void OnCurrentFrameIndexChanged(object sender, int currentFrameIndexChanged)
        {
            RaisePropertyChanged(() => IsActiveFrame);
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

        public bool IsActiveFrame
        {
            get
            {
                if(GameVM.CurrentFrameIndex == FrameIndex)
                {
                    return true;
                }
                return false;
            }
        }



        public int FrameNumber
        {
            get
            {
                return FrameIndex + 1;
            }
        }


        public int FrameIndex
        {
            get
            {

                return GameVM.FrameVMs.IndexOf(this);
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




    }
}

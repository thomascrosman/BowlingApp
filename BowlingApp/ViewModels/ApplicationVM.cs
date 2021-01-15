using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingApp
{
    public class ApplicationVM : ViewModelBase
    {
        public GameVM GameVM { get; set; }    
        public ApplicationVM()
        {
            GameVM = new GameVM();
        }
    }
}

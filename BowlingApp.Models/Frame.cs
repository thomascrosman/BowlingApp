using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingApp.Models
{
    public class Frame
    {
        public Frame()
        {
            RollIndexes = new ObservableCollection<int>();
            RollIndexes.CollectionChanged += OnRollIndexesChanged;
        }

        public ObservableCollection<int> RollIndexes { get; set; }
        public List<int> Rolls { get; }

        private void OnRollIndexesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            
        }


    }
}

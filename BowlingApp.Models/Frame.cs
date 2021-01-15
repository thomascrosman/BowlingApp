using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingApp.Models
{
    public class Frame
    {
        public Frame()
        {
            RollIndexes = new List<int>();
        }

        public List<int> RollIndexes { get; set; } 
    }
}

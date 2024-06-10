using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientik.Model
{
    public class Element
    {
        public string _color { get; set; }
        public int _row { get; set; }    
        public int _col { get; set; }
        public string GetPosition => $"{_row}: {_col}";

        public Element(string color, int row, int col)
        {
            _col = col;
            _color = color; 
            _row = row;
        }
    }
}

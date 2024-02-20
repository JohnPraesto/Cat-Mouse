using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cat___Mouse
{
    internal class Coordinate
    {
        string _form; // Detta är field
        public int score = 0;
        int _x { get; set; } // Men är detta field?
        int _y { get; set; }
        public Coordinate(string form, int x, int y) // Detta är konstruktor
        {
            _form = form;
            X = x; // Sätt property här, inte field
            Y = y;
        }
        public int X // Detta är property
        {
            get { return _x; }
            set { _x = Math.Max(0, Math.Min(value, 40)); }
        }
        public int Y
        {
            get { return _y; }
            set { _y = Math.Max(0, Math.Min(value, 20)); }
        }
        public void Print()
        {
            Console.SetCursorPosition(_x, _y);
            Console.Write(_form);
        }
    }
}

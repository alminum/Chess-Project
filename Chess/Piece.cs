using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Piece
    {
        private int x;
        private int y;
        string color;
        char p;
        bool haveMoved;

        public Piece (int _x, int _y, string c, char _p)
        {
            x = _x;
            y = _y;
            color = c;
            p = _p;
            haveMoved = false;
        }

        public Piece ()
        {
            x = 0;
            y = 0;
            p = ' ';

        }

        public int getX() { return x; }
        public int getY() { return y; }
        public char getP() { return p; }

        public void setX(int _x) { x = _x; }
        public void setY(int _y) { y = _y; }
        public void setP(char _p) { p = _p; }

        public string toString()
        {
            return "" + p + color + "";
        }
    }
}

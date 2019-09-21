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
        bool pawnMovedLastTurn;

        public Piece (int _x, int _y, string c, char _p)
        {
            x = _x;
            y = _y;
            color = c;
            p = _p;
            haveMoved = false;
            bool pawnMovedLastTurn = false;
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
        public string getColor() { return color; }
        public bool getPawnMoved() { return pawnMovedLastTurn; }
        public bool getHaveMoved() { return haveMoved; }

        public void setX(int _x) { x = _x; }
        public void setY(int _y) { y = _y; }
        public void setP(char _p) { p = _p; }
        public void Moved() { haveMoved = true; }
        public void setPawnMoved(bool b) { pawnMovedLastTurn = b; }

        public string toString()
        {
            return "" + p + color + "";
        }
    }
}

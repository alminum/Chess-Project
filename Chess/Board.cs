using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Board
    {
        private string[,] board;
        bool WhiteToMove;
        public Piece[] WhiteArr;
        public Piece[] BlackArr;
        private bool[,] checkedWhite;
        private bool[,] checkedBlack;
        private int _WhiteLastP;
        private int _BlackLastP;

        public Board()
        {
            checkedBlack = new bool[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    checkedBlack[i, j] = false;
                }
            }



            WhiteArr = new Piece[17];  // Array for white pieces
            WhiteArr[0] = new Piece(3, 3, "w", 'K');
            WhiteArr[1] = new Piece(7, 7, "w", 'R');
            WhiteArr[2] = new Piece(7, 1, "w", 'N');
            WhiteArr[3] = new Piece(2, 6, "w", 'N');
            WhiteArr[4] = new Piece(7, 2, "w", 'B');
            WhiteArr[5] = new Piece(2, 5, "w", 'B');
            WhiteArr[6] = new Piece(3, 4, "w", 'Q');
            WhiteArr[7] = new Piece(7, 0, "w", 'R');
            WhiteArr[8] = new Piece(6, 0, "w", 'p');
            WhiteArr[9] = new Piece(6, 1, "w", 'p');
            WhiteArr[10] = new Piece(6, 2, "w", 'p');
            WhiteArr[11] = new Piece(6, 3, "w", 'p');
            WhiteArr[12] = new Piece(6, 4, "w", 'p');
            WhiteArr[13] = new Piece(6, 5, "w", 'p');
            WhiteArr[14] = new Piece(6, 6, "w", 'p');
            WhiteArr[15] = new Piece(6, 7, "w", 'p');
            _WhiteLastP = 16;

            BlackArr = new Piece[17];  // Array for black pieces
            BlackArr[0] = new Piece(0, 4, "b", 'K');
            BlackArr[1] = new Piece(5, 4, "b", 'R');
            BlackArr[2] = new Piece(3, 2, "b", 'N');
            BlackArr[3] = new Piece(3, 6, "b", 'N');
            BlackArr[4] = new Piece(0, 2, "b", 'B');
            BlackArr[5] = new Piece(0, 5, "b", 'B');
            BlackArr[6] = new Piece(0, 3, "b", 'Q');
            BlackArr[7] = new Piece(0, 0, "b", 'R');
            BlackArr[8] = new Piece(1, 0, "b", 'p');
            BlackArr[9] = new Piece(1, 1, "b", 'p');
            BlackArr[10] = new Piece(1, 2, "b", 'p');
            BlackArr[11] = new Piece(1, 3, "b", 'p');
            BlackArr[12] = new Piece(1, 4, "b", 'p');
            BlackArr[13] = new Piece(1, 5, "b", 'p');
            BlackArr[14] = new Piece(1, 6, "b", 'p');
            BlackArr[15] = new Piece(1, 7, "b", 'p');
            _BlackLastP = 16;

            WhiteToMove = true;

            board = new string[8, 8];  // Initialize array
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j) % 2 == 0)
                        board[i, j] = "    ";  // White squares
                    else
                        board[i, j] = "****";  // Black Squares
                }
            }
        }

        public int getWLP() { return _WhiteLastP; }

        public bool CanMove(int x1, int y1, int x2, int y2)  // Check if piece at x1,y1 can move to x2,y2
        {
            if (x1 == x2 && y1 == y2)  // If it's the same square
                return false;
            bool found = false;
            char p = ' ';
            int index = 0;
            if (WhiteToMove)  // If it's white's turn
            {
                for (int i = 0; WhiteArr[i] != null; i++)
                {
                    if (WhiteArr[i].getX() == x1 && WhiteArr[i].getY() == y1)  // If we found the piece that is checked (the piece at x1,y1)
                    {
                        found = true;
                        index = i;
                        p = WhiteArr[i].getP();
                        break;
                    }
                }
                if (!found)  // If we didn't find
                    return false;
                if (p == 'R')  // If the piece is the rook
                {
                    if (x1 != x2 && y1 != y2) // If we try to move not on the horizontal or vertical
                        return false;
                    if (x1 == x2) // If we move on the horizontal (letters)
                    {
                        if (y1 < y2)  // If we move left to right
                        {
                            for (int i = 0; WhiteArr[i] != null; i++)
                            {
                                if (WhiteArr[i].getX() == x1 && (WhiteArr[i].getY() > y1 && WhiteArr[i].getY() <= y2))  // If there's a white piece in the way
                                    return false;
                            }
                            for (int i = 0; BlackArr[i] != null; i++)
                            {
                                if (BlackArr[i].getX() == x1 && (BlackArr[i].getY() > y1 && BlackArr[i].getY() < y2))  // If there's a black piece in the way
                                    return false;
                            }
                            for (int i = 0; BlackArr[i] != null; i++)
                            {
                                if (BlackArr[i].getX() == x1 && BlackArr[i].getY() == y2)  // If there's a black piece in the exact spot to where we move
                                {
                                    Piece[] NewWhite = new Piece[17];
                                    Piece[] NewBlack = new Piece[17];
                                    for (int j = 0; WhiteArr[j] != null; j++)
                                    {
                                        NewWhite[j] = new Piece(WhiteArr[j].getX(), WhiteArr[j].getY(), "w", WhiteArr[j].getP());
                                    }
                                    for (int j = 0; BlackArr[j] != null; j++)
                                    {
                                        NewBlack[j] = new Piece(BlackArr[j].getX(), BlackArr[j].getY(), "b", BlackArr[j].getP());
                                    }
                                    delete(x2, y2, NewWhite, NewBlack);
                                    NewWhite[i].setX(x2);
                                    NewWhite[i].setY(y2);
                                    if (!isWhiteChecked(NewWhite, NewBlack))  // If we can take the black piece (the king doesn't become checked)
                                    {
                                        WhiteArr[index].Moved();
                                        return true;
                                    }
                                    return false;
                                }

                            }
                            WhiteArr[index].setX(x2);
                            WhiteArr[index].setY(y2);
                            if (!isWhiteChecked(WhiteArr, BlackArr)) // Check if move has resulted in check
                            {
                                WhiteArr[index].setX(x1);
                                WhiteArr[index].setY(y1);
                                WhiteArr[index].Moved();
                                return true;
                            }

                            WhiteArr[index].setX(x1); // Return rook back to its place
                            WhiteArr[index].setY(y1);
                            return false;
                        }
                        else  // If rook is moving right to left
                        {

                            for (int i = 0; WhiteArr[i] != null; i++)
                            {
                                if (WhiteArr[i].getX() == x1 && (WhiteArr[i].getY() < y1 && WhiteArr[i].getY() >= y2))
                                    return false;
                            }
                            for (int i = 0; BlackArr[i] != null; i++)
                            {
                                if (BlackArr[i].getX() == x1 && (BlackArr[i].getY() < y1 && BlackArr[i].getY() > y2))
                                    return false;
                            }
                            for (int i = 0; BlackArr[i] != null; i++)
                            {
                                if (BlackArr[i].getX() == x1 && BlackArr[i].getY() == y2)
                                {
                                    Piece[] NewWhite = new Piece[17];
                                    Piece[] NewBlack = new Piece[17];
                                    for (int j = 0; WhiteArr[j] != null; j++)
                                    {
                                        NewWhite[j] = new Piece(WhiteArr[j].getX(), WhiteArr[j].getY(), "w", WhiteArr[j].getP());
                                    }
                                    for (int j = 0; BlackArr[j] != null; j++)
                                    {
                                        NewBlack[j] = new Piece(BlackArr[j].getX(), BlackArr[j].getY(), "b", BlackArr[j].getP());
                                    }
                                    delete(x2, y2, NewWhite, NewBlack);
                                    NewWhite[i].setX(x2);
                                    NewWhite[i].setY(y2);
                                    if (!isWhiteChecked(NewWhite, NewBlack))  // If we can take the black piece (the king doesn't become checked)
                                    {
                                        WhiteArr[index].Moved();
                                        return true;
                                    }
                                    return false;
                                }

                            }
                            WhiteArr[index].setX(x2);
                            WhiteArr[index].setY(y2);
                            if (!isWhiteChecked(WhiteArr, BlackArr))
                            {
                                WhiteArr[index].setX(x1);
                                WhiteArr[index].setY(y1);
                                WhiteArr[index].Moved();
                                return true;
                            }

                            WhiteArr[index].setX(x1);
                            WhiteArr[index].setY(y1);
                            return false;
                        }

                    }
                    else  // If we're moving vertically
                    {
                        if (x1 < x2)  // If we're moving down
                        {
                            for (int i = 0; WhiteArr[i] != null; i++)
                            {
                                if (WhiteArr[i].getY() == y1 && (WhiteArr[i].getX() > x1 && WhiteArr[i].getX() <= x2))
                                    return false;
                            }
                            for (int i = 0; BlackArr[i] != null; i++)
                            {
                                if (BlackArr[i].getY() == y1 && (BlackArr[i].getX() > x1 && BlackArr[i].getX() < x2))
                                    return false;
                            }
                            for (int i = 0; BlackArr[i] != null; i++)
                            {
                                if (BlackArr[i].getX() == x2 && BlackArr[i].getY() == y2)
                                {
                                    Piece[] NewWhite = new Piece[17];
                                    Piece[] NewBlack = new Piece[17];
                                    for (int j = 0; WhiteArr[j] != null; j++)
                                    {
                                        NewWhite[j] = new Piece(WhiteArr[j].getX(), WhiteArr[j].getY(), "w", WhiteArr[j].getP());
                                    }
                                    for (int j = 0; BlackArr[j] != null; j++)
                                    {
                                        NewBlack[j] = new Piece(BlackArr[j].getX(), BlackArr[j].getY(), "b", BlackArr[j].getP());
                                    }
                                    delete(x2, y2, NewWhite, NewBlack);
                                    NewWhite[i].setX(x2);
                                    NewWhite[i].setY(y2);
                                    if (!isWhiteChecked(NewWhite, NewBlack))  // If we can take the black piece (the king doesn't become checked)
                                    {
                                        WhiteArr[index].Moved();
                                        return true;
                                    }
                                    return false;
                                }

                            }
                            WhiteArr[index].setX(x2);
                            WhiteArr[index].setY(y2);
                            if (!isWhiteChecked(WhiteArr, BlackArr))
                            {
                                WhiteArr[index].setX(x1);
                                WhiteArr[index].setY(y1);
                                WhiteArr[index].Moved();
                                return true;
                            }

                            WhiteArr[index].setX(x1);
                            WhiteArr[index].setY(y1);
                            return false;

                        }
                        else  // If we're moving up
                        {
                            for (int i = 0; WhiteArr[i] != null; i++)
                            {
                                if (WhiteArr[i].getY() == y1 && (WhiteArr[i].getX() < x1 && WhiteArr[i].getX() >= x2))
                                    return false;
                            }

                            for (int i = 0; BlackArr[i] != null; i++)
                            {
                                if (BlackArr[i].getY() == y1 && (BlackArr[i].getX() < x1 && BlackArr[i].getX() > x2))
                                    return false;
                            }
                            for (int i = 0; BlackArr[i] != null; i++)
                            {
                                if (BlackArr[i].getX() == x2 && BlackArr[i].getY() == y2)
                                {
                                    Piece[] NewWhite = new Piece[17];
                                    Piece[] NewBlack = new Piece[17];
                                    for (int j = 0; WhiteArr[j] != null; j++)
                                    {
                                        NewWhite[j] = new Piece(WhiteArr[j].getX(), WhiteArr[j].getY(), "w", WhiteArr[j].getP());
                                    }
                                    for (int j = 0; BlackArr[j] != null; j++)
                                    {
                                        NewBlack[j] = new Piece(BlackArr[j].getX(), BlackArr[j].getY(), "b", BlackArr[j].getP());
                                    }
                                    delete(x2, y2, NewWhite, NewBlack);
                                    NewWhite[i].setX(x2);
                                    NewWhite[i].setY(y2);
                                    if (!isWhiteChecked(NewWhite, NewBlack))  // If we can take the black piece (the king doesn't become checked)
                                    {
                                        WhiteArr[index].Moved();
                                        return true;
                                    }
                                    return false;
                                }

                            }
                            WhiteArr[index].setX(x2);
                            WhiteArr[index].setY(y2);
                            if (!isWhiteChecked(WhiteArr, BlackArr))
                            {
                                WhiteArr[index].setX(x1);
                                WhiteArr[index].setY(y1);
                                WhiteArr[index].Moved();
                                return true;
                            }

                            WhiteArr[index].setX(x1);
                            WhiteArr[index].setY(y1);
                            return false;
                        }
                    }
                }
                else if (p == 'B') // If the piece is bishop
                {
                    if (Math.Abs(x1 - x2) != Math.Abs(y1 - y2))
                        return false;
                    if (x1 > x2 && y1 > y2) // If we're moving up and left
                    {
                        for (int i = x1 - 1; i > x2; i--)
                        {
                            if (GetPiece(i, Math.Abs(y1 - i)) != null)
                                return false;
                        }
                        if (GetPiece(x2, y2) != null)
                        {
                            if (GetPiece(x2, y2).getColor() == "w")
                                return false;
                            else
                            {
                                Piece[] NewWhite = new Piece[17];
                                Piece[] NewBlack = new Piece[17];
                                for (int j = 0; WhiteArr[j] != null; j++)
                                {
                                    NewWhite[j] = new Piece(WhiteArr[j].getX(), WhiteArr[j].getY(), "w", WhiteArr[j].getP());
                                }
                                for (int j = 0; BlackArr[j] != null; j++)
                                {
                                    NewBlack[j] = new Piece(BlackArr[j].getX(), BlackArr[j].getY(), "b", BlackArr[j].getP());
                                }
                                delete(x2, y2, NewWhite, NewBlack);
                                NewWhite[index].setX(x2);
                                NewBlack[index].setY(y2);
                                if (!isWhiteChecked(NewWhite, NewBlack))
                                    return true;
                                return false;
                            }
                        }
                        WhiteArr[index].setX(x2);
                        WhiteArr[index].setY(y2);
                        if (!isWhiteChecked(WhiteArr, BlackArr))
                        {
                            WhiteArr[index].setX(x1);
                            WhiteArr[index].setY(y1);
                            return true;
                        }

                        WhiteArr[index].setX(x1);
                        WhiteArr[index].setY(y1);
                        return false;
                    }
                    else if (x1 > x2 && y2 > y1) // We're moving up and right
                    {
                        for (int i = x1 - 1; i > x2; i--)
                        {
                            if (GetPiece(i, i + y1) != null)
                                return false;
                        }
                        if (GetPiece(x2, y2) != null)
                        {
                            if (GetPiece(x2, y2).getColor() == "w")
                                return false;
                            else
                            {
                                Piece[] NewWhite = new Piece[17];
                                Piece[] NewBlack = new Piece[17];
                                for (int j = 0; WhiteArr[j] != null; j++)
                                {
                                    NewWhite[j] = new Piece(WhiteArr[j].getX(), WhiteArr[j].getY(), "w", WhiteArr[j].getP());
                                }
                                for (int j = 0; BlackArr[j] != null; j++)
                                {
                                    NewBlack[j] = new Piece(BlackArr[j].getX(), BlackArr[j].getY(), "b", BlackArr[j].getP());
                                }
                                delete(x2, y2, NewWhite, NewBlack);
                                NewWhite[index].setX(x2);
                                NewBlack[index].setY(y2);
                                if (!isWhiteChecked(NewWhite, NewBlack))
                                    return true;
                                return false;
                            }
                        }
                        WhiteArr[index].setX(x2);
                        WhiteArr[index].setY(y2);
                        if (!isWhiteChecked(WhiteArr, BlackArr))
                        {
                            WhiteArr[index].setX(x1);
                            WhiteArr[index].setY(y1);
                            return true;
                        }

                        WhiteArr[index].setX(x1);
                        WhiteArr[index].setY(y1);
                        return false;
                    }
                    else if (x1 < x2 && y1 > y2) // We're moving down and left
                    {
                        for (int i = x1 + 1; i < x2; i++)
                        {
                            if (GetPiece(i, Math.Abs(y1 - i)) != null)
                                return false;
                        }
                        if (GetPiece(x2, y2) != null)
                        {
                            if (GetPiece(x2, y2).getColor() == "w")
                                return false;
                            else
                            {
                                Piece[] NewWhite = new Piece[17];
                                Piece[] NewBlack = new Piece[17];
                                for (int j = 0; WhiteArr[j] != null; j++)
                                {
                                    NewWhite[j] = new Piece(WhiteArr[j].getX(), WhiteArr[j].getY(), "w", WhiteArr[j].getP());
                                }
                                for (int j = 0; BlackArr[j] != null; j++)
                                {
                                    NewBlack[j] = new Piece(BlackArr[j].getX(), BlackArr[j].getY(), "b", BlackArr[j].getP());
                                }
                                delete(x2, y2, NewWhite, NewBlack);
                                NewWhite[index].setX(x2);
                                NewBlack[index].setY(y2);
                                if (!isWhiteChecked(NewWhite, NewBlack))
                                    return true;
                                return false;
                            }
                        }
                        WhiteArr[index].setX(x2);
                        WhiteArr[index].setY(y2);
                        if (!isWhiteChecked(WhiteArr, BlackArr))
                        {
                            WhiteArr[index].setX(x1);
                            WhiteArr[index].setY(y1);
                            return true;
                        }

                        WhiteArr[index].setX(x1);
                        WhiteArr[index].setY(y1);
                        return false;
                    }
                    else
                    {
                        for (int i = x1 + 1; i < x2; i++)
                        {
                            if (GetPiece(i, Math.Abs(y1 + i)) != null)
                                return false;
                        }
                        if (GetPiece(x2, y2) != null)
                        {
                            if (GetPiece(x2, y2).getColor() == "w")
                                return false;
                            else
                            {
                                Piece[] NewWhite = new Piece[17];
                                Piece[] NewBlack = new Piece[17];
                                for (int j = 0; WhiteArr[j] != null; j++)
                                {
                                    NewWhite[j] = new Piece(WhiteArr[j].getX(), WhiteArr[j].getY(), "w", WhiteArr[j].getP());
                                }
                                for (int j = 0; BlackArr[j] != null; j++)
                                {
                                    NewBlack[j] = new Piece(BlackArr[j].getX(), BlackArr[j].getY(), "b", BlackArr[j].getP());
                                }
                                delete(x2, y2, NewWhite, NewBlack);
                                NewWhite[index].setX(x2);
                                NewBlack[index].setY(y2);
                                if (!isWhiteChecked(NewWhite, NewBlack))
                                    return true;
                                return false;
                            }
                        }
                        WhiteArr[index].setX(x2);
                        WhiteArr[index].setY(y2);
                        if (!isWhiteChecked(WhiteArr, BlackArr))
                        {
                            WhiteArr[index].setX(x1);
                            WhiteArr[index].setY(y1);
                            return true;
                        }

                        WhiteArr[index].setX(x1);
                        WhiteArr[index].setY(y1);
                        return false;
                    }
                }
                else if (p == 'N') // If the piece is the knight
                {
                    if ((Math.Abs(x1 - x2) == 2 && Math.Abs(y1 - y2) == 1) || (Math.Abs(x1 - x2) == 1 && Math.Abs(y1 - y2) == 2))
                    {
                        if (GetPiece(x2, y2) != null)
                        {
                            if (GetPiece(x2, y2).getColor() == "w")
                                return false;
                            else
                            {
                                Piece[] NewWhite = new Piece[17];
                                Piece[] NewBlack = new Piece[17];
                                for (int j = 0; WhiteArr[j] != null; j++)
                                {
                                    NewWhite[j] = new Piece(WhiteArr[j].getX(), WhiteArr[j].getY(), "w", WhiteArr[j].getP());
                                }
                                for (int j = 0; BlackArr[j] != null; j++)
                                {
                                    NewBlack[j] = new Piece(BlackArr[j].getX(), BlackArr[j].getY(), "b", BlackArr[j].getP());
                                }
                                delete(x2, y2, NewWhite, NewBlack);
                                NewWhite[index].setX(x2);
                                NewBlack[index].setY(y2);
                                if (!isWhiteChecked(NewWhite, NewBlack))
                                    return true;
                                return false;
                            }
                        }
                        WhiteArr[index].setX(x2);
                        WhiteArr[index].setY(y2);
                        if (!isWhiteChecked(WhiteArr, BlackArr))
                        {
                            WhiteArr[index].setX(x1);
                            WhiteArr[index].setY(y1);
                            return true;
                        }

                        WhiteArr[index].setX(x1);
                        WhiteArr[index].setY(y1);
                        return false;
                    }
                    return false;
                }
                else if (p == 'Q') // If the piece is queen
                {
                    WhiteArr[index].setP('R');
                    Console.Clear();
                    Console.WriteLine(toString());
                    if (CanMove(x1, y1, x2, y2))
                    {
                        Console.WriteLine(1);
                        WhiteArr[index].setP('Q');
                        return true;
                    }
                    WhiteArr[index].setP('B');
                    if (CanMove(x1, y1, x2, y2))
                    {
                        WhiteArr[index].setP('Q');
                        return true;
                    }
                    WhiteArr[index].setP('Q');
                    return false;
                }
                else if (p == 'p')
                {
                    if (y1 == y2 && x1 == x2 + 1)
                    {
                        if (GetPiece(x2, y2) != null)
                            return false;
                        WhiteArr[index].setX(x2);
                        if (isWhiteChecked(WhiteArr, BlackArr))
                        {
                            WhiteArr[index].setX(x1);
                            return false;
                        }
                        WhiteArr[index].setX(x1);
                        WhiteArr[index].Moved();
                        return true;
                    }
                    else if (y1 == y2 && x1 == x2 + 2)
                    {
                        if (WhiteArr[index].getHaveMoved())
                            return false;
                        if (WhiteArr[index].getHaveMoved())
                            return false;
                        if (GetPiece(x1 - 1, y1) != null || GetPiece(x1 - 2, y1) != null)
                            return false;
                        WhiteArr[index].setX(x2);
                        if (isWhiteChecked(WhiteArr, BlackArr))
                        {
                            WhiteArr[index].setX(x1);
                            return false;
                        }
                        WhiteArr[index].setX(x1);
                        WhiteArr[index].Moved();
                        WhiteArr[index].setPawnMoved(true);
                        return true;
                    }
                    else if (Math.Abs(y1 - y2) == 1 && x1 == x2 + 1)
                    {
                        if (GetPiece(x2, y2) != null)
                        {
                            if (GetPiece(x2, y2).getColor() == "w")
                                return false;
                            else
                            {
                                Piece[] NewWhite = new Piece[17];
                                Piece[] NewBlack = new Piece[17];
                                for (int j = 0; WhiteArr[j] != null; j++)
                                {
                                    NewWhite[j] = new Piece(WhiteArr[j].getX(), WhiteArr[j].getY(), "w", WhiteArr[j].getP());
                                }
                                for (int j = 0; BlackArr[j] != null; j++)
                                {
                                    NewBlack[j] = new Piece(BlackArr[j].getX(), BlackArr[j].getY(), "b", BlackArr[j].getP());
                                }
                                delete(x2, y2, NewWhite, NewBlack);
                                NewWhite[index].setX(x2);
                                NewBlack[index].setY(y2);
                                if (!isWhiteChecked(NewWhite, NewBlack))
                                {
                                    NewWhite[index].Moved();
                                    return true;
                                }
                                return false;
                            }
                        }
                        if (GetPiece(x2 + 1, y2) != null)
                        {
                            if (GetPiece(x2 + 1, y2).getPawnMoved())
                            {
                                Piece[] NewWhite = new Piece[17];
                                Piece[] NewBlack = new Piece[17];
                                for (int j = 0; WhiteArr[j] != null; j++)
                                {
                                    NewWhite[j] = new Piece(WhiteArr[j].getX(), WhiteArr[j].getY(), "w", WhiteArr[j].getP());
                                }
                                for (int j = 0; BlackArr[j] != null; j++)
                                {
                                    NewBlack[j] = new Piece(BlackArr[j].getX(), BlackArr[j].getY(), "b", BlackArr[j].getP());
                                }
                                delete(x2 + 1, y2, NewWhite, NewBlack);
                                NewWhite[index].setX(x2);
                                NewBlack[index].setY(y2);
                                if (!isWhiteChecked(NewWhite, NewBlack))
                                {
                                    NewWhite[index].Moved();
                                    return true;
                                }
                                return false;
                            }
                        }

                    }

                }
                else if (p == 'K')
                {
                    if (Math.Abs(x1 - x2) > 1 || Math.Abs(y1 - y2) > 1)
                        return false;
                    if (GetPiece(x2, y2) != null)
                    {
                        if (GetPiece(x2, y2).getColor() == "w")
                            return false;
                        else
                        {
                            Piece[] NewWhite = new Piece[17];
                            Piece[] NewBlack = new Piece[17];
                            for (int j = 0; WhiteArr[j] != null; j++)
                            {
                                NewWhite[j] = new Piece(WhiteArr[j].getX(), WhiteArr[j].getY(), "w", WhiteArr[j].getP());
                            }
                            for (int j = 0; BlackArr[j] != null; j++)
                            {
                                NewBlack[j] = new Piece(BlackArr[j].getX(), BlackArr[j].getY(), "b", BlackArr[j].getP());
                            }
                            delete(x2, y2, NewWhite, NewBlack);
                            NewWhite[index].setX(x2);
                            NewBlack[index].setY(y2);
                            if (!isWhiteChecked(NewWhite, NewBlack))
                                return true;
                            return false;
                        }
                    }
                    WhiteArr[index].setX(x2);
                    WhiteArr[index].setY(y2);
                    if (!isWhiteChecked(WhiteArr, BlackArr))
                    {
                        WhiteArr[index].setX(x1);
                        WhiteArr[index].setY(y1);
                        return true;
                    }

                    WhiteArr[index].setX(x1);
                    WhiteArr[index].setY(y1);
                    return false;
                }
                else
                    return false;
            }
            else
            {

            }
            return false;
        }

        public bool isWhiteToMove() { return WhiteToMove; }

        public bool isWhiteChecked(Piece[] White, Piece[] Black)
        {
            return false;
        }

        public bool isBlackCheked()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    checkedBlack[i, j] = false;
                }
            }
            Console.WriteLine(1);
            for (int i = 0; WhiteArr[i] != null; i++)
            {
                checkedBlack[WhiteArr[i].getX(), WhiteArr[i].getY()] = true;
                for (int j = 0; j < 8; j++)
                {
                    for (int k = 0; k < 8; k++)
                    {
                        Console.WriteLine(2);
                        if (CanMove(WhiteArr[i].getX(), WhiteArr[i].getY(), j, k))
                        {
                            checkedBlack[j, k] = true;
                            Console.WriteLine("x = " + j + " ; y = " + k);
                        }
                    }
                }
            }
            if (checkedBlack[BlackArr[0].getX(), BlackArr[0].getY()])
                return true;
            return false;
        }

        public Piece[] getWhiteArr()
        {
            return WhiteArr;
        }

        public string CheckedBlackToString()
        {
            string s = "";

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (checkedBlack[i, j])
                        s += "1";
                    else
                        s += "0";
                }
                s += '\n';
            }

            return s;
        }

        public Piece[] getBlackArr()
        {
            return BlackArr;
        }

        public void Move(int x1, int y1, int x2, int y2)
        {
            delete(x2, y2, WhiteArr, BlackArr);
            if (WhiteToMove)
            {
                for (int i = 0; WhiteArr[i] != null; i++)
                {
                    if (WhiteArr[i].getX() == x1 && WhiteArr[i].getY() == y1)
                    {
                        WhiteArr[i].setX(x2);
                        WhiteArr[i].setY(y2);
                        WhiteToMove = false;
                        return;
                    }
                }
            }
            for (int i = 0; BlackArr[i] != null; i++)
            {
                if (BlackArr[i].getX() == x1 && BlackArr[i].getY() == y1)
                {
                    BlackArr[i].setX(x2);
                    BlackArr[i].setY(y2);
                    WhiteToMove = true;
                    return;
                }
            }
        }

        public Piece GetPiece(int x, int y)
        {
            for (int i = 0; WhiteArr[i] != null; i++)
            {
                if (WhiteArr[i].getX() == x && WhiteArr[i].getY() == y)
                {
                    return WhiteArr[i];
                }
            }

            for (int i = 0; BlackArr[i] != null; i++)
            {
                if (BlackArr[i].getX() == x && BlackArr[i].getY() == y)
                {
                    return BlackArr[i];
                }
            }
            return null;
        }

        public void delete(int x, int y, Piece[] White, Piece[] Black)
        {
            for (int i = 0; White[i] != null; i++)
            {
                if (White[i].getX() == x && White[i].getY() == y)
                {
                    int LastP = _WhiteLastP;
                    if (White == WhiteArr)
                        _WhiteLastP--;
                    for (int j = i; j < LastP; j++)
                    {
                        White[j] = White[j + 1];
                        White[LastP] = null;
                    }

                }
            }
            for (int i = 0; Black[i] != null; i++)
            {

                if (Black[i].getX() == x && Black[i].getY() == y)
                {
                    int LastP = _BlackLastP;
                    if (Black == BlackArr)
                        _BlackLastP--;
                    for (int j = i; j < LastP; j++)
                    {
                        Black[j] = Black[j + 1];
                        Black[LastP] = null;
                    }
                }
            }
        }

        public string toString()
        {
            bool isPiece = false;
            string s = "";
            for (int i = 0; i < 8; i++)
            {
                s += '\n';

                for (int j = 0; j < 8; j++)
                {
                    isPiece = false;
                    for (int r = 0; WhiteArr[r] != null; r++)
                    {
                        if (i == WhiteArr[r].getX() && j == WhiteArr[r].getY())
                        {
                            if ((i + j) % 2 == 0)
                                s += " " + WhiteArr[r].toString() + " ";
                            else
                                s += "*" + WhiteArr[r].toString() + "*";
                            isPiece = true;
                            break;
                        }
                    }
                    for (int r = 0; BlackArr[r] != null; r++)
                    {
                        if (i == BlackArr[r].getX() && j == BlackArr[r].getY())
                        {
                            if ((i + j) % 2 == 0)
                                s += " " + BlackArr[r].toString() + " ";
                            else
                                s += "*" + BlackArr[r].toString() + "*";
                            isPiece = true;
                            break;
                        }
                    }
                    if (!isPiece)
                        s += board[i, j];

                }
            }
            return s;
        }
    }
}

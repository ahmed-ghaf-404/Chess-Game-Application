using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Move{
    protected Piece _thisPiece;
    private int _x; // x coordinate of the target square
    public int X{
        get{ return _x;}
        set{ _x = value;}
    }
    private int _y; // y coordinate of the target square
    public int Y{
        get{return _y;}
        set{_y = value;}
    }
    public override string ToString(){
        return $"{_thisPiece}: ({_x},{_y})";
    }
    public bool IsSameMove(Move m){
        return m.X==this._x && m.Y==this._y && m.GetType()==GetType();
    }
}

class QuitMove : Move{
    public QuitMove(Piece p, int x, int y){
        this._thisPiece = p;
        this.X = x;
        this.Y = y;
    }
}

class CaptureMove : Move{
    public CaptureMove(Piece p, int x, int y){
        this._thisPiece = p;
        this.X = x;
        this.Y = y;
    }
}
class CheckMove : Move{
    public CheckMove(Piece p, int x, int y){
        this._thisPiece = p;
        this.X = x;
        this.Y = y;
    }
}
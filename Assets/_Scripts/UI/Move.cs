using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Move{
    protected Piece _thisPiece;
    protected int _xDist { get; set; } // x coordinate of the target square
    protected int _yDist { get; set; } // y coordinate of the target square
    
    public bool isMove(int to_x, int to_y){
        return to_x==_xDist && to_y==_yDist;
    }
    public override string ToString(){
        return $"{_thisPiece}: ({_xDist},{_yDist})";
    }

    public int GetXDist(){ return _xDist;}
    public int GetYDist(){ return _yDist;}
    abstract public bool IsSameMove(Move m);
}

class QuitMove : Move{
    public QuitMove(Piece p, int x, int y){
        this._thisPiece = p;
        this._xDist = x;
        this._yDist = y;
    }

    public override bool IsSameMove(Move m){
        return m.GetXDist()==GetXDist() && m.GetYDist()==GetYDist() && m.GetType()==GetType();
    }
}
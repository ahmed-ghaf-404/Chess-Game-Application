using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board{
    private Piece[,] pieces;
    
    public Board(int x, int y){
        this.pieces = new Piece[x,y];
    }

    public void SetPiece(int x, int y, Piece piece){
        pieces[x,y] = piece;
    }
    public Piece GetPiece(int x, int y){
        return pieces[x,y];
    }

}

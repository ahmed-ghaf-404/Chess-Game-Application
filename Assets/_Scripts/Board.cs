using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Board{
    static Square[,] squares;
    static Piece[,] pieces;
    
    static public void SetBoard(int x, int y){
        squares = new Square[x,y];
        pieces = new Piece[x,y];
    }

    static public void SetSquare(int x, int y, Square square){
        squares[x,y] = square;
    }
    static public void SetPiece(int x, int y, Piece piece){
        pieces[x,y] = piece;
    }
    static public Piece GetPiece(int x, int y){
        return pieces[x,y];
    }

    static public void MakeMove(Piece p, int to_rank, int to_file){
        QuitMove m = new QuitMove(p, to_rank, to_file);
        Debug.Log($"is m legal? {m}");
        if (p.IsLegalMove(m)){
            Debug.Log("Legal");
            // make the UI manager move the pieces
            BoardUIManager.MovePiece(p.GetRank(),p.GetFile(),to_rank,to_file);
            // make the move now
            pieces[to_rank,to_file] = pieces[p.GetRank(),p.GetFile()];
            pieces[p.GetRank(),p.GetFile()] = null; // empty the square
            p.SetFile(to_file);
            p.SetRank(to_rank);

            // ? special rule: pawns must be notifies that they have moved
            if (p.GetType()==typeof(Pawn)){
                p.SetHasMoved();
            }
            
        }
        else{
            Debug.Log("illegal");
            // ! nothing!  DEADCODE
        }
        BoardUIManager.ResetSelection();
    }

}

using System.Linq;
using UnityEngine;

public class King : Piece{

    King(){
        SetName(pieceName);
    }
    override 
    public void GenerateLegalMoves(Piece[,] board){
        // quite moves 
        _legalMoves = new Move[8];
        int index = 0;
        int x;
        int y;
        for(int i=-1; i<2; i++){
            for(int j=-1; j<2; j++){
                x = GetFile() + i;
                y = GetRank() + j;
                if (!(x==GetFile() && y == GetRank()) && x>=0 && x<8 && y>=0 && y<8){
                    if (board[x,y] == null){
                        _legalMoves[index++] = new QuitMove(this, x,y);
                    }
                    else if (IsEnemy(board[x,y])){
                        _legalMoves[index++] = new CaptureMove(this, x,y);
                    }
                }
            }
        }

    }
    
}

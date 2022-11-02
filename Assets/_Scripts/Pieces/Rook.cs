using System.Linq;
using UnityEngine;

public class Rook : Piece{
    
    Rook(){
        MAX_MOVEMENT = 14;
        SetName(pieceName);
    }

    override 
    public void GenerateLegalMoves(Piece[,] board){
        _legalMoves = new Move[MAX_MOVEMENT];
        int index = 0;
        int x = GetFile();
        int y = GetRank();
        
        // RIGHT
        while (x<7){
            if (board[++x,y]==null){
                _legalMoves[index++] = new QuitMove(this, x,y);
            }
            else{
                if (IsEnemy(board[x,y])){
                    _legalMoves[index++] = new CaptureMove(this, x, y);
                }
                break;
            }
        }
        x = GetFile();
        // LEFT
        while (x>0){
            if (board[--x,y]==null){
                _legalMoves[index++] = new QuitMove(this, x,y);
            }
            else{
                if (IsEnemy(board[x,y])){
                    _legalMoves[index++] = new CaptureMove(this, x, y);
                }
                break;
            }
        }
        x = GetFile();
        // UP
        while (y<7){
            if (board[x,++y]==null){
                _legalMoves[index++] = new QuitMove(this, x,y);
            }
            else{
                if (IsEnemy(board[x,y])){
                    _legalMoves[index++] = new CaptureMove(this, x, y);
                }
                break;
            }
        }
        y = GetRank();
        // DOWN
        while (y>0){
            if (board[x,--y]==null){
                _legalMoves[index++] = new QuitMove(this, x,y);
            }
            else{
                if (IsEnemy(board[x,y])){
                    _legalMoves[index++] = new CaptureMove(this, x, y);
                }
                break;
            }
        }

        
    }
    
}

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
                        _legalMoves[index++] = new Move(this, x,y, "quite");
                    }
                    else if (IsEnemy(board[x,y])){
                        _legalMoves[index++] = new Move(this, x,y, "capture");
                    }
                }
            }
        }
        Player playerPtr = GetColor()==0? GameState.Instance.RuntimeData.White : GameState.Instance.RuntimeData.Black;

        string player = GetColor()==0? "White" : "Black";

        // Castling
        // short
        if (playerPtr.CanCastleShort){
            if (board[GetFile()+1, GetRank()] == null && board[GetFile()+2, GetRank()] == null && board[GetFile()+3, GetRank()] != null){
                if (board[GetFile()+3, GetRank()].GetType() == typeof(Rook) && !board[GetFile()+3, GetRank()].HasMoved){
                    // legal to short castle
                    // Debug.Log($"{player} can short castle");
                    _legalMoves[index++] = new Move(this, GetFile()+2, GetRank(), "shortCastling");
                }
            }
        }
        // long
        if (playerPtr.CanCastleLong){
            if (board[GetFile()-1, GetRank()] == null && board[GetFile()-2, GetRank()] == null  && board[GetFile()-3, GetRank()] == null && board[GetFile()-4, GetRank()] != null){
                if (board[GetFile()-4, GetRank()].GetType() == typeof(Rook) && !board[GetFile()-4, GetRank()].HasMoved){
                    // legal to long castle
                    // Debug.Log($"{player} can long castle");
                    _legalMoves[index++] = new Move(this, GetFile()-2, GetRank(), "longCastling");
                }
            }
        }
    }
    
}

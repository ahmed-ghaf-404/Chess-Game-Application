using System.Linq;
using UnityEngine;

public class Rook : Piece{
    int og_file = -1;
    int og_rank = -1;
    Rook(){
        MAX_MOVEMENT = 14;
    }

    int CreateMove(Piece[,] board, int dx, int dy, int index){
        if (!(dx==0 || dy==0)){Debug.Log("ERROR: Either dx or dy must be 0!"); return -1;}
        int curr_x = GetFile() + dx;
        int curr_y = GetRank() + dy;
        while (curr_x>=0 && curr_x <8 && curr_y>=0 && curr_y<8){
            if (board[curr_x, curr_y] == null){
                _legalMoves[index++] = new Move(this, curr_x, curr_y, "quite", _runtimeData.FEN);
            }
            else{
                if (IsEnemy(board[curr_x,curr_y])){
                    if (board[curr_x, curr_y].GetType() == typeof(King)){
                        _legalMoves[index++] = new Move(this, curr_x, curr_y, "check", _runtimeData.FEN);
                    }
                    else{
                        _legalMoves[index++] = new Move(this, curr_x, curr_y, "capture", _runtimeData.FEN);
                    }
                }
                break;
            }

            curr_x += dx;
            curr_y += dy;
        }
        return index;
    }
    override 
    public void GenerateLegalMoves(Piece[,] board){
        if (og_file == -1){
            og_file = file;
        }
        if (og_rank == -1){
            og_rank = rank;
        }
        _legalMoves = new Move[MAX_MOVEMENT];
        int index = 0;
        foreach (int d in new int[]{-1,1}){
            index = CreateMove(board, d, 0, index);
            index = CreateMove(board, 0, d, index);
        }
        if (HasMoved){
        Debug.Log(og_file);
        Debug.Log(og_rank);
            if (og_rank<1){
                // white
                if (og_file<1)
                    // queen side
                    _runtimeData.White.CanCastleLong = false;
                else
                    _runtimeData.White.CanCastleShort = false;
            }
            else{
                // black
                if (og_file<1)
                    // queen side
                    _runtimeData.Black.CanCastleLong = false;
                else
                    _runtimeData.Black.CanCastleShort = false;
            }
        }
    }
}

using System.Linq;
using UnityEngine;

public class Rook : Piece{
    int og_file = -1;
    int og_rank = -1;

    void CreateMove(Piece[,] board, int dx, int dy){
        if (!(dx==0 || dy==0)){Debug.Log("ERROR: Either dx or dy must be 0!"); return;}
        int curr_x = GetFile() + dx;
        int curr_y = GetRank() + dy;
        while (curr_x>=0 && curr_x <8 && curr_y>=0 && curr_y<8){
            if (board[curr_x, curr_y] == null){
                _runtimeData.LegalMoves[this].Add(new Move(this, curr_x, curr_y, "quite", _runtimeData.FEN));
            }
            else{
                if (IsEnemy(board[curr_x,curr_y])){
                    if (board[curr_x, curr_y].GetType() == typeof(King)){
                        _runtimeData.LegalMoves[this].Add(new Move(this, curr_x, curr_y, "check", _runtimeData.FEN));
                    }
                    else{
                        _runtimeData.LegalMoves[this].Add(new Move(this, curr_x, curr_y, "capture", _runtimeData.FEN));
                    }
                }
                break;
            }

            curr_x += dx;
            curr_y += dy;
        }
    }
    override 
    public void GenerateLegalMoves(Piece[,] board){
        if (og_file == -1){
            og_file = file;
        }
        if (og_rank == -1){
            og_rank = rank;
        }
        foreach (int d in new int[]{-1,1}){
            CreateMove(board, d, 0);
            CreateMove(board, 0, d);
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

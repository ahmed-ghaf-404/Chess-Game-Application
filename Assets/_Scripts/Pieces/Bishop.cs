using UnityEngine;

public class Bishop : Piece{

    void CreateMove(Piece[,] board, int dx, int dy){
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
        foreach (int dx in new int[]{-1,1}){
            foreach (int dy in new int[]{-1,1}){
                CreateMove(board, dx, dy);
            }
        }
    }
}
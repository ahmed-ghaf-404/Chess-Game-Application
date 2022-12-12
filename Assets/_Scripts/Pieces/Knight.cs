using UnityEngine;

public class Knight : Piece{
    void CreateMove(Piece[,] board, int x, int y){
        if (x>=0 && x<8 && y>=0 && y<8){
            // within board
            if (board[x,y] == null){
                _runtimeData.LegalMoves[this].Add(new Move(this,x, y, "quite", _runtimeData.FEN));
            }
            else if (IsEnemy(board[x,y])){
                if (board[x,y].GetType() == typeof(King)){
                    _runtimeData.LegalMoves[this].Add(new Move(this,x, y, "check", _runtimeData.FEN));
                }
                else{
                    _runtimeData.LegalMoves[this].Add(new Move(this,x, y, "capture", _runtimeData.FEN));
                }
            }
        }
    }
    override 
    public void GenerateLegalMoves(Piece[,] board){
        for (int i=-1; i<2; i+=2){
            for (int j=-2; j<3; j+=4){
                CreateMove(board, GetFile()+i, GetRank()+j);
                CreateMove(board, GetFile()+j, GetRank()+i);
            }
        }
    }
}
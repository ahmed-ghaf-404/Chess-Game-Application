using UnityEngine;

public class Knight : Piece{

    Knight(){
        MAX_MOVEMENT = 8;
    }
    int CreateMove(Piece[,] board, int x, int y, int index){
        if (x>=0 && x<8 && y>=0 && y<8){
            // within board
            if (board[x,y] == null){
                _legalMoves[index++] = new Move(this,x, y, "quite", _runtimeData.FEN);
            }
            else if (IsEnemy(board[x,y])){
                if (board[x,y].GetType() == typeof(King)){
                    _legalMoves[index++] = new Move(this,x, y, "check", _runtimeData.FEN);
                }
                else{
                    _legalMoves[index++] = new Move(this,x, y, "capture", _runtimeData.FEN);
                }
            }
        }
        return index;
    }
    override 
    public void GenerateLegalMoves(Piece[,] board){
        _legalMoves = new Move[MAX_MOVEMENT];
        int index = 0; 
        for (int i=-1; i<2; i+=2){
            for (int j=-2; j<3; j+=4){
                index = CreateMove(board, GetFile()+i, GetRank()+j, index);
                index = CreateMove(board, GetFile()+j, GetRank()+i, index);
            }
        }
    }
}
using System.Linq;
using UnityEngine;

public class Pawn : Piece{
    private int MOVEMENT_OFSET;

    Pawn(){
        this._legalMoves = new Move[4];
    }
    
    override 
    public void GenerateLegalMoves(Piece[,] board){
        this.MOVEMENT_OFSET = GetColor()==0? 1: -1;

        Move temp_move;
        int x = GetFile();
        int y = GetRank();
        var index = 0;
        // quite moves:
        // 1- single push
        // Debug.Log($"offset:{MOVEMENT_OFSET}, x:{x}, y:{y}, color:{GetColor()}");
        y += MOVEMENT_OFSET;
        
        
        // check if there's a piece on (x,y)
        if (board[x,y]==null){
            temp_move = new Move(this, x, y, "quite", _runtimeData.FEN);
            // Debug.Log(temp_move);
            _legalMoves[index++] = temp_move;
        }
        
        // 2- double push
        if (!HasMoved && board[x,y]==null){
            y += MOVEMENT_OFSET;
            temp_move = new Move(this, x, y, "quite", _runtimeData.FEN);
            // Debug.Log(temp_move);
            _legalMoves[index++] = temp_move;
        }
        // adding captures
        x = GetFile();
        y = GetRank() + MOVEMENT_OFSET;
        
        for (int i=-1; i<2; i+=2){
            if (x+i>=0 && x+i<8 && y>=0 && y<8){
                if (IsEnemy(board[x+i,y])){
                    if (board[x+i,y].GetType() == typeof(King)){
                        _legalMoves[index++] = new Move(this, x+i, y, "check", _runtimeData.FEN);
                    }
                    else{
                        temp_move = new Move(this, x+i, y, "capture", _runtimeData.FEN);
                        _legalMoves[index++] = temp_move;
                    }
                }
            }
        }
    }
}

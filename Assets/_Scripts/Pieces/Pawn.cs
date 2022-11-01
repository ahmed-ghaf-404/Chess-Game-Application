using System.Linq;
using UnityEngine;

public class Pawn : Piece{
    private int MOVEMENT_OFSET;
    private int STARTING_RANK;

    Pawn(){
        // ? INFO/Question: Is this important??
        SetName(pieceName);
        this._legalMoves = new Move[4];
    }
    
    override 
    public void GenerateLegalMoves(Piece[,] board){
        this.MOVEMENT_OFSET = GetColor()==0? 1: -1;
        STARTING_RANK = GetColor()==0? 1: 6;

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
            temp_move = new QuitMove(this, x, y);
            // Debug.Log(temp_move);
            _legalMoves[index++] = temp_move;
        }
        
        // 2- double push
        if (STARTING_RANK==GetRank() && board[x,y]==null){
            Debug.Log(STARTING_RANK);
            y += MOVEMENT_OFSET;
            temp_move = new QuitMove(this, x, y);
            // Debug.Log(temp_move);
            _legalMoves[index++] = temp_move;
        }
        // adding captures
        x = GetFile();
        y = GetRank() + MOVEMENT_OFSET;
        
        for (int i=-1; i<2; i+=2){
            if (x+i>=0 && x+i<8 && y>=0 && y<8){
                if (IsEnemy(board[x+i,y])){
                    temp_move = new CaptureMove(this, x+i, y);
                    _legalMoves[index++] = temp_move;
                }
            }
        }
    }
}

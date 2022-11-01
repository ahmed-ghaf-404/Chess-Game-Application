using System.Linq;
using UnityEngine;

public class Pawn : Piece
{
    
    private int MOVEMENT_OFSET;

    Pawn(){
        // ? INFO/Question: Is this important??
        SetName(pieceName);
        this._legalMoves = new Move[4];
    }
    
    override 
    public void GenerateLegalMoves(Piece[,] board){
        this.MOVEMENT_OFSET = GetColor()==0? 1: -1;
        Move temp_move;
        int x = GetRank();
        int y = GetFile();
        // quite moves:
        // 1- single push
        // Debug.Log($"offset:{MOVEMENT_OFSET}, x:{x}, y:{y}, color:{GetColor()}");
        y += MOVEMENT_OFSET;
        
        
        // check if there's a piece on (x,y)
        if (board[x,y]==null){
            temp_move = new QuitMove(this, x, y);
            // Debug.Log(temp_move);
            _legalMoves[0] = temp_move;
        }
        
        // 2- double push
        y += MOVEMENT_OFSET;
        
        if (!GetHasMoved() && board[x,y]==null){
            temp_move = new QuitMove(this, x, y);
            // Debug.Log(temp_move);
            _legalMoves[1] = temp_move;
        }
        // adding captures
        x = GetRank();
        y = GetFile() + MOVEMENT_OFSET;
        var index = 2;
        for (int i=-1; i<2; i+=2){
            if (x>=0 && x<8 && y>=0 && y<8 && IsEnemy(board[x+i,y])){
                Debug.Log($"{x+i}, {y}");
                temp_move = new CaptureMove(this, x+i, y);
                _legalMoves[index++] = temp_move;
            }
        }
    }
    override
    public bool IsLegalMove(Move other_move){
        foreach (var move in _legalMoves){
            if (move == null)
                continue;
            else if (move.IsSameMove(other_move))
                return true;
        }
        return false;
    }
}

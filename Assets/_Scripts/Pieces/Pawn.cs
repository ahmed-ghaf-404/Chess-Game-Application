using System.Linq;
using UnityEngine;

public class Pawn : Piece
{
    
    private int MOVEMENT_OFSET;
    private Move[] _legalMoves = new Move[4];

    public const string pieceName = "Pawn";

    Pawn(){
        SetName(pieceName);
    }
    
    override 
    public void GenerateLegalMoves(){
        this.MOVEMENT_OFSET = GetColor()==0? 1: -1;
        Move temp_move;
        int x = GetRank();
        int y = GetFile();
        // quite moves:
        // 1- single push
        // Debug.Log($"offset:{MOVEMENT_OFSET}, x:{x}, y:{y}, color:{GetColor()}");
        y += MOVEMENT_OFSET;
        

        // check if there's a piece on (x,y)
        if (Board.GetPiece(x,y)==null){
            temp_move = new QuitMove(this, x, y);
            // Debug.Log(temp_move);
            _legalMoves[0] = temp_move;
        }
        // Debug.Log($"offset:{MOVEMENT_OFSET}, x:{x}, y:{y}");
        
        // 2- double push
        y += MOVEMENT_OFSET;
        
        if (!GetHasMoved() && Board.GetPiece(x,y)==null){
            temp_move = new QuitMove(this, x, y);
            // Debug.Log(temp_move);
            _legalMoves[1] = temp_move;
        }

        // attack moves:
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
    void OnMouseDown(){
        Debug.Log(gameObject);
        GenerateLegalMoves();
        
        if (BoardUIManager.selectedPiece == null){
            BoardUIManager.selectedSquare = null;
            BoardUIManager.selectedPiece = this;
        }
        else{
            BoardUIManager.otherSelectedPiece = this;
        }
        
    }
}

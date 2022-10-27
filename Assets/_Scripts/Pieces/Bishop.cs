using System.Linq;

public class Bishop : Piece{
    public const string pieceName = "Bishop";
    private Move[] _legalMoves;
    Bishop(){
        SetName(pieceName);
    }
    override 
    public void GenerateLegalMoves(){
        // quite moves 
    }
    
    override
    public bool IsLegalMove(Move other_move){
        return _legalMoves.Contains(other_move);
    }
}

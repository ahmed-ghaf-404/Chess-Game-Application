using System.Linq;

public class Rook : Piece
{
    public const string pieceName = "Rook";
    private Move[] _legalMoves;

    Rook(){
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

using System.Linq;

public class Queen : Piece
{
    public const string pieceName = "Queen";
    private Move[] _legalMoves;

    Queen(){
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

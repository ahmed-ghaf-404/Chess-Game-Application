using System.Linq;

public class Knight : Piece
{
    public const string pieceName = "knight";
    private Move[] _legalMoves;

    Knight(){
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

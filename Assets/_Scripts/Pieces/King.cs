using System.Linq;

public class King : Piece
{
    public const string pieceName = "King";
    private Move[] _legalMoves;

    King(){
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

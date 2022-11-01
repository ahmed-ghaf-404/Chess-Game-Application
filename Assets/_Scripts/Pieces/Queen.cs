using System.Linq;

public class Queen : Piece{

    Queen(){
        SetName(pieceName);
    }
    override 
    public void GenerateLegalMoves(Piece[,] board){
        // quite moves 
        
    }
    override
    public bool IsLegalMove(Move other_move){
        return _legalMoves.Contains(other_move);
    }
}

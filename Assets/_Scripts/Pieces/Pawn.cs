
using System.Collections.Generic;

public class  Pawn : Piece{
    private int MOVEMENT_OFSET;
    
    public bool HasMovedTwoSquares; 

    
    override 
    public void GenerateLegalMoves(Piece[,] board){
        this.MOVEMENT_OFSET = GetColor()==0? 1: -1;

        Move temp_move;
        int x = GetFile();
        int y = GetRank();
        // quite moves:
        // 1- single push
        // Debug.Log($"offset:{MOVEMENT_OFSET}, x:{x}, y:{y}, color:{GetColor()}");
        y += MOVEMENT_OFSET;
        
        
        // check if there's a piece on (x,y)
        if (board[x,y]==null){
            temp_move = new Move(this, x, y, "quite", _runtimeData.FEN);
            // Debug.Log(temp_move);
            _runtimeData.LegalMoves[this].Add(temp_move);
        }
        
        // 2- double push
        if (!HasMoved && board[x,y]==null){
            y += MOVEMENT_OFSET;
            
            temp_move = new Move(this, x, y, "quite", _runtimeData.FEN, 3);
            _runtimeData.LegalMoves[this].Add(temp_move);
        }
        // adding captures
        x = GetFile();
        y = GetRank() + MOVEMENT_OFSET;
        
        for (int i=-1; i<2; i+=2){
            if (x+i>=0 && x+i<8 && y>=0 && y<8){
                if (IsEnemy(board[x+i,y])){
                    // checking the king
                    if (board[x+i,y].GetType() == typeof(King)){
                        _runtimeData.LegalMoves[this].Add(new Move(this, x+i, y, "check", _runtimeData.FEN));;
                    }
                    else{
                        temp_move = new Move(this, x+i, y, "capture", _runtimeData.FEN);
                        _runtimeData.LegalMoves[this].Add(temp_move);
                    }
                }
                else if (_runtimeData.EnPassantSquare!=null){
                    if (_runtimeData.EnPassantSquare.Item1==x+i && _runtimeData.EnPassantSquare.Item2==y){
                        // checking the king
                        if (board[x+i,y].GetType() == typeof(King)){
                            _runtimeData.LegalMoves[this].Add(new Move(this, x+i, y, "check", _runtimeData.FEN));;
                        }
                        else{
                            temp_move = new Move(this, x+i, y, "enpassant", _runtimeData.FEN, 0);
                            _runtimeData.LegalMoves[this].Add(temp_move);
                        }
                    }
                }
            }
        }
    }
}

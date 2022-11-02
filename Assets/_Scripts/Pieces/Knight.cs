using UnityEngine;

public class Knight : Piece{

    Knight(){
        MAX_MOVEMENT = 8;
        SetName(pieceName);
    }

    override 
    public void GenerateLegalMoves(Piece[,] board){
        _legalMoves = new Move[MAX_MOVEMENT];
        int index = 0;
        int x = GetFile();
        int y = GetRank();
        if (x+1<8 && y+2<8){
            if (board[x+1, y+2] == null)
                _legalMoves[index++] = new QuitMove(this,x+1, y+2);
            else if (IsEnemy(board[x+1,y+2]))
                _legalMoves[index++] = new CaptureMove(this,x+1, y+2);
        }
        if (x+2<8 && y+1<8){
            if (board[x+2, y+1] == null)
                _legalMoves[index++] = new QuitMove(this,x+2, y+1);
            else if (IsEnemy(board[x+2, y+1]))
                _legalMoves[index++] = new CaptureMove(this,x+2, y+1);
        }
        if (x+1<8 && y-2>=0){
            if (board[x+1, y-2] == null)
                _legalMoves[index++] = new QuitMove(this,x+1, y-2);
            else if (IsEnemy(board[x+1,y-2]))
                _legalMoves[index++] = new CaptureMove(this,x+1, y-2);
        }
        if (x+2<8 && y-1>=0){
            if (board[x+2, y-1] == null)
                _legalMoves[index++] = new QuitMove(this,x+2, y-1);
            else if (IsEnemy(board[x+2,y-1]))
                _legalMoves[index++] = new CaptureMove(this,x+2, y-1);
        }
        if (x-1>=0 && y-2>=0){
            if (board[x-1, y-2] == null)
                _legalMoves[index++] = new QuitMove(this,x-1, y-2);
            else if (IsEnemy(board[x-1,y-2]))
                _legalMoves[index++] = new CaptureMove(this,x-1, y-2);
        }
        if (x-2>=0 && y-1>=0){
            if (board[x-2, y-1] == null)
                _legalMoves[index++] = new QuitMove(this,x-2, y-1);
            else if (IsEnemy(board[x-2,y-1]))
                _legalMoves[index++] = new CaptureMove(this,x-2, y-1);
        }
        if (x-2>=0 && y+1<8){
            if (board[x-2, y+1] == null)
                _legalMoves[index++] = new QuitMove(this,x-2, y+1);
            else if (IsEnemy(board[x-2,y+1]))
                _legalMoves[index++] = new CaptureMove(this,x-2, y+1);
        }
        if (x-1>=0 && y+2<8){
            if (board[x-1, y+2] == null)
                _legalMoves[index++] = new QuitMove(this,x-1, y+2);
            else if (IsEnemy(board[x-1,y+2]))
                _legalMoves[index++] = new CaptureMove(this,x-1, y+2);
        }
    }
    
}

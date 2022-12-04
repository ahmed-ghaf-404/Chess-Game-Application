public class Queen : Piece{
    Queen(){
        MAX_MOVEMENT = 27;
    }
    int CreateMove(Piece[,] board, int dx, int dy, int index){
        int curr_x = GetFile() + dx;
        int curr_y = GetRank() + dy;
        while (curr_x>=0 && curr_x <8 && curr_y>=0 && curr_y<8){
            if (board[curr_x, curr_y] == null){
                _legalMoves[index++] = new Move(this, curr_x, curr_y, "quite", _runtimeData.FEN);
            }
            else{
                if (IsEnemy(board[curr_x,curr_y])){
                    if (board[curr_x, curr_y].GetType() == typeof(King)){
                        _legalMoves[index++] = new Move(this, curr_x, curr_y, "check", _runtimeData.FEN);
                    }
                    else{
                        _legalMoves[index++] = new Move(this, curr_x, curr_y, "capture", _runtimeData.FEN);
                    }
                }
                break;
            }

            curr_x += dx;
            curr_y += dy;
        }
        return index;
    }

    override 
    public void GenerateLegalMoves(Piece[,] board){
        _legalMoves = new Move[MAX_MOVEMENT];
        int index = 0;
        foreach (int dx in new int[]{-1,1}){
            foreach (int dy in new int[]{-1,1}){
                index = CreateMove(board, dx, dy, index);
            }
        }
        foreach (int dx in new int[]{-1,1}){
            index = CreateMove(board, dx, 0, index);
            index = CreateMove(board, 0, dx, index);
        }
    }
    
}

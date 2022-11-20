using UnityEngine;

public class GameState : MonoBehaviour{
    [SerializeField] RuntimeData _runtimeData;
    public RuntimeData RuntimeData{
        get{return _runtimeData;}
        set{_runtimeData = value;}
    }
    [SerializeField] GameOverScreen GOS;
    private Move[] _gameMoves;
    private Player _winner;
    private string _winReason;

    public void SetGameOver(bool b, string s){
        _runtimeData.isGameOver = b;
        _winReason = s;
    }

    static public int num = 0;

    public static GameState Instance;
    void Awake(){
        Instance = this;
    }

    void Update(){
        if(_runtimeData.isGameOver){
            try
            {
                GOS.Setup(_winner.Color, _winReason);   
                SetGameOver(false, "");
            }
            catch (System.Exception){   
            }
        }
    }

    public void SwitchCurrentPlayer(){
        if (_runtimeData.CurrentPlayer == _runtimeData.White){
            _runtimeData.CurrentPlayer = _runtimeData.Black;
            return ;
        }
        _runtimeData.CurrentPlayer = _runtimeData.White;
        
        _runtimeData.moveNum += 1;
        _runtimeData.halfMoveNum += 1;
        
        return ;
    }
    public void MovePiece(Move move){
        int from_x = move.Piece.GetFile();
        int from_y = move.Piece.GetRank();
        int to_x = move.X;
        int to_y = move.Y;
        bool isCapture = move.Type == "capture";
        bool isShortCastling = move.Type == "shortCastling";
        bool isLongCastling = move.Type == "longCastling";

        BoardManager.Instance.Board[to_x, to_y] = BoardManager.Instance.Board[from_x, from_y];
        BoardManager.Instance.Board[from_x, from_y] = null;

        GameObject pieceObj = GameObject.Find($"Piece:({from_x},{from_y})");
        
        if (isCapture){
            Destroy(GameObject.Find($"Piece:({to_x},{to_y})"));
            SoundManager.PlaySound("capture");
        }
        else{
            SoundManager.PlaySound("move");
        }
        
        
        pieceObj.transform.position = new Vector3(to_x, to_y, 0);
        pieceObj.name = $"Piece:({to_x},{to_y})";
        BoardManager.Instance.Board[to_x,to_y].SetFile(to_x);
        BoardManager.Instance.Board[to_x,to_y].SetRank(to_y);
        
        // move rooks for castling
        if (isShortCastling || isLongCastling){
            _runtimeData.CurrentPlayer.CanCastleLong = false;
            _runtimeData.CurrentPlayer.CanCastleShort = false;
            
            if (isShortCastling){
                from_x += 3;
                to_x -= 1;
            }
            else{
                from_x -= 4;
                to_x += 1;
            }
            pieceObj = GameObject.Find($"Piece:({from_x},{from_y})");
            pieceObj.transform.position = new Vector3(to_x, to_y, 0);
            pieceObj.name = $"Piece:({to_x},{to_y})";
            BoardManager.Instance.Board[to_x, to_y] = BoardManager.Instance.Board[from_x, from_y];
            BoardManager.Instance.Board[from_x, from_y] = null;
            BoardManager.Instance.Board[to_x,to_y].SetFile(to_x);
            BoardManager.Instance.Board[to_x,to_y].SetRank(to_y);

        }
        _runtimeData.PreviousPlayer = _runtimeData.CurrentPlayer;

        // check for checks
        if (IsCheck()){
            _runtimeData.isCheck = true;
        }
        
    }
    bool IsCheck(){
        for (int i=0; i<8; i++){
            for (int j=0; j<8; j++){
                var p = BoardManager.Instance.Board[i,j];
                if (p!=null && !_runtimeData.isGameOver){
                    p.GenerateLegalMoves(BoardManager.Instance.Board);
                    foreach (var move in p.GetLegalMoves()){
                        if (move!=null){
                            if ("check" == move.Type){
                                if (p.GetColor() != _runtimeData.CurrentPlayer.Color){
                                    // Debug.Log($"{p.GetColor()} == {_runtimeData.CurrentPlayer.Color}");
                                    _winner = _runtimeData.PreviousPlayer;
                                    _winReason = "Don't hang your king!";
                                    _runtimeData.isGameOver = true;
                                }
                                else{
                                    // then it is a check!
                                    int rank;
                                    int color = _runtimeData.CurrentPlayer.Color==0? 1: 0;
                                    
                                    if (color==1){
                                        // if the color of the king is black, then it's white's check!
                                        // white starts at 0
                                        rank = _runtimeData.CurrentPlayer.GetScore();
                                    }
                                    else{
                                        // black start from 7
                                        rank = 7 - _runtimeData.CurrentPlayer.GetScore();
                                    }
                                    BoardManager.Instance.PutPiece(6, -1, rank, color);
                                    _runtimeData.CurrentPlayer.IncreaseScore();

                                    return true;
                                }
                            }
                        }
                    }
                }
            }
        }
        return false;
    }

    public void SetWinner(Player p){
        _winner = p;
    }
    
}

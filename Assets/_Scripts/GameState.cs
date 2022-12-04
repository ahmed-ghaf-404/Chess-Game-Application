using UnityEngine;
using TMPro;

public class GameState : MonoBehaviour{
    [SerializeField] RuntimeData _runtimeData;
    public RuntimeData RuntimeData{
        get{return _runtimeData;}
        set{_runtimeData = value;}
    }
    [SerializeField] GameOverScreen GOS;
    [SerializeField] TMP_Text _fenText;
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
        _fenText.text = _runtimeData.FEN;
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
        GameObject sqrObj;
        Square sqr;
        if (_runtimeData.highlightedFromSquare.Length > 1){
            var x = _runtimeData.highlightedFromSquare[0];
            var y = _runtimeData.highlightedFromSquare[1];
            sqrObj = GameObject.Find($"Square:({x},{y})");
            sqr = sqrObj.GetComponent<Square>();
            sqr.Init((x + y) % 2 != 0);
        }
        if (_runtimeData.highlightedToSquare.Length > 1){
            var x = _runtimeData.highlightedToSquare[0];
            var y = _runtimeData.highlightedToSquare[1];
            sqrObj = GameObject.Find($"Square:({x},{y})");
            sqr = sqrObj.GetComponent<Square>();
            sqr.Init((x + y) % 2 != 0);
        }
        
        int from_x = move.Piece.GetFile();
        int from_y = move.Piece.GetRank();
        int to_x = move.X;
        int to_y = move.Y;
        bool isCapture = move.Type == "capture";
        bool isShortCastling = move.Type == "shortCastling";
        bool isLongCastling = move.Type == "longCastling";

        _runtimeData.Board[to_x, to_y] = _runtimeData.Board[from_x, from_y];
        _runtimeData.Board[from_x, from_y] = null;

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
        _runtimeData.Board[to_x,to_y].SetFile(to_x);
        _runtimeData.Board[to_x,to_y].SetRank(to_y);
        
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
            _runtimeData.Board[to_x, to_y] = _runtimeData.Board[from_x, from_y];
            _runtimeData.Board[from_x, from_y] = null;
            _runtimeData.Board[to_x,to_y].SetFile(to_x);
            _runtimeData.Board[to_x,to_y].SetRank(to_y);

        }
        _runtimeData.PreviousPlayer = _runtimeData.CurrentPlayer;

        // check for checks
        if (IsCheck()){
            _runtimeData.isCheck = true;
        }
        // highlight new squares
        sqrObj = GameObject.Find($"Square:({to_x},{to_y})");
        sqr = sqrObj.GetComponent<Square>();
        sqr.HighlightToSquare();
        _runtimeData.highlightedToSquare = new int[2]{to_x, to_y};

        sqrObj = GameObject.Find($"Square:({from_x},{from_y})");
        sqr = sqrObj.GetComponent<Square>();
        sqr.HighlightFromSquare();
        _runtimeData.highlightedFromSquare = new int[2]{from_x, from_y};
        
        // encode current position to FEN
        EncodeFEN();
        
    }

    private string EncodeFEN(){
        string[] rankFEN = new string[8];
        for (int i=0;i<8;i++){
            for (int j=0; j<8;j++){
                switch 
            }
        }
        return null;
    }
    bool IsCheck(){
        for (int i=0; i<8; i++){
            for (int j=0; j<8; j++){
                var p = _runtimeData.Board[i,j];
                if (p!=null && !_runtimeData.isGameOver){
                    p.GenerateLegalMoves(_runtimeData.Board);
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

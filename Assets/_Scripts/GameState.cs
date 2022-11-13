using UnityEngine;

public class GameState : MonoBehaviour{
    private Move[] _gameMoves;
    private Player _white = new Player(0);
    private Player _black = new Player(1);
    private Player _currentPlayer;
    private Player _prevPlayer; // Will be used to detect change in players
    private Player _winner;
    private bool _isGameOver;
    private string _winReason;

    public bool IsGameOver(){
        return _isGameOver;
    }
    public void SetGameOver(bool b, string s){
        _isGameOver = b;
    }

    public Player CurrentPlayer{
        get{return _currentPlayer;}
        set{_currentPlayer = value;}
    }
    // private int _halfMoveCounter = 0;
    private int _fullMoveCounter = 1; 
    public int FullMoveCounter{
        get{return _fullMoveCounter;}
        set{_fullMoveCounter = value;}
    }

    static public int num = 0;

    [SerializeField] GameOverScreen GOS;
    public static GameState Instance;
    void Awake(){
        Instance = this;
        _currentPlayer = _white;
        _prevPlayer = _black;
    }

    void Update(){
        // detect movement
        if (_prevPlayer==_currentPlayer){
            SwitchCurrentPlayer();
            BoardManager.Instance.GenerateAllLegalMoves();
        }
        if(IsGameOver()){
            GOS.Setup(_winner.Color, _winReason);
        }
    }

    private void SwitchCurrentPlayer(){
        if (_currentPlayer==_white){
            _currentPlayer = _black;
            return ;
        }
        _currentPlayer = _white;
        _fullMoveCounter++;
        return ;
    }
    public void MovePiece(int from_x, int from_y, int to_x, int to_y, bool isCapture){
        BoardManager.Instance.Board[to_x, to_y] = BoardManager.Instance.Board[from_x, from_y];
        BoardManager.Instance.Board[from_x, from_y] = null;

        GameObject pieceObj = GameObject.Find($"Piece:({from_x},{from_y})");
        
        if (isCapture){
            Destroy(GameObject.Find($"Piece:({to_x},{to_y})"));
        }
        
        pieceObj.transform.position = new Vector3(to_x, to_y, -1);
        pieceObj.name = $"Piece:({to_x},{to_y})";
        BoardManager.Instance.Board[to_x,to_y].SetFile(to_x);
        BoardManager.Instance.Board[to_x,to_y].SetRank(to_y);
        _prevPlayer = _currentPlayer;

        // check for checks
        IsCheck();
        
    }
    bool IsCheck(){
        for (int i=0; i<8; i++){
            for (int j=0; j<8; j++){
                var p = BoardManager.Instance.Board[i,j];
                if (p!=null){
                    p.GenerateLegalMoves(BoardManager.Instance.Board);
                    foreach (var move in p.GetLegalMoves()){
                        if (move!=null){
                            if (typeof(CheckMove)==move.GetType()){
                                if (p.GetColor()==_currentPlayer.Color){
                                    _currentPlayer.IncreaseScore();
                                    return true;
                                }
                                else{
                                    _winner = _prevPlayer;
                                    _winReason = "Don't hang your king!";
                                    _isGameOver = true;
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

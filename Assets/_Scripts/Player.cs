

public class Player{
    private bool _isCheck;
    private Move _checkMove;
    private int _color;
    public int Color{
        get{ return _color;}
        set{_color = value;}
    }
    private int _score = 0;
    private bool _canCastleShort;
    public bool CanCastleShort{
        get{return _canCastleShort;}
        set{_canCastleShort = value;}
    }
    private bool _canCastleLong;
    public bool CanCastleLong{
        get{return _canCastleLong;}
        set{_canCastleLong = value;}
    }


    public Player(int color){
        _color = color;
    }
    public void IncreaseScore(){
        _score++;
        if (_score>=3){
            GameState.Instance.SetWinner(this);
            GameState.Instance.SetGameOver(true, "3 checks!");
        }
    }
    public int GetScore(){
        return _score;
    }
}
public class Move{
    public Move(Piece p, int x, int y, string type, string fen){
        this._piece = p;
        this._x = x;
        this._y = y;
        this._type = type;
        this._fen = fen;
    }
    public Move(Piece p, int x, int y, string type, string fen, int specialMove){
        this._piece = p;
        this._x = x;
        this._y = y;
        this._type = type;
        this._fen = fen;
        this._specialMove = specialMove;
    }
    protected Piece _piece;
    public Piece Piece{
        get{return _piece;}
        set{_piece = value;}
    }
    private int _x; // x coordinate of the target square
    public int X{
        get{ return _x;}
        set{ _x = value;}
    }
    private int _y; // y coordinate of the target square
    public int Y{
        get{return _y;}
        set{_y = value;}
    }
    private string _type;
    public string Type{
        get{return _type;}
        set{_type = value;}
    }
    private string _fen;
    public string FEN{
        get{return _fen;}
        set{_fen = value;}
    }
    private int _specialMove;
    public int SpecialMove{
        get{return _specialMove;}
        set{_specialMove = value;}
    }
    
    public override string ToString(){
        return $"{_piece}: ({_x},{_y})";
    }
    public bool IsSameMove(Move m){
        return m.X==this._x && m.Y==this._y && m.Type==_type;
    }
    
    
}
public class Move{
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
    
    public override string ToString(){
        return $"{_piece}: ({_x},{_y})";
    }
    public bool IsSameMove(Move m){
        return m.X==this._x && m.Y==this._y && m.Type==_type;
    }
    public Move(Piece p, int x, int y, string type){
        this._piece = p;
        this._x = x;
        this._y = y;
        this._type = type;
    }
}

// class QuitMove : Move{
//     public QuitMove(Piece p, int x, int y){
//         this._thisPiece = p;
//         this.X = x;
//         this.Y = y;
//     }
// }

// class CaptureMove : Move{
//     public CaptureMove(Piece p, int x, int y){
//         this._thisPiece = p;
//         this.X = x;
//         this.Y = y;
//     }
// }
// class CheckMove : Move{
//     public CheckMove(Piece p, int x, int y){
//         this._thisPiece = p;
//         this.X = x;
//         this.Y = y;
//     }
// }

// class ShortCastleMove: Move{
//     public ShortCastleMove(Piece p, int x, int y){
//         this._thisPiece = p;      
//     }
// }
// class LongCastleMove: Move{
//     public LongCastleMove(Piece p, int x, int y){
//         this._thisPiece = p;
//     }
// }
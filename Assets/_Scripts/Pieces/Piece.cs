using System;
using System.Collections.Generic;
using UnityEngine;



public abstract class Piece : MonoBehaviour{
       
    [SerializeField] Sprite _black;
    [SerializeField] protected RuntimeData _runtimeData;
    public static string[] MoveTypes = {"check", "capture", "quite", "shortCastling", "longCastling"};
    protected int file;
    protected int rank;
    protected int color;

    protected int MAX_MOVEMENT;
    protected string _name;
    public string Name{
        get{return _name;}
        set{value = _name;}
    }
    protected char _code;
    public char Code{
        get{return _code;}
        set{value = _code;}
    }
    
    protected bool _hasMoved;
    public bool HasMoved{
        get{return _hasMoved;}
        set{_hasMoved = value;}
    }
    
    SpriteRenderer spriteRenderer;
    public void Awake(){
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (!_runtimeData.LegalMoves.ContainsKey(this)){
            _runtimeData.LegalMoves.Add(this, new List<Move>());
        }
    }
    
    abstract public void GenerateLegalMoves(Piece[,] board);

    
    public void Init(string name, int file, int rank, int color){
        this._name = name;
        this.rank = rank;
        this.file = file;
        this.color = color;
        if (color==1)
            spriteRenderer.sprite = _black;
        this._code = this.color==0? Char.ToUpper(_name[0]) : Char.ToLower(_name[0]);
    }

    public int GetFile(){return this.file;}
    public int GetRank(){return this.rank;}
    public int GetColor(){return this.color;}
    public void SetFile(int file){this.file=file;} 
    public void SetRank(int rank){this.rank=rank;} 
    public void SetColor(int color){
        this.color=color;
        if (color==1)
            spriteRenderer.sprite = _black;
    } 
    public List<Move> GetLegalMoves(){
        return _runtimeData.LegalMoves[this];
    }
    public bool IsEnemy(Piece other){
        if (other==null)
            return false;
        return GetColor()!=other.GetColor();
    }

    public string PieceString(){
        return $"{color} {_name}: ({rank},{file})";
    }
    
    public bool IsLegalMove(Move other_move){
        foreach (var move in _runtimeData.LegalMoves[this]){
            if (move.IsSameMove(other_move)){
                _runtimeData.CurrentMoveLegalMove = move;
                return true;
            }
        }
        return false;
    }
    public void ClearLegalMoves(){
        _runtimeData.LegalMoves[this] = new List<Move>();
    }

    Vector3 GetMousePosition(){
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }
    void OnMouseDrag(){
        if(_runtimeData.CurrentPlayer.Color == color && !_runtimeData.isGameOver){
            var pos = GetMousePosition();
            pos.z = -0.5f;
            transform.position = pos;
        }
    }
    void OnMouseUp(){
        if(_runtimeData.CurrentPlayer.Color == color && !_runtimeData.isGameOver){
            var piecePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var x = (int) Math.Round(piecePosition.x, MidpointRounding.AwayFromZero);
            var y = (int) Math.Round(piecePosition.y, MidpointRounding.AwayFromZero);
            var moved = false;
            Move temp;
            foreach (string moveType in MoveTypes){
                temp = new Move(this, x, y, moveType, _runtimeData.FEN);
                if (IsLegalMove(temp)){
                    GameState.Instance.SwitchCurrentPlayer();
                    if (temp.Type == "capture" || this.Name == "pawn"){
                        _runtimeData.halfMoveNum = 0;
                    }
                    MoveTo(_runtimeData.CurrentMoveLegalMove);
                    moved = true;
                    BoardManager.Instance.GenerateAllLegalMoves();
                    HasMoved = true;
                }
            }
            
            if (!moved){
                gameObject.transform.position = new Vector3(GetFile(), GetRank(), 0);
            }
        }
    }

    protected void MoveTo(Move m){
        // move the piece block!
        // TODO: otherwise no moves!
        transform.position = new Vector3(m.X, m.Y, 0);
        //

        _runtimeData.EnPassantSquare = null; // reset enpassant

        if (m.SpecialMove == 3){
            Debug.Log("This pawn doubled the moves!");
            // ! ENPASSANT IS LEGAL AT X - OFFSET, Y!
            var rank = m.Piece.color==0? m.Y - 1 : m.Y + 1;
            _runtimeData.EnPassantSquare = new Tuple<int, int>(m.X, rank);
            Debug.Log(_runtimeData.EnPassantSquare);
        }
        else
            Debug.Log(m.SpecialMove);
    }
    
}
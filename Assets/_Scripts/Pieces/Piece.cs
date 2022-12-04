using System;
using UnityEngine;



public abstract class Piece : MonoBehaviour{
       
    [SerializeField] Sprite _black;
    protected RuntimeData _runtimeData;
    public static string[] MoveTypes = {"check", "capture", "quite", "shortCastling", "longCastling"};
    protected int file;
    protected int rank;
    protected int color;

    protected int MAX_MOVEMENT;
    protected string pieceName;
    protected Move[] _legalMoves;
    protected bool _hasMoved;
    public bool HasMoved{
        get{return _hasMoved;}
        set{_hasMoved = value;}
    }
    
    SpriteRenderer spriteRenderer;
    public void Awake(){
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _runtimeData = BoardManager.Instance._runtimeData;
    }
    
    abstract public void GenerateLegalMoves(Piece[,] board);

    
    public void Init(int file, int rank, int color){
        this.rank = rank;
        this.file = file;
        this.color = color;
        if (color==1)
            spriteRenderer.sprite = _black;
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

    public string GetName(){return this.pieceName;} 
    public void SetName(string pieceName){this.pieceName=pieceName;} 
    public Move[] GetLegalMoves(){
        return _legalMoves;
    }
    public void SetLegalMoves(Move[] moves){
        this._legalMoves = moves;
    }
    public bool IsEnemy(Piece other){
        if (other==null)
            return false;
        return GetColor()!=other.GetColor();
    }

    public string PieceString(){
        return $"{color} {pieceName}: ({rank},{file})";
    }
    public char GetSymbol(){
        return pieceName[0];
    }
    public bool IsLegalMove(Move other_move){
        foreach (var move in _legalMoves){
            if (move == null)
                continue;
            else if (move.IsSameMove(other_move))
                return true;
        }
        return false;
    }
    public void ClearLegalMoves(){
        _legalMoves = new Move[1];
    }

    Vector3 GetMousePosition(){
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }
    void OnMouseDrag(){
        if(GameState.Instance.RuntimeData.CurrentPlayer.Color == color && !GameState.Instance.RuntimeData.isGameOver){
            var pos = GetMousePosition();
            pos.z = -0.5f;
            transform.position = pos;
        }
    }
    void OnMouseUp(){
        if(GameState.Instance.RuntimeData.CurrentPlayer.Color == color && !GameState.Instance.RuntimeData.isGameOver){
            var piecePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var x = (int) Math.Round(piecePosition.x, MidpointRounding.AwayFromZero);
            var y = (int) Math.Round(piecePosition.y, MidpointRounding.AwayFromZero);
            var moved = false;
            Move temp;
            foreach (string moveType in MoveTypes){
                temp = new Move(this, x, y, moveType, _runtimeData.FEN);
                if (IsLegalMove(temp)){
                    GameState.Instance.MovePiece(temp);
                    GameState.Instance.SwitchCurrentPlayer();
                    BoardManager.Instance.GenerateAllLegalMoves();
                    HasMoved = true;
                    moved = true;
                }    
            }
            
            if (!moved){
                gameObject.transform.position = new Vector3(GetFile(), GetRank(), 0);
            }
        }
    }
    
}
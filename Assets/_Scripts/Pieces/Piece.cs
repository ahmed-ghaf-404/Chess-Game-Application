using UnityEngine;



public abstract class Piece : MonoBehaviour{
       
    [SerializeField] Sprite _black;
    protected int file;
    protected int rank;
    protected int color;
    protected bool hasMoved=false;

    protected string pieceName;
    protected Move[] _legalMoves;
    
    SpriteRenderer spriteRenderer;
    public void Awake(){
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    
    abstract public void GenerateLegalMoves(Piece[,] board);

    
    public void Init(int file, int rank, int color, BoardUIManager boardManager ){
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
    public void SetHasMoved(){
        hasMoved = true;
    }
    public bool GetHasMoved(){
        return hasMoved;
    }
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

    public void DestroyPiece(){
        Destroy(this.gameObject);
    }

    public string PieceString(){
        return $"{color} {pieceName}: ({rank},{file})";
    }
    public char GetSymbol(){
        return pieceName[0];
    }
    public bool IsLegalMove(Move other_move){
        foreach (var move in _legalMoves){
            Debug.Log(move);
            if (move == null)
                continue;
            else if (move.IsSameMove(other_move))
                return true;
        }
        Debug.Log("No legal moves found");
        return false;
    }
    
}
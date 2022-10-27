using UnityEngine;



public abstract class Piece : MonoBehaviour{
       
    [SerializeField] Sprite _black;
    private int file;
    private int rank;
    private int color;
    private bool hasMoved=false;

    private string pieceName;
    
    SpriteRenderer spriteRenderer;
    public void Awake(){
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    
    abstract public void GenerateLegalMoves();
    abstract public bool IsLegalMove(Move other_move);
    


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

    public void DestroyPiece(){
        Destroy(this.gameObject);
    }
    
}
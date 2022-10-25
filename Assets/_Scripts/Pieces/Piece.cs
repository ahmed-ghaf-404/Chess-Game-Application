using UnityEngine;



public class Piece : MonoBehaviour{
       
    [SerializeField] Sprite _black;
    private int file;
    private int rank;
    private int color;

    private string pieceName;
    
    public bool isEnemy(Piece other){
        /*
        input: Piece other. A different piece
        output: bool. Does the other piece have a different color. 
        */
        return !(other.color==this.color);
    }
    SpriteRenderer spriteRenderer;
    public void Awake(){
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
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

    public void DestroyPiece(){
        Destroy(this.gameObject);
    }

    
}
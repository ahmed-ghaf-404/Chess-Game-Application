using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Threading;

public class BoardUIManager : MonoBehaviour{
    [SerializeField] private int _width = 8;
    [SerializeField] private int _height = 8;
    [SerializeField] private Square _squarePrefab;
    [SerializeField] private Transform _cam;
    [SerializeField] private Pawn _pawnPrefab;
    [SerializeField] private Knight _knightPrefab;
    [SerializeField] private Bishop _bishopPrefab;
    [SerializeField] private Rook _rookPrefab;
    [SerializeField] private Queen _queenPrefab;
    [SerializeField] private King _kingPrefab;


    private int _halfMoveCounter;
    private int _fullMoveCounter;
    Piece prefab;
    
    Player[] players;
    private Player _currPlayer; 

    public static Piece selectedPiece;
    public static Square selectedSquare;
    public static Piece otherSelectedPiece;
    
    
    public BoardUIManager(){
        Board.SetBoard(_height, _width);
        this.players = new Player[] {new Player(), new Player()};
        this._currPlayer = players[0];
    }
    // Start is called before the first frame update
    void Start(){
        Debug.Log("Generating squares:");
        GenerateSquares();
        Debug.Log("End generating squares:");

        Debug.Log("Setting board FEN:");
        
        ReadFEN("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
        Debug.Log("End setting board");
    }

    void Update(){
        // if we're ready to make a move
        // make it
        if (selectedPiece!=null && selectedSquare!=null){
            Board.MakeMove(selectedPiece, (int) selectedSquare.transform.position.x, (int) selectedSquare.transform.position.y);
            selectedSquare = null;
        }
        // else
        // reset square to null
    }

    void GenerateSquares(){
        for (int x=0; x < _width; x++){
            for (int y=0; y < _height; y++){
                var generatedSquare = Instantiate(_squarePrefab, new Vector3(x,y), Quaternion.identity);
                generatedSquare.name = $"Square:({x},{y})";
                var isDarkSquare = (x + y) % 2 != 0;
                generatedSquare.Init(isDarkSquare);
                Board.SetSquare(x, y, generatedSquare);
            }
        }
        _cam.transform.position = new Vector3((float) _width*0.43f, (float) _height*0.43f, -10);
    }
    Piece PutPiece(int piece, int x, int y, int color){
        string name = "";
        switch (piece){
            case 0: return null;
            case 1: prefab = _pawnPrefab; name = "pawn"; break;
            case 2: prefab = _knightPrefab; name = "knight"; break;
            case 3: prefab = _bishopPrefab; name = "bishop"; break;
            case 4: prefab = _rookPrefab; name = "rook"; break;
            case 5: prefab = _queenPrefab; name = "queen"; break;
            case 6: prefab = _kingPrefab; name = "king"; break;
        }
        var generatedPiece = Instantiate(prefab, new Vector3(x,y,-1), Quaternion.identity);
        // generatedPiece.name = $"{name}:({x},{y})";
        generatedPiece.name = $"Piece:({x},{y})";
        generatedPiece.SetRank(x);
        generatedPiece.SetFile(y);
        generatedPiece.SetName(name);
        generatedPiece.SetColor(color);
        

        Board.SetPiece(x, y, generatedPiece);
        return generatedPiece;
    }

    private void _FenToPiecePlacement(string fen){
        int index = 0;
        int x = 0;
        int y = 0;
        char curr;
        int color;
        
        while (index<fen.Length){
            // Debug.Log($"{fen.Length}>{index}");
            // Debug.Log($"while loop {index}: ({x},{y})");
            curr = fen[index];
            color = char.IsLower(curr)? 0 : 1;
            
            index++;
            
            if (char.IsDigit(curr))
                x += curr - '0';
            switch (char.ToLower(curr)){
                case 'r': PutPiece(4,x,y,color); x++; break;
                case 'n': PutPiece(2,x,y,color); x++; break;
                case 'b': PutPiece(3,x,y,color); x++; break;
                case 'q': PutPiece(5,x,y,color); x++; break;
                case 'k': PutPiece(6,x,y,color); x++; break;
                case 'p': PutPiece(1,x,y,color); x++; break;
                case '/': y++; x=0; break;
                // rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR
            }
        }
    }
    void ReadFEN(string fen){
        List<string> fen_parts = new List<string>(fen.Split(' '));
        foreach (var part in fen_parts)
        {
            Debug.Log($"|{part}|");
        }
        // first part: piece placement
        _FenToPiecePlacement(fen_parts[0]);

        // second part: active color (current player)
        _currPlayer = char.ToString('w')==fen_parts[1]? players[0] : players[1];

        // TODO: rest parts
        // third part: castling ability

        // fourth part: enpassant for next move is legal

        // fifth part: half move counter
        int.TryParse(fen_parts[4], out this._halfMoveCounter);
        
        // sixth part: full move counter
        int.TryParse(fen_parts[5], out this._fullMoveCounter);
        
    }
    private void MakeMove(GameObject from, GameObject to){
        float x = to.transform.position.x;
        float y = to.transform.position.y;


        // TODO: if legal move, excute next line
        from.transform.position = new Vector3(x,y,-1);
        
    }
    static public void ResetSelection(){
        selectedPiece = null;
        selectedSquare = null;
        otherSelectedPiece = null;
    }
    static public void MovePiece(int from_rank, int form_file, int to_rank, int to_file){
        if(otherSelectedPiece!=null)
            Destroy(otherSelectedPiece);
        selectedPiece.transform.position = new Vector3(to_rank, to_file, -1);

    }
}   

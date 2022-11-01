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

    public Piece selectedPiece = null;
    public Piece selectedEmptySquare = null;
    public Piece otherSelectedPiece = null;
    public Piece[,] board;

    void Awake(){
        this.board = new Piece[_height, _width];
        this.players = new Player[] {new Player(), new Player()};
        this._currPlayer = players[0];
        Debug.Log("Generating squares:");
        GenerateSquares();
        Debug.Log("End generating squares:");

        Debug.Log("Setting board FEN:");
        
        ReadFEN("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
        // ReadFEN("rnbqkbnr/ppp1pppp/8/3p4/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 1");
        Debug.Log("End setting board");
        
    }

    void GenerateSquares(){
        for (int x=0; x < _width; x++){
            for (int y=0; y < _height; y++){
                var generatedSquare = Instantiate(_squarePrefab, new Vector3(x,y), Quaternion.identity);
                generatedSquare.name = $"Square:({x},{y})";
                var isDarkSquare = (x + y) % 2 != 0;
                generatedSquare.Init(isDarkSquare);
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
        generatedPiece.Init(x,y,color, this);
        generatedPiece.SetName(name);

        board[x, y] = generatedPiece;
        
        return generatedPiece;
    }

    private void _FenToPiecePlacement(string fen){
        int index = 0;
        int x = 0;
        int y = 0;
        char curr;
        int color;
        
        while (index<fen.Length){
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
    public void MovePiece(int from_x, int from_y, int to_x, int to_y, bool isCapture){
        board[to_x, to_y] = board[from_x, from_y];
        board[from_x, from_y] = null;

        GameObject pieceObj = GameObject.Find($"Piece:({from_x},{from_y})");
        
        if (isCapture){
            Debug.Log("destroyed piece via capture");
            Destroy(GameObject.Find($"Piece:({to_x},{to_y})"));
        }
        
        pieceObj.transform.position = new Vector3(to_x, to_y, -1);
        pieceObj.name = $"Piece:({to_x},{to_y})";
        board[to_x,to_y].SetRank(to_x);
        board[to_x,to_y].SetFile(to_y);
    }
    static public void PrintBoard(Piece[,] board){
        string s = "";
        for (int i=0; i<8;i++){
            for(int j=0; j<8; j++){
                if (board[j,i] == null)
                    s+="  |";
                else{
                    s += $"{board[j,i].GetSymbol()}|";
                }
            }
            s+='\n';
        }
        Debug.Log(s);
    }

    void OnMouseDown(){
        var x = (int) Input.mousePosition.x - 85;
        var y = (int) Input.mousePosition.y - 80;

        // first square aka (0,0):
        // x: 0~80
        // y: 0~80
        int square_x = x / 80;
        int square_y = y / 80;
        if (x%80>15 && x%80<65 && y%80>15 && y%80<65 && square_x<8 && square_y<8){
            // guarded area
            CheckClicking(board, square_x, square_y);
            if (selectedPiece != null){
                selectedPiece.GenerateLegalMoves(this.board); 
            }
            
        }
    }
    void CheckClicking(Piece[,] board, int x, int y){
        // case 1: selecting a new piece
        if (selectedPiece == null && board[x, y] != null){
            selectedPiece = board[x, y];
        }
        else if(selectedPiece != null){
            // case 2: selecting a new square
            if (board[x, y] == null){
                selectedEmptySquare = board[x, y];
                if (selectedPiece.IsLegalMove(new QuitMove(selectedPiece, x, y))){
                    MovePiece(selectedPiece.GetRank(), selectedPiece.GetFile(), x, y, false);
                }
            }
            // case 3: select another piece
            if (board[x, y] != null && selectedPiece.GetRank()!=x && selectedPiece.GetFile()!=y){
                otherSelectedPiece = board[x, y];
                PrintBoard(board);
                Debug.Log($"Selected another piece: {otherSelectedPiece}");
                if (selectedPiece.IsLegalMove(new CaptureMove(selectedPiece, x, y))){
                    MovePiece(selectedPiece.GetRank(), selectedPiece.GetFile(), x, y, true);
                }
            }
            ResetSelection();
        }
        // Debug.Log($"Selected piece: {selectedPiece}");
        // Debug.Log($"Selected empty square: {selectedEmptySquare}");

    }
    void ResetSelection(){
        selectedEmptySquare = null;
        selectedPiece = null;
        otherSelectedPiece = null;
        return ;
    }
}   

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour{
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
    
    static public BoardManager Instance;
    Piece prefab;
    public Piece selectedPiece = null;
    public Piece selectedEmptySquare = null;
    public Piece otherSelectedPiece = null;
    private Piece[,] _board;
    public Piece[,] Board{
        get{return _board;}
        set{_board = value;}
    }


    void Awake(){
        Instance = this;
        this._board = new Piece[_height, _width];
        Debug.Log("Generating squares:");
        GenerateSquares();
        Debug.Log("End generating squares:");

        Debug.Log("Setting board FEN:");
        
        ReadFEN("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
        // ReadFEN("2k5/1p6/8/4B3/8/8/8/4K3 w - - 0 1");
        // ReadFEN("1k6/6p1/8/8/4N3/8/8/1K6 w - - 0 1");
        // ReadFEN("rnbqkbnr/ppp1pppp/8/3p4/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 1");
        Debug.Log("End setting board");
        // FlipBoard();

        Debug.Log("generating all legal moves:");
        GenerateAllLegalMoves();
        Debug.Log("End generating all legal moves");
        
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
    Piece PutPiece(int piece, int file, int rank, int color){
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
        var generatedPiece = Instantiate(prefab, new Vector3(file,rank,-1), Quaternion.identity);
        // generatedPiece.name = $"{name}:({x},{y})";
        generatedPiece.name = $"Piece:({file},{rank})";
        generatedPiece.Init(file,rank,color);
        generatedPiece.SetName(name);

        Board[file, rank] = generatedPiece;
        
        return generatedPiece;
    }

    private void _FenToPiecePlacement(string fen){
        int index = 0;
        int x = 0;
        int y = 7;
        char curr;
        int color;
        
        while (index<fen.Length){
            curr = fen[index];
            color = char.IsLower(curr)? 1 : 0;
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
                case '/': y--; x=0; break;
                // rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR
            }
        }
    }
    void ReadFEN(string fen){
        List<string> fen_parts = new List<string>(fen.Split(' '));
        // first part: piece placement
        _FenToPiecePlacement(fen_parts[0]);

        // second part: active color (current player)
        // _currPlayer = char.ToString('w')==fen_parts[1]? players[0] : players[1];

        // TODO: rest parts
        // third part: castling ability

        // fourth part: enpassant for next move is legal

        // fifth part: half move counter
        // int.TryParse(fen_parts[4], out this._halfMoveCounter);
        
        // sixth part: full move counter
        int temp;
        int.TryParse(fen_parts[5], out temp);
        GameState.Instance.FullMoveCounter = temp;
        
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
            CheckClicking(Board, square_x, square_y);
        }
    }

    public void GenerateAllLegalMoves(){
        foreach (var piece in Board){
            if (piece!=null){
                if(piece.GetColor()==GameState.Instance.CurrentPlayer.Color){
                    piece.GenerateLegalMoves(Board);
                }
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
                if (selectedPiece.IsLegalMove(new QuitMove(selectedPiece, x, y)) && GameState.Instance.CurrentPlayer.Color == selectedPiece.GetColor()){
                    GameState.Instance.MovePiece(selectedPiece.GetFile(), selectedPiece.GetRank(), x, y, false);
                }
            }
            // case 3: select another piece
            else if (board[x, y] != null){
                otherSelectedPiece = board[x, y];
                if (selectedPiece.IsLegalMove(new CaptureMove(selectedPiece, x, y)) &&  GameState.Instance.CurrentPlayer.Color == selectedPiece.GetColor()){
                    GameState.Instance.MovePiece(selectedPiece.GetFile(), selectedPiece.GetRank(), x, y, true);
                }
            }
            ResetSelection();
        }
    }
    void ResetSelection(){
        selectedEmptySquare = null;
        selectedPiece = null;
        otherSelectedPiece = null;
        return ;
    }

    void FlipBoard(){
        var rotationVector = _cam.transform.rotation.eulerAngles;
        rotationVector.z = 180;
        _cam.transform.rotation = Quaternion.Euler(rotationVector);
        for (int i=0; i<8; i++){
            for(int j=0; j<8; j++){
                if (Board[i,j] != null){
                    GameObject go = GameObject.Find($"Piece:({i},{j})");
                    go.transform.rotation = Quaternion.Euler(rotationVector);
                }
            }
        }
    }
}   

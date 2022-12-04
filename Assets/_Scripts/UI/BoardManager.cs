using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

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

    [SerializeField] public RuntimeData _runtimeData;
    
    
    static public BoardManager Instance;
    Piece prefab;
    private object f;

    void Awake(){
        Instance = this;
        _runtimeData.White = new Player(0);
        _runtimeData.Black = new Player(1);

        this._runtimeData.Board = new Piece[_height, _width];
        // Debug.Log("Generating squares:");
        GenerateSquares();
        // Debug.Log("End generating squares:");
        
        // Debug.Log("Setting board FEN:");
        _runtimeData.FEN = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
        // _runtimeData.FEN = "r3k2r/pppqppbp/2np1np1/5b2/5B2/2NP1NP1/PPPQPPBP/R3K2R w KQkq - 6 8";
        ReadFEN(_runtimeData.FEN);
        // Debug.Log("End setting board");
        // FlipBoard();

        // Debug.Log("generating all legal moves:");
        GenerateAllLegalMoves();
        // Debug.Log("End generating all legal moves");
    }
    void Start(){
        SoundManager.PlaySound("notification");
    }

    void GenerateSquares(){
        for (int x=0; x < _width; x++){
            for (int y=0; y < _height; y++){
                var generatedSquare = Instantiate(_squarePrefab, new Vector3(x,y,1), Quaternion.identity, GameObject.Find("Board").transform);
                generatedSquare.name = $"Square:({x},{y})";
                var isDarkSquare = (x + y) % 2 != 0;
                generatedSquare.SetCoord(x,y);
                generatedSquare.Init(isDarkSquare);
            }
        }
        _cam.transform.position = new Vector3((float) _width*0.45f, (float) _height*0.45f, -1);
    }
    public Piece PutPiece(int piece, int file, int rank, int color){
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
        var generatedPiece = Instantiate(prefab, new Vector3(file,rank,0), Quaternion.identity, GameObject.Find("Pieces").transform);
        
        // 
        generatedPiece.name = $"Piece:({file},{rank})";
        generatedPiece.Init(name,file,rank,color);
        
        if (file>=0 && file<8 && rank>=0 && rank<8){
            // this settings are only for playable pieces ON THE BOARD
            _runtimeData.Board[file, rank] = generatedPiece;
        }
        
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
    void ParseCaslting(string castling){
        foreach (char c in castling){
            switch (c){
                case 'K':  
                    if (_runtimeData.Board[4,0]!=null){
                        if (_runtimeData.Board[4,0].GetType() == typeof(King))
                            _runtimeData.White.CanCastleShort = true; 
                    }
                    else{
                        _runtimeData.Black.CanCastleShort = true; 
                    }
                    break;
                case 'Q':  
                    if (_runtimeData.Board[4,0] != null)
                        if (_runtimeData.Board[4,0].GetType() == typeof(King)){
                            _runtimeData.White.CanCastleLong = true; 
                        }
                        else{
                            _runtimeData.Black.CanCastleLong = false; 
                        }
                    break;
                case 'k':  
                    if (_runtimeData.Board[4,7] != null)
                        if (_runtimeData.Board[4,7].GetType() == typeof(King)){
                            _runtimeData.Black.CanCastleShort = true; 
                        }
                        else{
                            _runtimeData.Black.CanCastleShort = false; 
                        }
                    break;
                case 'q':  
                    if (_runtimeData.Board[4,7] != null)
                        if (_runtimeData.Board[4,7].GetType() == typeof(King)){                        
                            _runtimeData.Black.CanCastleLong = true; 
                        }
                        else{
                            _runtimeData.Black.CanCastleLong = false; 
                        }
                    break;
            }
        }
    }
    void ReadFEN(string fen){
        List<string> fen_parts = new List<string>(fen.Split(' '));
        // first part: piece placement
        _FenToPiecePlacement(fen_parts[0]);

        // second part: active color (current player)
        _runtimeData.CurrentPlayer = char.ToString('w')==fen_parts[1]? _runtimeData.White : _runtimeData.Black;
        
        // third part: castling ability
        ParseCaslting(fen_parts[2]);
        
        // fourth part: enpassant for next move is legal

        // fifth part: half move counter
        int.TryParse(fen_parts[4], out _runtimeData.halfMoveNum);
        
        // sixth part: full move counter
        int.TryParse(fen_parts[5], out _runtimeData.moveNum);
        
    }
    static public void PrintBoard(Piece[,] board){
        string s = "";
        for (int i=0; i<8;i++){
            for(int j=0; j<8; j++){
                if (board[j,i] == null)
                    s+="  |";
                else{
                    s += $"{board[j,i].Code}|";
                }
            }
            s+='\n';
        }
        Debug.Log(s);
    }

    public void GenerateAllLegalMoves(){
        foreach (var piece in _runtimeData.Board){
            if (piece!=null){
                if(piece.GetColor()==_runtimeData.CurrentPlayer.Color){
                    piece.GenerateLegalMoves(_runtimeData.Board);
                }
            }
        }
    }

    public void FlipBoard(){
        var rotationVector = _cam.transform.rotation.eulerAngles;
        if (rotationVector.z != 180){
            rotationVector.z = 180;
        }
        else{
            rotationVector.z = 0;
        }

        _cam.transform.rotation = Quaternion.Euler(rotationVector);
        for (int i=0; i<8; i++){
            for(int j=0; j<8; j++){
                GameObject go;
                if (_runtimeData.Board[i,j] != null){
                    go = GameObject.Find($"Piece:({i},{j})");
                    go.transform.rotation = Quaternion.Euler(rotationVector);
                }
                go = GameObject.Find($"Square:({i},{j})");
                go.transform.rotation = Quaternion.Euler(rotationVector);
            }
        }
    }
    // public void ShowPieces(){
    //     GameObject pieces = GameObject.FindGameObjectWithTag("pieces");
    //     if (pieces != null)
    //         pieces.SetActive(true);
    // }

    public void ClearAllLegalMoves(){
        foreach (var piece in _runtimeData.Board){
            if (piece!=null){
                piece.ClearLegalMoves();
            }
        }
    }
}   

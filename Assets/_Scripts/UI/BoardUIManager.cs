using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour{
    [SerializeField] private int _width = 8;
    [SerializeField] private int _height = 8;
    [SerializeField] private Square _squarePrefab;
    [SerializeField] private Transform _cam;
    [SerializeField] private Piece _whitePawnPrefab;
    [SerializeField] private Piece _whiteKnightPrefab;
    [SerializeField] private Piece _whiteBishopPrefab;
    [SerializeField] private Piece _whiteRookPrefab;
    [SerializeField] private Piece _whiteQueenPrefab;
    [SerializeField] private Piece _whiteKingPrefab;
    [SerializeField] private Piece _blackPawnPrefab;
    [SerializeField] private Piece _blackKnightPrefab;
    [SerializeField] private Piece _blackBishopPrefab;
    [SerializeField] private Piece _blackRookPrefab;
    [SerializeField] private Piece _blackQueenPrefab;
    [SerializeField] private Piece _blackKingPrefab;


    private Square[] squares;
    Piece prefab;
    
    public BoardManager(){
        squares = new Square[64];
        
    }
    // Start is called before the first frame update
    void Start(){
        GenerateSquares();
    }

    // Update is called once per frame
    void Update(){
        
    }

    void GenerateSquares(){
        for (int x=0; x < _width; x++){
            for (int y=0; y < _height; y++){
                var generatedSquare = Instantiate(_squarePrefab, new Vector3(x,y), Quaternion.identity);
                generatedSquare.name = $"Square:({x},{y})";
                var isDarkSquare = (x + y) % 2 != 0;
                generatedSquare.Init(isDarkSquare);
                squares[8*x+y] = generatedSquare;
                
                int rndint = Random.Range(0, 12);
                PutPiece(rndint,x,y);
            }
        }
        _cam.transform.position = new Vector3((float) _width*0.43f, (float) _height*0.43f, -10);
    }
    void PutPiece(int piece, int x, int y){
        Debug.Log(piece);
        switch (piece){
            case 0: return;
            case 1: prefab = _whitePawnPrefab; break;
            case 2: prefab = _whiteKnightPrefab; break;
            case 3: prefab = _whiteBishopPrefab; break;
            case 4: prefab = _whiteRookPrefab; break;
            case 5: prefab = _whiteQueenPrefab; break;
            case 6: prefab = _whiteKingPrefab; break;
            case 7: prefab = _blackPawnPrefab; break;
            case 8: prefab = _blackKnightPrefab; break;
            case 9: prefab = _blackBishopPrefab; break;
            case 10: prefab = _blackRookPrefab; break;
            case 11: prefab = _blackQueenPrefab; break;
            case 12: prefab = _blackKingPrefab; break;
        }

        Debug.Log("prefab null?");
        if (prefab!=null){
            Debug.Log("no");
            var generatedPiece = Instantiate(prefab, new Vector3(x,y,-1), Quaternion.identity);
            generatedPiece.name = $"Piece:({x},{y})";
        }
            
    }
}   

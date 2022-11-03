using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour{
    private Move[] _gameMoves;
    private Player _currentPlayer = Player.White;
    private Player _prevPlayer = Player.Black; // Will be used to detect change in players
    
    public Player CurrentPlayer{
        get{return _currentPlayer;}
        set{_currentPlayer = value;}
    }
    // private int _halfMoveCounter = 0;
    private int _fullMoveCounter = 1; 

    static public int num = 0;

    public enum Player{
        White,
        Black
    }
    public static GameState Instance;
    void Awake(){
        Instance = this;
    }

    void Update(){
        // detect movement
        if (_prevPlayer==_currentPlayer){
            SwitchCurrentPlayer();
        }
    }

    private void SwitchCurrentPlayer(){
        if (_currentPlayer==Player.White){
            _currentPlayer = Player.Black;
            return ;
        }
        _currentPlayer = Player.White;
        _fullMoveCounter++;
        return ;
    }
    public void MovePiece(int from_x, int from_y, int to_x, int to_y, bool isCapture){
        BoardManager.Instance.Board[to_x, to_y] = BoardManager.Instance.Board[from_x, from_y];
        BoardManager.Instance.Board[from_x, from_y] = null;

        GameObject pieceObj = GameObject.Find($"Piece:({from_x},{from_y})");
        
        if (isCapture){
            Destroy(GameObject.Find($"Piece:({to_x},{to_y})"));
        }
        
        pieceObj.transform.position = new Vector3(to_x, to_y, -1);
        pieceObj.name = $"Piece:({to_x},{to_y})";
        BoardManager.Instance.Board[to_x,to_y].SetFile(to_x);
        BoardManager.Instance.Board[to_x,to_y].SetRank(to_y);
        _prevPlayer = _currentPlayer;
    }
}

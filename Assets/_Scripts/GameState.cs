using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour{
    private Move[] _gameMoves;
    private Player _currentPlayer = Player.White;
    private Player _prevPlayer; // Will be used to detect change in players
    
    // private int _halfMoveCounter = 0;
    private int _fullMoveCounter = 1; 

    static public int num = 0;

    enum Player{
        White,
        Black
    }
    public static GameState Instance;
    void Awake(){
        Debug.Log("Awaken");
        Instance = this;
    }
    private void SwitchPlayer(){
        if (_currentPlayer==Player.White){
            _currentPlayer = Player.Black;
            return ;
        }
        _currentPlayer = Player.White;
        _fullMoveCounter++;
        return ;
    }
}

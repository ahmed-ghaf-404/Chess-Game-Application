using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player{
    private bool _isCheck;
    private Move _checkMove;
    private int _color;
    private int _score = 0;
    public int Color{
        get{ return _color;}
        set{_color = value;}
    }
    public Player(int color){
        _color = color;
    }
    public void IncreaseScore(){
        _score++;
        if (_score>=3){
            GameState.Instance.SetWinner(this);
            GameState.Instance.SetGameOver(true, "3 checks!");
        }
    }
}

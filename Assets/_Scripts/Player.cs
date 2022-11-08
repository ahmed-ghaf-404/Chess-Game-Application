using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player{
    private bool _isCheck;
    private Move _checkMove;
    private int _color;
    public int Color{
        get{ return _color;}
        set{_color = value;}
    }
    public Player(int color){
        _color = color;
    }
}

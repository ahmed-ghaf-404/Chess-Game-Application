using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player{
    private bool _canShortCastles;
    private bool _canLongCastles;
    
    public void SetCanShortCastles(bool b){this._canShortCastles = b;}
    public void SetCanLongCastles(bool b){this._canLongCastles = b;}
    
    public Player(){
        // unless specified from the FEN
        // players can't castle
        this._canLongCastles = false;
        this._canShortCastles = false;
    }
}

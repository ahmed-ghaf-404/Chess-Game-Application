using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour{

    [SerializeField] Color _lightColor;
    [SerializeField] Color _darkColor;
    [SerializeField] Color _toHighlightColor;
    [SerializeField] Color _fromHighlightColor;
    [SerializeField] SpriteRenderer _renderer;
    private int x;
    private int y;
    public int X{
        get{return x;}
        set{x = value;}
    }
    public int Y{
        get{return y;}
        set{y = value;}
    }
    public void Init(bool isLightSquare){
        _renderer.color = isLightSquare?  _lightColor: _darkColor;
    }
    public void HighlightToSquare(){
        _renderer.color = _toHighlightColor;
    }
    
    public void HighlightFromSquare(){
        _renderer.color = _fromHighlightColor;
    }

    public void SetCoord(int x, int y){
        this.x = x;
        this.y = y;
    }
}

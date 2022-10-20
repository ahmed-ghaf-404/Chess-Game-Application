using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour{

    [SerializeField] Color _lightColor = Color.white;
    [SerializeField] Color _darkColor = Color.black;
    [SerializeField] SpriteRenderer _renderer;
    int _piece = 0;


    public void Init(bool isLightSquare){
        _renderer.color = isLightSquare?  _lightColor: _darkColor;
        // Debug.Log(_renderer.color);
    }
    public bool IsEmpty(){
        return _piece == 0;
    }
    public void ClearSquare(){
        _piece = 0;
    }
}

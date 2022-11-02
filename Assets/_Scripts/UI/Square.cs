using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour{

    [SerializeField] Color _lightColor = Color.white;
    [SerializeField] Color _darkColor = Color.black;
    [SerializeField] SpriteRenderer _renderer;
    public void Init(bool isLightSquare){
        _renderer.color = isLightSquare?  _lightColor: _darkColor;
    }
}

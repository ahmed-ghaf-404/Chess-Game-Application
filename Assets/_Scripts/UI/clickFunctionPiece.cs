using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickFunctionPiece : MonoBehaviour{
    void OnMouseDown(){
        Debug.Log(gameObject);

        if (BoardUIManager.selectedPiece == null){
            BoardUIManager.selectedSquare = null;
            BoardUIManager.selectedPiece = gameObject;
        }
        else{
            BoardUIManager.otherSelectedPiece = gameObject;
        }
    }
}

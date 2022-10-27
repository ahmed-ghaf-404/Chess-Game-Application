using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour{
    private bool new_move = false;
    void Update(){
        if(new_move){
            new_move = false; // set it to false until next move
        }
    }
}

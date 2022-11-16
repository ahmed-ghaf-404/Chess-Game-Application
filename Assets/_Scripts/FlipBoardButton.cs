using UnityEngine;

public class FlipBoardButton : MonoBehaviour{

    enum Side{
        White,
        Black
    } 
    Side _currentSide;

    void Awake(){
        // make it visible
        gameObject.SetActive(true);
        _currentSide = Side.White;
    }

    void OnMouseDown(){
        BoardManager.Instance.FlipBoard();
        if (_currentSide == Side.White){
            _currentSide = Side.Black;
            GameObject.Find("Flip_Board_Icon").transform.position = new Vector3(8.0f,3.5f,0.0f);
        }
        else{
            _currentSide = Side.White;
            GameObject.Find("Flip_Board_Icon").transform.position = new Vector3(-1.0f,3.5f,0.0f);
        }
    }
}

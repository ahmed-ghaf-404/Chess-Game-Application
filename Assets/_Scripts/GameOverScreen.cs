using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour{
    public TMP_Text _winner;
    public void Setup(int winner, string reason){
        gameObject.SetActive(true);
        _winner.text = winner==0?    $"White wins: {reason}" :
                                    $"Black wins: {reason}";
        BoardManager.Instance.ClearAllLegalMoves();
        
    }

    void Update(){
        if (Input.GetKey("space")){
            SceneManager.LoadScene(0);
        }
    }
}

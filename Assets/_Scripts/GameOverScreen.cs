using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;

public class GameOverScreen : MonoBehaviour{
    public TMP_Text _winner;
    public void Setup(int winner, string reason){
        SoundManager.PlaySound("notification");
        gameObject.SetActive(true);
        _winner.text = winner==0?    $"White wins: {reason}" :
                                    $"Black wins: {reason}";
        BoardManager.Instance.ClearAllLegalMoves();
        
    }

    void Update(){
        if (Input.GetKeyUp("space")){
            SceneManager.LoadScene("Tutorial");
        }
    }
}

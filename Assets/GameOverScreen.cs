using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour{
    public Text Winner;
    public void Setup(int winner, string reason){
        gameObject.SetActive(true);
        Winner.text = winner==0? $"White wins: {reason}" : $"Black wins: {reason}";
    }

    public void RestartButton(){
        SceneManager.LoadScene("WhiteGame");
    }

    public void ExitButton(){
        Application.Quit();
    }
    
}

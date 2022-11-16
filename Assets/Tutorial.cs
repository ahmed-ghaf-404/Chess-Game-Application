using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour{
    void Awake(){
        GameObject tutorial = gameObject.transform.GetChild(0).gameObject;;
        tutorial.SetActive(true);
    }
    void Update(){
        if (Input.GetKey("space")){
            GameObject tutorial = GameObject.Find("Tutorial");
            tutorial.SetActive(false);
            SceneManager.LoadScene("WhiteGame");
        }
    }
}

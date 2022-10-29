using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    void Start(){
        DontDestroyOnLoad(gameObject);
    }

    public void goToNextLevel () {
        if(SceneManager.GetActiveScene().buildIndex == 3){
            Debug.Log("Win!!!");
        }
        else{SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);}
    }
}

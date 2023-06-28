using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public ObjectManager objManager;
    public void lose(){
        SceneManager.LoadScene("Retry");
    }

    public void win(){
        SceneManager.LoadScene("Victory");
    }

    public void startLevel(string lvlName){
        SceneManager.LoadScene("Day " + objManager.getDay());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public ObjectManager objManager;
    public void lose(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("Retry");
    }

    public void win(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("Victory");
    }

    public void startLevel(string lvlName){
        UnityEngine.SceneManagement.SceneManager.LoadScene("Day " + objManager.getDay());
    }
}

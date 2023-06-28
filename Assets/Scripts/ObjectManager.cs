using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectManager : MonoBehaviour
{
    public int numItems;
    public int numObjectives;
    public int collected;
    public int highestLayer;
    public int targetID;
    public GameObject[] layer1;
    public GameObject[] layer2;
    public GameObject[] layer3;
    public GameObject[] layer4;
    public GameObject[] layer5;
    public GameObject[][] allLayers = new GameObject[5][];
    public static int day;
    public GameManager gameManager;
    public GameObject highestText;
    public GameObject lowestText;
    public GameObject bookText;
    public GameObject paperText;
    public GameObject pencilText;
    public TMP_Text collectionText;


    void Start(){
        collected = 0;
        allLayers[0] = layer1;
        allLayers[1] = layer2;
        allLayers[2] = layer3;
        allLayers[3] = layer4;
        allLayers[4] = layer5;
        fillRandom(numItems);
        generateObjective();
    }

    //return the day#
    public int getDay(){
        return day;
    }

    //fills the scene with a random amount of items
    public void fillRandom(int amount){
        for (int i = 0; i < amount;){
            int randID = Random.Range(1, 4);
            int randLayer = Random.Range(0, highestLayer);
            int randPos = Random.Range(0, allLayers[randLayer].Length);

            if(allLayers[randLayer][randPos].GetComponent<ObjectSpanwer>().hasItem == false){
                allLayers[randLayer][randPos].GetComponent<ObjectSpanwer>().summonItem(randID);
                i++;
            }
        }
    }

    //finds the highest layer with an object of a certain id
    public GameObject[] findLowest(int id){
        foreach (GameObject obj in layer5){
            if(obj.GetComponent<ObjectSpanwer>().itemID == id){
                return layer5;
            }
        }
        foreach (GameObject obj in layer4){
            if(obj.GetComponent<ObjectSpanwer>().itemID == id){
                return layer4;
            }
        }
        foreach (GameObject obj in layer3){
            if(obj.GetComponent<ObjectSpanwer>().itemID == id){
                return layer3;
            }
        }
        foreach (GameObject obj in layer2){
            if(obj.GetComponent<ObjectSpanwer>().itemID == id){
                return layer2;
            }
        }
        foreach (GameObject obj in layer1){
            if(obj.GetComponent<ObjectSpanwer>().itemID == id){
                return layer1;
            }
        }
        return layer1;
    }

    //finds the lowest layer containing an object of a certain id
    public GameObject[] findHighest(int id){
        foreach (GameObject obj in layer1){
            if(obj.GetComponent<ObjectSpanwer>().itemID == id){
                return layer1;
            }
        }
        foreach (GameObject obj in layer2){
            if(obj.GetComponent<ObjectSpanwer>().itemID == id){
                return layer2;
            }
        }
        foreach (GameObject obj in layer3){
            if(obj.GetComponent<ObjectSpanwer>().itemID == id){
                return layer3;
            }
        }
        foreach (GameObject obj in layer4){
            if(obj.GetComponent<ObjectSpanwer>().itemID == id){
                return layer4;
            }
        }
        foreach (GameObject obj in layer5){
            if(obj.GetComponent<ObjectSpanwer>().itemID == id){
                return layer5;
            }
        }
        return layer5;
    }

    //assigns all items of a certain ID on a layer as the target
    public void assignTarget(int id, GameObject[] layer){
        foreach (GameObject obj in layer){
            if(obj.GetComponent<ObjectSpanwer>().itemID == id){
                obj.GetComponent<ObjectSpanwer>().target = true;
            }
        }
    }

    //
    public void correctItem(){
        collected++; 
        generateObjective();
    }

    //
    public void wrongItem(){

    }

    //makes all items in scene not be the target
    public void unassignTargets(){
        foreach (GameObject obj in layer1){
            obj.GetComponent<ObjectSpanwer>().target = false;
        }
        foreach (GameObject obj in layer2){
            obj.GetComponent<ObjectSpanwer>().target = false;
        }
        foreach (GameObject obj in layer3){
            obj.GetComponent<ObjectSpanwer>().target = false;
        }
        foreach (GameObject obj in layer4){
            obj.GetComponent<ObjectSpanwer>().target = false;
        }
        foreach (GameObject obj in layer5){
            obj.GetComponent<ObjectSpanwer>().target = false;
        }
    }

    //generates a new objective for the player or checks for win
    public void generateObjective(){
        if(collected >= numObjectives){
            day += 1; 
            gameManager.win();
        }else{
            unassignTargets();
            int randID = Random.Range(1, 4);
            if(hasId(randID)){
                if(Random.Range(0, 2) == 1){
                    assignTarget(randID, findHighest(randID));
                    setDisplay(randID, true);
                }else{
                    assignTarget(randID, findLowest(randID));
                    setDisplay(randID, false);
                }
            }else{
                generateObjective();
            }
        }
    }

    //
    public void setDisplay(int id, bool highest){
        highestText.SetActive(highest);
        lowestText.SetActive(!highest);
        bookText.SetActive(false);
        paperText.SetActive(false);
        pencilText.SetActive(false);
        if(id == 1){
            bookText.SetActive(true);
        }else if(id == 2){
            pencilText.SetActive(true);
        }else{
            paperText.SetActive(true);
        }
        collectionText.text = collected + "/" + numObjectives;
    }
    //checks for a certain itemID in scene
    public bool hasId(int id){
        foreach (GameObject obj in layer5){
            if(obj.GetComponent<ObjectSpanwer>().itemID == id){
                return true;
            }
        }
        foreach (GameObject obj in layer4){
            if(obj.GetComponent<ObjectSpanwer>().itemID == id){
                return true;
            }
        }
        foreach (GameObject obj in layer3){
            if(obj.GetComponent<ObjectSpanwer>().itemID == id){
                return true;
            }
        }
        foreach (GameObject obj in layer2){
            if(obj.GetComponent<ObjectSpanwer>().itemID == id){
                return true;
            }
        }
        foreach (GameObject obj in layer1){
            if(obj.GetComponent<ObjectSpanwer>().itemID == id){
                return true;
            }
        }
        return false;
    }
}

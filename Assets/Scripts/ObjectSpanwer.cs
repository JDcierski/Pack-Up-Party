using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpanwer : MonoBehaviour
{
    public GameObject book;
    public GameObject pencil;
    public GameObject paper;
    public GameObject lunch;
    public bool target;
    public bool hasItem;
    public int itemID;
    public int goal;

    void Update(){
        if(Input.GetKeyDown("up")){
            summonItem(1);
        }
    }
    public void summonItem(int id){
        hasItem = true;
        itemID = id;
        target = false;
        if(id == 1){
            Instantiate(book, this.transform);
        }else if(id == 2){
            Instantiate(pencil, this.transform);
        }else if(id == 3){
            Instantiate(paper, this.transform);
        }else{
            Instantiate(lunch, this.transform);
        }
    }
    public void selectItem(){
        if(hasItem){
            transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
    public void reset(){
        if(hasItem){
            Destroy(transform.GetChild(0).gameObject);
        }
        itemID = -1;
        hasItem = false;
        target = false;
    }
    public void deselectItem(){
        if(hasItem){
            transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
    

}

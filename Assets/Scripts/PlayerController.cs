using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool grounded = true;
    public bool moveable = false;
    public bool jumping = false;
    public Rigidbody2D rb2D;
    public float speed;
    public float jumpSpeed;
    private Collider2D col;
    public GameObject selectedItem;
    public int numItems;
    
    [SerializeField] private LayerMask ground;
    
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
            if(Input.GetKey("right")){
                rb2D.velocity = new Vector2(speed, rb2D.velocity.y);
            }
            else if(Input.GetKey("left") && !Input.GetKey("right")){
                rb2D.velocity = new Vector2(-1 * speed, rb2D.velocity.y);
            }else{
                rb2D.velocity = new Vector2(0f, rb2D.velocity.y);
            }
            
            grounded = Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, .1f, ground);
            if(jumping){
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
                grounded = false;
            }
            if(Input.GetKeyDown("space") && selectedItem != null){
                if(selectedItem.GetComponent<ObjectSpanwer>().target){
                    Debug.Log("Correct Item");
                }else{
                    Debug.Log("Wrong Item");
                }
                selectedItem.GetComponent<ObjectSpanwer>().reset();
            }
            

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag.Equals("jump")){
            jumping = true;
        }
        if(col.gameObject.tag.Equals("landing")){
            jumping = false;
            rb2D.velocity = new Vector2(rb2D.velocity.x, -7f);
        }
        if(col.gameObject.tag.Equals("item")){
            col.gameObject.GetComponent<ObjectSpanwer>().selectItem();
            selectedItem = col.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag.Equals("item")){
            col.gameObject.GetComponent<ObjectSpanwer>().deselectItem();
            selectedItem = null;
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag.Equals("ceiling")){
            jumping = false;
            rb2D.velocity = new Vector2(rb2D.velocity.x, -7f);
        }
    }
    
}

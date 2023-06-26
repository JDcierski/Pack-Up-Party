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
    
    // Start is called before the first frame update
    void Start()
    {
        
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
            
            if(jumping){
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
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

        
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag.Equals("ceiling")){
            jumping = false;
            rb2D.velocity = new Vector2(rb2D.velocity.x, -7f);
        }
    }
    
}

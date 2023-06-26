using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool grounded = true;
    public bool moveable = false;
    public bool jumping = false;
    public Rigidbody2D rb2D;
    public float speed;
    public float jumpSpeed;
    public Transform player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frameGameObject.FindGameObjectWithTag("player")
    void Update()
    {
            if(jumping && false){

            }else if(player.transform.position.x > this.transform.position.x){
                rb2D.velocity = new Vector2(speed, rb2D.velocity.y);
            }
            else{
                rb2D.velocity = new Vector2(-1 * speed, rb2D.velocity.y);
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

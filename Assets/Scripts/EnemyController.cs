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
    private Collider2D col;
    public float moveTime;
    public float nextTime;
    public bool moveRight;
    public SpriteRenderer spriteRenderer;
    

    [SerializeField] private LayerMask ground;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frameGameObject.FindGameObjectWithTag("player")
    void Update()
    {
            if(Mathf.Abs(player.transform.position.x - transform.position.x) <= .05){
                nextTime = Time.time + moveTime;
                moveRight = Random.Range(0, 2) == 1;
                
            }
            if((jumping || !grounded) && player.transform.position.y > transform.position.y + 1.75f){
                rb2D.velocity = new Vector2(0, rb2D.velocity.y);
            }else if(nextTime > Time.time){
                if(moveRight){
                    spriteRenderer.flipX = false;
                    rb2D.velocity = new Vector2(speed, rb2D.velocity.y);
                }else{
                    spriteRenderer.flipX = true;
                    rb2D.velocity = new Vector2(-1 * speed, rb2D.velocity.y);
                }
            }else if(player.transform.position.x > this.transform.position.x){
                spriteRenderer.flipX = false;
                rb2D.velocity = new Vector2(speed, rb2D.velocity.y);
            }
            else{
                spriteRenderer.flipX = true;
                rb2D.velocity = new Vector2(-1 * speed, rb2D.velocity.y);
            }
            
            if(jumping){
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
            }

            grounded = Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, .1f, ground);
            if(jumping){
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
                grounded = false;
                nextTime = -1;
            }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag.Equals("jump")){
            jumping = true;
            col.gameObject.GetComponent<Animator>().SetBool("Squished", true);
        }
        if(col.gameObject.tag.Equals("landing")){
            jumping = false;
            rb2D.velocity = new Vector2(rb2D.velocity.x, -7f);
        }
        if(col.gameObject.tag.Equals("player")){
            if(player.GetComponent<PlayerController>().grounded && grounded){
                player.GetComponent<PlayerController>().hit();
            }
        }
        
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag.Equals("jump")){
            col.gameObject.GetComponent<Animator>().SetBool("Squished", false);
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

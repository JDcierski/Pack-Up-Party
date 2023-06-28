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
    public Collider2D col;
    public GameObject selectedItem;
    public ObjectManager objManager;
    public GameManager gameManager;
    public int hp;
    public float invTime;
    public float nextTime;
    public GameObject confetti;
    public GameObject failfetti;
    public GameObject x1;
    public GameObject x2;
    public GameObject x3;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public GameObject fallingBook;
    public GameObject fallingPencil;
    public GameObject fallingPaper;
    
    
    [SerializeField] private LayerMask ground;
    
    // Start is called before the first frame update
    void Start()
    {
        
        objManager.fillRandom(objManager.numItems);
        objManager.generateObjective();
        updateHp();
        invTime = -1;
        hp = 3;
        col = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
            if(Input.GetKey("right")){
                spriteRenderer.flipX = false;
                animator.SetBool("isRunning", true);
                rb2D.velocity = new Vector2(speed, rb2D.velocity.y);
            }
            else if(Input.GetKey("left") && !Input.GetKey("right")){
                spriteRenderer.flipX = true;
                animator.SetBool("isRunning", true);
                rb2D.velocity = new Vector2(-1 * speed, rb2D.velocity.y);
            }else{
                animator.SetBool("isRunning", false);
                rb2D.velocity = new Vector2(0f, rb2D.velocity.y);
            }
            
            grounded = Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, .5f, ground);
            if(jumping){
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
                grounded = false;
            }
            if(Input.GetKeyDown("space") && selectedItem != null){
                if(selectedItem.GetComponent<ObjectSpanwer>().hasItem){
                    if(selectedItem.GetComponent<ObjectSpanwer>().target){
                        if(selectedItem.GetComponent<ObjectSpanwer>().itemID == 1){
                            Instantiate(fallingBook);
                        }
                        if(selectedItem.GetComponent<ObjectSpanwer>().itemID == 2){
                            Instantiate(fallingPencil);
                        }
                        if(selectedItem.GetComponent<ObjectSpanwer>().itemID == 3){
                            Instantiate(fallingPaper);
                        }
                        selectedItem.GetComponent<ObjectSpanwer>().reset();
                        objManager.correctItem();
                        Instantiate(confetti, transform.position, Quaternion.Euler(-90, 0, 0));

                    }else{
                        hp--;
                        selectedItem.GetComponent<ObjectSpanwer>().reset();
                        objManager.wrongItem();
                        Instantiate(failfetti, transform.position, Quaternion.Euler(-90, 0, 0));
                        updateHp();
                    }
                }
            }
            if(hp == 0){
                gameManager.lose();           
            }
            

    }

    //
    public void hit(){
        if(invTime < Time.time){
            updateHp();
            Instantiate(failfetti, transform.position, Quaternion.Euler(-90, 0, 0));
            hp--;
            nextTime = Time.time + invTime;
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

    //
    public void updateHp(){
        if(hp == 3){
            x1.SetActive(false);
            x2.SetActive(false);
            x3.SetActive(false);
        }else if(hp == 2){
            x1.SetActive(true);
            x2.SetActive(false);
            x3.SetActive(false);
        }else if(hp == 1){
            x1.SetActive(true);
            x2.SetActive(true);
            x3.SetActive(false);
        }else{
            x1.SetActive(true);
            x2.SetActive(true);
            x3.SetActive(true);
        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript instance;

    [SerializeField]
    private float speed = 10f;

    [SerializeField]
    private float maxVelocity = 5f;

    [SerializeField]
    private Rigidbody2D myRigidbody;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject[] arrows;

    private float height;

    private bool canWalk;

    [SerializeField]
    private AnimationClip clip;

    [SerializeField]
    private AudioClip shootClip;

    private bool shootOnce, shootTwice;

    private bool moveLeft, moveRight;
    private bool facingLeft, facingRight;

    private Button shootBtn;

    [SerializeField]
    private GameObject shield;

    private string arrow;

    public bool hasShield, isInvincible, singleArrow, doubleArrows, singleStickyArrow, doubleStickyArrows, shootFirstArrow, shootSecondArrow;


    public delegate void Explode(bool touchedGoldBall);

    public static event Explode explode;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }

        myRigidbody = GetComponent<Rigidbody2D>();

        float cameraHeight = Camera.main.orthographicSize;
        height = -cameraHeight - 0.8f;
       
        

        shootBtn = GameObject.FindGameObjectWithTag("ShootButton").GetComponent<Button>();
        shootBtn.onClick.AddListener(() => ShootTheArrow());
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializePlayer();
    }

    //used when using "transform" on player
    void Update()
    {
        //ShootTheArrow();
       
    }

    //used when using physics on player
    void FixedUpdate()
    {
        MoveThePlayer();
        //PlayerWalkKeyboard();

    }

    void InitializePlayer()
    {
        canWalk = true;

        switch (GameController.instance.selectedWeapon)
        {
            case 0:
                arrow = "Arrow";
                shootOnce = true;
                shootTwice = false;

                singleArrow = true;
                doubleArrows = false;
                singleStickyArrow = false;
                doubleStickyArrows = false;
                break;
            case 1:
                arrow = "Arrow";
                shootOnce = true;
                shootTwice = true;

                singleArrow = false;
                doubleArrows = true;
                singleStickyArrow = false;
                doubleStickyArrows = false;
                break;
            case 2:
                arrow = "StickyArrow";
                shootOnce = true;
                shootTwice = false;

                singleArrow = false;
                doubleArrows = false;
                singleStickyArrow = true;
                doubleStickyArrows = false;
                break;
            case 3:
                arrow = "StickyArrow";
                shootOnce = true;
                shootTwice = true;

                singleArrow = false;
                doubleArrows = false;
                singleStickyArrow = true;
                doubleStickyArrows = true;
                break;
        }

        Vector3 bottomBrick = GameObject.FindGameObjectWithTag("BottomBrick").transform.position;
        Vector3 temp = transform.position;

        switch (gameObject.name)
        {
            case "Homosapien(Clone)":
                temp.y = bottomBrick.y + 1f;
                break;
            case "Joker(Clone)":
                temp.y = bottomBrick.y + 1f;
                break;
            case "Spartan(Clone)":
                temp.y = bottomBrick.y + 1f;
                break;
            case "Pirate(Clone)":
                temp.y = bottomBrick.y + 1f;
                break;
            case "Player(Clone)":
                temp.y = bottomBrick.y + 1f;
                break;
            case "Zombie(Clone)":
                temp.y = bottomBrick.y + 1f;
                break;
        }

        transform.position = temp;

    
    }

    public void PlayerShootOnce(bool shootOnce)
    {
        this.shootOnce = shootOnce;
        shootFirstArrow = false;
    }

    public void PlayerShootTwice(bool shootTwice)
    {
        if(doubleArrows || doubleStickyArrows)
        {
            this.shootTwice = shootTwice;
        }
        
        shootSecondArrow = false;
    }

    //needs to be public to attach function to ui button
    public void ShootTheArrow()
    {
        if (GameplayController.instance.levelInProgress) //if pressed left mouse button 
        {
            if (shootOnce)
            {
                if(arrow == "Arrow")
                {
                    GameObject arrow1 = Instantiate(arrows[0], new Vector3(transform.position.x + 1.2f, height, 0), Quaternion.identity) as GameObject;

                }
                else if(arrow == "StickyArrow")
                {
                    GameObject arrow1 = Instantiate(arrows[2], new Vector3(transform.position.x + 0f, height, 0), Quaternion.identity) as GameObject;
                }                
                StartCoroutine(PlayTheShootAnimation());
                shootOnce = false;
                shootFirstArrow = true;
            }
            else if (shootTwice)
            {
                if (arrow == "Arrow")
                {
                    GameObject arrow2 = Instantiate(arrows[1], new Vector3(transform.position.x + 1.2f, height, 0), Quaternion.identity) as GameObject;

                }
                else if (arrow == "StickyArrow")
                {
                    GameObject arrow2 = Instantiate(arrows[3], new Vector3(transform.position.x +0f, height, 0), Quaternion.identity) as GameObject;
                }
                StartCoroutine(PlayTheShootAnimation());
                shootTwice = false;
                shootSecondArrow = true;
            }


            
        }
    }

    IEnumerator PlayTheShootAnimation()
    {
        //canWalk = false;

        shootBtn.interactable = false;

            animator.Play("Player Shoot Animation");
            AudioSource.PlayClipAtPoint(shootClip, transform.position);
            yield return new WaitForSeconds(clip.length);
            animator.SetBool("Shoot", false);

        shootBtn.interactable = true;

        //canWalk = true;
    }

    public void DestroyShield()
    {
        StartCoroutine(SetPlayerInvincible());
        hasShield = false;
        shield.SetActive(false);
    }

    IEnumerator SetPlayerInvincible()
    {
        isInvincible = true;
        yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(5f));
        isInvincible = false;
    }

    public void StopMoving()
    {
        moveLeft = moveRight = false;
        animator.SetBool("Walk", false);
    }

    public void MoveThePlayerLeft()
    {
        moveLeft = true;
        moveRight = false;
    }

    public void MoveThePlayerRight()
    {
        moveRight = true;
        moveLeft = false;
    }

    void MoveThePlayer()
    {
        if (GameplayController.instance.levelInProgress)
        {
            if (moveLeft)
            {
                MoveLeft();
            }
            if (moveRight)
            {
                MoveRight();
            }
        }
    }

    void MoveRight()
    {
        float force = 0.0f;
        float velocity = Mathf.Abs(myRigidbody.velocity.x);

        float h = Input.GetAxis("Horizontal");

        if (canWalk)
        {
            
            if (velocity < maxVelocity)
            {
                force = speed;
            }

            if (facingLeft)
            {
                transform.position = new Vector3(transform.position.x + 0.7f, transform.position.y, 0);
            }
            Vector3 scale = transform.localScale;
            scale.x = 1.0f;
            transform.localScale = scale;

            animator.SetBool("Walk", true);
            facingRight = true;
            facingLeft = false;

            animator.SetBool("Walk", true);

            
        }
        myRigidbody.AddForce(new Vector2(force, 0));
    }

    public void MoveLeft()
    {
        float force = 0.0f;
        float velocity = Mathf.Abs(myRigidbody.velocity.x);

        float h = Input.GetAxis("Horizontal");

        if (canWalk)
        {
           
            if (velocity < maxVelocity)
            {
                force = -speed;
            }

            if (facingRight)
            {
                transform.position = new Vector3(transform.position.x - 0.7f, transform.position.y, 0);
            }
            Vector3 scale = transform.localScale;
            scale.x = -1.0f;
            transform.localScale = scale;

            animator.SetBool("Walk", true);
            facingLeft = true;
            facingRight = false;

            animator.SetBool("Walk", true);

            
        }
        myRigidbody.AddForce(new Vector2(force, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag){
            case "SingleArrow":
                if (!singleArrow)
                {
                    arrow = "Arrow";
                    if (!shootFirstArrow)
                    {
                        shootOnce = true;
                    }
                    shootTwice = false;
                    singleArrow = true;
                    doubleArrows = false;
                    singleStickyArrow = false;
                    doubleStickyArrows = false;
                }
                break;
            case "DoubleArrow":
                if (!doubleArrows)
                {
                    arrow = "Arrow";
                    if (!shootFirstArrow)
                    {
                        shootOnce = true;
                    }
                    if (!shootSecondArrow)
                    {
                        shootTwice = true;
                    }
                    singleArrow = false;
                    doubleArrows = true;
                    singleStickyArrow = false;
                    doubleStickyArrows = false;
                }
                break;
            case "SingleStickyArrow":
                if (!singleStickyArrow)
                {
                    arrow = "StickyArrow";
                    if (!shootFirstArrow)
                    {
                        shootOnce = true;
                    }
                    shootTwice = false;
                    singleArrow = false;
                    doubleArrows = false;
                    singleStickyArrow = true;
                    doubleStickyArrows = false;
                }
                break;
            case "DoubleStickyArrow":
                if (!doubleStickyArrows)
                {
                    arrow = "StickyArrow";
                    if (!shootFirstArrow)
                    {
                        shootOnce = true;
                    }
                    if (!shootSecondArrow)
                    {
                        shootTwice = true;
                    }
                   
                    singleArrow = false;
                    doubleArrows = false;
                    singleStickyArrow = false;
                    doubleStickyArrows = true;
                }
                break;
            case "Watch":
                GameplayController.instance.levelTime += Random.Range(10, 20);
                break;
            case "Shield":
                hasShield = true;
                shield.SetActive(true);
                break;
            case "Dynamite":
                if(explode != null)
                {
                    explode(false);
                }
                break;
        }
    }

    void PlayerWalkKeyboard()
    {
        float force = 0.0f;
        float velocity = Mathf.Abs(myRigidbody.velocity.x);

        float h = Input.GetAxis("Horizontal");

        if (canWalk)
        {
            if (h > 0)//d or right arrow, moving right
            {
                if (velocity < maxVelocity)
                {
                    force = speed;
                }



                if (facingLeft)
                {
                    transform.position = new Vector3(transform.position.x + 0.7f, transform.position.y, 0);
                }
                Vector3 scale = transform.localScale;
                scale.x = 1.0f;
                transform.localScale = scale;

                animator.SetBool("Walk", true);
                facingRight = true;
                facingLeft = false;

            }
            else if (h < 0) //a or left arrow, moving left
            {
                if (velocity < maxVelocity)
                {
                    force = -speed;
                }



                if (facingRight)
                {
                    transform.position = new Vector3(transform.position.x - 0.7f, transform.position.y, 0);
                }
                Vector3 scale = transform.localScale;
                scale.x = -1.0f;
                transform.localScale = scale;

                animator.SetBool("Walk", true);
                facingLeft = true;
                facingRight = false;
            }
            else
            {
                animator.SetBool("Walk", false);
                moveLeft = moveRight = false;

            }
            myRigidbody.AddForce(new Vector2(force, 0));
        }

    }
}

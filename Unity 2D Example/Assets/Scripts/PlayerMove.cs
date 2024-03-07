using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMove : MonoBehaviour
{
    public GameManager gameManager;
    public AudioClip audioJump;
    public AudioClip audioAttack;
    public AudioClip audioDamaged;
    public AudioClip audioDie;
    public AudioClip audioFinish;
    public AudioClip audioItem;
    public float MaxSpeed;
    public float jumpPower;
    CapsuleCollider2D boxCollider;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<CapsuleCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }
    void PlaySound(string action)
    {
        switch (action)
        {
            case "JUMP":
                audioSource.clip = audioJump;
                break;
            case "ATTACK":
                audioSource.clip = audioAttack;
                break;
            case "DAMAGED":
                audioSource.clip = audioDamaged;
                break;
            case "ITEM":
                audioSource.clip = audioItem;
                break;
            case "DIE":
                audioSource.clip = audioDie;
                break;
            case "FINISH":
                audioSource.clip = audioFinish;
                break;
        }
    }
    void Update()
    {
        if (Input.GetButtonDown("Jump") && !anim.GetBool("isJumping"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
            PlaySound("JUMP");
            
        }
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2((rigid.velocity.normalized.x)*0.5f, rigid.velocity.y);
        }
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }
        if (Mathf.Abs(rigid.velocity.x) < 0.3)
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {

      
        
            float h = Input.GetAxisRaw("Horizontal");

            rigid.AddForce(Vector2.right * h*2, ForceMode2D.Impulse);

            if (rigid.velocity.x > MaxSpeed)
            {
                rigid.velocity = new Vector2(MaxSpeed, rigid.velocity.y);
            }
            else if (rigid.velocity.x < (-1) * MaxSpeed)
            {
                rigid.velocity = new Vector2(MaxSpeed * (-1), rigid.velocity.y);
            }
            if (rigid.velocity.y < 0)
            {
                Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));

                RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));

                if (rayHit.collider != null)
                {
                    if (rayHit.distance < 0.5f )
                    {
                        anim.SetBool("isJumping", false);
                    }
                }
            }
        }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            if (rigid.velocity.y < 0 && transform.position.y > (collision.transform.position.y)+0.07f )
            {
                OnAttack(collision.transform);
               
            }
            else
            {
                OnDamaged(collision.transform.position);

            }
          
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            bool isBronze = collision.gameObject.name.Contains("bronze");
            bool isSilver = collision.gameObject.name.Contains("silver");
            bool isGold = collision.gameObject.name.Contains("gold");
            if (isBronze)
            {
                gameManager.stagePoint += 50;
            }
            else if (isSilver)
            {
                gameManager.stagePoint += 100;
            }
            else if (isGold)
            {
                gameManager.stagePoint += 300;
            }

            collision.gameObject.SetActive(false);
            PlaySound("ITEM");
        }
        else if(collision.gameObject.tag == "Finish")
        {
            PlaySound("FINISH");
            gameManager.NextStage();
        }
    }
    void OnAttack(Transform enemy)
    {
        gameManager.stagePoint += 100;
        rigid.AddForce(Vector2.up*5, ForceMode2D.Impulse);
        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        enemyMove.OnDamaged();
        PlaySound("ATTACK");
    }
    void OnDamaged(Vector2 targetPos)
    {
        gameObject.layer = 11;

        gameManager.HealthDown();
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 5, ForceMode2D.Impulse);
        PlaySound("DAMAGED");
        anim.SetTrigger("doDamaged");
        Invoke("OffDamaged", 1);
    }
    void OffDamaged()
    {
        gameObject.layer = 10;
       

        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
    public void OnDie()
    {
        PlaySound("DIE");
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        spriteRenderer.flipY = true;

        boxCollider.enabled = false;

        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

       
    }
    public void velocityZero()
    {
        rigid.velocity = Vector2.zero;
    }
}

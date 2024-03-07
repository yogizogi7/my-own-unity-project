using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;
    Animator anim;
    SpriteRenderer spriteRender;
    CapsuleCollider2D capsulecollider;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
        capsulecollider = GetComponent<CapsuleCollider2D>();
        Invoke("Think", 5);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.3f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
        {
            Turn();
        }

    }
    void Think()
    {
        nextMove = Random.Range(-1, 2);

        if (nextMove != 0)
        {
            spriteRender.flipX = nextMove == 1;
        }

        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);

        anim.SetInteger("WalkSpeed", nextMove);
    }

    void Turn()
    {
        nextMove = nextMove * -1;
        spriteRender.flipX = nextMove == 1;
        CancelInvoke();
        Invoke("Think", 2);
    }
    public void OnDamaged()
    {
        spriteRender.color = new Color(1, 1, 1, 0.4f);

        spriteRender.flipY = true;

        capsulecollider.enabled = false;

        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        Invoke("DeActive", 5);
    }

    void DeActive()
    {
        gameObject.SetActive(false);
    }
}
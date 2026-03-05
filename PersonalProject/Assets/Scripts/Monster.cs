using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float Speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animatorController;
    public Rigidbody2D Target;

    private bool isAlive;

    private Rigidbody2D rb;
    Collider2D coll;
    Animator anim;
    SpriteRenderer sr;
    private WaitForFixedUpdate wait;
    
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive) return;
        
        if (!isAlive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;

        Vector2 dirVec = Target.position - rb.position;
        Vector2 nextVec = dirVec.normalized * Speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextVec);
        rb.linearVelocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!GameManager.instance.isLive) return;
        
        if (!isAlive) return;
        sr.flipX = Target.position.x < rb.position.x;
    }

    void OnEnable()
    {
        Target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isAlive = true;
        coll.enabled = true;
        rb.simulated = true;
        sr.sortingOrder = 2;
        anim.SetBool("Dead", false);
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animatorController[data.spriteType];
        Speed =  data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isAlive) return;

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(Knockback());
        
        if (health > 0)
        {
            anim.SetTrigger("Hit");
        }
        else
        {
            isAlive = false;
            coll.enabled = false;
            rb.simulated = false;
            sr.sortingOrder = 1;
            anim.SetBool("Dead", true);
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        }
    }

    IEnumerator Knockback() // 넉백을 주는 로직
    {
        yield return wait;
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rb.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }

    private void Dead()
    {
        gameObject.SetActive(false);
    }

}

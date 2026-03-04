using System;
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
    Animator anim;
    SpriteRenderer sr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (!isAlive) return;

        Vector2 dirVec = Target.position - rb.position;
        Vector2 nextVec = dirVec.normalized * Speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextVec);
        rb.linearVelocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!isAlive) return;
        sr.flipX = Target.position.x < rb.position.x;
    }

    void OnEnable()
    {
        Target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isAlive = true;
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animatorController[data.spriteType];
        Speed =  data.speed;
        maxHealth = data.health;
        health = data.health;
    }
}

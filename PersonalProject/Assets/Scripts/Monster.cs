using System;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float Speed;
    public Rigidbody2D Target;

    private bool isAlive = true;

    private Rigidbody2D rb;
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
    }
}

using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 inputVec;
    private Rigidbody2D rb;
    public float _speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        inputVec.x = Input.GetAxis("Horizontal");
        inputVec.y = Input.GetAxis("Vertical");
        
    }

    public void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * _speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextVec);
    }
}

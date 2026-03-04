using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _anim;
    
    public Vector2 inputVec;
    public float Speed;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * Speed * Time.fixedDeltaTime;
        _rb.MovePosition(_rb.position + inputVec * Time.fixedDeltaTime);
        
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
}

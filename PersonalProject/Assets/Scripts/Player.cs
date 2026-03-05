using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _anim;
    private SpriteRenderer _sr;

    public Hand[] hands;
    public Scanner scanner;
    public Vector2 inputVec;
    [SerializeField] public float Speed;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);
    }

    void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * Speed * Time.fixedDeltaTime;
        _rb.MovePosition(_rb.position + nextVec);
    }

    void LateUpdate()
    {
        _anim.SetFloat("Speed", inputVec.magnitude);
        
        if (inputVec.x != 0)
        {
            _sr.flipX = inputVec.x < 0;
        }
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
}

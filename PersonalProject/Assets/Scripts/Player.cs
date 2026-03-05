using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _anim;
    private SpriteRenderer _sr;

    public RuntimeAnimatorController[] animCon;
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

    private void OnEnable()
    {
        Speed *= Character.Speed;
        _anim.runtimeAnimatorController = animCon[GameManager.instance.playerID];
    }

    void FixedUpdate()
    {
        if (!GameManager.instance.isLive) return;
        
        Vector2 nextVec = inputVec.normalized * Speed * Time.fixedDeltaTime;
        _rb.MovePosition(_rb.position + nextVec);
    }

    void LateUpdate()
    {
        if (!GameManager.instance.isLive) return;
        
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.isLive) return;

        GameManager.instance.health -= Time.deltaTime * 10;

        if (GameManager.instance.health <= 0)
        {
            for (int i = 2; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            
            _anim.SetTrigger("Dead");
            GameManager.instance.GameOver();
        }
    }

}

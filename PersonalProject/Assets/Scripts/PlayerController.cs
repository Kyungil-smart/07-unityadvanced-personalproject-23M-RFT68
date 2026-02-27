using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

using UnityEngine;

// 입력 처리 / 물리 판정 / 애니메이션 결정
public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Animator _animator;

    [SerializeField] StateMachine _stateMachine;

    // Player가 사용할 State들
    public IdleState Idle { get; private set; }
    public MoveState Move { get; private set; }
    public DeadState Dead { get; private set; }
    
    MainActions _inputActions;
    
    public float MoveInput { get; private set; }
    public bool IsAlive { get; private set; }

    [SerializeField] private float _moveSpeed = 5f;
    
    
    public void Awake()
    {
        _stateMachine = new StateMachine();
        Idle = new IdleState(this);
        Move = new MoveState(this);
        Dead = new DeadState(this);
        
        _inputActions = new MainActions();
        
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }


    public void OnEnable()
    {
        _inputActions.Enable();

        _inputActions.PlayerActions.Move.performed += OnMove;
        _inputActions.PlayerActions.Move.canceled += OnMove;
    }

    public void OnDisable()
    {
        _inputActions.PlayerActions.Move.performed -= OnMove;
        _inputActions.PlayerActions.Move.canceled -= OnMove;

        _inputActions.Disable();
    }


    void Start()
    {
        _stateMachine.ChangeState(Idle);
    }

    void FixedUpdate()
    {
        Movement();
    }

    void Update()
    {
        _stateMachine.Update();
    }

    public void ChangeState(IState state)
    {
        _stateMachine.ChangeState(state);
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 value = ctx.ReadValue<Vector2>();
        MoveInput = value.x;
        MoveInput = value.y;
        
    }

    public void OnDead()
    {
    }

    void Movement()
    {
        _rb.linearVelocity = new Vector2(MoveInput * _moveSpeed,  _rb.linearVelocity.y);
    }

    public void SetSpeed(float value)
    {
        _animator.SetFloat("Speed", Mathf.Abs(value));
    }
    
    public void SetDead(bool value)
    {
        IsAlive = false;
        _animator.SetBool("IsDead", value);
    }

}

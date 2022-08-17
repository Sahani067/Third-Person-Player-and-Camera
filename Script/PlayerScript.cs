using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public CharacterController _controller;
    public Animator _animator;
    Vector3 _PlayerVelocity;

    [Header("Player Inputs")]
    [SerializeField] float _InputX, _InputZ;
    [SerializeField] bool _JumpButton;

    [Header("Player Settings")]
    [SerializeField] float _PlayerSpeed = 2f;
    [SerializeField] float _JumpForce = 2f;
    [SerializeField] float _Gravity = -10f;
    [SerializeField] bool _Grounded;

    [SerializeField] Transform _Pivot;

    private void Start()
    {
        
    }

    private void Update()
    {
        Movements();
        Jumping();
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(0f, _Pivot.eulerAngles.y, 0f);
    }

    private void Movements()
    {
        _InputX = Input.GetAxis("Horizontal");
        _InputZ = Input.GetAxis("Vertical");

        _animator.SetFloat("SpeedX", _InputX, 0.07f, Time.deltaTime);
        _animator.SetFloat("SpeedZ", _InputZ, 0.07f, Time.deltaTime);

        Vector3 moveDir = transform.forward * _InputZ + transform.right * _InputX;
        _controller.Move(moveDir * _PlayerSpeed * Time.deltaTime);
    }

    private void Jumping()
    {
        // Input
        _JumpButton = Input.GetKeyDown(KeyCode.Space);

        // Ground Check
        if (Physics.Raycast(transform.position + new Vector3(0f, 0.2f, 0f), Vector3.down, 0.35f))
        {
            _animator.SetBool("fall", false);
            _Grounded = true;
        }
        else
        {
            _animator.SetBool("fall", true);
            _Grounded = false;
        }

        // Jump
        if (_Grounded && _PlayerVelocity.y < 0)
        {
            _PlayerVelocity.y = -2f;
        }

        if (_JumpButton && _Grounded)
        {
            _animator.Play("jump up");
            _PlayerVelocity.y += Mathf.Sqrt(_JumpForce * -3f * _Gravity);
        }

        _PlayerVelocity.y += _Gravity * Time.deltaTime;
        _controller.Move(_PlayerVelocity * Time.deltaTime);
    }

}

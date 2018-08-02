﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    private Animator _animator;
    public float xRunWithWeapon = 3.18f;
    public float yRunWithWeaon = 3.04f;
    public float xWalkWithWeapon = 2.0f;
    public float yWalkWithWeapon = 2.0f;
    //public float xRunWithoutWeapon = 1;
    //public float yRunWithoutWeapon = 1;
    //public float xWalkWithoutWeapon = 4;
    //public float yWalkWithoutWeapon = 4;

    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveControl();
    }

    private void MoveControl()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftShift))
        {
            _animator.SetFloat("SpeedX", h * xRunWithWeapon);// A D
            _animator.SetFloat("SpeedZ", v * yRunWithWeaon);// W S
        }
        else
        {
            _animator.SetFloat("SpeedX", h * xWalkWithWeapon);// A D
            _animator.SetFloat("SpeedZ", v * yWalkWithWeapon);// W S
        }

        #region 必须长按才能跳跃
        // 前跳
        //if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.W))
        //{
        //    _animator.SetBool("JumpForward", true);
        //}
        //else
        //{
        //    _animator.SetBool("JumpForward", false);
        //}

        //if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.S))
        //{
        //    _animator.SetBool("JumpBackward", true);
        //}
        //else
        //{
        //    _animator.SetBool("JumpBackward", false);
        //} 
        #endregion

        // 前跳
        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.W))
        {
            _animator.SetTrigger("JumpForward");
        }
        else// 恢复时间过长手动设置trigger为false
        {
            _animator.ResetTrigger("JumpForward");
        }
        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.S))
        {
            _animator.SetTrigger("JumpBackward");
        }
        else
        {
            _animator.ResetTrigger("JumpBackward");
        }
    }
}

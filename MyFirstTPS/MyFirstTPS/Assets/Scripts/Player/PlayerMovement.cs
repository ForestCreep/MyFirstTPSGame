using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator _animator;
    public float kSpeedX = 3.18f;
    public float kSpeedZ = 3.04f;

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

        _animator.SetFloat("SpeedX", h * kSpeedX);// A D
        _animator.SetFloat("SpeedZ", v * kSpeedZ);// W S

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaleRoleMoveController : MonoBehaviour
{
    private Animator _animator;

    // 有枪时的移动参数
    public float xRunWithWeapon = 3.18f;
    public float yRunWithWeaon = 3.04f;
    public float xWalkWithWeapon = 2.0f;
    public float yWalkWithWeapon = 2.0f;
    // 无枪时的移动参数
    public float xRunWithoutWeapon = 4.5f;
    public float yRunWithoutWeapon = 4.5f;
    public float xWalkWithoutWeapon = 2;
    public float yWalkWithoutWeapon = 2;

    // 键盘 wasd 的偏移量
    private float h;
    private float v;

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
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        if (_animator.GetBool("HasWeapon"))
        {
            SetMoveWithWeapon();
            SetJumpWithWeapon();
        }
        else
        {
            SetMoveWithoutWeapon();
            SetJumpWithoutWeapon();
        }
    }

    private void SetMoveWithWeapon()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftShift))
        {
            _animator.SetFloat("SpeedXWithWeapon", h * xRunWithWeapon);// A D
            _animator.SetFloat("SpeedZWithWeapon", v * yRunWithWeaon);// W S
        }
        else
        {
            _animator.SetFloat("SpeedXWithWeapon", h * xWalkWithWeapon);// A D
            _animator.SetFloat("SpeedZWithWeapon", v * yWalkWithWeapon);// W S
        }
    }

    private void SetJumpWithWeapon()
    {
        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.W))
        {
            _animator.SetTrigger("JumpForwardWithWeapon");
        }
        else
        {
            _animator.ResetTrigger("JumpForwardWithWeapon");
        }

        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.S))
        {
            _animator.SetTrigger("JumpBackwardWithWeapon");
        }
        else
        {
            _animator.ResetTrigger("JumpBackwardWithWeapon");
        }
    }

    /// <summary>
    /// 无枪时的运动参数设置
    /// </summary>
    private void SetMoveWithoutWeapon()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftShift))
        {
            _animator.SetFloat("SpeedXWithoutWeapon", h * xRunWithoutWeapon);// A D
            _animator.SetFloat("SpeedZWithoutWeapon", v * yRunWithoutWeapon);// W S
        }
        else
        {
            _animator.SetFloat("SpeedXWithoutWeapon", h * xWalkWithoutWeapon);// A D
            _animator.SetFloat("SpeedZWithoutWeapon", v * yWalkWithoutWeapon);// W S
        }
    }

    /// <summary>
    /// 无枪时的跳跃参数设置
    /// </summary>
    private void SetJumpWithoutWeapon()
    {
        if (Input.GetKey(KeyCode.Space) && h == 0 && v == 0)
        {
            _animator.SetTrigger("JumpUpWithoutWeapon");
        }
        else
        {
            _animator.ResetTrigger("JumpUpWithoutWeapon");
        }

        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.W))
        {
            _animator.SetTrigger("JumpForwardWithoutWeapon");
        }
        else
        {
            _animator.ResetTrigger("JumpForwardWithoutWeapon");
        }

        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.S))
        {
            _animator.SetTrigger("JumpBackwardWithoutWeapon");
        }
        else
        {
            _animator.ResetTrigger("JumpBackwardWithoutWeapon");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaleRoleMoveController : MonoBehaviour
{
    private Animator _animator;

    // 有枪时的移动参数
    public float XRunWithWeapon = 3.18f;
    public float YRunWithWeaon = 3.04f;
    public float XWalkWithWeapon = 2.0f;
    public float YWalkWithWeapon = 2.0f;
    // 无枪时的移动参数
    public float XRunWithoutWeapon = 4.5f;
    public float YRunWithoutWeapon = 4.5f;
    public float XWalkWithoutWeapon = 2;
    public float YWalkWithoutWeapon = 2;

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
            _animator.SetFloat("SpeedXWithWeapon", h * XRunWithWeapon);// A D
            _animator.SetFloat("SpeedZWithWeapon", v * YRunWithWeaon);// W S
        }
        else
        {
            _animator.SetFloat("SpeedXWithWeapon", h * XWalkWithWeapon);// A D
            _animator.SetFloat("SpeedZWithWeapon", v * YWalkWithWeapon);// W S
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
            _animator.SetFloat("SpeedXWithoutWeapon", h * XRunWithoutWeapon);// A D
            _animator.SetFloat("SpeedZWithoutWeapon", v * YRunWithoutWeapon);// W S
        }
        else
        {
            _animator.SetFloat("SpeedXWithoutWeapon", h * XWalkWithoutWeapon);// A D
            _animator.SetFloat("SpeedZWithoutWeapon", v * YWalkWithoutWeapon);// W S
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

    private void OnAnimatorIK(int layerIndex)
    {
        // 获取人物看向的点，深度为10
        var position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 10));
        // 设置人物动画看向点
        _animator.SetLookAtPosition(position);
        // 设置看向权重，完全看向指定方向，权重为1
        _animator.SetLookAtWeight(1);
    }
}

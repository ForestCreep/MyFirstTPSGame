using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public PhysicsMaterial2D HomeBounce;// 小球返回后的材质
    public PhysicsMaterial2D RunningBounce;// 小球射出后的材质

    public Transform RightHomePos;// 左回归点
    public Transform LeftHomePos;// 右回归点

    public bool IsRunning = false;// 小球运行状态

    public float ForceCoefficient = 10;// 射击力量系数
    public int Damage = 1;// 小球伤害

    private Rigidbody2D _rigidbody2D;// 小球刚体
    private bool _isHasCollision = false;// 小球射出后是否发生碰撞，用于射击角度校准

    // Use this for initialization
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// 小球重置回归点处设置
    /// </summary>
    /// <param name="isLeft"></param>
    public void Reset(bool isLeft)
    {
        // 回归点判断
        if (isLeft)
        {
            transform.position = LeftHomePos.position;
        }
        else
        {
            transform.position = RightHomePos.position;
        }

        // 小球运动状态重置
        BallStateReset();

        _rigidbody2D.sharedMaterial = HomeBounce;
        // 小球状态设定为停止
        IsRunning = false;
        // 每次有小球触碰触发器后检查是否所有的小球已通过
        transform.GetComponentInParent<BallController>().OnBallBack();
    }

    private void BallStateReset()
    {
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.angularVelocity = 0;
    }

    public void InitBeforeShoot()
    {
        BallStateReset();
        _rigidbody2D.sharedMaterial = RunningBounce;
    }

    /// <summary>
    /// 发射小球
    /// </summary>
    /// <param name="direction"></param>
    public void Shoot(Vector2 direction)
    {
        InitBeforeShoot();
        IsRunning = true;
        // 不受重力影响，用于第一次撞击前角度校准
        _rigidbody2D.gravityScale = 0;
        // 标定尚未发生碰撞
        _isHasCollision = false;
        // 施加力
        _rigidbody2D.AddForce(direction * ForceCoefficient, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 当小球第一次发生碰撞后使其受到重力影响
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isHasCollision)
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        }

        _isHasCollision = true;
    }
}

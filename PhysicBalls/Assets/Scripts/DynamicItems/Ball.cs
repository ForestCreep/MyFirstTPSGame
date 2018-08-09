using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public PhysicsMaterial2D HomeBounce;
    public PhysicsMaterial2D RunningBounce;

    public Transform RightHomePos;
    public Transform LeftHomePos;

    public bool IsRunning = false;

    public float ForceCoefficient = 10;
    public int Damage = 1;

    private Rigidbody2D _rigidbody2D;

    // Use this for initialization
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Reset(bool isLeft)
    {
        if (isLeft)
        {
            transform.position = LeftHomePos.position;
        }
        else
        {
            transform.position = RightHomePos.position;
        }

        BallStateReset();
        _rigidbody2D.sharedMaterial = HomeBounce;

        IsRunning = false;
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

    public void Shoot(Vector2 direction)
    {
        InitBeforeShoot();
        IsRunning = true;
        _rigidbody2D.AddForce(direction * ForceCoefficient, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

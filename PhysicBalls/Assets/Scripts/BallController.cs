using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D _rigidBody2D;
    public float ForceCoefficient = 10;
    public Sprite Arrow;

    // Use this for initialization
    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // 鼠标坐标
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // 方向向量
            Vector2 direction = mousePos - transform.position;
            // 单位化
            direction.Normalize();
            // 施加力
            _rigidBody2D.AddForce(direction * ForceCoefficient, ForceMode2D.Impulse);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public Transform ShootPoint;// 发射点
    public GameObject ShootRay;// 发射引导线

    public Color LineColor = Color.white;
    public Color LineStartrColor = Color.white;// 引导线初始颜色
    public Color LineEndColor = Color.white;// 引导线末尾颜色

    public float ShootInterval = 0.5f;// 射击间隔
    public float LineStartWidth = 0.07f;// 引导线初始宽度
    public float LineEndWidth = 0.01f;// 引导线末尾宽度

    private GameObject _ray;// 引导线实例

    // Use this for initialization
    void Start()
    {
        LineInit();
    }

    /// <summary>
    /// 初始化引导线
    /// </summary>
    private void LineInit()
    {
        _ray = Instantiate(ShootRay);
        _ray.GetComponent<LineRenderer>().positionCount = 2;
        _ray.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && CheckIfAllBallsAreBack())
        {
            // 发射指引线
            _ray.SetActive(true);
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _ray.transform.position = new Vector3(pos.x, pos.y, 0);

            var line = _ray.GetComponent<LineRenderer>();
            // 引导线参数设置
            line.startWidth = LineStartWidth;
            line.endWidth = LineEndWidth;
            line.startColor = LineStartrColor;
            line.endColor = LineEndColor;
            line.alignment = LineAlignment.View;

            line.SetPosition(0, ShootPoint.position);
            line.SetPosition(1, pos);
        }

        if (Input.GetMouseButtonUp(0) && CheckIfAllBallsAreBack())
        {
            // 松开鼠标后关闭引导线
            _ray.SetActive(false);

            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePos - ShootPoint.position;
            direction.Normalize();
            // 发射小球
            StartCoroutine(ShootBalls(direction));
        }
    }

    private IEnumerator ShootBalls(Vector2 direction)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var childBall = transform.GetChild(i);
            childBall.position = ShootPoint.position;
            // 启动拖尾效果
            childBall.GetComponent<TrailRenderer>().enabled = true;
            // 小球颜色设置
            var renderer = childBall.GetComponent<SpriteRenderer>();
            if (renderer)
            {
                renderer.color = Color.white;
            }
            // 发射小球
            childBall.GetComponent<Ball>().Shoot(direction);
            // 延时
            yield return new WaitForSeconds(ShootInterval);
        }
    }

    /// <summary>
    /// 检查是否所有小球已返回
    /// </summary>
    /// <returns></returns>
    public bool CheckIfAllBallsAreBack()
    {
        // 运动状态检查
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Ball>().IsRunning) return false;
        }
        // 小球伤害、大小重置
        foreach (Transform child in transform)
        {
            var ball = child.GetComponent<Ball>();
            if (ball.Damage == 2)
            {
                ball.Damage = 1;
                ball.transform.localScale /= DoubleProp.Instance.BiggerSize;
            }
        }

        return true;
    }

    /// <summary>
    /// 所有小球已返回
    /// </summary>
    public void OnBallBack()
    {
        var allStopped = CheckIfAllBallsAreBack();
        // 所有小球已返回
        if (allStopped)
        {
            // 抬升已有障碍物
            ObstacleManager.Instance.UpliftOldObstacles();
            // 产生新的小球
            var newBall = Instantiate(transform.GetChild(0), gameObject.transform);
            // 小球位置设定
            var pos = newBall.GetComponent<Ball>().LeftHomePos.position;
            newBall.transform.position = new Vector2(pos.x, pos.y + 0.05f);
            // 颜色设为黄色
            newBall.GetComponent<SpriteRenderer>().color = Color.yellow;
            // 关闭拖尾效果
            newBall.GetComponent<TrailRenderer>().enabled = false;
            // 小球数量增加
            UIManager.Instance.BallCount++;
        }
    }
}

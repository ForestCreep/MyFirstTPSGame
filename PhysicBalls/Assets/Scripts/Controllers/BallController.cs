using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public Transform ShootPoint;
    public GameObject ShootRay;

    public Color LineColor = Color.white;
    public Color LineStartrColor = Color.white;
    public Color LineEndColor = Color.white;

    public float ShootInterval = 0.5f;
    public float LineStartWidth = 0.07f;
    public float LineEndWidth = 0.01f;

    private GameObject _ray;

    // Use this for initialization
    void Start()
    {
        _ray = Instantiate(ShootRay);
        _ray.GetComponent<LineRenderer>().positionCount = 2;
        _ray.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && CheckIfAllBallsAreStopped())
        {
            // TODO 发射指引线
            _ray.SetActive(true);
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _ray.transform.position = new Vector3(pos.x, pos.y, 0);

            var line = _ray.GetComponent<LineRenderer>();

            line.startWidth = LineStartWidth;
            line.endWidth = LineEndWidth;
            line.startColor = LineStartrColor;
            line.endColor = LineEndColor;
            line.alignment = LineAlignment.View;

            line.SetPosition(0, ShootPoint.position);
            line.SetPosition(1, pos);
        }

        if (Input.GetMouseButtonUp(0) && CheckIfAllBallsAreStopped())
        {
            _ray.SetActive(false);

            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePos - ShootPoint.position;
            direction.Normalize();

            StartCoroutine(ShootBalls(direction));
        }
    }

    private IEnumerator ShootBalls(Vector2 direction)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var childBall = transform.GetChild(i);
            childBall.position = ShootPoint.position;

            childBall.GetComponent<TrailRenderer>().enabled = true;

            var renderer = childBall.GetComponent<SpriteRenderer>();
            if (renderer)
            {
                renderer.color = GetRandomColor();
            }

            var trailRenderer = childBall.GetComponent<TrailRenderer>();
            trailRenderer.startColor = renderer.color;
            trailRenderer.endColor = renderer.color;

            childBall.GetComponent<Ball>().Shoot(direction);

            yield return new WaitForSeconds(ShootInterval);
        }
    }

    private Color GetRandomColor()
    {
        var r = Random.Range(0f, 1f);
        var g = Random.Range(0f, 1f);
        var b = Random.Range(0f, 1f);
        return new Color(r, g, b);
    }

    public bool CheckIfAllBallsAreStopped()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Ball>().IsRunning) return false;
        }

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

    public void OnBallBack()
    {
        var allStopped = CheckIfAllBallsAreStopped();
        if (allStopped)
        {
            ObstacleManager.Instance.UpliftOldObstacles();

            var newBall = Instantiate(transform.GetChild(0), gameObject.transform);
            newBall.transform.position = newBall.GetComponent<Ball>().LeftHomePos.position;

            UIManager.Instance.BallCount++;
        }
    }
}

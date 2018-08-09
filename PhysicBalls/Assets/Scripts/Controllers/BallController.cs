using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public Transform ShootPoint;
    public Image TipImage;

    public float ShootInterval = 0.5f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            TipImage.gameObject.SetActive(true);
            TipImage.transform.position = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0) && CheckIfAllBallsAreStopped())
        {
            TipImage.gameObject.SetActive(false);

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

            var renderer = childBall.GetComponent<SpriteRenderer>();
            if (renderer)
            {
                renderer.color = GetRandomColor();
            }

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

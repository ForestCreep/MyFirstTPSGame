using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleManager : MonoBehaviour
{
    public GameObject[] ObstaclePrefabs;
    public GameObject UIText;

    public Transform UICanvas;
    public Transform ObstaclesParent;

    public float[] XPositions = { 0.5f, 1.5f, -1.5f, -0.5f };
    public float MinY = -3.7f;

    public static ObstacleManager Instance;

    // Use this for initialization
    void Start()
    {
        Instance = this;
        BuildObstacle();
        UIManager.Instance.BallCount++;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BuildObstacle()
    {
        foreach (var positionX in XPositions)
        {
            if (Random.Range(0, 2) == 0) continue;

            var index = Random.Range(0, ObstaclePrefabs.Length);
            var obstaclePrefab = ObstaclePrefabs[index];

            var newObstacle = Instantiate(obstaclePrefab);
            newObstacle.transform.position = new Vector2(positionX, MinY);
            newObstacle.transform.parent = ObstaclesParent;

            var renderer = newObstacle.GetComponent<SpriteRenderer>();
            if (renderer)
            {
                renderer.color = GetRandomColor();
            }

            var obstacle = newObstacle.GetComponent<Obstacle>();
            if (obstacle)
            {
                obstacle.Init(BuildObstacleText());
                obstacle.Hp = UIManager.Instance.BallCount;
            }
        }
    }

    private Color GetRandomColor()
    {
        var r = Random.Range(0f, 1f);
        var g = Random.Range(0f, 1f);
        var b = Random.Range(0f, 1f);
        return new Color(r, g, b);
    }

    public GameObject BuildObstacleText()
    {
        var ui = Instantiate(UIText, UICanvas);
        return ui;
    }

    public void UpliftOldObstacles()
    {
        foreach (Transform item in transform)
        {
            item.position += new Vector3(0, 1f, 1);

            var obstacle = item.GetComponent<Obstacle>();
            if (obstacle)
            {
                obstacle.UpdateUI();
            }
        }

        BuildObstacle();
    }
}

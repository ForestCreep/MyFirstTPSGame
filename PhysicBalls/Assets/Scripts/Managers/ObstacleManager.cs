using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObstacleManager : MonoBehaviour
{
    public GameObject[] ObstaclePrefabs;
    public GameObject UIText;
    public GameObject[] PropsPrefabs;

    public Transform UICanvas;
    public Transform ObstaclesParent;

    public float[] XPositions = { 0.5f, 1.5f, -1.5f, -0.5f };
    public float MinY = -3.7f;
    public float MaxY = 2.3f;

    public static ObstacleManager Instance;

    // private Dictionary<float, float>[,] _positionsMatrix = new Dictionary<float, float>[7, 4];
    private List<Vector3> _allPosMat = new List<Vector3>()
    {
        new Vector3(-1.5f, 2.3f),new Vector3(-0.5f, 2.3f),new Vector3(0.5f, 2.3f),new Vector3(1.5f, 2.3f),
        new Vector3(-1.5f, 1.3f),new Vector3(-0.5f, 1.3f),new Vector3(0.5f, 1.3f),new Vector3(1.5f, 1.3f),
        new Vector3(-1.5f, 0.3f),new Vector3(-0.5f, 0.3f),new Vector3(0.5f, 0.3f),new Vector3(1.5f, 0.3f),
        new Vector3(-1.5f, -0.7f),new Vector3(-0.5f, -0.7f),new Vector3(0.5f, -0.7f),new Vector3(1.5f, -0.7f),
        new Vector3(-1.5f, -1.7f),new Vector3(-0.5f, -1.7f),new Vector3(0.5f, -1.7f),new Vector3(1.5f, -1.7f),
        new Vector3(-1.5f, -2.7f),new Vector3(-0.5f, -2.7f),new Vector3(0.5f, -2.7f),new Vector3(1.5f, -2.7f),
        new Vector3(-1.5f, -3.7f),new Vector3(-0.5f, -3.7f),new Vector3(0.5f, -3.7f),new Vector3(1.5f, -3.7f),
    };
    private List<Vector3> _freePosMat = new List<Vector3>();

    // Use this for initialization
    void Start()
    {
        Instance = this;
        BuildObstacle();
        UIManager.Instance.BallCount++;

        var test = _allPosMat[1];
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
            newObstacle.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));

            var renderer = newObstacle.GetComponent<SpriteRenderer>();
            if (renderer)
            {
                renderer.color = GetRandomColor();
            }

            var obstacle = newObstacle.GetComponent<Obstacle>();
            if (obstacle)
            {
                obstacle.Init(BuildObstacleText());
                obstacle.Hp = UIManager.Instance.HitNumber;
            }
        }

        UIManager.Instance.HitNumber++;
    }

    private void BuildProp()
    {
        if (UIManager.Instance.BallCount % 3 == 0)
        {
            var index = Random.Range(0, PropsPrefabs.Length);

            if (Random.Range(0, 100) > 30)
            {
                _freePosMat.Clear();

                foreach (Vector3 item in _allPosMat)
                {
                    if (!Physics.Raycast(item, new Vector3(item.x, item.y, 1)))
                    {
                        _freePosMat.Add(item);
                    }
                }

                var freeIndex = Random.Range(0, _freePosMat.Count);
                var prop = Instantiate(PropsPrefabs[index]);
                prop.transform.position = _freePosMat[freeIndex];
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
            item.position += new Vector3(0, 1f, 0);

            var obstacle = item.GetComponent<Obstacle>();
            if (obstacle)
            {
                obstacle.UpdateUI();
            }

            if (item.position.y > MaxY)
            {
                OnGameOver();
            }
        }

        BuildObstacle();
        BuildProp();
    }

    private void OnGameOver()
    {
        SceneManager.LoadScene("PhysicsBall");
    }
}

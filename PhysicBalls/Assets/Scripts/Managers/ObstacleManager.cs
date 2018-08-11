using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObstacleManager : MonoBehaviour
{
    public GameObject[] ObstaclePrefabs;// 障碍物预制体
    public GameObject UIText;// 血量显示预制体
    public GameObject[] PropsPrefabs;// 道具预制体

    public Transform UICanvas;// 画布
    public Transform ObstaclesParent;// 障碍物父节点

    public float[] XPositions = { 0.5f, 1.5f, -1.5f, -0.5f };// 障碍物X坐标
    public float MinY = -3.7f;// 障碍物最低点
    public float MaxY = 2.3f;// 障碍物可升至的最高点

    public static ObstacleManager Instance;

    // private Dictionary<float, float>[,] _positionsMatrix = new Dictionary<float, float>[7, 4];
    // 所有位置坐标
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
        // 初始化
        Instance = this;
        BuildObstacle();
        UIManager.Instance.BallCount++;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 建造障碍物
    /// </summary>
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
    
    /// <summary>
    /// 建造道具
    /// </summary>
    private void BuildProp()
    {
        // 有Bug
        // 生成后，抬升障碍物会覆盖道具
        // 应在抬升时加以判断

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

    /// <summary>
    /// 获取一个随机颜色
    /// </summary>
    /// <returns></returns>
    private Color GetRandomColor()
    {
        var r = Random.Range(0f, 1f);
        var g = Random.Range(0f, 1f);
        var b = Random.Range(0f, 1f);
        return new Color(r, g, b);
    }

    /// <summary>
    /// 建造UIText
    /// </summary>
    /// <returns></returns>
    public GameObject BuildObstacleText()
    {
        var ui = Instantiate(UIText, UICanvas);
        return ui;
    }

    /// <summary>
    /// 抬升已有障碍物
    /// </summary>
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

    /// <summary>
    /// 游戏结束操作
    /// </summary>
    private void OnGameOver()
    {
        SceneManager.LoadScene("PhysicsBall");
    }
}

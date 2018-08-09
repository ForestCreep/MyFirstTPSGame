using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text UICount;

    public int BallCount = 1;

    public static UIManager Instance;

    // Use this for initialization
    void Start()
    {
        Instance = this;
    }

    private UIManager()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UICount.text = "小球数量：" + (BallCount - 1).ToString();
        UICount.color = GetColor();
    }

    private Color GetColor()
    {
        float r = 0f;
        float g = 1f;
        float b = 0f;

        //r += Time.deltaTime * 0.01f;
        //g -= Time.deltaTime * 0.01f;
        //b += Time.deltaTime * 0.01f;

        return new Color(r, g, b);
    }
}

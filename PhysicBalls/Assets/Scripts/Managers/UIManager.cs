using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text UICount;

    public int BallCount = 1;

    public static UIManager Instance;

    private float r = 0f; // + 3  1  - 6
    private float g = 1f; // - 2  0  + 5
    private float b = 0f; // + 1  1  - 4

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
        if (r == 0 && g == 1 && b != 1)
        {
            b += Time.deltaTime;
            if (b > 1)
            {
                b = 1;
            }
        }
        else if (r == 0 && b == 1 && g != 0)
        {
            g -= Time.deltaTime;
            if (g < 0)
            {
                g = 0;
            }
        }
        else if (g == 0 && b == 1 && r != 1)
        {
            r += Time.deltaTime;
            if (r > 1)
            {
                r = 1;
            }
        }
        else if (r == 1 & g == 0 && b != 0)
        {
            b -= Time.deltaTime;
            if (b < 0)
            {
                b = 0;
            }
        }
        else if (r == 1 & b == 0 && g != 1)
        {
            g += Time.deltaTime;
            if (g > 1)
            {
                g = 1;
            }
        }
        else if (g == 1 && b == 0 && r != 0)
        {
            r -= Time.deltaTime;
            if (r < 0)
            {
                r = 0;
            }
        }

        return new Color(r, g, b);
    }
}

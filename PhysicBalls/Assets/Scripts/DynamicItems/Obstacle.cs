using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour
{
    public int Hp;// 障碍物血量
    public int HitIncreaseSise = 2;// 血量增幅

    public Text TextHp;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 血量减少
        var damage = collision.gameObject.GetComponent<Ball>().Damage;
        Hp -= damage;
        // 显示血量
        TextHp.text = Hp.ToString();
        // 分数增加
        UIManager.Instance.AddScore(damage);
        // 当血量小于0销毁障碍物
        if (Hp <= 0)
        {
            Destroy(this.gameObject);
            Destroy(TextHp.gameObject);
        }
    }

    /// <summary>
    /// 障碍物初始化
    /// </summary>
    /// <param name="ui"></param>
    public void Init(GameObject ui)
    {
        TextHp = ui.GetComponent<Text>();
        // 当血量是三的倍数的时候血量增加两点
        if (UIManager.Instance.HitNumber % 3 == 0)
        {
            UIManager.Instance.HitNumber += HitIncreaseSise;
            UIManager.Instance.HitNumber--;
        }
        TextHp.text = UIManager.Instance.HitNumber.ToString();
        // 血量显示更新
        UpdateUI();
    }

    /// <summary>
    /// 血量显示更新
    /// </summary>
    public void UpdateUI()
    {
        var pos = Camera.main.WorldToScreenPoint(transform.position);
        TextHp.transform.position = pos;
    }
}

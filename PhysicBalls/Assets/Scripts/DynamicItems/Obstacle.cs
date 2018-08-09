using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour
{
    public int Hp;
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
        Hp -= collision.gameObject.GetComponent<Ball>().Damage;
        TextHp.text = Hp.ToString();

        if (Hp <= 0)
        {
            Destroy(this.gameObject);
            Destroy(TextHp.gameObject);
        }
    }

    public void Init(GameObject ui)
    {
        TextHp = ui.GetComponent<Text>();
        TextHp.text = UIManager.Instance.BallCount.ToString();

        UpdateUI();
    }

    public void UpdateUI()
    {
        var pos = Camera.main.WorldToScreenPoint(transform.position);
        TextHp.transform.position = pos;
    }
}

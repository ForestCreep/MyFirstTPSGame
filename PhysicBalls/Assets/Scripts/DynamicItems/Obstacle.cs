using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour
{
    public int Hp;
    public int HitIncreaseSise = 2;

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
        var damage = collision.gameObject.GetComponent<Ball>().Damage;
        Hp -= damage;
        TextHp.text = Hp.ToString();

        UIManager.Instance.AddScore(damage);

        if (Hp <= 0)
        {
            Destroy(this.gameObject);
            Destroy(TextHp.gameObject);
        }
    }

    public void Init(GameObject ui)
    {
        TextHp = ui.GetComponent<Text>();

        if (UIManager.Instance.HitNumber % 3 == 0)
        {
            UIManager.Instance.HitNumber += HitIncreaseSise;
            UIManager.Instance.HitNumber--;
        }
        TextHp.text = UIManager.Instance.HitNumber.ToString();

        UpdateUI();
    }

    public void UpdateUI()
    {
        var pos = Camera.main.WorldToScreenPoint(transform.position);
        TextHp.transform.position = pos;
    }
}

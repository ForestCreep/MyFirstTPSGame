using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowEnemyHp : MonoBehaviour
{
    private Text _enemyHp;

    // Use this for initialization
    void Start()
    {
        _enemyHp = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _enemyHp.text = "当前敌人血量：" + CountManager.EnemyHp;
    }
}

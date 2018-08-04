using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SumPlayerToEnemy : MonoBehaviour
{
    private Text _playerToEnemy;

    // Use this for initialization
    void Start()
    {
        _playerToEnemy = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _playerToEnemy.text = "玩家对敌人造成的总伤害：" + CountManager.SumDamagePlayerToEnemy;
    }
}

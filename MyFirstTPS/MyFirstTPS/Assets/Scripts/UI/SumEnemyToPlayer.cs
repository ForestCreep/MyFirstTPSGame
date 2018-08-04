using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SumEnemyToPlayer : MonoBehaviour
{
    private Text _enemyToPlayer;
 
    // Use this for initialization
    void Start()
    {
        _enemyToPlayer = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _enemyToPlayer.text = "敌人对玩家造成的总伤害：" + CountManager.SumDamageEnemyToPlayer;
    }
}

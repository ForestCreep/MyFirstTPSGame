using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountManager : MonoBehaviour
{
    public static CountManager Instance;// 唯一实例 
    public static float SumDamageEnemyToPlayer;
    public static float SumDamagePlayerToEnemy;
    public static float PlayerHp = 100;
    public static float EnemyHp = 100;

    private CountManager()
    {

    }

    // Use this for initialization
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetPlayerHp(float value)
    {
        PlayerHp = value;
    }

    public void SetEnemyHp(float value)
    {
        EnemyHp = value;
    }

    public void OnDamageEnemyToPlayer(float value)
    {
        SumDamageEnemyToPlayer += value;
    }

    public void OnDamagePlayerToEnemy(float value)
    {
        SumDamagePlayerToEnemy += value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;// 唯一实例 
    public static float SumDamageEnemyToPlayer;
    public static float SumDamagePlayerToEnemy;
    public static float PlayerHp = 100;
    public static float EnemyHp = 100;
    public static bool IsPlayerAlive;

    public Text UIDeathTip;// 死亡提示
    public Text UIEnemyHp;// 敌人血量
    public Text UIPlayerHp;// 玩家血量
    public Text UISumPlayerToEnemy;// 玩家对敌人造成的总伤害
    public Text UISumEnemyToPlayer;// 敌人对玩家造成的总伤害
    public Image UISightBead;// 准星

    private UIManager()
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
        if (!IsPlayerAlive && UIDeathTip)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            UIDeathTip.text = "你已死亡";
            if (UISightBead)
            {
                Destroy(UISightBead);
            }
        }
        if (UIEnemyHp)
        {
            UIEnemyHp.text = "当前敌人血量：" + EnemyHp;
        }
        if (UIPlayerHp)
        {
            UIPlayerHp.text = "当前玩家血量：" + PlayerHp;
        }
        if (UISumPlayerToEnemy)
        {
            UISumPlayerToEnemy.text = "玩家对敌人造成的总伤害：" + SumDamagePlayerToEnemy;
        }
        if (UISumEnemyToPlayer)
        {
            UISumEnemyToPlayer.text = "敌人对玩家造成的总伤害：" + SumDamageEnemyToPlayer;
        }
    }

    public void SetPlayerHp(float value)
    {
        PlayerHp = value;
    }

    public void SetEnemyHp(float value)
    {
        EnemyHp = value;
    }

    public void SetPlayerIsAlive(bool isAlive)
    {
        IsPlayerAlive = isAlive;
    }

    public bool GetPlayerIsAlive()
    {
        return IsPlayerAlive;
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

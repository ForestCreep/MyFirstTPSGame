using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPAndEnergyController : MonoBehaviour
{
    public float Hp = 100;// 血量值
    public float Energy = 0;// 能量值
    private float _increaseEnergyIntervalTime = 0;// 能量增长时间间隔
    private float _increaseHpIntervalTime = 0;// 血量增长时间间隔
    private bool _isAlive = true;// 玩家存活状态
    private Animator _animator;

    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
        UIManager.Instance.SetPlayerIsAlive(true);
    }

    // Update is called once per frame
    void Update()
    {
        ReduceEnergyByTime();
        IncreaseHPByTime();
        KeyUpdate();

        ////test
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ReduceHpRandomly();
        }
    }

    private void KeyUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))//按下主键盘数字1
        {
            UseBandage();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UseFirstAidKit();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UseMedicalBox();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UseRedBull();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            UseAnodyne();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            UseAdrenaline();
        }
    }

    #region 使用不同补给物品

    private void UseBandage()
    {
        if (Hp >= 75)
        {
            Debug.Log("生命值大于75时无法使用绷带");
            return;
        }
        else
        {
            Hp += 10;
            if (Hp > 75)
            {
                Hp = 75;
            }
        }
    }

    private void UseFirstAidKit()
    {
        if (Hp >= 75)
        {
            Debug.Log("生命值大于等于75时无法使用急救包");
            return;
        }
        else
        {
            Hp = 75;
        }
    }

    private void UseMedicalBox()//使用医疗箱
    {
        if (Hp >= 100)
        {
            Debug.Log("生命值大于等于100时无法使用医疗箱");
            return;
        }
        else
        {
            Hp = 100;
        }
    }

    private void UseRedBull()
    {
        if (Energy >= 100)
        {
            Debug.Log("生命值大于等于100时无法使用红牛");
            return;
        }
        else
        {
            Energy += 40;
            if (Energy > 100)
            {
                Energy = 100;
            }
        }
    }

    private void UseAnodyne()//使用止痛药
    {
        if (Energy >= 100)
        {
            Debug.Log("生命值大于等于100时无法使用止痛药");
            return;
        }
        else
        {
            Energy += 60;
            if (Energy > 100)
            {
                Energy = 100;
            }
        }
    }

    private void UseAdrenaline()//使用肾上腺素
    {
        if (Energy >= 100)
        {
            Debug.Log("生命值大于等于100时无法使用肾上腺素");
            return;
        }
        else
        {
            Energy = 100;
        }
    }
    #endregion

    private void ReduceHpRandomly()
    {
        if (Hp > 0 && Hp <= 100)
        {
            Hp -= Random.Range(1, Hp);
        }
        if (Hp <= 0)
        {
            Hp = 1;
        }
    }

    private void IncreaseHPByTime()
    {
        if (Energy > 0 && Energy <= 100)
        {
            _increaseHpIntervalTime += Time.deltaTime;
        }

        if (_increaseHpIntervalTime >= 8)
        {
            if (Energy <= 20)
            {
                Hp += 1;
            }
            else if (Energy <= 60)
            {
                Hp += 2;
            }
            else if (Energy <= 90)
            {
                Hp += 3;
            }
            else
            {
                Hp += 4;
            }

            _increaseHpIntervalTime = 0;
            if (Hp > 100)
            {
                Hp = 100;
            }
        }
    }

    private void ReduceEnergyByTime()
    {
        if (Energy > 0)
        {
            _increaseEnergyIntervalTime += Time.deltaTime;
            if (_increaseEnergyIntervalTime >= 3)
            {
                Energy -= 1;
                _increaseEnergyIntervalTime = 0;
            }
        }
    }

    private void ReceiveDamage(float value)
    {
        if (Hp > 0)
        {
            Hp -= value;
            UIManager.Instance.OnDamageEnemyToPlayer(value);
        }
        if (Hp <= 0)
        {
            value += Hp;
            if (_isAlive)
            {
                _isAlive = false;
                _animator.SetTrigger("PlayerIsDead");
                UIManager.Instance.SetPlayerIsAlive(false);
                UIManager.Instance.OnDamageEnemyToPlayer(Hp);

            }
        }

        if (_isAlive)
        {
            UIManager.Instance.SetPlayerHp(Hp);
        }
        else
        {
            UIManager.Instance.SetPlayerHp(0);
        }
    }

    private void OnFinishedDead()
    {
        //_animator.SetTrigger("PlayerIsRevived");
        //Hp = 100;
        //UIManager.Instance.SetPlayerHp(100);
        //UIManager.Instance.SetPlayerIsAlive(true);
        //_isAlive = true;
    }
}

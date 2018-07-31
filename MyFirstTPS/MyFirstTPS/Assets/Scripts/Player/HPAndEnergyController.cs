using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPAndEnergyController : MonoBehaviour
{
    [SerializeField] private int hP = 100;
    [SerializeField] private int energy = 0;
    private float _IncreaseEnergyIntervalTime = 0;
    private float _IncreaseHpIntervalTime = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ReduceEnergyByTime();
        IncreaseHPByTime();
        KeyUpdate();

        //test
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
        if (hP >= 75)
        {
            Debug.Log("生命值大于75时无法使用绷带");
            return;
        }
        else
        {
            hP += 10;
            if (hP > 75)
            {
                hP = 75;
            }
        }
    }

    private void UseFirstAidKit()
    {
        if (hP >= 75)
        {
            Debug.Log("生命值大于等于75时无法使用急救包");
            return;
        }
        else
        {
            hP = 75;
        }
    }

    private void UseMedicalBox()//使用医疗箱
    {
        if (hP >= 100)
        {
            Debug.Log("生命值大于等于100时无法使用医疗箱");
            return;
        }
        else
        {
            hP = 100;
        }
    }

    private void UseRedBull()
    {
        if (energy >= 100)
        {
            Debug.Log("生命值大于等于100时无法使用红牛");
            return;
        }
        else
        {
            energy += 40;
            if (energy > 100)
            {
                energy = 100;
            }
        }
    }

    private void UseAnodyne()//使用止痛药
    {
        if (energy >= 100)
        {
            Debug.Log("生命值大于等于100时无法使用止痛药");
            return;
        }
        else
        {
            energy += 60;
            if (energy > 100)
            {
                energy = 100;
            }
        }
    }

    private void UseAdrenaline()//使用肾上腺素
    {
        if (energy >= 100)
        {
            Debug.Log("生命值大于等于100时无法使用肾上腺素");
            return;
        }
        else
        {
            energy = 100;
        }
    }
    #endregion

    private void ReduceHpRandomly()
    {
        if (hP > 0 && hP <= 100)
        {
            hP -= Random.Range(1, hP);
        }
        if (hP <= 0)
        {
            hP = 1;
        }
    }

    private void IncreaseHPByTime()
    {
        if (energy > 0 && energy <= 100)
        {
            _IncreaseHpIntervalTime += Time.deltaTime;
        }

        if (_IncreaseHpIntervalTime >= 8)
        {
            if (energy <= 20)
            {
                hP += 1;
            }
            else if (energy <= 60)
            {
                hP += 2;
            }
            else if (energy <= 90)
            {
                hP += 3;
            }
            else
            {
                hP += 4;
            }

            _IncreaseHpIntervalTime = 0;
            if (hP > 100)
            {
                hP = 100;
            }
        }
    }

    private void ReduceEnergyByTime()
    {
        if (energy > 0)
        {
            _IncreaseEnergyIntervalTime += Time.deltaTime;
            if (_IncreaseEnergyIntervalTime >= 3)
            {
                energy -= 1;
                _IncreaseEnergyIntervalTime = 0;
            }
        }
    }

}

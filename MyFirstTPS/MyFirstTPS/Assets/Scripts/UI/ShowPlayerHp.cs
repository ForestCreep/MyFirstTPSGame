using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPlayerHp : MonoBehaviour
{
    private Text _playerHp;

    // Use this for initialization
    void Start()
    {
        _playerHp = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _playerHp.text = "当前玩家血量：" + CountManager.PlayerHp;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWeaponManager : MonoBehaviour
{
    public GameObject[] SpawnableWeapons;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            var r = Random.Range(0, SpawnableWeapons.Length);
            var num = Random.Range(0, 100);
            if (num >= 50)
            {
                var newWeapon = Instantiate(SpawnableWeapons[r], this.transform.GetChild(i).transform.position, Quaternion.Euler(-90, 0, 0), this.transform.GetChild(i).transform);
                newWeapon.name = SpawnableWeapons[r].name;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

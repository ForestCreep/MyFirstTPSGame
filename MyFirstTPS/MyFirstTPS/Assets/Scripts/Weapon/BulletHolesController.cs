using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHolesController : MonoBehaviour
{
    public GameObject[] MetalBulletHoles;
    public GameObject[] StoneBulletHoles;
    public GameObject[] WoodBulletHoles;
    public static BulletHolesController Instance;

    private BulletHolesController()
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

    public GameObject GetBulletHole(string tag)
    {
        switch (tag)
        {
            case "Metal":
                {
                    var index = Random.Range(0, MetalBulletHoles.Length);
                    return Instantiate(MetalBulletHoles[index]);
                }
            case "Stone":
                {
                    var index = Random.Range(0, StoneBulletHoles.Length);
                    return Instantiate(StoneBulletHoles[index]);
                }
            case "Wood":
                {
                    var index = Random.Range(0, WoodBulletHoles.Length);
                    return Instantiate(WoodBulletHoles[index]);
                }
        }
        return null;
    }
}

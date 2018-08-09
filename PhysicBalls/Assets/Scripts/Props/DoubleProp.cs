using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleProp : MonoBehaviour
{
    public float BiggerSize = 1.2f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.transform.localScale *= BiggerSize;
        collision.GetComponent<Ball>().Damage = 2;

        Destroy(gameObject);
    }
}

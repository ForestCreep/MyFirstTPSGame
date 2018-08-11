using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetController : MonoBehaviour
{
    public bool IsLeft;

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
        if (IsLeft)
        {
            collision.GetComponent<Ball>().Reset(true);
            collision.GetComponent<TrailRenderer>().enabled = false;
            collision.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else
        {
            collision.GetComponent<Ball>().Reset(false);
            collision.GetComponent<TrailRenderer>().enabled = false;
            collision.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }
}

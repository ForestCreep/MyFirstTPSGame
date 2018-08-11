using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallUp : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var rg = collision.GetComponent<Rigidbody2D>();
        rg.AddForce(new Vector2(collision.transform.position.x, collision.transform.position.y + 35), ForceMode2D.Impulse);
    }
}

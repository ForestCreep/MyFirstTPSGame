using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTriggerController : MonoBehaviour
{
    public bool IsLeft;// 左或右上侧触发器
    public float ForceSize = 5;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    var rg = collision.GetComponent<Rigidbody2D>();

    //    rg.constraints = RigidbodyConstraints2D.None;
    //    rg.AddForce(new Vector2(collision.transform.position.x, collision.transform.position.y + 5));

    //}

    private void OnTriggerStay2D(Collider2D collision)
    {
        var rg = collision.GetComponent<Rigidbody2D>();

        rg.constraints = RigidbodyConstraints2D.None;
        if (IsLeft)
        {
            rg.AddForce(new Vector2(collision.transform.position.x + ForceSize, collision.transform.position.y));
        }
        else
        {
            rg.AddForce(new Vector2(collision.transform.position.x - ForceSize, collision.transform.position.y));
        }
    }
}

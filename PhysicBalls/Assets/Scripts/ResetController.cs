using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetController : MonoBehaviour
{
    public bool IsLeft;
    public Transform RightHomePos;
    public Transform LeftHomePos;
    public float HomeBounciness = 0.5f;

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
            collision.transform.position = LeftHomePos.position;
        }
        else
        {
            collision.transform.position = RightHomePos.position;
        }

        var rg = collision.GetComponent<Rigidbody2D>();
        rg.velocity = Vector2.zero;
        rg.angularVelocity = 0;
        rg.sharedMaterial.bounciness = HomeBounciness;
    }
}

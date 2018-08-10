using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitProp : MonoBehaviour
{

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
        var oldBall = collision.gameObject;
        var newBall = Instantiate(oldBall, oldBall.transform.parent);

        var oldBallRigidbody2D = oldBall.GetComponent<Rigidbody2D>();
        var newBallRigidbody2D = newBall.GetComponent<Rigidbody2D>();

        newBallRigidbody2D.velocity = oldBallRigidbody2D.velocity;
        newBallRigidbody2D.angularVelocity = oldBallRigidbody2D.angularVelocity;

        UIManager.Instance.BallCount++;

        Destroy(gameObject);
    }
}

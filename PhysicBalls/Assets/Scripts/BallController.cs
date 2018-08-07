using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public Transform ShootPos;
    public float ForceCoefficient = 10;
    public float Bounciness = 0.9f;
    public float ShootInterval = 0.5f;
    public Image TipImage;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            TipImage.gameObject.SetActive(true);
            TipImage.transform.position = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            TipImage.gameObject.SetActive(false);
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePos - ShootPos.position;
            direction.Normalize();
            StartCoroutine(ShootBalls(direction));
        }
    }

    private IEnumerator ShootBalls(Vector2 direction)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var childBall = transform.GetChild(i);
            childBall.position = ShootPos.position;
            var rg = childBall.GetComponent<Rigidbody2D>();
            rg.sharedMaterial.bounciness = Bounciness;
            rg.AddForce(direction * ForceCoefficient, ForceMode2D.Impulse);
            yield return new WaitForSeconds(ShootInterval);
        }
    }
}

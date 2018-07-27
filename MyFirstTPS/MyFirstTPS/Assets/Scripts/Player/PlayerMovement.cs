using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator _animator;
    public float kSpeedX = 1.76f;
    public float kSpeedZ = 2.87f;

    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        _animator.SetFloat("SpeedX", h * kSpeedX);// A D
        _animator.SetFloat("SpeedZ", v * kSpeedZ);// W S
    }
}

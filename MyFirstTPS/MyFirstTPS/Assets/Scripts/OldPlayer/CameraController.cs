using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerNeck;
    public Transform player;
    public float maxAngle = 50;
    public float minAngle = -50;

    //public Transform playerSpine;
    public float sensitivityY = 180;
    public float sensitivityX = 180;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var offsetX = Input.GetAxis("Mouse X");
        var offsetY = Input.GetAxis("Mouse Y");

        CameraTransform(offsetX, offsetY);

        // 控制人物身体旋转
        BodyRotateFromAxisX();
    }

    /// <summary>
    /// 相机旋转
    /// </summary>
    /// <param name="offsetX"></param>
    /// <param name="offsetY"></param>
    private void CameraTransform(float offsetX, float offsetY)
    {
        // 限制旋转角度
        // 当x大于180时强制变负
        var local = transform.localEulerAngles;
        var cameraX = local.x;
        while (cameraX > 180f)
        {
            cameraX -= 360f;
        }

        // 当旋转达到仰视上限时，使偏移量为0
        if (cameraX > maxAngle && offsetY < 0 || cameraX < minAngle && offsetY > 0)
        {
            offsetY = 0;
        }

        // 相机绕人物颈部旋转
        transform.RotateAround(playerNeck.position, transform.right, -offsetY * sensitivityY * Time.deltaTime);
        // 人物左右旋转
        player.Rotate(0f, offsetX * sensitivityX * Time.deltaTime, 0f);
    }

    private void BodyRotateFromAxisX()
    {
        // Do something
    }
}

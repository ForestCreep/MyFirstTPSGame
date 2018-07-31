using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerNeck;
    public Transform player;
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
        offsetY = Mathf.Clamp(offsetY, -45, 45);
        // 限制旋转角度
        // 当x大于180时强制变负
        var locale = transform.localEulerAngles;
        var cameraX = locale.x;
        while (cameraX > 180f)
        {
            cameraX -= 360f;
        }
        // 当旋转达到仰视上限时，使偏移量为0
        if (cameraX > 70f )
        {
            offsetY = 0;
            //transform.localEulerAngles = new Vector3(70f, locale.y, locale.z);
        }
        else if (cameraX < -50)
        {
            offsetY = 0;
            //transform.localEulerAngles = new Vector3(-50f, locale.y, locale.z);
        }
        Debug.Log(cameraX);
        
        // 相机绕人物颈部旋转
        transform.RotateAround(playerNeck.position, transform.right, -offsetY * sensitivityY * Time.deltaTime);

        // 人物左右旋转
        player.Rotate(0f, offsetX * sensitivityX * Time.deltaTime, 0f);

        // 控制人物身体旋转


    }
}

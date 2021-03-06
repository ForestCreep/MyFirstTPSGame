﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveController : MonoBehaviour
{
    public Transform PlayerNeck;// 相机围绕旋转中心点
    public Transform Player;// 玩家对象
    public Transform TopCameraL;// 死亡时要移出去的父物体
    public float MaxAngle = 50;// 最大仰视角度
    public float MinAngle = -50;// 最大俯视角度
    public float SensitivityY = 180;// 俯仰灵敏度
    public float SensitivityX = 180;// 左右旋转灵敏度

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.parent == Player)
        {
            var offsetX = Input.GetAxis("Mouse X");
            var offsetY = Input.GetAxis("Mouse Y");

            if (UIManager.Instance.GetPlayerIsAlive())
            {
                CameraTransform(offsetX, offsetY);
                BodyRotateFromAxisX();
            }
            else
            {
                this.transform.parent = TopCameraL;
            }
        }
        else if(this.transform.parent == TopCameraL)
        {
            if (UIManager.Instance.GetPlayerIsAlive())
            {
                this.transform.parent = Player;
            }            
        }
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
        if (cameraX > MaxAngle && offsetY < 0 || cameraX < MinAngle && offsetY > 0)
        {
            offsetY = 0;
        }

        // 相机绕人物颈部旋转
        transform.RotateAround(PlayerNeck.position, transform.right, -offsetY * SensitivityY * Time.deltaTime);
        // 人物左右旋转
        Player.Rotate(0f, offsetX * SensitivityX * Time.deltaTime, 0f);
    }

    private void BodyRotateFromAxisX()
    {
        // Do something
    }
}

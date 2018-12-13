﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

    public Transform lookAt;
    public Transform camTransform;
    Camera cam;

    private float distance = 7.0f;
    private float currentX = 0.0f;
    private float currentY = 30.0f;
    private float sensitivityX = 3.3f;
    private float sensitivityY = 1.0f;

    GameUI ui;

    private void Start()
    {
        ui = GameObject.FindGameObjectWithTag("Game UI").GetComponent<GameUI>();
        camTransform = transform;
        cam = Camera.main;
    }

    private void Update()
    {
        if (!ui.game_Paused)   
            currentX += Input.GetAxis("Mouse X");
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0,0,-distance);
        Quaternion rotation = Quaternion.Euler(currentY, sensitivityX*currentX, 0);
        camTransform.position = lookAt.position + rotation * dir;

        camTransform.LookAt(lookAt.position + new Vector3(0,3,0));
    }

}

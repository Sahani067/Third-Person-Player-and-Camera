using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    RaycastHit hit;
    [Header("Camera Inputs")]
    [SerializeField] float _MouseX, _MouseY;

    [Header("Camera Settings")]
    [SerializeField] float _SenstivityX;
    [SerializeField] float _SenstivityY;
    [SerializeField] int _MinX = -45;
    [SerializeField] int _MaxX = -45;
    [SerializeField] Transform _Target;
    [Space(10)]
    [SerializeField] Transform _Pivot;
    [SerializeField] Transform _Cam;
    [SerializeField] Transform _CameraNormalPos;


    private void LateUpdate()
    {
        transform.position = _Target.position;
        CameraCollision();
    }

    

    private void Update()
    {
        _MouseX -= Input.GetAxis("Mouse Y") * _SenstivityX;
        _MouseY += Input.GetAxis("Mouse X") * _SenstivityY;

        _MouseX = Mathf.Clamp(_MouseX, _MinX, _MaxX);

        _Pivot.transform.rotation = Quaternion.Euler(_MouseX, _MouseY, 0f);
    }

    private void CameraCollision()
    {
        Debug.DrawLine(_Pivot.transform.position, _Cam.transform.position, Color.magenta);
        if (Physics.Linecast(_Pivot.transform.position, _Cam.transform.position, out hit))
        {
            _Cam.transform.position = Vector3.Lerp(_Cam.transform.position, hit.point, 10f * Time.deltaTime);
        }

        else
        {
            _Cam.transform.position = Vector3.Lerp(_Cam.transform.position, _CameraNormalPos.transform.position, 0.7f * Time.deltaTime);
        }
    }

}

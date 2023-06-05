using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MauseLook : MonoBehaviour
{
    [Range(50, 500)]
    public float sensitivity;  //hassasiyet

    public Transform body;

    float xRotation=0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;// fare imlecini kilitler yani g�r�nmez yapar
    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime;  //GetAxisRaw fare x eksenindeki hareketini 0 ile 1 de�erine �evirir
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;      //  y eksenindeki hareketi ��kar�yoruz

        xRotation = Mathf.Clamp(xRotation, -80f, 80f);  // rotasyonu -80 ile 80 aras�nda s�n�rlad�k 360 derce d�nmesin diye 

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        body.Rotate(Vector3.up * mouseX);  // nesneyi d�nd�rmek i�in
    }
}

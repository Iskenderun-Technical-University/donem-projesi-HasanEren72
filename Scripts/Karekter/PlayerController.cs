using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public  CharacterController controller;

    public float speed = 5f;
    public float FastSpeed = 10;
    public float gravity = -10;  // yer�ekimi

    Vector3 velocity; // vector3 te bir tane h�z  de�i�keni

    public Transform groundCheck;  // zemin kontrol� i�in
    public float groundDistance = 0.4f; // yer mesafesi
    public LayerMask groundMask;   // zemin maskesi   zemini ay�rt etmek i�in
    public float jumpheight=3f;   //z�plama y�ksekli�i
   
    bool isGrounded; // toprak m�  kontorl i�in

    void Start()
    {
       
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position , groundDistance , groundMask); //groundCheck pozisyonuna dayal� bir k�re yaratacakt�r 
                                                                                           //yer mesafesi ve zemin maskesi de eklenir
        if (isGrounded && velocity.y<0) //toprak ta ve y deki h�z� o dan k���k ise h�z�n� s�f�rlar�z
        {
            velocity.y = -2f;  //   0 yerine -2 yaparsak  daha optimize olur
        }


        float x = Input.GetAxis("Horizontal") * Time.deltaTime; //yatay hareket -1 ile 1 aras�nda de�er d�nd�r�r.
        float z = Input.GetAxis("Vertical") * Time.deltaTime;  //dikey hareket
                                                                       // oyuncu hangi y�ne bakarsa o y�ne gidecektir
        Vector3 move = transform.right * x  + transform.forward * z;    // Vector3 move = new Vector3(x , 0f ,z); b�yle yapm�� olsayd�k oyuncu 
      //Vector3 move = new vector3(1,0,0) * x  + new vector3(0,0,1) * z;   //hangi yone bakarsa baks�n hep ayn� yone hareket ederdi


        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(move * FastSpeed );  //  karekteri h�zl� hareket ettirir.
        }
        else
        {
            controller.Move(move * speed );  // karekter hareket ettirir.
        }


        velocity.y += gravity * Time.deltaTime * Time.deltaTime;  //Yer�ekimi ��in  serbest d��mede h�z h=1/2gt^2 old i�in  vector3 te y ekseninde 
        controller.Move(velocity);                               //hareket old i�in gravity(yer�ekimi) ile zaman�n karesi ile �arp�yoruz.


        if (Input.GetButtonDown("Jump") && isGrounded )
        {           
            velocity.y = Mathf.Sqrt(jumpheight * -2f *gravity);          
        }

    }
}

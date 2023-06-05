using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook2 : MonoBehaviour
{
    public Transform hedef;
    public Vector3 hedefMesafe;

    [Range(0, 100)]
    [SerializeField]
    private float MauseSensitivity;  // Fare Hassasiyeti hassasiyet
    float mouseX, mouseY;

    public Transform karekterGovde;//kamera ile vucut dondurmek i�in
    Vector3 objRotasyon;

    KarekterKontrol2 nesne;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;// fare imlecini kilitler yani g�r�nmez yapar
        nesne = GameObject.Find("character").GetComponent<KarekterKontrol2>();  //mauselook ve karekterkontrol varkl� nesneler i�inde old i�in
    }                                                                           //find ile arat�p compenentlerine ula�t�k

    void Update()
    {

    }
    private void LateUpdate()  //kamera ile ilgili kodlar genelde LateUpdate e yaz�l�r
    {
        if (nesne.hayattaMi ==true)  //oyuncu hayatta ise yap�lacaklar
        {
            transform.position = Vector3.Lerp(transform.position, hedef.position + hedefMesafe, Time.deltaTime * 10); //kamera pozisyonuna oyuncunun pozisyonu atad�k
                                                                                                                      //Bunun yerine kameray� direk oyuncunun i�inede atayabilirdk ayn� i�lev
            mouseX += Input.GetAxis("Mouse X") * MauseSensitivity;
            mouseY += Input.GetAxis("Mouse Y") * MauseSensitivity;

            transform.eulerAngles = new Vector3(-mouseY, mouseX, 0);  //giri�lere g�re kameray� rotasyon de�i�tirir 
            hedef.transform.eulerAngles = new Vector3(0, mouseX, 0); //giri�lere g�re hedefi rotasyon de�i�tirir

            if (mouseY > 30)
            {
                mouseY = 30;
            }
            if (mouseY < -30)
            {
                mouseY = -30;
            }

            // kamera hareketi ile karekterin vucudunu hareket ettirmek i�in

            Vector3 gecici = transform.eulerAngles; //kameran�n rotasyonunu de�i�kene atad�k
            gecici.z = 0;
            gecici.y = transform.localEulerAngles.y;      // y ekseninde kamera ile ayn� rotasyon olacak
            gecici.x = transform.localEulerAngles.x + 10; // x ekseninde 10 birim dondurecek
            objRotasyon = gecici;

            karekterGovde.transform.eulerAngles = objRotasyon;  // karekterGovde rotasyon transformuna olu�turdu�umuz rotasyonu atad�k

        }



    }
}

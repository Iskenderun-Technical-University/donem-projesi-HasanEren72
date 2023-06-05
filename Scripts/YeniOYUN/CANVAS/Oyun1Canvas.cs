using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI (user interface) kullan�cu aray�z� k�t�phanesi
using TMPro; //Text mesh pro k�t�phanesi  
using UnityEngine.SceneManagement;

public class Oyun1Canvas : MonoBehaviour
{
    public TextMeshProUGUI CanText;
    public TextMeshProUGUI MermiText;
   
    public GameObject OyunDurduPaneli;
    GameObject oyuncu;

    public int olusayisi = 0;
    public int nesnesayisi = 0;
    public TextMeshProUGUI olu_text;

    public GameObject[] ClonZombiler1;  //her bir dizide farkl� �zellikte zombiler olacak;
    public GameObject[] ClonZombiler2;
    public GameObject[] ClonZombiler3;

    public TextMeshProUGUI dalgaText;
    public GameObject dalgatextobjesi;

    public GameObject karekterKamerasi;
    public GameObject DurduKamerasi;

    public GameObject yagmurEfekti;
    public GameObject yagmurSesi;
    public float sayi;
    void Start()
    {
        oyuncu = GameObject.Find("character"); //character nesnesini arat�p private nesneye atad�k  karekterkontrol2 ve atesetme2 s�n�flar�na ula�mak i�in

        dalgaText.text = "1.Dalga Ba�l�yor .....";
        dalgatextobjesi.SetActive(true);
        StartCoroutine(dalgaTextKapat());

        for (int i = 0; i < 10; i++)  //1.Dalga rasgele 10 zombi
        {   
            float xpozisyon=0f;
            float zpozisyon=0f;
            int rastgeleBolge = Random.Range(1,4);
            if (rastgeleBolge==1) //1,b�lge
            {
                xpozisyon = Random.Range(-111, -106);
                zpozisyon = Random.Range(9, 13);
            }
            else if (rastgeleBolge == 2) //2.b�lge
            {
                xpozisyon = Random.Range(-111, -106);
                zpozisyon = Random.Range(-70, 6);
            }
            else if (rastgeleBolge == 3) //3.b�lge
            {
                 xpozisyon = Random.Range(-111, -90);
                 zpozisyon = Random.Range(-20, -16);

            }   

            float RastgeleRotasyonY = Random.Range(0, 360);
            Quaternion randomRotation = Quaternion.Euler(0f, RastgeleRotasyonY, 0f);

            int zomb�ndis = Random.Range(0, ClonZombiler1.Length);
            GameObject nesne = Instantiate(ClonZombiler1[zomb�ndis], new Vector3(xpozisyon, 0, zpozisyon), randomRotation);
            nesnesayisi++;
        }

        // InvokeRepeating("AzaltNesneSayisi", 1f, 0.5f);
    }
    IEnumerator dalgaTextKapat()
    {
        yield return new WaitForSeconds(3); //3 saniye bekler
        dalgatextobjesi.SetActive(false);
    }
    public void Dalga2()    //2.Dalga farkl� �zelliklere sahip rasgele 20 zombi
    {
        dalgaText.text = "2.Dalga Ba�l�yor ... Dikatli olun evrim ge�irmi�ler art�k daha g��l�ler !!!";
        dalgatextobjesi.SetActive(true);
        StartCoroutine(dalgaTextKapat());

        for (int i = 0; i < 20; i++)  //1.Dalga rasgele 10 zombi
        {
            float xpozisyon = 0f;
            float zpozisyon = 0f;
            int rastgeleBolge = Random.Range(1, 4);
            if (rastgeleBolge == 1) //1,b�lge
            {
                xpozisyon = Random.Range(-111, -106);
                zpozisyon = Random.Range(9, 13);
            }
            else if (rastgeleBolge == 2) //2.b�lge
            {
                xpozisyon = Random.Range(-111, -106);
                zpozisyon = Random.Range(-70, 6);
            }
            else if (rastgeleBolge == 3) //3.b�lge
            {
                xpozisyon = Random.Range(-111, -90);
                zpozisyon = Random.Range(-20, -16);

            }

            float RastgeleRotasyonY = Random.Range(0, 360);
            Quaternion randomRotation = Quaternion.Euler(0f, RastgeleRotasyonY, 0f);

            int zomb�ndis = Random.Range(0, ClonZombiler1.Length);
            GameObject nesne = Instantiate(ClonZombiler1[zomb�ndis], new Vector3(xpozisyon, 0, zpozisyon), randomRotation);
            nesnesayisi++;
        }
    }
    public void Dalga3()    //3.Dalga farkl� �zelliklere sahip rasgele 30 zombi
    {
        dalgaText.text = "3.Dalga Ba�l�yor ...  Dikatli olun evrim ge�irmi�ler art�k daha g��l�ler !!!";
        dalgatextobjesi.SetActive(true);
        StartCoroutine(dalgaTextKapat());

        for (int i = 0; i < 40; i++)  //1.Dalga rasgele 10 zombi
        {
            float xpozisyon = 0f;
            float zpozisyon = 0f;
            int rastgeleBolge = Random.Range(1, 4);
            if (rastgeleBolge == 1) //1,b�lge
            {
                xpozisyon = Random.Range(-111, -106);
                zpozisyon = Random.Range(9, 13);
            }
            else if (rastgeleBolge == 2) //2.b�lge
            {
                xpozisyon = Random.Range(-111, -106);
                zpozisyon = Random.Range(-70, 6);
            }
            else if (rastgeleBolge == 3) //3.b�lge
            {
                xpozisyon = Random.Range(-111, -90);
                zpozisyon = Random.Range(-20, -16);

            }

            float RastgeleRotasyonY = Random.Range(0, 360);
            Quaternion randomRotation = Quaternion.Euler(0f, RastgeleRotasyonY, 0f);

            int zomb�ndis = Random.Range(0, ClonZombiler1.Length);
            GameObject nesne = Instantiate(ClonZombiler1[zomb�ndis], new Vector3(xpozisyon, 0, zpozisyon), randomRotation);
            nesnesayisi++;
        }
    }
    void AzaltNesneSayisi()  //zombi �l� say�lar�n� tutabilmek i�in.
    {
        GameObject[] zombiNesneleri = GameObject.FindGameObjectsWithTag("Zombi"); // "Zombi" etiketine sahip nesneleri bul

        int aktifZombiSayisi = zombiNesneleri.Length;

       
        if (aktifZombiSayisi < nesnesayisi)
        {
            nesnesayisi = aktifZombiSayisi;
            olusayisi += 1;
            olu_text.text = "�l� Sayisi:" + olusayisi.ToString();
        }
        if (aktifZombiSayisi==0 && olusayisi ==10) //2.dalga
        {
            Dalga2();

        }
        else if (aktifZombiSayisi == 0 && olusayisi == 30) //3.dalga
        {
            Dalga3();
        }

    }
    void Update()    // MermiText textine oyuncu nesnesin kompenentlerine ula��p  AtesEtme2 s�n�f�ndan  GetSarjor() ve GetCephane() 
    {                //fonk �a��r�p stinge d�b��t�rerek atad�k

        MermiText.text = oyuncu.GetComponent<AtesEtme2>().GetSarjor().ToString() + "/" + oyuncu.GetComponent<AtesEtme2>().GetCephane().ToString();
        CanText.text = "Can :" + oyuncu.GetComponent<KarekterKontrol2>().GetKarekerCan().ToString();

        if (Input.GetKey(KeyCode.Escape)) // esc ye bas�nca oyunu durdurur.
        {
            OyunuDurdur();
        }
       
        AzaltNesneSayisi();

      
    }

    public void HavaDurumuDegistir(int deger)
    {
        if (deger == 0) //default g�ne�li
        {
            yagmurSesi.SetActive(false);
        }
        else if (deger == 1)  //g�ne�li
        {
            yagmurEfekti.SetActive(false);
            yagmurSesi.SetActive(false);

        }
        else if (deger == 2) //ya�murlu
        {
            yagmurEfekti.SetActive(true);
            yagmurSesi.SetActive(true);
        }


    }
    public void OyunuDevamEt()
    {
        Time.timeScale = 1; //oyunu devam ettirir.
        OyunDurduPaneli.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;// fare imlecini kilitler
        DurduKamerasi.SetActive(false);
        karekterKamerasi.SetActive(true);

        GameObject[] zombiNesneleri = GameObject.FindGameObjectsWithTag("Zombi"); // "Zombi" etiketine sahip nesneleri bul diziye at

        for (int i = 0; i < zombiNesneleri.Length; i++)   // oyun devam ederse " t�m zombilerin SESLER�N� A�MAK ���N" 
        {
            AudioSource zombiSesi = zombiNesneleri[i].GetComponent<AudioSource>();
            if (zombiSesi != null)  // Zombi nesnesinde ses bile�eni varsa
            {
                zombiSesi.enabled = true;  // Zombi sesini etkinle�tir
            }
        }
        // Raycast i�lemini aktif hale getirir.
        Physics.queriesHitTriggers = true;
    }


    public void OyunuDurdur()
    {
        Time.timeScale = 0; //oyunu durdurur.
        OyunDurduPaneli.SetActive(true);  //devam et butonunu aktif yapar
        Cursor.lockState = CursorLockMode.None;// fare imlecini kilidini kald�r�r.
        DurduKamerasi.SetActive(true);
        karekterKamerasi.SetActive(false);

        GameObject[] zombiNesneleri = GameObject.FindGameObjectsWithTag("Zombi"); // "Zombi" etiketine sahip nesneleri bul diziye at

        for (int i = 0; i < zombiNesneleri.Length; i++) //oyun durmu�sa " t�m zombilerin SESLER�N� KESMEK ���N" 
        {
            AudioSource zombiSesi = zombiNesneleri[i].GetComponent<AudioSource>();
            if (zombiSesi != null)  // Zombi nesnesinde ses bile�eni varsa
            {
                zombiSesi.enabled = false;  // Zombi sesini kapat�r
            }
        }
        // Raycast i�lemini devre d��� b�rak�r.
        Physics.queriesHitTriggers = false;
    }

    public void Home()
    {
        SceneManager.LoadScene("Menu");
        Cursor.lockState = CursorLockMode.None;// fare imlecini kilidini
    }
      
    public void Exit()
    {
        Application.Quit();  //uygulamadan ��k�� yapar
    }
    
}

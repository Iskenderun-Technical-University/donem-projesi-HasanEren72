using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI (user interface) kullanýcu arayüzü kütüphanesi
using TMPro; //Text mesh pro kütüphanesi  
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

    public GameObject[] ClonZombiler1;  //her bir dizide farklý özellikte zombiler olacak;
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
        oyuncu = GameObject.Find("character"); //character nesnesini aratýp private nesneye atadýk  karekterkontrol2 ve atesetme2 sýnýflarýna ulaþmak için

        dalgaText.text = "1.Dalga Baþlýyor .....";
        dalgatextobjesi.SetActive(true);
        StartCoroutine(dalgaTextKapat());

        for (int i = 0; i < 10; i++)  //1.Dalga rasgele 10 zombi
        {   
            float xpozisyon=0f;
            float zpozisyon=0f;
            int rastgeleBolge = Random.Range(1,4);
            if (rastgeleBolge==1) //1,bölge
            {
                xpozisyon = Random.Range(-111, -106);
                zpozisyon = Random.Range(9, 13);
            }
            else if (rastgeleBolge == 2) //2.bölge
            {
                xpozisyon = Random.Range(-111, -106);
                zpozisyon = Random.Range(-70, 6);
            }
            else if (rastgeleBolge == 3) //3.bölge
            {
                 xpozisyon = Random.Range(-111, -90);
                 zpozisyon = Random.Range(-20, -16);

            }   

            float RastgeleRotasyonY = Random.Range(0, 360);
            Quaternion randomRotation = Quaternion.Euler(0f, RastgeleRotasyonY, 0f);

            int zombÝndis = Random.Range(0, ClonZombiler1.Length);
            GameObject nesne = Instantiate(ClonZombiler1[zombÝndis], new Vector3(xpozisyon, 0, zpozisyon), randomRotation);
            nesnesayisi++;
        }

        // InvokeRepeating("AzaltNesneSayisi", 1f, 0.5f);
    }
    IEnumerator dalgaTextKapat()
    {
        yield return new WaitForSeconds(3); //3 saniye bekler
        dalgatextobjesi.SetActive(false);
    }
    public void Dalga2()    //2.Dalga farklý özelliklere sahip rasgele 20 zombi
    {
        dalgaText.text = "2.Dalga Baþlýyor ... Dikatli olun evrim geçirmiþler artýk daha güçlüler !!!";
        dalgatextobjesi.SetActive(true);
        StartCoroutine(dalgaTextKapat());

        for (int i = 0; i < 20; i++)  //1.Dalga rasgele 10 zombi
        {
            float xpozisyon = 0f;
            float zpozisyon = 0f;
            int rastgeleBolge = Random.Range(1, 4);
            if (rastgeleBolge == 1) //1,bölge
            {
                xpozisyon = Random.Range(-111, -106);
                zpozisyon = Random.Range(9, 13);
            }
            else if (rastgeleBolge == 2) //2.bölge
            {
                xpozisyon = Random.Range(-111, -106);
                zpozisyon = Random.Range(-70, 6);
            }
            else if (rastgeleBolge == 3) //3.bölge
            {
                xpozisyon = Random.Range(-111, -90);
                zpozisyon = Random.Range(-20, -16);

            }

            float RastgeleRotasyonY = Random.Range(0, 360);
            Quaternion randomRotation = Quaternion.Euler(0f, RastgeleRotasyonY, 0f);

            int zombÝndis = Random.Range(0, ClonZombiler1.Length);
            GameObject nesne = Instantiate(ClonZombiler1[zombÝndis], new Vector3(xpozisyon, 0, zpozisyon), randomRotation);
            nesnesayisi++;
        }
    }
    public void Dalga3()    //3.Dalga farklý özelliklere sahip rasgele 30 zombi
    {
        dalgaText.text = "3.Dalga Baþlýyor ...  Dikatli olun evrim geçirmiþler artýk daha güçlüler !!!";
        dalgatextobjesi.SetActive(true);
        StartCoroutine(dalgaTextKapat());

        for (int i = 0; i < 40; i++)  //1.Dalga rasgele 10 zombi
        {
            float xpozisyon = 0f;
            float zpozisyon = 0f;
            int rastgeleBolge = Random.Range(1, 4);
            if (rastgeleBolge == 1) //1,bölge
            {
                xpozisyon = Random.Range(-111, -106);
                zpozisyon = Random.Range(9, 13);
            }
            else if (rastgeleBolge == 2) //2.bölge
            {
                xpozisyon = Random.Range(-111, -106);
                zpozisyon = Random.Range(-70, 6);
            }
            else if (rastgeleBolge == 3) //3.bölge
            {
                xpozisyon = Random.Range(-111, -90);
                zpozisyon = Random.Range(-20, -16);

            }

            float RastgeleRotasyonY = Random.Range(0, 360);
            Quaternion randomRotation = Quaternion.Euler(0f, RastgeleRotasyonY, 0f);

            int zombÝndis = Random.Range(0, ClonZombiler1.Length);
            GameObject nesne = Instantiate(ClonZombiler1[zombÝndis], new Vector3(xpozisyon, 0, zpozisyon), randomRotation);
            nesnesayisi++;
        }
    }
    void AzaltNesneSayisi()  //zombi ölü sayýlarýný tutabilmek için.
    {
        GameObject[] zombiNesneleri = GameObject.FindGameObjectsWithTag("Zombi"); // "Zombi" etiketine sahip nesneleri bul

        int aktifZombiSayisi = zombiNesneleri.Length;

       
        if (aktifZombiSayisi < nesnesayisi)
        {
            nesnesayisi = aktifZombiSayisi;
            olusayisi += 1;
            olu_text.text = "Ölü Sayisi:" + olusayisi.ToString();
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
    void Update()    // MermiText textine oyuncu nesnesin kompenentlerine ulaþýp  AtesEtme2 sýnýfýndan  GetSarjor() ve GetCephane() 
    {                //fonk çaðýrýp stinge döbüþtürerek atadýk

        MermiText.text = oyuncu.GetComponent<AtesEtme2>().GetSarjor().ToString() + "/" + oyuncu.GetComponent<AtesEtme2>().GetCephane().ToString();
        CanText.text = "Can :" + oyuncu.GetComponent<KarekterKontrol2>().GetKarekerCan().ToString();

        if (Input.GetKey(KeyCode.Escape)) // esc ye basýnca oyunu durdurur.
        {
            OyunuDurdur();
        }
       
        AzaltNesneSayisi();

      
    }

    public void HavaDurumuDegistir(int deger)
    {
        if (deger == 0) //default güneþli
        {
            yagmurSesi.SetActive(false);
        }
        else if (deger == 1)  //güneþli
        {
            yagmurEfekti.SetActive(false);
            yagmurSesi.SetActive(false);

        }
        else if (deger == 2) //yaðmurlu
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

        for (int i = 0; i < zombiNesneleri.Length; i++)   // oyun devam ederse " tüm zombilerin SESLERÝNÝ AÇMAK ÝÇÝN" 
        {
            AudioSource zombiSesi = zombiNesneleri[i].GetComponent<AudioSource>();
            if (zombiSesi != null)  // Zombi nesnesinde ses bileþeni varsa
            {
                zombiSesi.enabled = true;  // Zombi sesini etkinleþtir
            }
        }
        // Raycast iþlemini aktif hale getirir.
        Physics.queriesHitTriggers = true;
    }


    public void OyunuDurdur()
    {
        Time.timeScale = 0; //oyunu durdurur.
        OyunDurduPaneli.SetActive(true);  //devam et butonunu aktif yapar
        Cursor.lockState = CursorLockMode.None;// fare imlecini kilidini kaldýrýr.
        DurduKamerasi.SetActive(true);
        karekterKamerasi.SetActive(false);

        GameObject[] zombiNesneleri = GameObject.FindGameObjectsWithTag("Zombi"); // "Zombi" etiketine sahip nesneleri bul diziye at

        for (int i = 0; i < zombiNesneleri.Length; i++) //oyun durmuþsa " tüm zombilerin SESLERÝNÝ KESMEK ÝÇÝN" 
        {
            AudioSource zombiSesi = zombiNesneleri[i].GetComponent<AudioSource>();
            if (zombiSesi != null)  // Zombi nesnesinde ses bileþeni varsa
            {
                zombiSesi.enabled = false;  // Zombi sesini kapatýr
            }
        }
        // Raycast iþlemini devre dýþý býrakýr.
        Physics.queriesHitTriggers = false;
    }

    public void Home()
    {
        SceneManager.LoadScene("Menu");
        Cursor.lockState = CursorLockMode.None;// fare imlecini kilidini
    }
      
    public void Exit()
    {
        Application.Quit();  //uygulamadan çýkýþ yapar
    }
    
}

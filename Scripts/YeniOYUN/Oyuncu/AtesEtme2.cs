using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AtesEtme2 : MonoBehaviour
{
    
    public Camera camera;
    public LayerMask zombiKatman;
    Animator anim;
    public ParticleSystem Muzzleflash;
    public GameObject mermiefekti;

    KarekterKontrol2 nesne;

    private float Sarjor = 30;   
    private float Sarjorkapasitesi = 30;
    private float Cephane = 300;

    RaycastHit hit;  //ate� etmek i�in ���n
    float GunTimer;
    public float TaramaHizi;
    public float Menzil;

    AudioSource seskaynak;
    public AudioClip ak47AtesSesi;
    public AudioClip m16AtesSesi;
    public AudioClip ump45AtesSesi;
    public AudioClip MermiToplamaSesi;
    public AudioClip SarjorDegistirSesi;
    public AudioClip SilahDegistirmeSesi;

    RaycastHit hit2;  //mermi toplamak i�in ���n
    public float mermiKutusuMesafesi;
    public GameObject crosshair;
    public GameObject Etext;

    public GameObject ak47;
    public GameObject m16a4;
    public GameObject ump45;

    

    void Start()
    {
        
        nesne = gameObject.GetComponent<KarekterKontrol2>();
        anim = gameObject.GetComponent<Animator>();
        seskaynak= gameObject.GetComponent<AudioSource>();
    }


    void Update()
    {
        if (nesne.hayattaMi == true)
        {
            MermiTopla();
            SliahDegistir();

            if (Input.GetMouseButton(0) && Time.time > GunTimer) //mause sol t�k bas�ld���nda
            {   
                if (Sarjor>0)  //sarjorda mermi varsa
                {
                    GunTimer = Time.time + TaramaHizi;
                    AtesEtme();
                    anim.SetBool("atesEt", true);  //atesEt animasyonu aktif ediyoruz animasyonda AtesEtme fonksinu zaten �a�r�ld��� i�in burada �a��rmaya gerek yok
                    Instantiate(mermiefekti, hit.point, Quaternion.LookRotation(hit.normal)); //mermi izi efekti
                    seskaynak.Play();
                    if (ak47.activeSelf)//ak47 secili ise true dondurur
                    {
                        seskaynak.clip = ak47AtesSesi;  //ates sesi ak47AtesSesi yap�yoruz
                    }
                    else if (m16a4.activeSelf)
                    {
                        seskaynak.clip = m16AtesSesi;
                    }
                    else if (ump45.activeSelf)
                    {
                        seskaynak.clip = ump45AtesSesi;
                    }
                    
                }
                if(Sarjor<=0)  //�arjor bo� ise
                {
                    anim.SetBool("atesEt", false); //animasyonu durdurur.                  
                }                              
            } 
            
            if (Input.GetMouseButtonUp(0)) //mause sol t�k kald�r�ld���nda
            {
                anim.SetBool("atesEt", false); //animasyonu durdurur.
            }

            if (Sarjor < Sarjorkapasitesi  && Cephane > 0 ) // sarjor da mermi azalm��sa ve cephane de mermi varsa
            {
                if (Input.GetKeyDown(KeyCode.R))  //R tu�una bas�l�rsa  SarjorDegistirme animasyonu �al��t�r  bu animasyonun i�inde
                {                                 //SarjorDegistirme() fonksiyonu �a��r�l�r.
                    anim.SetBool("SarjorDegistirme", true);
                }

            }
            if ( Sarjor <= 0 &&  Cephane > 0 ) // sarjor da mermi bittiyse ve cephane de mermi varsa
            {
                anim.SetBool("SarjorDegistirme", true);
            }

            
        }     
    }
    public void SliahDegistir() //silah de�i�tirme i�lemleri
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            seskaynak.PlayOneShot(SilahDegistirmeSesi);//1 kere sesi �alar

            if (ak47.activeSelf)// ak47 se�ili ise yap�lacaklar
            {               
                m16a4.SetActive(true);
                ump45.SetActive(false);
                ak47.SetActive(false);               
            }
            else if (m16a4.activeSelf)
            {               
                m16a4.SetActive(false);
                ump45.SetActive(true);
                ak47.SetActive(false);                
            }
            else if (ump45.activeSelf)
            {
                m16a4.SetActive(false);
                ump45.SetActive(false);
                ak47.SetActive(true);             
            }
        }
    }
    public void MermiTopla()   //yerden mermi toplamak i�in
    {
        if (Physics.Raycast(Camera.main.gameObject.transform.position, Camera.main.gameObject.transform.forward, out hit2, mermiKutusuMesafesi))
        {
            if ( hit2.collider.gameObject.CompareTag("MermiKutusu")) // �arpt��� nesnenin tag'� "MermiKutusu" ise
            {
                crosshair.GetComponent<Image>().material.color = Color.red;  //crosshair rengini de�i�tirir
                Etext.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))  // e tusuna bas�ld�ysa  cephane art�r�r ve o nesneyi siler.
                {
                    Cephane += 100;
                    seskaynak.PlayOneShot(MermiToplamaSesi);
                    Destroy(hit2.collider.gameObject);
                }
            }
            else // �arpt��� nesnenin tag'� "MermiKutusu" de�il ise
            {
                crosshair.GetComponent<Image>().material.color = Color.white;
                Etext.SetActive(false);
            }
        }
    }


    public void AtesEtme()
    {
        if (Sarjor>0)
        {
            //
            
            //seskaynak.PlayOneShot(AtesSesi); //seskaynag� audiosource undaki AtesSesini 1 kere �alar
           // Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // ���n�n y�n�n�  kameran�n orta noktas�nda (croshair)  noktas�nda g�sterir

           // RaycastHit hit;   // ���n vuru�u

            //if (Physics.Raycast(ray, out hit, 100f, zombiKatman))  // (���n , ��k�� vuru�u hedef  , mesafe (mathf.infinity =sonsuz)  , hangi katman (nesne) )
            //{

            //    hit.collider.gameObject.GetComponent<zombi>().HasarAl(); // ���n vuru�unun �arpt��� nesnenin collider�n�n  gameobject 'inden compenentlerine ula�t�k 
            //                                                            // zombi s�n�f�n�n HasarAl fonk �a��rd�k
            //}
            if (Physics.Raycast(Camera.main.gameObject.transform.position, Camera.main.gameObject.transform.forward, out hit, Menzil, zombiKatman))
            {
                Muzzleflash.Play();
                hit.collider.gameObject.GetComponent<zombi>().HasarAl(); // ���n vuru�unun �arpt��� nesnenin collider�n�n  gameobject 'inden compenentlerine ula�t�k 
                                                                         // zombi s�n�f�n�n HasarAl fonk �a��rd�k
                Debug.Log(hit.transform.name);
            }

            Sarjor--;
        }       
    }
    public void SarjorDegistirme()
    {      
        Cephane -= Sarjorkapasitesi - Sarjor;
        Sarjor = Sarjorkapasitesi;
        anim.SetBool("SarjorDegistirme", false);
    }
    public void SarjorDegistirmeSesiPlay()
    {
        seskaynak.PlayOneShot(SarjorDegistirSesi); //�arjor de�i�tirme animasyonu i�inde �a��r�l�r.      
    }

    public float GetSarjor()  // private de�i�kenleri ba�ka s�n�flardan  �a��r�p kullanabilmek i�in 
    {
        return Sarjor;
    }
    public float GetCephane()// private de�i�kenleri ba�ka s�n�flardan  �a��r�p kullanabilmek i�in
    {
        return Cephane;
    }


}

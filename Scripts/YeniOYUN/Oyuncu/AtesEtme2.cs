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

    RaycastHit hit;  //ateþ etmek için ýþýn
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

    RaycastHit hit2;  //mermi toplamak için ýþýn
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

            if (Input.GetMouseButton(0) && Time.time > GunTimer) //mause sol týk basýldýðýnda
            {   
                if (Sarjor>0)  //sarjorda mermi varsa
                {
                    GunTimer = Time.time + TaramaHizi;
                    AtesEtme();
                    anim.SetBool("atesEt", true);  //atesEt animasyonu aktif ediyoruz animasyonda AtesEtme fonksinu zaten çaýrýldýðý için burada çaðýrmaya gerek yok
                    Instantiate(mermiefekti, hit.point, Quaternion.LookRotation(hit.normal)); //mermi izi efekti
                    seskaynak.Play();
                    if (ak47.activeSelf)//ak47 secili ise true dondurur
                    {
                        seskaynak.clip = ak47AtesSesi;  //ates sesi ak47AtesSesi yapýyoruz
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
                if(Sarjor<=0)  //þarjor boþ ise
                {
                    anim.SetBool("atesEt", false); //animasyonu durdurur.                  
                }                              
            } 
            
            if (Input.GetMouseButtonUp(0)) //mause sol týk kaldýrýldýðýnda
            {
                anim.SetBool("atesEt", false); //animasyonu durdurur.
            }

            if (Sarjor < Sarjorkapasitesi  && Cephane > 0 ) // sarjor da mermi azalmýþsa ve cephane de mermi varsa
            {
                if (Input.GetKeyDown(KeyCode.R))  //R tuþuna basýlýrsa  SarjorDegistirme animasyonu çalýþtýr  bu animasyonun içinde
                {                                 //SarjorDegistirme() fonksiyonu çaðýrýlýr.
                    anim.SetBool("SarjorDegistirme", true);
                }

            }
            if ( Sarjor <= 0 &&  Cephane > 0 ) // sarjor da mermi bittiyse ve cephane de mermi varsa
            {
                anim.SetBool("SarjorDegistirme", true);
            }

            
        }     
    }
    public void SliahDegistir() //silah deðiþtirme iþlemleri
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            seskaynak.PlayOneShot(SilahDegistirmeSesi);//1 kere sesi çalar

            if (ak47.activeSelf)// ak47 seçili ise yapýlacaklar
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
    public void MermiTopla()   //yerden mermi toplamak için
    {
        if (Physics.Raycast(Camera.main.gameObject.transform.position, Camera.main.gameObject.transform.forward, out hit2, mermiKutusuMesafesi))
        {
            if ( hit2.collider.gameObject.CompareTag("MermiKutusu")) // Çarptýðý nesnenin tag'ý "MermiKutusu" ise
            {
                crosshair.GetComponent<Image>().material.color = Color.red;  //crosshair rengini deðiþtirir
                Etext.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))  // e tusuna basýldýysa  cephane artýrýr ve o nesneyi siler.
                {
                    Cephane += 100;
                    seskaynak.PlayOneShot(MermiToplamaSesi);
                    Destroy(hit2.collider.gameObject);
                }
            }
            else // Çarptýðý nesnenin tag'ý "MermiKutusu" deðil ise
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
            
            //seskaynak.PlayOneShot(AtesSesi); //seskaynagý audiosource undaki AtesSesini 1 kere çalar
           // Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // ýþýnýn yönünü  kameranýn orta noktasýnda (croshair)  noktasýnda gösterir

           // RaycastHit hit;   // ýþýn vuruþu

            //if (Physics.Raycast(ray, out hit, 100f, zombiKatman))  // (ýþýn , çýkýþ vuruþu hedef  , mesafe (mathf.infinity =sonsuz)  , hangi katman (nesne) )
            //{

            //    hit.collider.gameObject.GetComponent<zombi>().HasarAl(); // ýþýn vuruþunun çarptýðý nesnenin colliderýnýn  gameobject 'inden compenentlerine ulaþtýk 
            //                                                            // zombi sýnýfýnýn HasarAl fonk çaðýrdýk
            //}
            if (Physics.Raycast(Camera.main.gameObject.transform.position, Camera.main.gameObject.transform.forward, out hit, Menzil, zombiKatman))
            {
                Muzzleflash.Play();
                hit.collider.gameObject.GetComponent<zombi>().HasarAl(); // ýþýn vuruþunun çarptýðý nesnenin colliderýnýn  gameobject 'inden compenentlerine ulaþtýk 
                                                                         // zombi sýnýfýnýn HasarAl fonk çaðýrdýk
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
        seskaynak.PlayOneShot(SarjorDegistirSesi); //þarjor deðiþtirme animasyonu içinde çaðýrýlýr.      
    }

    public float GetSarjor()  // private deðiþkenleri baþka sýnýflardan  çaðýrýp kullanabilmek için 
    {
        return Sarjor;
    }
    public float GetCephane()// private deðiþkenleri baþka sýnýflardan  çaðýrýp kullanabilmek için
    {
        return Cephane;
    }


}

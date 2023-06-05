using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;  //yapay zeka k�t�phanesi  mesh e g�re nesne takip i�in 
using TMPro;

public class zombi : MonoBehaviour
{
    public float Zombi_Can = 100f;
    public Animator anim;
    public bool zombiOldu;


    GameObject hedefOyuncu;
 // public GameObject hedefOyuncu; //Ayn� �ey
    public float mesafe;
    public float KovalamaMesafesi;
    public float SaldirmaMesafesi;
    NavMeshAgent zombiNavmesh;

    KarekterKontrol2 nesne;
    AudioSource seskaynak;
    public AudioClip hasarversesi;


    void Start()
    {
        anim = this.GetComponent<Animator>();
        hedefOyuncu = GameObject.Find("character");  // hedefoyuncu yu public olarak tan�mlayabilirdik  ve burda ekstradan nesneyi tan�tmayabilirdik
                                                     //ama her zombi nesnesi i�in hedefoyuncu(character) imizi atamam�z gerekecekti
        zombiNavmesh = this.GetComponent<NavMeshAgent>(); //gezinme a�� arac� atad�k  nesne takibi i�in
     // nesne = this.gameObject.GetComponent<KarekterKontrol2>(); // her iki s�n�fda ayn� obje i�inde ise
        nesne = GameObject.Find("character").GetComponent<KarekterKontrol2>();//her iki s�n�f farkl� objelerde ise find ie arat�p compenentlerine ula��r�z.
        seskaynak = gameObject.GetComponent<AudioSource>();
    }


    void Update()
    {


        if (Zombi_Can <= 0)
        {
            zombiOldu = true;
        }

        if (zombiOldu == true)
        {
            anim.SetBool("die", true);         
            StartCoroutine(Yoketme());  //zombi old�kten sonra yok etme fonk �a�r�s�        
        }
        else     //zombi ya��yorsa  yap�lacakalar
        {
            
            if (nesne.hayattaMi == true) //oyuncu hayatta ise
            {
                mesafe = Vector3.Distance(this.transform.position, hedefOyuncu.transform.position);

                if (mesafe <= KovalamaMesafesi) // mesafe kovalama mesafesine k���k e�it old. zaman
                {

                    zombiNavmesh.SetDestination(hedefOyuncu.transform.position);  //hedef oyuncuyu pozisyonunu navmeshe atad�k hedef oyuncuyu takip edecek
                    zombiNavmesh.isStopped = false;      // takip durdurma kapal�
                    anim.SetBool("run", true); //y�r�me animasyonu               
                    this.transform.LookAt(hedefOyuncu.transform.position); //zombi nesnesinin  hedefe bakmas�n� sa�lar.hedefe do�ru d�ner.
                }
                else
                {
                    zombiNavmesh.isStopped = true;  // takip durdurur
                    anim.SetBool("run", false); //durma animasyonu
                    anim.SetBool("sald�r�", false);
                }

                if (mesafe <= SaldirmaMesafesi)
                {
                    zombiNavmesh.isStopped = true;
                    anim.SetBool("run", false);//vurma animasyonu
                    anim.SetBool("sald�r�", true);
                    this.transform.LookAt(hedefOyuncu.transform.position); //zombi nesnesinin  hedefe bakmas�n� sa�lar.hedefe do�ru d�ner.
                    
                }
            }
            else  //zombi hayatta ve oyuncu �ldu ise 
            {
                StartCoroutine(YemeAnimasyunu());  //yeme animasyonu �a�r�r
            }


        }
    }
   
    IEnumerator YemeAnimasyunu()
    {
        yield return new WaitForSeconds(3); //3 saniye bekler
        anim.SetBool("oyuncuYeme", true);

    }
    IEnumerator Yoketme()
    {
        yield return new WaitForSeconds(5); //5 saniye bekler
        Destroy(this.gameObject);       // bu nesneyi(zombiyi) siler
    }
   
    public void HasarAl()
    {
        Zombi_Can -= Random.Range(15, 25);      
    }
    public void HasarVer()
    {
        hedefOyuncu.GetComponent<KarekterKontrol2>().HasarAL();
    }
    public void HasarVerSes() // sald�rma animasyonunda bu fonk �a��r�l�r.
    {
        seskaynak.PlayOneShot(hasarversesi);
    }
}

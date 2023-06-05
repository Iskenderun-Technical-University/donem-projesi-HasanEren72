using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KarekterKontrol2 : MonoBehaviour
{
    public Animator anim;
    // public CharacterController controller;//

    [SerializeField]
    private float Speed = 5f, FastSpeed = 10f;
    private float KarekerCan = 100;

    public Transform groundCheck;  // zemin kontrol� i�in
    public float groundDistance = 0.4f; // yer mesafesi
    public LayerMask groundMask;   // zemin maskesi   zemini ay�rt etmek i�in

    public float ZiplamaUzunlugu = 3f;   //z�plama y�ksekli�i
    bool isGrounded; // toprak m�  kontorl i�in

    public bool hayattaMi=true;
    public GameObject olduKamerasi;
    public GameObject karekterKamerasi;

    

   
    void Start()
    {
        anim = GetComponent<Animator>();
        //controller = GetComponent<CharacterController>();
        hayattaMi = true;
        
    }
    IEnumerator OlduKameras�()
    {
        yield return new WaitForSeconds(2);
        olduKamerasi.SetActive(true);
        karekterKamerasi.SetActive(false);
        yield return new WaitForSeconds(2); //2 saniye bekler
        Time.timeScale = 1.0f; //zaman� ba�lat�r
        SceneManager.LoadScene("Game_Over"); //game over sahnesini a�ar
    }

    void Update()
    {
        if (KarekerCan <= 0)  //karekter �ld� ise
        {
            KarekerCan = 0;
            hayattaMi = false;
            anim.SetBool("die", true);
            Cursor.lockState = CursorLockMode.None;// fare imlecini kilidini kald�r�r.
            StartCoroutine(OlduKameras�());
            
        }

        if (hayattaMi == true)
        {
            Animsayon_Hareket();
            Hareket();
            Ziplama();


        }
       
    }
   
    void Animsayon_Hareket()
    {
        float yatay = Input.GetAxis("Horizontal");
        float dikey = Input.GetAxis("Vertical");

        anim.SetFloat("Horizontal", yatay);
        anim.SetFloat("Vertical", dikey);

        if (Input.GetKey(KeyCode.LeftControl))
        {
            anim.SetBool("crouch", true);                    
        }
        else
        {
            anim.SetBool("crouch", false);
        }

    }
    void Hareket()
    {
        float yatay = Input.GetAxis("Horizontal") * Time.deltaTime;
        float dikey = Input.GetAxis("Vertical") * Time.deltaTime;

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(yatay * FastSpeed, 0, dikey * FastSpeed);
            anim.SetBool("fastrun", true);           
        }
        else
        {
            transform.Translate(yatay * Speed, 0, dikey * Speed);
            anim.SetBool("fastrun", false);           
        }

        //Vector3 move = transform.right * yatay + transform.forward * dikey;
        //controller.Move(move);
    }
    void Ziplama()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //groundCheck pozisyonuna dayal� bir k�re yaratacakt�r 
                                                                                            //yer mesafesi ve zemin maskesi de eklenir
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            transform.Translate(Vector3.up * ZiplamaUzunlugu);
            // transform.Translate(0 , 1* ZiplamaUzunlugu , 0); ayn�s�
            anim.SetBool("jump", true);
        }
        else
        {
            anim.SetBool("jump", false);
        }
    }
    public void HasarAL()
    {
        KarekerCan -= Random.Range(5, 15);
    }
    public float GetKarekerCan()  // private de�i�kenleri ba�ka s�n�flardan  �a��r�p kullanabilmek i�in 
    {                             //public fonk olu�turup private de�i�keni return ettik.
        return KarekerCan;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.layer==LayerMask.NameToLayer("ground"))
    //    {
    //        Yurmusesi.SetActive(true);
    //    }
    //}
}

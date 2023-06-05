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

    public Transform groundCheck;  // zemin kontrolü için
    public float groundDistance = 0.4f; // yer mesafesi
    public LayerMask groundMask;   // zemin maskesi   zemini ayýrt etmek için

    public float ZiplamaUzunlugu = 3f;   //zýplama yüksekliði
    bool isGrounded; // toprak mý  kontorl için

    public bool hayattaMi=true;
    public GameObject olduKamerasi;
    public GameObject karekterKamerasi;

    

   
    void Start()
    {
        anim = GetComponent<Animator>();
        //controller = GetComponent<CharacterController>();
        hayattaMi = true;
        
    }
    IEnumerator OlduKamerasý()
    {
        yield return new WaitForSeconds(2);
        olduKamerasi.SetActive(true);
        karekterKamerasi.SetActive(false);
        yield return new WaitForSeconds(2); //2 saniye bekler
        Time.timeScale = 1.0f; //zamaný baþlatýr
        SceneManager.LoadScene("Game_Over"); //game over sahnesini açar
    }

    void Update()
    {
        if (KarekerCan <= 0)  //karekter öldü ise
        {
            KarekerCan = 0;
            hayattaMi = false;
            anim.SetBool("die", true);
            Cursor.lockState = CursorLockMode.None;// fare imlecini kilidini kaldýrýr.
            StartCoroutine(OlduKamerasý());
            
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
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //groundCheck pozisyonuna dayalý bir küre yaratacaktýr 
                                                                                            //yer mesafesi ve zemin maskesi de eklenir
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            transform.Translate(Vector3.up * ZiplamaUzunlugu);
            // transform.Translate(0 , 1* ZiplamaUzunlugu , 0); aynýsý
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
    public float GetKarekerCan()  // private deðiþkenleri baþka sýnýflardan  çaðýrýp kullanabilmek için 
    {                             //public fonk oluþturup private deðiþkeni return ettik.
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

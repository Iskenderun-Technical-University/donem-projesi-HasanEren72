using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class Menu_paneli : MonoBehaviour
{
   
  
    public  AudioSource ses;

    public TextMeshProUGUI puan, toplam_altin ,elmas  ,satinalmamesaj1, satinalmamesaj2, satinalmamesaj3 ,altin1_Mesaj, altin2_Mesaj,elmas_mesaj;
    public TextMeshProUGUI kullaniciadi_txt_hosgeldin;
    public TextMeshProUGUI market_btn ,Cevirme_btn;
    public GameObject marketpaneli,Conver_Paneli;
    public GameObject kareker1_satinAlmaPaneli, kareker2_satinAlmaPaneli, kareker3_satinAlmaPaneli;
    public GameObject altin1_satinalmapaneli, altin2_satinalmapaneli, elmas_satinalmapaneli;

    public GameObject kullan_btn1 , kullan_btn2, kullan_btn3;
    public GameObject satýnAlma_sesi;

    public string kuladi;
    public string sifre;

    public int  toplamAltin,toplamElmas;

    public static bool bolum1;

    public static bool karekter1_buy , karekter2_buy, karekter3_buy;

    // string karekter1_satinalindi, karekter2_satinalindi, karekter3_satinalindi; // kullan btn aktiflik için
    string karekter_1satnalindik, karekter_2satnalindik, karekter_3satnalindik;
    public void Start()
    {

        satýnAlma_sesi.SetActive(false);

        kullan_btn1.SetActive(false);
        kullan_btn2.SetActive(false);
        kullan_btn3.SetActive(false);
 

        if (PlayerPrefs.GetString("karekter1_kullanbtn")=="aktif1")
        {   
            kullan_btn1.SetActive(true);         
        }
        
        if (PlayerPrefs.GetString("karekter2_kullanbtn") == "aktif2")
        {
            kullan_btn2.SetActive(true);

        }
       
        if (PlayerPrefs.GetString("karekter3_kullanbtn") == "aktif3")
        {
            kullan_btn3.SetActive(true);

        }
        else
        {
            kullan_btn1.SetActive(false);
            kullan_btn2.SetActive(false);
            kullan_btn3.SetActive(false);
            PlayerPrefs.DeleteKey("karekter1_kullanbtn");
            PlayerPrefs.DeleteKey("karekter2_kullanbtn");
            PlayerPrefs.DeleteKey("karekter3_kullanbtn");
        }


        // Veriyi al
        kuladi = Login.V_kullanici_degeri;
        sifre = Login.V_sifre_degeri;

        PlayerPrefs.DeleteKey("altin_sayisi_verisi"); // deðerleri siler 
        PlayerPrefs.DeleteKey("puan_verisi");
        PlayerPrefs.DeleteKey("elmas_verisi");
       
        // puan.text = kuladi;
        Debug.Log(kuladi);
        Debug.Log(sifre);
        kullaniciadi_txt_hosgeldin.text = kuladi;

        //skor iþlemleri skor çekme iþlemi
        StartCoroutine(puan_cekme());
        StartCoroutine(toplam_altin_cekme());
        StartCoroutine(toplam_elmas_cekme());

    }
    public void satýnAlma_sesiAktiflik()
    {
        satýnAlma_sesi.SetActive(false);
    }
    public void karekter1_satin_AL()
    {
        kareker1_satinAlmaPaneli.SetActive(true); // karekter1 satýnalma paneli açar
        kareker2_satinAlmaPaneli.SetActive(false);
        kareker3_satinAlmaPaneli.SetActive(false);
    }
    
    public void karekter2_satin_AL()
    {
        kareker2_satinAlmaPaneli.SetActive(true);// karekter2 satýnalma paneli açar
        kareker1_satinAlmaPaneli.SetActive(false);
        kareker3_satinAlmaPaneli.SetActive(false);
    }
    public void karekter3_satin_AL()
    {
        kareker3_satinAlmaPaneli.SetActive(true); // karekter3 satýnalma paneli açar
        kareker2_satinAlmaPaneli.SetActive(false);
        kareker1_satinAlmaPaneli.SetActive(false);
    }

    public void karekter1_Kullanbtn_Aktiflik()  //kullan1btn aktif eder
    {
        kullan_btn1.SetActive(true);
        karekter_1satnalindik = "aktif1";
        PlayerPrefs.SetString("karekter1_kullanbtn", karekter_1satnalindik);    
    }
    public void Kullan_btn() //butona basýnca karekter seçer
    {
        karekter1_buy = true;

        karekter2_buy = false;
        karekter3_buy = false;

        satinalmamesaj1.text = "*** Karekter seçildi ***";
    }
    public void karekter2_Kullanbtn_Aktiflik()//kullan2btn aktif eder
    {

        kullan_btn2.SetActive(true);
        karekter_2satnalindik = "aktif2";
        PlayerPrefs.SetString("karekter2_kullanbtn", karekter_2satnalindik);
      
    }
    public void Kullan_btn2()//butona basýnca karekter seçer
    {
        karekter2_buy = true;

        karekter1_buy = false;  
        karekter3_buy = false;
        satinalmamesaj2.text = "*** Karekter seçildi ***";
    }
    public void karekter3_Kullanbtn_Aktiflik()//kullan3btn aktif eder
    {
        kullan_btn3.SetActive(true);
        karekter_3satnalindik = "aktif3";
        PlayerPrefs.SetString("karekter3_kullanbtn", karekter_3satnalindik);
     
    }
    public void Kullan_btn3() //butona basýnca karekter seçer
    {
        karekter3_buy = true;

        karekter1_buy = false;
        karekter2_buy = false;
        satinalmamesaj3.text = "*** Karekter seçildi ***";
    }
    public void hayir_btn()
    {
        kareker1_satinAlmaPaneli.SetActive(false); 
        kareker2_satinAlmaPaneli.SetActive(false);
        kareker3_satinAlmaPaneli.SetActive(false);

    }
    public void karekterpanelleri_kapat()
    {
        kareker1_satinAlmaPaneli.SetActive(false);
        kareker2_satinAlmaPaneli.SetActive(false);
        kareker3_satinAlmaPaneli.SetActive(false);

        altin1_satinalmapaneli.SetActive(false);
        altin2_satinalmapaneli.SetActive(false);
        elmas_satinalmapaneli.SetActive(false);
    }

    public void karekter1_evet_btn()
    {

        if (toplamAltin >= 5000)
        {

            StartCoroutine(AltinGuncelleme_ekleme());

            satýnAlma_sesi.SetActive(true);

            Invoke("satýnAlma_sesiAktiflik()", 0.8f);

            karekter1_Kullanbtn_Aktiflik();

            satinalmamesaj1.text = "ak47 silahý Satýn Alýndý .";
            Debug.Log("*** ak47 satin alindi ***");
        }
        else
        {
            satinalmamesaj1.text = "ak47 satýn alma baþarýsýz ! altýnýnýz yetersiz!";
            Debug.Log(" silah satýn alma baþarýsýz ! altýnýnýz yetersiz!");
        }

        // karekter2_buy = false;

    }
    public void karekter2_evet_btn()
    {
        if (toplamElmas >= 200)
        {

            StartCoroutine(ElmasGuncelleme_ekleme());


            satýnAlma_sesi.SetActive(true);

            Invoke("satýnAlma_sesiAktiflik()", 0.8f);

            karekter2_Kullanbtn_Aktiflik();

            satinalmamesaj2.text = "m416 silahý Satýn Alýndý .";
            Debug.Log("*** m416 satin alindi ***");
        }
        else
        {
            satinalmamesaj2.text = "m416 satýn alma baþarýsýz ! altýnýnýz yetersiz!";
            Debug.Log(" m416 satýn alma baþarýsýz ! altýnýnýz yetersiz!");
        }

    }

    public void karekter3_evet_btn()
    {
        if (toplamAltin >= 7000)
        {

            StartCoroutine(AltinGuncelleme_ekleme());


            satýnAlma_sesi.SetActive(true);

            Invoke("satýnAlma_sesiAktiflik()", 0.8f);

            karekter3_Kullanbtn_Aktiflik();

            satinalmamesaj3.text = "m16a4 silah Satýn Alýndý .";
            Debug.Log("*** m16a4 satin alindi ***");
        }
        else
        {
            satinalmamesaj3.text = "m16a4 satýn alma baþarýsýz ! altýnýnýz yetersiz!";
            Debug.Log(" m16a4 satýn alma baþarýsýz ! altýnýnýz yetersiz!");
        }

    }

    public void altina_cevir1_evet_btn()
    {

        if (toplamElmas >= 500)
        {

           StartCoroutine(donusturme1_altin_elmas_guncelleme());

            satýnAlma_sesi.SetActive(true);

            Invoke("satýnAlma_sesiAktiflik()", 0.4f);



            altin1_Mesaj.text = "Dönüþtürme baþarýlý ";
            Debug.Log("*** Dönüþtürme1 baþarýlý ***");
        }
        else
        {
            altin1_Mesaj.text = "Dönüþtürme baþarýsýz ! elmasýnýz yetersiz!";
            Debug.Log(" Dönüþtürme1 baþarýsýz ! elmasýnýz yetersiz!");
        }

    }
    public void altina_cevir2_evet_btn()
    {

        if (toplamElmas >= 100)
        {

            StartCoroutine(donusturme2_altin_elmas_guncelleme());

            satýnAlma_sesi.SetActive(true);

            Invoke("satýnAlma_sesiAktiflik()", 0.4f);



            altin2_Mesaj.text = "Dönüþtürme baþarýlý ";
            Debug.Log("*** Dönüþtürme2 baþarýlý ***");
        }
        else
        {
            altin2_Mesaj.text = "Dönüþtürme baþarýsýz ! elmasýnýz yetersiz!";
            Debug.Log(" Dönüþtürme2 baþarýsýz ! elmasýnýz yetersiz!");
        }

    }
    public void elmasa_cevir1_evet_btn()
    {

        if (toplamAltin >= 5000)
        {

            StartCoroutine(Donusturme3_altin_elmas_guncelleme());

            satýnAlma_sesi.SetActive(true);

            Invoke("satýnAlma_sesiAktiflik()", 0.4f);



            elmas_mesaj.text = "Dönüþtürme baþarýlý ";
            Debug.Log("*** Dönüþtürme3 baþarýlý ***");
        }
        else
        {
            elmas_mesaj.text = "Dönüþtürme baþarýsýz ! altinýnýz yetersiz!";
            Debug.Log(" Dönüþtürme3 baþarýsýz ! altýnýnýz yetersiz!");
        }

    }

    IEnumerator donusturme1_altin_elmas_guncelleme() 
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "donusturme1_altin_elmas_guncelleme");
        form.AddField("kullaniciAdi", kuladi);
        form.AddField("sifre", sifre);

        form.AddField("Dusecekelmasmiktari", 500);


        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/user.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("donusturme1 sonucu :" + www.downloadHandler.text);

            }
        }
    }

    IEnumerator donusturme2_altin_elmas_guncelleme()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "donusturme2_altin_elmas_guncelleme");
        form.AddField("kullaniciAdi", kuladi);
        form.AddField("sifre", sifre);

        form.AddField("Dusecekelmasmiktari", 100);


        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/user.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("donusturme2 sonucu :" + www.downloadHandler.text);

            }
        }
    }


    IEnumerator Donusturme3_altin_elmas_guncelleme()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "donusturme3_altin_elmas_guncelleme");
        form.AddField("kullaniciAdi", kuladi);
        form.AddField("sifre", sifre);

        form.AddField("Dusecek_Altin_miktari", 5000);


        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/user.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("donusturme3 sonucu :" + www.downloadHandler.text);

            }
        }
    }



    public void cevirme_Paneli_AC_btn()
    {
        Conver_Paneli.SetActive(true);
     
    }
    public void Karekter_Paneli_AC_btn()
    {
        marketpaneli.SetActive(true);
        Conver_Paneli.SetActive(false);
    }

    void Update()
    {
        StartCoroutine(puan_cekme());
        StartCoroutine(toplam_altin_cekme());
        StartCoroutine(toplam_elmas_cekme());

    }

    public void market_paneli()
    {       
         market_btn.color = Color.red;
         marketpaneli.SetActive(true);       
    }
    public void market_paneli_kapat()
    {
        marketpaneli.SetActive(false);
        Conver_Paneli.SetActive(false);
        
        market_btn.color = Color.yellow;
     
    }
   

    IEnumerator puan_cekme()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "Puan_cekme");
        form.AddField("kullaniciAdi", kuladi);
        form.AddField("sifre", sifre);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/user.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
               // Debug.Log("Sorgu Sonucu:" + www.downloadHandler.text);
                string a = www.downloadHandler.text;
                if (a!= "baþarýsýz")
                {
                    puan.text = a.ToString();
                }

               
                
            }
        }
    }

    IEnumerator toplam_altin_cekme()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "Altin_cekme");
        form.AddField("kullaniciAdi", kuladi);
        form.AddField("sifre", sifre);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/user.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
               // Debug.Log("Sorgu Sonucu:" + www.downloadHandler.text);
                string  b = www.downloadHandler.text;
                if (b!= "baþarýsýz")
                {
                    toplam_altin.text = b.ToString();
                    toplamAltin = Convert.ToInt32(b); // toplanan altin deðeri  satýn alma panelinde kullanmak için

                }

                

            }
        }
    }

    IEnumerator toplam_elmas_cekme()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "Elmas_cekme");
        form.AddField("kullaniciAdi", kuladi);
        form.AddField("sifre", sifre);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/user.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
               // Debug.Log("Sorgu Sonucu:" + www.downloadHandler.text);
                string c = www.downloadHandler.text;
                if (c!= "baþarýsýz")
                {
                    elmas.text = c.ToString();


                    toplamElmas = Convert.ToInt32(c);
                }

                
                
            }
        }
    }

    IEnumerator AltinGuncelleme_ekleme() // Karekter satýn alýndýktan sonra top altýn günceller
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "AltinGuncelleme_ekleme");
        form.AddField("kullaniciAdi", kuladi);
        form.AddField("sifre", sifre);
      
        form.AddField("DusecekAltinmiktari", 5000);
     

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/user.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Karekter satin alma sonucu :" + www.downloadHandler.text);
    
            }
        }
    }


    IEnumerator ElmasGuncelleme_ekleme() // Karekter satýn alýndýktan sonra top elmas günceller
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "ElmasGuncelleme_ekleme");
        form.AddField("kullaniciAdi", kuladi);
        form.AddField("sifre", sifre);

        form.AddField("DusecekElmas_miktari", 200);


        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/user.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Karekter satin alma sonucu :" + www.downloadHandler.text);
              


            }
        }
    }
  

    public void bolum1_basla()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Bolum1");
        bolum1 = true;
        Cursor.lockState = CursorLockMode.Locked;// fare imlecini kilidini
    }
    public void bolum2_basla()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("bolum2");
        bolum1 = false;
        Cursor.lockState = CursorLockMode.Locked;// fare imlecini kilidini
    }
    public void loginegit()
    {
        SceneManager.LoadScene("login_scene");
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void ses_kapat()
    {
        if (ses.enabled==true)
        {
            ses.enabled = false;
          
        }
        else
        {
            ses.enabled = true;

        }
       
    }


    public void gold1_satinal_btn()
    {
        altin1_satinalmapaneli.SetActive(true);
    }
    public void gold2_satinal_btn()
    {
        altin2_satinalmapaneli.SetActive(true);
    }
    public void elmas_satinal_btn()
    {
       elmas_satinalmapaneli.SetActive(true);
    }
}

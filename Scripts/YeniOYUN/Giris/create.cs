using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class create : MonoBehaviour
{

    public TMP_InputField kullaniciAdi_IF, sifre_IF, sifreTekrar_IF;
    public Toggle sozlesme;

    panel_gecis pK_Script;

    public static  bool kayitoldu;

    void Start()
    {
         kayitoldu = false;
         pK_Script = GetComponent<panel_gecis>();
    }

    
    void Update()
    {

    }


    public void uyeligiOlustur_Buton()
    {
        if (kullaniciAdi_IF.text.Equals("") || sifre_IF.text.Equals("") || sifreTekrar_IF.text.Equals(""))
        {
            StartCoroutine(pK_Script.hataPanel("Bo� BIRAKMAYINIZ!"));

        }
        else
        {
             
            if (sifre_IF.text.Equals(sifreTekrar_IF.text))
            {
                if (sozlesme.isOn)
                {
                   

                    Debug.Log("Veritaban� Ba�lant�s�");
                    StartCoroutine(kayitOl());

                }
                else
                {
                    StartCoroutine(pK_Script.hataPanel("L�tfen S�zle�meyi Kabul Ediniz!"));
                }
            }
            else
            {
                StartCoroutine(pK_Script.hataPanel("�ifreler E�le�miyor!"));
            }
        }
    }



    IEnumerator kayitOl()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity","kayitOlma"); //php dosyas�nda kayitOlma if blo�una gider

        form.AddField("kullaniciAdi", kullaniciAdi_IF.text); 
        form.AddField("sifre", sifre_IF.text);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/user.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);

            }
            else
            {
                
                if (www.downloadHandler.text.Contains("kay�t ba�ar�l�"))
                {
                    StartCoroutine(pK_Script.hataPanel(www.downloadHandler.text));  //kay�t ba�ar�l� olma durumunda sorgu sonucunu hata paneline yazar

                    //PlayerPrefs.DeleteKey("karekter1_kullanbtn"); // oyuna kay�t olduktan sonra karekterlerler ilk ba�ta siliniyor
                    //PlayerPrefs.DeleteKey("karekter2_kullanbtn");
                    //PlayerPrefs.DeleteKey("karekter3_kullanbtn");

                    PlayerPrefs.DeleteAll();
                    kayitoldu = true;
                    Debug.Log("karekterler kay�t olunca default olarak silindi!");
                }
                else
                {

                    Debug.Log("l�tfen benzersiz kullan�c� ad� kullan�n�z!" ); //hata olma durumunda//
                    StartCoroutine(pK_Script.hataPanel("L�tfen benzersiz kullan�c� ad� kullan�n�z!"));
                }
            
            }
        }
    }


}

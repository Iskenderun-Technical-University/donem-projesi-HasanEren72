using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] hayvanlar;
    void Start()
    {
     // Invoke("HayvanUret",0.5f);  //("fonk" , ka� saniye sonra �a��r�lacak) 5 saniye sonra fonk �a��r�r
        InvokeRepeating("HayvanUret", 3 , 1f); //("fonk" , ka� saniye sonra �a��r�lacak , ka� saniye aral�klarla �a��r�lacak)  3 saniye sonra 1 saniye aral�klarla �a��r�lacak
    } 
      
    void Update()
    {
        
    }
    
    void HayvanUret()
    {
        int rastgeleXkordinat = Random.Range(-37, 37);
        int hayvanindis = Random.Range(0, hayvanlar.Length);

        Instantiate(hayvanlar[hayvanindis], new Vector3(rastgeleXkordinat, 0, 90), hayvanlar[hayvanindis].transform.rotation);
    }
}

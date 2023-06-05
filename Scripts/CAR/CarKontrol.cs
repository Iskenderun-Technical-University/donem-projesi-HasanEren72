using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarKontrol : MonoBehaviour
{
    public float speed=10f;    
    public float highSpeed = 20f;
    public float donmehiz = 20f;

    void Start()
    {
        
    }

    void Update()
    {
        //transform.Translate(0, 0, speed * Time.deltaTime); //time.deltatime saniyede 0.003 birime e�de�er

        float tusDegerX= Input.GetAxis("Horizontal"); //1 ile -1 aras�nda de�er al�r (Yatay)
        float tusDegerZ= Input.GetAxis("Vertical") ;  //(Dikey)


        if (Input.GetKey(KeyCode.LeftShift) && tusDegerZ !=0) //shifte  ve ileriye bas�lm��sa y�ksek h�z z ekseninde hareket ve x ekseninde d�nme hareketi
        {
 
            transform.Translate(0, 0, tusDegerZ * Time.deltaTime * highSpeed);
            //transform.Translate(Vector3.forward * tusDegerZ  * Time.deltaTime* highSpeed); �stteki ile ayn�d�r
            transform.Rotate(0, tusDegerX  * Time.deltaTime * donmehiz, 0);
            //transform.Rotate(Vector3.up * tusDegerX  * Time.deltaTime* donmehiz); �stteki ile ayn�d�r
        }
        else if(tusDegerZ != 0)
        {                                                               //h�z z ekseninde hareket ve x ekseninde d�nme hareketi
            transform.Translate(0, 0, tusDegerZ  * Time.deltaTime * speed);
            transform.Rotate(0, tusDegerX  * Time.deltaTime * donmehiz, 0);
        }
          
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopKontrol : MonoBehaviour
{
    public UnityEngine.UI.Button btn;
    public UnityEngine.UI.Text zaman, can,Durum;
    private Rigidbody rg;
    public float hiz = 10;
    float zamansayaci = 20;
    float cansayaci = 3 ;
    bool oyunDevam=true;
    bool oyunTamam = false;

    // Start is called before the first frame update
    void Start()
    {
        can.text = cansayaci+"";
        rg = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (oyunDevam && !oyunTamam)
        {
            zamansayaci -= Time.deltaTime;
            zaman.text = (int)zamansayaci + "";
        }
        else if(!oyunTamam)
        {
            Durum.text = "Tekrar Dene...";
            btn.gameObject.SetActive(true);
        }
        if(zamansayaci<0)
        {
            oyunDevam = false;
        }
    }

    private void FixedUpdate()
    {
        if(oyunDevam && !oyunTamam) { 
            float yatay = Input.GetAxis("Horizontal");
            float dikey = Input.GetAxis("Vertical");
            Vector3 kuvvet = new Vector3(-dikey, 0, yatay);
            rg.AddForce(kuvvet*hiz);
        }
        else
        {
            rg.velocity = Vector3.zero;
            rg.angularVelocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision cls)
    {
        string objeismi=cls.gameObject.name;
        if(objeismi.Equals("BitisZemin"))
        {
            oyunTamam = true;
            Durum.text = "Tebrikler...";
            btn.gameObject.SetActive(true);

        }
        else if(!objeismi.Equals("Zemin") && !objeismi.Equals("LabirentZemin"))
        {
            cansayaci -= 1;
            can.text = (int)cansayaci + "";
            if (cansayaci == 0)
            {
                oyunDevam=false;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public int counter = 0;
    public int counter2 = 0;
    Material mat;
    bool JustOnce = false;
    public int neighboor;
    int Fixedcounter = 0;
    public int questNR = 1;
    byte red, green, blue,transparentsy;
    public int dublicants = 0;
    // Start is called before the first frame update
    void Start()
    {
        red = 255;
        green = 255;
        blue = 255;
        transparentsy = 100;
        //neihboors = new List<string>();
        mat = GetComponent<Renderer>().material;
        mat.SetColor("_BaseColor", new Color32(255, 255, 255, 25));
        // neihboors.Insert(0, gameObject.name);
    }
    private void OnTriggerEnter(Collider other){
        if (other.name.Contains("pos")){
            neighboor++;
        }
    }

    private void FixedUpdate() {
        Fixedcounter++;
        if (!JustOnce && Fixedcounter % Mathf.Round(1f / Time.fixedDeltaTime) == 0)
        {
            ChangeColorPrSharedPos();
            JustOnce = true;
        }
    }
    public void quest(int nr, byte myC ,byte notMyC, byte notMyC2, byte t) {
        
            if (counter  < 20){
                notMyC = 25;
                notMyC2 = 25;
                t = 125;
            } else if (counter >= 20 && counter < 30) {
                notMyC = 50;
                notMyC2 = 50;
                t = 125;
            }else if (counter >= 30 && counter < 40) {
                notMyC = 75;
                notMyC2 = 75;
                t = 125;    
            } else if (counter >= 40 && counter < 50) {
                notMyC = 100;
                notMyC2 = 100;
                t = 150;
            }else if (counter >= 50 && counter < 60) {
                notMyC = 125;
                notMyC2 = 125;
                t = 150;
            } else if (counter >= 60 && counter < 70) {
                notMyC = 150;
                notMyC2 = 150;
                t = 150;
            } else if (counter >= 70 && counter < 80) {
                notMyC = 175;
                notMyC2 = 175;
            } else if (counter >= 80 && counter < 90) {
                notMyC = 200;
                notMyC2 = 200;
                t= 175;
            } else if (counter >= 100 && counter < 110) {
                notMyC = 225;
                notMyC2 = 225;
                t= 175;
            } else if (counter >= 100 && counter < 110) {
                notMyC = 250;
                notMyC2 = 250;
                t= 175;
            }else if (counter >= 300) {
                notMyC = 255;
                notMyC2 = 255;
                t = 255;
            }else {
                notMyC = 255;
                notMyC2 = 255;
                t= 175;
            }
        if (dublicants>10) { t = (byte)(t + dublicants); }
        if(nr==1){          
            mat.SetColor("_Color", new Color32(myC, notMyC, notMyC2, t));
        } else if (nr == 2) {           
            mat.SetColor("_Color", new Color32(notMyC2, myC, notMyC2, t));
        } else if (nr==3) {          
            mat.SetColor("_Color", new Color32(notMyC2, notMyC2, myC, t));
        } 
    }


    void ChangeColorPrSharedPos(){
        counter = (neighboor + getDub());
        quest(questNR, red, green, blue, transparentsy);        
        //cc();
    }


   public  void setDub(int set) {
        dublicants = set;
    }

    int getDub() {
        return dublicants;
    }

    void cc()
    {
        counter = (neighboor + dublicants);
        // start green
        if (counter < 10)
        {
            mat.SetColor("_Color", new Color32(200, 200, 200, 25));
        }
        else
        if (counter >= 10 && counter < 20)
        {
            mat.SetColor("_Color", new Color32(225, 225, 225, 50));
        }
        else
        if (counter >= 20 && counter < 30)
        {
            mat.SetColor("_Color", new Color32(255, 255, 255, 75));
        }
        // start blue
        else if (counter >= 30 && counter < 40)
        {
            mat.SetColor("_Color", new Color32(0, 0, 150, 150));
        }
        else if (counter >= 40 && counter < 50)
        {
            mat.SetColor("_Color", new Color32(0, 0, 200, 150));
        }
        else if (counter >= 60 && counter < 70)
        {
            mat.SetColor("_Color", new Color32(0, 0, 255, 150));

            // start red
        }
        else if (counter >= 70 && counter < 80)
        {
            mat.SetColor("_Color", new Color32(150, 0, 0, 185));
        }
        else if (counter >= 80 && counter < 90)
        {
            mat.SetColor("_Color", new Color32(200, 0, 0, 200));
        }
        else if (counter >= 90)
        {
            mat.SetColor("_Color", new Color32(255, 0, 0, 255));
        }
        else
        {
            mat.SetColor("_Color", new Color32(255, 0, 0, 255));
        }
    }

}

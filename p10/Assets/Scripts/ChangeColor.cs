using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public int counter = 0;
    Material mat;
    public List<string> neihboors;
    bool JustOnce = false;
    public int neighboor;
    int Fixedcounter = 0;
    public int questNR = 1;
    byte red, green, blue,transparentsy;
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("pos"))
        {
            neighboor++;
        }
    }

    private void FixedUpdate()
    {
        Fixedcounter++;
        // 1 sec delay before color changes
        if (!JustOnce && Fixedcounter % Mathf.Round(1f / Time.fixedDeltaTime) == 0)
        {
            ChangeColorPrSharedPos();
            // ColorChange();
            JustOnce = true;
        }
    }

    /*
        default color if there is less than 10 shared positions = white
        depended on which quest that is active the color will go from
        white -> red
        white -> green
        white -> blue
        with transparentsy groes from 100 -> 175
        just for fun I have said that if there are more than 300 shared positions the color should be black
     
     */
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
                myC = 0;
                notMyC = 0;
                notMyC2 = 0;
                t= 255;
            }else {
                notMyC = 255;
                notMyC2 = 255;
                t= 175;
            }
        if(nr==1){
            // mosted shared pos = red
            mat.SetColor("_Color", new Color32(myC, notMyC, notMyC2, t));
        } else if (nr == 2) {
            // mosted shared pos = green
            mat.SetColor("_Color", new Color32(notMyC2, myC, notMyC2, t));
        } else if (nr==3) {
            // mosted shared pos = blue
            mat.SetColor("_Color", new Color32(notMyC2, notMyC2, myC, t));
        } 
    }


    void ChangeColorPrSharedPos(){
        counter = neighboor;
        quest(questNR, red, green, blue, transparentsy);        
    }
}

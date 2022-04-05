using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest1Controller : MonoBehaviour
{
    /*
     * Quest Complete if
            Destroyed 2 boats
            Free a fire spirit
            Something with spirit and landpost
     */
    [SerializeField] private WeaponScript ws;
    public  int boatsSinked = 0;

    private bool BoatComplete;
    private bool FireSpiritComplete;
    private bool LandpostComplete;

    // Start is called before the first frame update
    void Start()
    {

    }
    public void incrementBoatsSinked() {
        boatsSinked++;
    }
    private int getBoatsSinked() {
        return boatsSinked;
    }
    // Update is called once per frame
    void Update()
    {
        if (getBoatsSinked()>=2) {
            BoatComplete = true;
        }
    }

  
}

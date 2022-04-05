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
    [SerializeField] private GameObject fireSpiritQuest;
    public  int boatsSinked = 0;

    private bool BoatComplete;
    private bool FireSpiritComplete;
    private bool LandpostComplete;
    private bool LevelOneComplete;
    // Start is called before the first frame update
    void Start()
    {
        fireSpiritQuest.SetActive(false);
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
        if (BoatComplete) {
            fireSpiritQuest.SetActive(true);
        }
        LevelOneComplete = BoatComplete && FireSpiritComplete && LandpostComplete;

    }
    public void setSpirirtComplete( bool set) {
        FireSpiritComplete = set;
    }
    public bool getFireSpirtComplete() {
        return FireSpiritComplete;
    }
    public void setLandpostComplete(bool set) {
        LandpostComplete = set;
    }
    public bool getLandpostComplete() {
        return LandpostComplete;
    }
    public bool getLevelOneComplete() {
        return LevelOneComplete;
    }


}

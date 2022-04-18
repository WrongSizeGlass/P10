using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEHIt : MonoBehaviour
{
   public RotateObjects ro;
   
    private bool PlayerIsHit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && ro.getCanDamage()) {
            Debug.Log("player is hit");
            setPlayerIsHit(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && ro.getCanDamage()){
            setPlayerIsHit(false);
        }
    }

    bool getPlayerIsHit() {
        return PlayerIsHit;
    }
    public void setPlayerIsHit(bool set){
        PlayerIsHit = set;
    }
}

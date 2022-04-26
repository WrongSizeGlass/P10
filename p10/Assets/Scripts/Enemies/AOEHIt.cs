using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEHIt : MonoBehaviour
{
   public RotateObjects ro;
    public AudioSource audioSource;
    //public AudioClip Anticipation;
    public AudioClip Explosion;
    public float startvolume = 0.5f;
    public float volume = 0.5f;

    private bool PlayerIsHit;
    public bool isHitting = false;
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
        if (other.tag == "Player") {
            isHitting = true;
            Debug.Log("Hitting player");
        }
        if(other.transform != null)
        {
            //play sound explosion
            volume = startvolume;
            audioSource.PlayOneShot(Explosion, volume);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && ro.getCanDamage()){
            setPlayerIsHit(false);
        }
        if (other.tag =="Player") {
            isHitting = false;
        }
        volume = 0;
    }
    public bool getIsHitting() {
        return isHitting;
    }
    public void setIsHitting(bool set) {
        isHitting = set;
    }
    public bool getPlayerIsHit() {
        return PlayerIsHit;
    }
    public void setPlayerIsHit(bool set){
        PlayerIsHit = set;
    }
    
}

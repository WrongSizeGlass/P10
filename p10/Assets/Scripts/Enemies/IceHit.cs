using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceHit : MonoBehaviour
{
    private bool hit;
    private bool reset=false;
    private Vector3 startpos;
    public bool getIceHit(){
        return hit;
    }
    public bool getIceReset() {
        return reset;
    }
    public void setIceReset(bool set){
        reset = set;
    }
    public void setIceHit(bool set){
        hit = set;
    }

    private void OnTriggerEnter(Collider other){

        if (other.tag!="Ice") {
            setIceHit(true);
            Debug.Log("I hit " + other.name);
        }

    }
    private void Start()
    {
        startpos = transform.position;
    }
    private void Update(){

        if (reset){
            transform.position = Vector3.MoveTowards(transform.position, startpos, 15 * Time.deltaTime);           
        }
        if (  Vector3.Distance( transform.position, startpos)<1.5f) {
            setIceReset(false);
        }
        
    }

    

}

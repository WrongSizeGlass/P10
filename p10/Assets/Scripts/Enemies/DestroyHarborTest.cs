using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma strict
using System.Collections.Generic;
public class DestroyHarborTest : MonoBehaviour
{

    // Start is called before the first frame update
    SinkScript ss;

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other){
        if (other.tag == "Boat"){
            other.GetComponent<Health>().setHp(2);
        }
    }

    private void OnTriggerStay(Collider  other)
    {
        if (other.GetComponent<SinkScript>()!=null) {
            if (!other.GetComponent<SinkScript>().enabled) {
                other.GetComponent<SinkScript>().enabled = true;
            }
        }
       
    }

}

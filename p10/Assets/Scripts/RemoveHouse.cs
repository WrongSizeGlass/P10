using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveHouse : MonoBehaviour
{
    MeshRenderer temp;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        temp = other.gameObject.GetComponent<MeshRenderer>();
       
    }
    private void OnTriggerStay(Collider other)
    {
        if (temp!=null) {
            temp.enabled = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (temp != null){
            temp.enabled = true;
            temp = null;
        }
       
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

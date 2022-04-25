using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q2TriggerStart : MonoBehaviour
{
    [SerializeField] private RotateObjects ro;
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
        if (other.tag == "Player")
        {
            ro.setStart(true);
        }
      
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag=="Player") {
            ro.setStart(false);
        }
    }

}

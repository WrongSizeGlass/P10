using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapedFireSpirit : MonoBehaviour
{
    RaycastHit hit;
    Vector3 myPos;
    public bool trapped = true;
    FireSpiretWalkRoute FSWR;
    // Start is called before the first frame update
    void Start()
    {
        myPos = transform.position;
        FSWR = GetComponent<FireSpiretWalkRoute>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (trapped) {
            trapped = IamTrapped();
        }
        if (!trapped) {
            FSWR.setCanWalk(true);
            this.enabled=false;
        }
    }

    bool IamTrapped() {
        Debug.DrawRay(myPos, transform.forward, Color.red);
        Debug.DrawRay(myPos, transform.forward * -1, Color.green);
        Debug.DrawRay(myPos, transform.right * -1, Color.blue);
        Debug.DrawRay(myPos, transform.right, Color.cyan);
        if (Physics.Raycast(myPos, transform.forward, out hit, 1f) ||
            Physics.Raycast(myPos, transform.forward * -1, out hit, 1f) ||
            Physics.Raycast(myPos, transform.right * -1, out hit, 1f) ||
            Physics.Raycast(myPos, transform.right, out hit, 1f)){
            if ( hit.collider.tag == "Ice") {
                return true;
            }
            
        } 
        return false;
    }


}

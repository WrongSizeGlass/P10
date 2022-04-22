using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapedFireSpirit : MonoBehaviour
{
    RaycastHit hit;
    Vector3 myPos;
    public bool trapped = true;
    FireSpiretWalkRoute FSWR;
    private Transform player;
    [SerializeField] private Transform myParent;
    [SerializeField] private Transform ice;
    bool once = false;
    // Start is called before the first frame update
    void Start()
    {
        myParent = transform.parent.GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
        FSWR = GetComponent<FireSpiretWalkRoute>();
        


       
    }

    // Update is called once per frame
    void Update()
    {
        if (!once && Vector3.Distance(transform.position, myParent.position) < 0.1f)
        {
            myPos = transform.position;
            once = true;
        }else if(!once) {
            transform.position = Vector3.MoveTowards(transform.position, myParent.position, 1.25f * Time.deltaTime);
            ice.position = Vector3.MoveTowards(ice.position, myParent.position, 1.25f * Time.deltaTime);
        }   

        if (trapped && once) {
            trapped = IamTrapped();
        }
        if (!trapped && once) {
            player.GetComponent<Quest1Controller>().setSpirirtComplete(true);
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

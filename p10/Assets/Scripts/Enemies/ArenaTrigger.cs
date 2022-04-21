using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaTrigger : MonoBehaviour
{
    private bool bossCanAttack = false;

    public bool getBossCanAttack() {
        return bossCanAttack;
    }
    private void setBossCanAttack(bool set) {
        bossCanAttack = set;
    }
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
        if (other.tag =="Player") {
            setBossCanAttack(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player"){
            setBossCanAttack(false);
        }
    }
}

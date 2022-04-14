using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffectHItArea : MonoBehaviour
{
    [SerializeField]private bool hit;
    [SerializeField]private Transform player;
    Vector3 startPos;
    public bool getHit() {
        return hit;
    }
    public void setHit(bool set) {
        hit = set;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag!="Ice") {
            hit = true;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        transform.position = startPos;
    }
    private void Start()
    {
        startPos = transform.position;
    }
    private void Update()
    {
        if (!hit && Vector3.Distance(transform.position, player.position)<0.05f) {
            hit = true;
        }
    }
}

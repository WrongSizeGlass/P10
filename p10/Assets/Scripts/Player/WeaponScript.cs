using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{

    public bool activated;

    public float rotationSpeed;
    public Rigidbody targetToPushBack;
    private Rigidbody rB;
    public Transform tempTarget;
    public AudioSource audioSource;
    public AudioClip hammerHit;
    public float volume = 0.01f;

    private void Awake()
    {
        rB = GetComponent<Rigidbody>();
    }
    void Update()
    {

        if (activated)
        {
            transform.localEulerAngles += Vector3.forward * rotationSpeed * Time.deltaTime;
        }

    }
    public Transform getTempTarget() {

        return tempTarget;
    }
    private void OnCollisionEnter(Collision collision)
    {
        activated = false;
        /*if (collision.gameObject.layer == 11)
        {
            print(collision.gameObject.name);
            GetComponent<Rigidbody>().Sleep();
            GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            GetComponent<Rigidbody>().isKinematic = true;
            activated = false;
            audioSource.PlayOneShot(hammerHit, volume);
        }
        GetComponent<Rigidbody>().Sleep();
        GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        GetComponent<Rigidbody>().isKinematic = true;*/
        
        print(collision.gameObject.name);
        audioSource.PlayOneShot(hammerHit, volume);
        if (!activated && collision.collider.tag == "Ice")
        {
            tempTarget = collision.transform;
            rB.isKinematic = true;
        }
    }
    /*private void OnCollisionStay(Collision collision)
    {
        if (collision.rigidbody != null && collision.transform.tag!="Player" )
        {
            targetToPushBack = collision.rigidbody.GetComponent<Rigidbody>();
        }
    }*/
    private void OnCollisionExit(Collision collision)
    {
        if (targetToPushBack!=null) {
            targetToPushBack = null;
        }
        tempTarget = null;
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Breakable"))
    //    {
    //        if(other.GetComponent<BreakBoxScript>() != null)
    //        {
    //            other.GetComponent<BreakBoxScript>().Break();
    //        }
    //    }
    //}
}

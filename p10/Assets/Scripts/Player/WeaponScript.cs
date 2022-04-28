using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{

    public bool activated;

    public float rotationSpeed;
    public Rigidbody targetToPushBack;
    public Transform tempTarget;
    public AudioSource audioSource;
    public AudioClip hammerHit;
    Vector3 temp;
    public float volume = 0.5f;
    bool returnHammer = false;
    void Update()
    {

        if (activated)
        {
            transform.localEulerAngles += Vector3.forward * rotationSpeed * Time.deltaTime;
        }
       /* if (tempTarget!=null && !returnHammer) {
            transform.position = tempTarget.position;
        }*/

    }
    public Transform getTempTarget() {

        return tempTarget;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.layer == 11)
        //{
            print(collision.gameObject.name);
            GetComponent<Rigidbody>().Sleep();
            GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            GetComponent<Rigidbody>().isKinematic = true;
            activated = false;
            audioSource.PlayOneShot(hammerHit, volume);
            temp = transform.position;
        //}
        tempTarget = collision.transform;

    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.rigidbody != null && collision.transform.tag!="Player" )
        {
            targetToPushBack = collision.rigidbody.GetComponent<Rigidbody>();
        }
    }
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

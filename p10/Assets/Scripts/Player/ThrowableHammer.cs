using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ThrowableHammer : MonoBehaviour
{
    public Image crosshair;
    public WeaponScript ws;
    public Rigidbody hammer;
    public float throwForce = 50;
    public Transform target, curve_point;
    private Vector3 old_pos;
    private bool isReturning = false;
    private float time = 0.0f;
    public Transform hand;
    private Rigidbody temp;
    int counter = 0;
    public Vector3 ThrowBackForceVector;
    bool hammerIsThrown = true;
    public bool getIsReturning() {
        return isReturning;
    }
    private void Start()
    {
        ReturnHammer();
    }
    // Update is called once per frame
    void ThrowBackForce() {
        if (ws.targetToPushBack != null) {
            ws.targetToPushBack.AddForceAtPosition(ThrowBackForceVector, hammer.position, ForceMode.Impulse);
        }
    }
    void Update()
    {
        // Melee
        if (Input.GetButtonDown("Fire1")&& !hammerIsThrown) { 
        
        }

        //Range
        if (Input.GetButtonUp("Fire2") && !hammerIsThrown)
        {
            ThrowHammer();
            hammerIsThrown = true;
        }

        if (Input.GetButtonDown("Fire2")&& hammerIsThrown && !isReturning)
        {
            
            ThrowBackForce();
            ReturnHammer();
            temp = null;
        }

        if (isReturning) //returning Calcs - Quadratic Bezier curves
        {
            if(time < 1.0f)
            {
                hammer.position = getBQCPoint(time, old_pos, curve_point.position, target.position);
                hammer.rotation = Quaternion.Lerp(hammer.transform.rotation, target.rotation, 50 * Time.deltaTime);
                time += Time.deltaTime;
            }
            else
            {            
                ResetHammer();
            }
        }
    }

    //Throw Hammer
    void ThrowHammer()
    {
        hammer.transform.parent = null;
        hammer.isKinematic= false;
        hammer.AddForce(Camera.main.transform.TransformDirection(Vector3.forward) * throwForce, ForceMode.Impulse);
        hammer.AddTorque(hammer.transform.TransformDirection(Vector3.right) * 100, ForceMode.Impulse);
    }
    //Return Hammer
    void ReturnHammer()
    {
        old_pos = hammer.position;
        isReturning = true;
        hammer.velocity = Vector3.zero;
        hammer.isKinematic = true;
    }
    //Reset
    void ResetHammer()
    {
        time = 0.0f;
        isReturning = false;
        hammerIsThrown = false;
        hammer.transform.parent = hand;
        hammer.position = target.position;
        hammer.rotation = target.rotation;
    }

    Vector3 getBQCPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = (uu * p0) + (2 * u * t * p1) + (tt * p2);
        return p;
    }
}
/* if (counter % Mathf.Round(1.5f / Time.fixedDeltaTime)==0) { 

         }*/
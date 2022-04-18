using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualAOERotateAround : MonoBehaviour
{
    public float y = 30;
    Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, y * Time.deltaTime);
    }

    public void setTarget(Vector3 set) {
        target = set;
    
    }

}

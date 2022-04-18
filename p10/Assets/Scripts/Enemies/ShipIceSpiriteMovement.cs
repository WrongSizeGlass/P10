using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipIceSpiriteMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> waypointsList;
    int indexCounter=1;
    public float speed = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypointsList[indexCounter].position, speed * Time.deltaTime);
        transform.RotateAround(waypointsList[indexCounter].position, Vector3.up, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, waypointsList[indexCounter].position)<=0.1) {
            indexCounter++;
        }
        if (indexCounter>= waypointsList.Count) {
            indexCounter = 0;
        }
        
    }
}

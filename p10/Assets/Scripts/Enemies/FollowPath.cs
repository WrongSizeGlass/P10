using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    private Transform me;
    List<Transform> waypointList;
    public Transform waypointGroup;
    int activeWaypoint = 0;
    // Start is called before the first frame update
    void Start(){
        me = GetComponent<Transform>();
        waypointList = new List<Transform>();

     
        for (int i = 0; i < waypointGroup.childCount; i++)
        {
            waypointList.Add(waypointGroup.GetChild(i).GetComponent<Transform>());
        }
    }
    void followWaypoints() {
        me.position = Vector3.MoveTowards(me.position, waypointList[activeWaypoint].position, 1.35f * Time.deltaTime);
       // me.LookAt(waypointList[activeWaypoint].position);
        if (Vector3.Distance(me.position, waypointList[activeWaypoint].position) < 0.1f)
        {
            activeWaypoint++;
        }
        if (activeWaypoint > waypointList.Count - 1)
        {
            activeWaypoint = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {
        followWaypoints();
    }
}

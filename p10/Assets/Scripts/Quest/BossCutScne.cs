using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCutScne : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform waypointGroup;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject cam;
    [SerializeField] private BossBehavior bb;
    private List<Transform> waypoint;
    private List<Transform> descwaypoint;
    Vector3 targetDirection, newDirection;
    CameraTest ct;
    int index = 0;
    int descindex = 0;
    Vector3 start;
    Quaternion startq;
    [SerializeField] GameObject lw;
    [SerializeField] MeshRenderer mr;
    Transform looktarget;
    void Start()
    {
        //ct = GetComponent<CameraTest>();
        //ct.enabled = false;
        cam.SetActive(false);
        //start = cam.transform.position;
        //startq= cam.transform.rotation;
        waypoint = new List<Transform>();
        for (int i=0; i<waypointGroup.childCount; i++) {
            waypoint.Add(waypointGroup.GetChild(i).GetComponent<Transform>());
        }
        for (int i = waypointGroup.childCount; i >-1 ; i--){
            descwaypoint.Add(waypointGroup.GetChild(i).GetComponent<Transform>());
        }
    }
    bool once = false;
    bool playerPos=false;
    bool backwards = false;
    // Update is called once per frame
    void LateUpdate()
    {
        if (Vector3.Distance(bb.transform.position, transform.position) < 7.5f)
        {
            backwards = true;
           
        }
        else {
            if (mr.enabled)
            {
                looktarget = mr.transform;
            }
            else {
                looktarget = bb.transform;
            
            }
        }
        if (!backwards && !playerPos) {
            moveTowardsTheBoss();
        }
        if (backwards && !playerPos) {
            moveBack();
            if (Vector3.Distance(transform.position, waypoint[0].position) < 1.5) {
                playerPos = true;
            }
        }
        if (playerPos && bb.getReturnCamera()) {
            transform.position = Vector3.MoveTowards(transform.position, player.position, 2.5f * Time.deltaTime);
            transform.LookAt(start);
            if (Vector3.Distance(transform.position, player.position) < 1.5) {
                transform.position = cam.transform.position;
                transform.rotation = cam.transform.rotation;
                cam.SetActive(true);
               // ct.enabled = true;
                this.enabled = false;
            }
            //this.enabled = false;
        }
 

    }
    void moveTowardsTheBoss() {
        if (Vector3.Distance(transform.position, waypoint[index].position) < 0.1 && index < 4 && !bb.getReturnCamera())
        {
            index++;
        }
        
        setTransform(waypoint[index].position, 5, bb.getReturnCamera());
    }

    void moveBack() {
        if (Vector3.Distance(transform.position, waypoint[descindex].position) < 0.1 && descindex < 4 && bb.getReturnCamera())
        {
            descindex++;
        }
        Debug.LogError("¤¤ return");
        
        setTransform(waypoint[descindex].position, 5, bb.getReturnCamera());
    }
    void setTransform(Vector3 target, float speed, bool boss)
    {

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (!boss) {
            targetDirection = looktarget.position - transform.position;
            newDirection = Vector3.RotateTowards(transform.forward, targetDirection, (speed / 2) * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
            /*targetDirection = target - transform.position;
            newDirection = Vector3.RotateTowards(transform.forward, targetDirection, (speed / 2) * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);*/

        }
        else {
            targetDirection = looktarget.position - transform.position;
            newDirection = Vector3.RotateTowards(transform.forward, targetDirection, (speed / 2) * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);

        }
    }


}

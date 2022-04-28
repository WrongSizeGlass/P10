using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpiretWalkRoute : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform player;
    [Header("Must contain children to be used as the route")]
    public Transform waypointGroup;
    [Header("Must contain child/child->lightComponet")]
    public Transform LightGroup;
    private List<Transform> waypointList;
    private List<Light> lightList;
    int activeWaypoint = 0;
    private bool finishedRoute = false;

    
    private Transform me;
    private bool canWalk = false;
    private bool exitLightTurnOn;
    int counter = 0;
    int LowLightCounter;
    int HighLightCounter;
    
    void Start()
    {      
        lightList = new List<Light>();
        waypointList = new List<Transform>();
        for (int i = 0; i < waypointGroup.childCount; i++) {
            waypointList.Add(waypointGroup.GetChild(i).GetComponent<Transform>());
        }
        for (int i = 0; i < LightGroup.childCount; i++)
        {
            lightList.Add(LightGroup.GetChild(i).GetComponent<Transform>().GetChild(0).GetComponent<Light>());
        }

        me = GetComponent<Transform>();
        LowLightCounter = -1;
        HighLightCounter = lightList.Count;
    }

    // Walking the route
    void Update(){
        if (getCanWalk()) {
            if (Vector3.Distance(me.position,player.position)<3.5) {
                followWaypoints();
            }
        }
    }
    void followWaypoints() {
        me.position = Vector3.MoveTowards(me.position, waypointList[activeWaypoint].position, 2.5f * Time.deltaTime);
        me.LookAt(waypointList[activeWaypoint].position);
        if (Vector3.Distance(me.position, waypointList[activeWaypoint].position)<0.25f) {
            activeWaypoint++;
        }
        if (activeWaypoint>waypointList.Count-1) {
            finishedRoute = true;
            setCanWalk(false);
        }
        if (activeWaypoint == 1)
        {
            startBanter = true;
        }
        if (activeWaypoint == 4)
        {
            startBanter2 = true;
        }
    }
    public void setCanWalk(bool ICan) {
        canWalk = ICan;
    }
    public bool getCanWalk() {
        return canWalk;
    }
    public bool getFinishedRoute() {
        return finishedRoute;
    }
    public bool getLastLightIsOn() {
        return exitLightTurnOn;
    }
    // Turn on the light
    private void FixedUpdate()
    {
        counter++;
        if (!exitLightTurnOn && finishedRoute) {
            turnOnLights();
        }
    }
    void turnOnLights()
    {
        // inspired by divied and councur algorithm
        if (counter % Mathf.Round(0.15f / Time.fixedDeltaTime) == 0) {
            LowLightCounter++;
            HighLightCounter--;
            lightList[LowLightCounter].enabled = true;
            lightList[HighLightCounter].enabled = true;
        }
        if (lightList[LowLightCounter+1].enabled) {
            exitLightTurnOn = true;
            Destroy(this.gameObject);          
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("turn on " + other.name);
        if (other.tag=="Lamppost") {
            
            other.transform.GetComponent<Transform>().GetChild(0).GetComponent<Light>().enabled = true;
        }
    }
    bool startBanter = false;
    bool startBanter2 = false;
    public bool getBanter1()
    {
        return startBanter;
    }
    public bool getBanter2()
    {
        return startBanter2;
    }

}

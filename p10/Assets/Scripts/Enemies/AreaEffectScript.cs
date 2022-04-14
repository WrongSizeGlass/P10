using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffectScript : MonoBehaviour
{
    public bool rotate = true;
    public List<Transform> myObjects;
    int counter = 0;
    float y = 2.5f;
    public float timer = 1;
    public float speed = 5f;
    public float iceSpeed = 2.5f;
    [SerializeField] private Transform target;
    private Transform player;
    List<bool> turnOnObjects;
    List<IceHit> GetIceCollisionList;
    private List<Vector3> startPos;
    private AreaEffectHItArea AEHA;
    public bool setHit = false;
    private Vector3 myTarget;
    public bool haveReset = true;
    int startposition = 0;
    int resetCounter = 0;
    int iceHasReset = 0;
    Vector3 targetStartPos;
    BoxCollider box;
    int temp = 1;
    // Start is called before the first frame update
    void Start()
    {
        box = target.GetComponent<BoxCollider>();
        targetStartPos = target.position;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        AEHA = target.GetChild(0).GetComponent<AreaEffectHItArea>();
        // myObjects = new List<Transform>();
        GetIceCollisionList = new List<IceHit>();
        turnOnObjects = new List<bool>();
        startPos = new List<Vector3>();
        for (int i = 0; i < myObjects.Count; i++)
        {
            // myObjects.Add(transform.GetChild(i).GetComponent<Transform>());
            GetIceCollisionList.Add(myObjects[i].GetComponent<IceHit>());
            startPos.Insert(0, myObjects[i].position);
            turnOnObjects.Add(false);

        }

    }
    bool sendTarget = false;
    // Update is called once per frame
    void Update(){
        sendTarget = Vector3.Distance(player.position, transform.position) > 5;
        // rotate
        if (!setHit){
            rotateMe(0);
        }
        // Find target
   

        if (sendTarget && !AEHA.getHit())
        {
           target.position = Vector3.MoveTowards(target.position, player.position, speed * Time.deltaTime);
        }
        if (AEHA.getHit()) {
            attack();
        }
        

    }
    void rotateMe(int childed) {
        if (counter % Mathf.Round(Random.Range(1, 5) / Time.fixedDeltaTime) == 0){
            y = y * -1;
        }
        transform.Rotate(0.0f, y, 0.0f);
    }
    int iceHitCounter = 0;
    void countIceHit(){
        for (int i = 0; i < GetIceCollisionList.Count; i++){
            if (GetIceCollisionList[i].getIceHit()) {
                iceHitCounter++;
            }
        }
    }
    void attack() {
        Debug.Log("AEHA.getHit() " + AEHA.getHit());
        // get temp position
        if (AEHA.getHit()) {

            myTarget = player.position;


            //box.enabled = false;
            target.position = targetStartPos;
            setHit = true;
            //
        }
        // Send objects towards players
        if (Vector3.Distance(target.position, targetStartPos) < 1 || setHit) {

            Debug.LogError("ATTACK");
            countIceHit();
            throwObjects();
        }
    }

    void throwObjects() {
        turnOnObjects[temp] = true;
        for (int i = 0; i < temp; i++) {

            if (turnOnObjects[i] == true) {
                myObjects[i].position = Vector3.MoveTowards(myObjects[i].position, myTarget, iceSpeed * Time.deltaTime);
            }

        }
        if (counter % Mathf.Round(0.1f / Time.fixedDeltaTime) == 0) {
            temp++;
        }
        if (temp == myObjects.Count)  {
            temp = 1;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAOEHit : AOEHIt
{
    [SerializeField]private Transform myParent;
    private BossBehavior bb;
    private DestroyHarborTest dht;
    private Vector3 myResetPos;
    private bool canAttack = true;
    private bool resetAttact = true;
    public GameObject beam;
    bool isBeamActive = false;
    bool sinkTheHarbor = false;
    SphereCollider sc;

    // setters
    public void setBossBehavior(Transform set) {
      //  myParent = transform.parent.GetComponent<Transform>();
        bb = set.GetComponent<BossBehavior>();
        dht = GetComponent<DestroyHarborTest>();
        sc = GetComponent<SphereCollider>();
        sc.enabled = false;
        // beam = transform.GetChild(0).GetComponent<GameObject>();

    }
    public void setCanAttack(bool set) {
        canAttack = set;
    }
    public void setResetAttact(bool set) {
        resetAttact = set;
    }
    public void setMyResetPos() {
        myResetPos = myParent.position;
    }
    public void setSinkTheHarbor(bool set) {
        sinkTheHarbor = set;
    }
    public void setBeamActive(bool set) {
        isBeamActive = set;
        beam.SetActive(set);
    }
    // getters
    public bool getCanAttack() {
        return canAttack;
    }
    public bool getResetAttack() {
        return resetAttact;
    }
    public bool getBeamActive()
    {
        return isBeamActive;
    }
    public bool getSinkTheHarbor() {
        return sinkTheHarbor;
    }
    public Transform getMyParent() {
        return myParent;
    }
    public Transform getMyTransform() {
        return this.transform;
    }
    public void resetPos() {
        transform.position = myResetPos;
    }
    public void Resetposition(){
        if (!getCanAttack() && transform.position != myResetPos){
            setSinkTheHarbor(false);
            transform.position = myResetPos;
            setBeamActive(false);
            setResetAttact(true);
            setCanAttack(true);
            test();
            
            //bb.incrementWeapon();
        }
    }
    public void test() {

        Debug.Log("current pos " + transform.position + " startpos " + myResetPos + " name " + transform.name+ "\n can attack: " + getCanAttack());
    }

    // Start is called before the first frame update
    void Start(){
        //Resetposition();
    }

    // Update is called once per frame
    void Update()
    {
        //setMyResetPos();
        if (getSinkTheHarbor())
        {
            Debug.Log("!!! sink harbour");
            dht.enabled = true;
            sc.enabled = true;
        }
        else {
            sc.enabled = false;
            dht.enabled = false;
        }
        setMyResetPos();
    }
    


}

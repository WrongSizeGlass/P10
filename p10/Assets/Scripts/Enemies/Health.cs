using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int hp = 2;
    Rigidbody rb;
    List<string> tags = new List<string> { "House", "Boat", "Player" };
    private string myTag;
    private bool IWasHit = false;
    private bool IamDead = false;
    private Transform player;
    bool once = false;
    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        myTag = this.gameObject.tag;
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update() {
        updateMyState();
        deathState();
    }
    public bool getIamDead(){
        return IamDead;
    }

    private void updateMyState() {
        if (IWasHit) {
            IWasHit = false;
        }
        if (getHp()<=0) {
            IamDead = true;
        }   
    }
    private void deathState() {
        if (IamDead) {
            switch (myTag) {
                case "House":
                    Debug.LogError("I am dead I don't have a function:" + gameObject.name);
                    break;
                case "Player":
                    Debug.LogError("I am dead I don't have a function:" + gameObject.name);
                    break;
                case "Boat":
                    deadBoat();
                    break;
                default:
                    Debug.LogError("I am dead I don't have a tag name:" + gameObject.name);
                    break;
            }
        }  
    }

    private void deadBoat() {
        if (!once) {
            player.GetComponent<Quest1Controller>().incrementBoatsSinked();
            once = true;
        }
        GetComponent<MeshCollider>().convex = true;
        GetComponent<MeshCollider>().isTrigger = true;
        int child = transform.childCount-1;
        transform.position = Vector3.MoveTowards(transform.position,transform.GetChild(child).transform.position, 1 * Time.deltaTime);
        if (transform.position== transform.GetChild(child).transform.position) {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Hammer" && this.gameObject.tag != "Player") {
            
            setHp(1);
            IWasHit = true;
            
        }
    }
    private void setHp(int dmg) {
        hp = hp - dmg;
    }
    private int getHp() {
        return hp;
    }

}

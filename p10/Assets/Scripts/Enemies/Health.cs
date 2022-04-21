using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int hp = 2;
    Rigidbody rb;
   // List<string> tags = new List<string> { "House", "Boat", "Player" };
    private string myTag;

    private bool IamDead = false;
    private Transform player;
    bool once = false;
    private bool damageEffect = false;
    public int hpLost = 1;
    float x, y, z;
    public bool bossIce = false;
    [SerializeField] private BossBehavior bb;
   // Vector3 IwasHitHere;
    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        myTag = this.gameObject.tag;
        rb = GetComponent<Rigidbody>();
        x = transform.localScale.x;
        y = transform.localScale.y;
        z = transform.localScale.z;
    }
    // Update is called once per frame
    void Update() {
        updateMyState();      
    }
    public bool getIamDead(){
        return IamDead;
    }

    private void updateMyState() {
        if (damageEffect) {
            
            effectWhenDamaged();
            damageEffect = false;
        }
        if (getHp()<=0) {
            IamDead = true;
            deathState();
        }   
    }

    private void effectWhenDamaged(){
        setHp(hpLost);
        switch (myTag){
            case "House":
                damageEffect = false;
                break;
            case "Player":
                damageEffect = false;
                break;
            case "Ice":
                shrinkingEffect();
                damageEffect = false;
                break;
            case "Boat":
                damageEffect = false;
                break;
            default:
                damageEffect = false;
                break;
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
                case "Ice":
                    deadIce();
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
 
    void shrinkingEffect() {
            x = x - 10;
            y = y - 10;
            z = z - 10;
        
        
        transform.localScale = new Vector3(x,y,z);  
    }
    private void deadIce() {
        if (bossIce) {
            bb.degrementHP();
        }
        Destroy(gameObject);   
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
        if (collision.collider.tag == "Hammer" && this.gameObject.tag != "Player" || collision.collider.tag == "shockWave") {
            damageEffect = true;
            //IwasHitHere = collision.transform.position;
        }
    }
    public void setHp(int dmg) {
        hp = hp - dmg;
    }
    private int getHp() {
        return hp;
    }
    

}

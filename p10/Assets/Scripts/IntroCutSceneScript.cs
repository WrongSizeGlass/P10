using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroCutSceneScript : MonoBehaviour
{
    public GameObject hammer;
    public Transform hammerStartPos;
    public Camera cam;
    public Transform endPosCamra;
    Rigidbody rig;
    public Transform mainCameraPos;
    public Camera mainCamera;
    public ThirdPersonController tpc;
    public ThrowableHammer th;
    public Transform player;
    bool returnToPlayer = false;
    Vector3 lowVel;
    Vector3  myTarget;
    bool transit = false;
    public float cameraSpeed=5;
   public Transform camTarget;
    bool camSet = false;
    Quaternion mainCamRot;
    Quaternion hammererRot;
    // Start is called before the first frame update
    void Start()
    {
        lowVel = new Vector3(0.05f, 0.05f, 0.05f);
        tpc.enabled = false;
        th.enabled = false;
        hammer.transform.SetParent(hammerStartPos);
        hammer.transform.position = hammerStartPos.position;
       // hammererRot = hammer.transform.rotation;
       // hammererRot.y = 0;
       // hammer.transform.rotation = hammererRot;
        rig = hammer.GetComponent<Rigidbody>();
        rig.isKinematic = false;
        cam.transform.SetParent(hammer.transform);
        myTarget= mainCameraPos.position;
        mainCamRot = mainCamera.transform.rotation;

    }

    // Update is called once per frame
    void Update()
    {
        if (rig.velocity != Vector3.zero)
        {
            cam.transform.RotateAround(hammer.transform.position, Vector3.up*-1, (30*rig.velocity.y) * Time.deltaTime);
           // Debug.Log(rig.velocity);
        }
        if (rig.velocity.magnitude<= lowVel.magnitude && !returnToPlayer)
        {
            

            if (Vector3.Distance(cam.transform.position, hammer.transform.position) < 0.75f )
            {
                returnToPlayer = true;
            }
            else {
                cam.transform.position = Vector3.MoveTowards(cam.transform.position, hammer.transform.position, .5f * Time.deltaTime);
            }
            
        }
        if (returnToPlayer) {

            if (Vector3.Distance(cam.transform.position, mainCameraPos.transform.position) < 4f && Vector3.Distance(cam.transform.position, mainCameraPos.transform.position) > 3.9f) {
               
            }


            if (Vector3.Distance(cam.transform.position, mainCameraPos.transform.position) < 5f){
                player.gameObject.SetActive(true);
                /*if (!camSet) {
                    myTarget.x = cam.transform.position.x;
                    myTarget.z = cam.transform.position.z;
                    cam.transform.position = myTarget;
                    cam.transform.rotation = mainCamera.transform.rotation;

                    camSet = true;
                }*/
               
                // cam.transform.LookAt(camTarget.position);
                // cam.transform.SetParent(mainCameraPos.transform);
            }
           
            
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, mainCameraPos.transform.position, cameraSpeed * Time.deltaTime);


            if (Vector3.Distance(cam.transform.position, mainCameraPos.transform.position) < 1.45f)
            {

                cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, mainCamRot, 0.5f * Time.deltaTime);
            }
            else {
                cam.transform.LookAt(hammer.transform.position);
            }
            // if (Vector3.Distance(cam.transform.position, mainCameraPos.transform.position) < 0.05f)
            if (cam.transform.position == mainCameraPos.transform.position) {
                setTransit(true);
                cam.gameObject.SetActive(false);
                activatePlayer();

            }

        }
        Debug.Log(rig.velocity);
    }
    private void setTransit(bool newTransit) {
        transit = newTransit;  
    }
    public bool getTransit() {
        return transit;
    }
    void activatePlayer() {
        mainCamera.gameObject.SetActive(true);
        tpc.gameObject.SetActive(true);
        tpc.enabled = true;
        this.enabled = false;
    }
}

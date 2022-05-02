using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class ThirdPersonController : MonoBehaviour
{

    private MovementInput input;
    public Camera MyCamera;
    public float Speed = 5f;
    public float SprintSpeed = 5f;
    public float RotationSpeed = 15f;
    public float AnimationBlendSpeed = 2f;
    public float JumpSpeed = 15;
    CharacterController MyController;
    Animator MyAnimator;
    private AudioSource audioSource;
    public AudioClip walk;
    public AudioClip run;
    public float volume = 1f;
    public float picth = 1f;
    public float mDesiredRotation = 0f;
    public float mDesiredAnimationSpeed = 0f;
    public bool mSprinting = false;
    public float mSpeedyY = 0;
    public float mGravity = -9.81f;
    [Space]
    [Header("Bools")]
    public bool aiming = false;
    public bool hasWeapon = true;
    public bool mThrow = false;
    public bool mJumping = false;
    public bool mPull = false;
    public bool pressE = false;
    //public bool interactable = false;
    [Space]
    [Header("Parameters")]
    public float cameraZoomOffset = .3f;
    [Space]
    [Header("Tutorial UI and interact")]
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    public GameObject interactButton;
    [SerializeField]
    public GameObject movementinfo;
    [SerializeField]
    public GameObject mouseinfo;
    [SerializeField]
    public GameObject interactinfo;

    float x;
    float z;
    CameraTest cam;
    ThrowableHammer th;



    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        cam = MyCamera.GetComponent<CameraTest>();
        input = GetComponent<MovementInput>();
        MyController = GetComponent<CharacterController>();
        MyAnimator = GetComponent<Animator>();
        th = GetComponent<ThrowableHammer>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        interactButton.SetActive(false);
        movementinfo.SetActive(true);
        mouseinfo.SetActive(true);
        interactinfo.SetActive(true);
    }
    void activateTHscript() {
        if (th.enabled==false && Input.GetKeyDown(KeyCode.E)) {
            th.enabled = true;
        }
    
    }
    bool movementBool = false;
    bool isPlaying = false;
    bool setPlay = false;
    void playSound(AudioClip audio) {
        if (!audioSource.isPlaying) {
            isPlaying = false;
            
            audioSource.Stop();
        }
        if ( !isPlaying)
        {
            isPlaying = true;
            audioSource.volume = Random.Range(0.3f, 0.5f);
            audioSource.pitch = Random.Range(0.3f, 0.65f);
           // audioSource.pitch = picth;
            audioSource.PlayOneShot(audio);
            audioSource.Play();
        }

    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "PressE" && !pressE)
        {
            Debug.Log("Im here");
            if(other.name == "Thor Hammer")
            {
                other.tag = "Hammer";
            }
            pressE = true;
            interactButton.SetActive(true);
        }
    }

    void onTriggerExit(Collider collider)
    {
        if (collider.tag == "PressE")
        {
        Debug.Log("Im out");
        pressE = false;
        interactButton.SetActive(false);
        }
        
    }


    void Update()
    {

        if(pressE == true)
        {
            if (Input.GetKey(KeyCode.E) == true)
            {
                pressE = false;
                interactButton.SetActive(false);
                //GetComponent<Collider>().enabled = false;
            }
        }

        if (x != 0 || z != 0 == true && hasWeapon)
        {
            movementinfo.SetActive(false);
        }

        if(Input.GetKey(KeyCode.E) == true || Input.GetKey(KeyCode.LeftShift) == true && hasWeapon)
        {
            interactinfo.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0) == true)
        {
            mouseinfo.SetActive(false);
        }

        if (RotationSpeed<2 && x!=0) {
            RotationSpeed = 2;
        }
        activateTHscript();
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
        movementBool = x != 0 || z != 0;
        if (Input.GetButtonDown("Jump") && !mJumping)
        {
            mJumping = true;
            MyAnimator.SetTrigger("Jump");

            mSpeedyY += JumpSpeed;
        }

        if (!MyController.isGrounded)
        {
            mSpeedyY += mGravity * Time.deltaTime;
        }
        else
        {
            mSpeedyY = 0;
        }
        MyAnimator.SetFloat("SpeedY", mSpeedyY / JumpSpeed);

        if (movementBool && !audioSource.isPlaying && !mSprinting) {
            playSound(walk);
        }
        if (movementBool && !audioSource.isPlaying && mSprinting){
            playSound(run);
        }

        /*if (Input.GetButton("Horizontal"))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.volume = Random.Range(0.8f, 1f);
                audioSource.pitch = Random.Range(0.7f, 1.1f);
                audioSource.PlayOneShot(walk, volume);
            }
        }
        else
        {
            audioSource.Stop();
        }*/

        if (mJumping && mSpeedyY < 0)
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, Vector3.down, out hit, .5f, LayerMask.GetMask("Default")))
            {
                mJumping = false;
                MyAnimator.SetTrigger("Land");
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(Throw());
            //StartCoroutine(Pulling());
        }

        IEnumerator Throw()
        {
            mThrow = true;
            MyAnimator.SetTrigger("throw");
            yield return new WaitForSeconds(0.9f);
            mThrow = false;
            MyAnimator.SetTrigger("ActionFinish");
        }

        /*IEnumerator Pulling()
        {
            mPull = true;
            MyAnimator.SetTrigger("pulling");
            yield return new WaitForSeconds(0.9f);
            mPull = false;
            MyAnimator.SetTrigger("ActionFinish");
        }*/

        mSprinting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Mouse3);

        Vector3 movement = new Vector3(x, 0, z).normalized;

        //Vector3 rotatedMovement = Quaternion.Euler(0, MyCamera.transform.rotation.eulerAngles.y, 0) * movement;
        Vector3 rotatedMovement = Quaternion.Euler(cam.getTargetRotationBody()) * movement;
        Vector3 verticalMovement = Vector3.up * mSpeedyY;
        MyController.Move(Speed * Time.deltaTime * (verticalMovement + (rotatedMovement * (mSprinting ? SprintSpeed : Speed))));

        if (rotatedMovement.magnitude > 0) 
        {
            mDesiredRotation = Mathf.Atan2(rotatedMovement.x, rotatedMovement.z) * Mathf.Rad2Deg;
            mDesiredAnimationSpeed = mSprinting ? 1 : .5f;
        }
        else
        {
            mDesiredAnimationSpeed = 0;
        }

        MyAnimator.SetFloat("Speed", Mathf.Lerp(MyAnimator.GetFloat("Speed"), mDesiredAnimationSpeed, AnimationBlendSpeed * Time.deltaTime));

        Quaternion currentRotation = Quaternion.Euler(cam.getTargetRotationBody());
        Quaternion targetRotation = Quaternion.Euler(cam.getTargetRotationBody());
        //Quaternion currentRotation = transform.rotation;
        //Quaternion targetRotation = Quaternion.Euler(0, mDesiredRotation, 0);
        transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, RotationSpeed * Time.deltaTime);
    }

    
}

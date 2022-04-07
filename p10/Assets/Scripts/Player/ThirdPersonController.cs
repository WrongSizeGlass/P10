using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public Camera MyCamera;
    public float Speed = 5f;
    public float SprintSpeed = 5f;
    public float RotationSpeed = 15f;
    public float AnimationBlendSpeed = 2f;
    public float JumpSpeed = 15;
    CharacterController MyController;
    Animator MyAnimator;
    public float mDesiredRotation = 0f;
    public float mDesiredAnimationSpeed = 0f;
    public bool mSprinting = false;
    public float mSpeedyY = 0;
    public float mGravity = -9.81f;
    public float meleeSpeed;
    public bool aiming = false;
    public bool hasWeapon = true;
    public bool mThrow = false;
    public bool mJumping = false;
    public bool meleeAni = false;
    float x;
    float z;
    CameraTest cam;
    ThrowableHammer th;
    void Awake()
    {
        cam = MyCamera.GetComponent<CameraTest>();
        MyController = GetComponent<CharacterController>();
        MyAnimator = GetComponent<Animator>();
        th = GetComponent<ThrowableHammer>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    void activateTHscript() {
        if (th.enabled==false && Input.GetKeyDown(KeyCode.E)) {
            th.enabled = true;
        }
    
    }
    

    void Update()
    {
        if (RotationSpeed<2 && x!=0) {
            RotationSpeed = 2;
        }
        activateTHscript();
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");



        if (Input.GetButtonDown("Fire1")) {

            StartCoroutine(Melee());

        }

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
        
        if(mJumping && mSpeedyY < 0)
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, Vector3.down, out hit, .5f, LayerMask.GetMask("Default")))
            {
                mJumping = false;
                MyAnimator.SetTrigger("Land");
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            StartCoroutine(Throw());
        }

        IEnumerator Throw()
        {
            mThrow = true;
            MyAnimator.SetTrigger("Throw");

            yield return new WaitForSeconds(0.9f);
            mThrow = false;
            MyAnimator.SetTrigger("ActionFinish");
        }
        IEnumerator Melee()
        {

            meleeAni = true;
            //meleeSpeed = 0.1f;
            MyAnimator.SetTrigger("Melee");


            yield return new WaitForSeconds(0.9f);
            meleeAni = false;
            MyAnimator.SetTrigger("ActionFinish");
        }
       

        mSprinting = Input.GetKey(KeyCode.LeftShift);

        Vector3 movement = new Vector3(x, 0, z).normalized;

        Vector3 rotatedMovement = Quaternion.Euler(cam.getTargetRotationBody()) * movement;
        Vector3 verticalMovement = Vector3.up * mSpeedyY;
        MyController.Move((verticalMovement + (rotatedMovement * (mSprinting ? SprintSpeed : Speed))) * Speed * Time.deltaTime);

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
        transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, RotationSpeed * Time.deltaTime);
    }

    
}

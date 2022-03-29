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
    float mDesiredRotation = 0f;
    float mDesiredAnimationSpeed = 0f;
    bool mSprinting = false;
    float mSpeedyY = 0;
    float mGravity = -9.81f;
    bool mThrow = false;
    bool mJumping = false;

    void Awake()
    {
        MyController = GetComponent<CharacterController>();
        MyAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

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

        if (Input.GetKeyDown(KeyCode.Mouse0))
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
        

        mSprinting = Input.GetKey(KeyCode.LeftShift);

        Vector3 movement = new Vector3(x, 0, z).normalized;

        Vector3 rotatedMovement = Quaternion.Euler(0, MyCamera.transform.rotation.eulerAngles.y, 0) * movement;
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

        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, mDesiredRotation, 0);
        transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, RotationSpeed * Time.deltaTime);
    }

    
}

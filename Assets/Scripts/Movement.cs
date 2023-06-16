using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float walkingSpeed = 2.5f;
    public float runningSpeed = 11.5f;
    public float gravity = 2000.0f;
    public GameObject modelplayer;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    public Animator anim;
    public Joystick myJoystick;
    public CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    private float smoothTime = 0.2f;
    float rotationX = 0;
    private Vector3 velocity = Vector3.zero;
    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        movementstuff();
        if(moveDirection.magnitude > 0.1f){
            anim.SetFloat("Running",moveDirection.magnitude);
            anim.SetBool("IsRunning",true);
        }
        else{
            anim.SetFloat("Running",moveDirection.magnitude);
            anim.SetBool("IsRunning",false);
        }
    }   


    private void movementstuff(){
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * myJoystick.Vertical : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * myJoystick.Horizontal : 0;
        Vector3 targetMoveDirection = (forward * curSpeedX) + (right * curSpeedY);

        moveDirection = Vector3.SmoothDamp(moveDirection, targetMoveDirection, ref velocity, smoothTime);   



        if (!characterController.isGrounded)
        {
        }
        characterController.Move(moveDirection * Time.deltaTime);
        if(myJoystick.Vertical != 0 ||  myJoystick.Horizontal !=0){
                var newRotation = Quaternion.LookRotation((forward * curSpeedX) + (right * curSpeedY));
                modelplayer.transform.rotation = Quaternion.Slerp(modelplayer.transform.rotation, newRotation, Time.deltaTime * 10f);
            }

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        }
    }
}

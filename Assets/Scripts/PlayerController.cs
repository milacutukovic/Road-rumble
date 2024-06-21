using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;

    private int desiredLane = 1;
    public float laneDistance = 4;
    public float jumpForce;
    public float Gravity = -20;
    void Start()
    {
        controller = GetComponent<CharacterController>(); 
    }

    // Update is called once per frame
    void Update()
    {
        direction.z = forwardSpeed;
        if (controller.isGrounded)
        {
            if (SwipeManager.swipeUp)
            {
                Jump();
            }
        } else
        {
            direction.y += Gravity * Time.deltaTime;

        }



        if (SwipeManager.swipeRight) {
            desiredLane++;
            if (desiredLane == 3)
            {
                desiredLane = 2; 
            }
        }

        direction.z = forwardSpeed;
        if (SwipeManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
            {
                desiredLane = 0;
            }
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if(desiredLane == 0) {
            targetPosition += Vector3.left*laneDistance;
        } else if(desiredLane == 2) {
            targetPosition += Vector3.right * laneDistance;

        }
        transform.position = Vector3.Lerp(transform.position, targetPosition, 80*Time.fixedDeltaTime);
        controller.center = controller.center;
    }

    private void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);

    }

    private void Jump()
    {
        direction.y= jumpForce;
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
        }
    }
}

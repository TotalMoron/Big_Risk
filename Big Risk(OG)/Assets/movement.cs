using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public CharacterController controll;

    public float pSpeed = 15f;

    public float g = 21f;

    Vector3 velocity;

    public float jumpH = 2.45f;


    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded = true;
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (Input.GetKey("space"))
        {
            velocity.y = Mathf.Sqrt(jumpH * 2f * g);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            velocity.y = Mathf.Sqrt(-jumpH * 2f * g);
        }
        /*
                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    pSpeed = 10;
                    timmystime = 0.32f;
                }
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    pSpeed = 20;
                    timmystime = 0.23f;
                }*/

        velocity.y -= g * Time.deltaTime;
        controll.Move(velocity * Time.deltaTime);

        controll.Move(move * pSpeed * Time.deltaTime);
    }
}

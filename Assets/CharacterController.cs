using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float maxspeed = 1.0f;
    public float normalSpeed = 10.0f;
    public float sprintSpeed = 20.0f;

    float rotation = 0.0f;
    float camRotation = 0.0f;
    GameObject cam;
    Rigidbody myRigidbody;

    bool IsOnGround;
    public GameObject groundChecker;
    public LayerMask groundLayer;
    public float jumpForce = 300.0f;

    float rotationSpeed = 2.0f;
    float camRotationSpeed = 1.5f;

    public float maxSprint = 5.0f;
    float sprintTimer;

    Animator myAnim;
    void Start()
    {
        myAnim = GetComponentInChildren<Animator>();

        

        Cursor.lockState = CursorLockMode.Locked;

        sprintTimer = maxSprint;

        cam = GameObject.Find("Main Camera");
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        IsOnGround = Physics.CheckSphere(groundChecker.transform.position, 0.1f, groundLayer);
        myAnim.SetBool("IsOnGround", IsOnGround);

        if (IsOnGround == true && Input.GetKeyDown(KeyCode.Space))
        {
            myAnim.SetTrigger("Jumped");
            myRigidbody.AddForce(transform.up * jumpForce);
        }
       
        if (Input.GetKey(KeyCode.LeftShift) && sprintTimer > 0.0f)
        {
            maxspeed = sprintSpeed;
            sprintTimer = sprintTimer - Time.deltaTime;
        }else
        {
            maxspeed = normalSpeed;
            sprintTimer = sprintTimer + Time.deltaTime;
        }

        sprintTimer = Mathf.Clamp(sprintTimer, 0.0f, maxSprint);

        Vector3 newVelocity = (transform.forward * Input.GetAxis("Vertical") * maxspeed) + (transform.right * Input.GetAxis("Horizontal") * maxspeed);

        myAnim.SetFloat("Speed", newVelocity.magnitude);

        myRigidbody.velocity = new Vector3(newVelocity.x, myRigidbody.velocity.y, newVelocity.z); 



        //Debug.Log(Input.GetAxis("Vertical"));

        rotation = rotation + Input.GetAxis("Mouse X");
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, rotation, 0.0f));

        camRotation = camRotation + Input.GetAxis("Mouse Y");

        camRotation = Mathf.Clamp(camRotation, -40.0f, 40.0f);

        cam.transform.localRotation = Quaternion.Euler(new Vector3(-camRotation, 0.0f, 0.0f));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   //Overlap Sphere method - Check Collusion method - Raycast method
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;

    private bool jumpWasPressed;
    private Rigidbody rigidBodyComponent;
    private float horizontalInput;
    private float verticalInput;
    private Animator animationMovement;
    private float points;
    

    // Start is called before the first frame update
    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
        animationMovement = GetComponent<Animator>();
 
    }

    // Update is called once per frame
    void Update()
    {
       
        if(Input.GetKeyDown(KeyCode.Space))
        {
            jumpWasPressed = true;
            
        }

        horizontalInput = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        if (horizontalInput != 0)
        {
            rigidBodyComponent.velocity = new Vector3(horizontalInput * 5
                , rigidBodyComponent.velocity.y, 0);
            animationMovement.SetBool("isRunning", true);
        }
        else
        { 
            animationMovement.SetBool("isRunning", false);
        }


        //Checks if anything is colliding with the sphere
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
        {
            animationMovement.SetBool("isJumping", true);
            return;
        }

        animationMovement.SetBool("isJumping", false);

        if (jumpWasPressed)
        {
            rigidBodyComponent.AddForce(Vector3.up * 7, ForceMode.VelocityChange);
            jumpWasPressed = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);
            points++;
            Debug.Log(points);
        }
    }

}

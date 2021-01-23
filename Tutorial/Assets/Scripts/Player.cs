using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour //Namespace- UnityEngine.MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform = null;  //This creates a visible property (in inspector)
    [SerializeField] private LayerMask playerMask; 
    private bool jumpKey;
    private float horizontalInput;
    private Rigidbody rigidbodyComponent;
    private bool isGrounded;
    private int superJump;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if space key is pressed down
        if (Input.GetKeyDown(KeyCode.Space))
        { 
            jumpKey = true;
            Debug.Log("Space was pressed!");
        }

        horizontalInput  = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        
        // if (!isGrounded){
        //     return;  // if it is ground we don't it to just more.
        // }
        rigidbodyComponent.velocity = new Vector3(horizontalInput, rigidbodyComponent.velocity.y , 0);

        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0) 
        {
            return;

        }

        if (jumpKey)
        {

            float jumpForce = 5f;
            if (superJump > 0)
            {
                jumpForce *= 2;
                superJump--;
            }
            rigidbodyComponent.AddForce(Vector3.up*jumpForce, ForceMode.VelocityChange);
            jumpKey=false;
        }


        // velocity is specified with Vector3
        // GetComponent<Rigidbody>().velocity = new Vector3(horizontalInput, 0, 0);

        // Theoretically horizontal movements of player must have 0 component in y and z axis, but if we do that here, it will nullify the 
        // AddForce() method above. So we have to maintain y by just putting the y component there

        // Vector3(x, y, z)
    }

    private void OnCollisionEnter(Collision collision) // inherited from MonoBehaviour
    {
        // collision object has lot of information; coordinates about where the collision happened etc
       isGrounded = true;
    }
    
    private void OnCollisionExit(Collision collision) // inherited from MonoBehaviour
    {
        // collision object has lot of information; coordinates about where the collision happened etc
        isGrounded = false;
    }
    
    private void OnTriggerEnter(Collider other){  // other is a collider which has a gameObject which refers to the "other" gameObject that this class has just collided with.
        if (other.gameObject.layer == 7)  // 7 is the coin layer
        {
            Debug.Log("Collided with other gameobj");
            Destroy(other.gameObject);
            superJump++;
        }
    }

}

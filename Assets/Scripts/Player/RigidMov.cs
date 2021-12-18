using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidMov : MonoBehaviour
{
    Rigidbody rb;
    public float Speed; //normal 12
    public float JumpStrength; //normal 3

    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;

   Vector3 oldMove = new Vector3();
    bool Grounded;

    float x;
    float z;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        groundCheck = this.transform.GetChild(2);     
    }

    void FixedUpdate()
    {
        Grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(x!=0 && z != 0)
        {
            x /= 2;
            z /= 2;
        }

        Vector3 move = transform.right * x + transform.forward * z; //creates direction local to the player's rotation
        move *= Speed;
        move.y = rb.velocity.y;
        //rb.velocity -= oldMove;
        //Debug.Log("Old: " + oldMove + "\tNew: " + move + "\t Velocity: " + rb.velocity);
        //if (!(move == new Vector3(0, 0, 0)))
        rb.velocity = move; //idea was to add movement speed, move, remove the movement speed applied before to get original (zero) velocity, then re-apply the force to sto pthe added movment force being 1 then 2 then 3 then so on
        //Debug.Log("Old: " + oldMove + "\tNew: "+move+"\t Velocity: "+rb.velocity);
        oldMove = move;
        

        
    }

    private void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && Grounded)
        {
            Vector3 jump = new Vector3(0f, JumpStrength, 0f);
            rb.AddForce(jump, ForceMode.VelocityChange);
        }
    }
}

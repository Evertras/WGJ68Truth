using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController controller;
    public Animator animator;
    public float speed = 1.0f;

    Vector3 movement = new Vector3();

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");

        if (movement.sqrMagnitude > 0)
        {
            transform.forward = movement;
        }

        animator.SetBool("Moving", controller.velocity.sqrMagnitude > 0);
	}

    private void FixedUpdate()
    {
        controller.Move(movement * Time.fixedDeltaTime * speed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour {

    public CharacterController controller;
    public Animator animator;
    public float speed = 1.0f;
    public AudioSource footsteps;

    Vector3 movement = new Vector3();

    private void Start()
    {
        Cursor.visible = false;
    }

    void Update () {
        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");

        if (movement.sqrMagnitude > 0)
        {
            transform.forward = movement;
        }

        bool moving = controller.velocity.sqrMagnitude > 0;
        animator.SetBool("Moving", moving);

        if (moving && !footsteps.isPlaying)
        {
            footsteps.Play();
        }
        else if (!moving && footsteps.isPlaying)
        {
            footsteps.Stop();
        }
	}

    private void FixedUpdate()
    {
        controller.Move(movement * Time.fixedDeltaTime * speed);
    }
}

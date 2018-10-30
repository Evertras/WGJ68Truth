using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour {

    public CharacterController controller;
    public Animator animator;
    public float speed = 1.0f;
    public AudioSource footsteps;
    public Joystick joystick;

    Vector3 movement = new Vector3();
    Collider ground;

    private void Start()
    {
        ground = GameObject.FindGameObjectWithTag("Ground").GetComponent<Collider>();
    }

    void Update () {
        if (joystick != null)
        {
            movement.x = joystick.Horizontal;
            movement.z = joystick.Vertical;
        }
        else if (Input.touches.Length > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            RaycastHit hitInfo;
            if (ground.Raycast(ray, out hitInfo, float.PositiveInfinity))
            {
                var direction = Vector3.ClampMagnitude(hitInfo.point - transform.position, 1.0f);

                movement.x = direction.x;
                movement.z = direction.z;
            }
        }
        else
        {
            movement.x = Input.GetAxis("Horizontal");
            movement.z = Input.GetAxis("Vertical");
        }

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

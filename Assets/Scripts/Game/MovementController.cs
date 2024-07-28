using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Specialized;


public class MovementController : MonoBehaviour
{
    public float horizontalMove;
    public float verticalMove;
    public CharacterController player;

    public float speed;

    private Vector3 playerInput;

    public Camera mainCamera;

    private Vector3 camForward;
    private Vector3 camRight;

    private Vector3 movePlayer;

    public float gravity = 9.8f;
    public float fallVelocity;
    public float jumpForce;

    private float lastDamageTime;
    public float damageInterval = 1f; // Intervalo de tiempo entre decrementos de HP

    public GameObject levelController;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
        lastDamageTime = -damageInterval;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        CamDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;

        movePlayer = movePlayer * speed;

        player.transform.LookAt(player.transform.position + movePlayer);

        SetGravity();

        if (Input.GetButtonDown("Jump") && player.isGrounded)
        {
            SetJump();
        }

        player.Move(movePlayer * Time.deltaTime);

       // Debug.Log(player.velocity.magnitude);
    }

    void CamDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    void SetGravity()
    {
        if (player.isGrounded)
        {
            fallVelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
    }

    void SetJump()
    {
       
        fallVelocity = jumpForce;
        movePlayer.y = fallVelocity;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            GameObject.Find("Pointer").GetComponent<HPController>().DecrementHp(20f);
        }else if (other.tag == "Water")
        {
            GameObject.Find("Pointer").GetComponent<HPController>().IncrementHp(10f);
            Destroy(other.transform.parent.gameObject);
        }else if(other.tag == "PickUp")
        {
            levelController.GetComponent<LevelController>().IncrementCounter(1);
            Destroy(other.transform.parent.gameObject);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (Time.time - lastDamageTime >= damageInterval)
            {
                lastDamageTime = Time.time;
                GameObject.Find("Pointer").GetComponent<HPController>().DecrementHp(5f);
            }
        }
    }
}

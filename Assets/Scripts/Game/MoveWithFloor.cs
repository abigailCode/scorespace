using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithFloor : MonoBehaviour
{
    CharacterController player;


    GameObject groundIn;

    string groundName;

    Vector3 groundPosition;

    Vector3 lastGroundPosition;

    string lastGroundName;

    LayerMask finalmask;

    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()

    {

        player = this.GetComponent<CharacterController>();

        finalmask = ~(1 << 2);

    }



    void Update()

    {

        if (player.isGrounded)

        {

            RaycastHit hit;

            if (Physics.Raycast(transform.position, Vector3.down, out hit, 2, finalmask)) //Physics.SphereCast(transform.position, player.height / 4.2f, -transform.up, out hit)

            {

                groundIn = hit.collider.gameObject;

                groundName = groundIn.name;

                groundPosition = groundIn.transform.position;

                if ((groundPosition != lastGroundPosition) && (groundName == lastGroundName))

                {

                    this.transform.position += groundPosition - lastGroundPosition;

                    player.enabled = false;

                    player.transform.position = this.transform.position;

                    player.enabled = true;

                }

                lastGroundName = groundName;

                lastGroundPosition = groundPosition;



            }

        }

        else if (!player.isGrounded)

        {

            lastGroundName = null;

            lastGroundPosition = Vector3.zero;

        }

    }

    private void OnDrawGizmos()

    {

        player = this.GetComponent<CharacterController>();

        Gizmos.DrawRay(transform.position, Vector3.down * 2);

    }
}
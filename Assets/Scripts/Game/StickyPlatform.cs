using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{

    public Transform pointA; // Primer punto
    public Transform pointB; // Segundo punto
    public float speed = 1.0f; // Velocidad de movimiento
    private bool movingToB = true; // Estado de dirección

    public Transform rotatingCube; // Asigna el cubo giratorio en el Inspector

    private Vector3 initialOffset;
    private Quaternion initialRotationOffset;

    void Start()
    {
        initialOffset = transform.position - rotatingCube.position;
        initialRotationOffset = Quaternion.Inverse(rotatingCube.rotation) * transform.rotation;
    }

    void Update()
    {
        FollowRotation();
    }

    void FollowRotation()
    {
        transform.position = rotatingCube.position + rotatingCube.rotation * initialOffset;
        transform.rotation = rotatingCube.rotation * initialRotationOffset;
    }

    //void Update()
    //{
    //    MoveBetweenPoints();
    //}

    //void MoveBetweenPoints()
    //{
    //    if (movingToB)
    //    {
    //        // Mover la plataforma hacia el punto B
    //        transform.position = Vector3.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);

    //        // Si la plataforma alcanza el punto B, cambiar la dirección
    //        if (transform.position == pointB.position)
    //        {
    //            movingToB = false;
    //        }
    //    }
    //    else
    //    {
    //        // Mover la plataforma hacia el punto A
    //        transform.position = Vector3.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);

    //        // Si la plataforma alcanza el punto A, cambiar la dirección
    //        if (transform.position == pointA.position)
    //        {
    //            movingToB = true;
    //        }
    //    }
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslationPlatform : MonoBehaviour
{
    public Transform pointA; // Primer punto
    public Transform pointB; // Segundo punto
    public float speed = 1.0f; // Velocidad de movimiento
    private bool movingToB = true; // Estado de dirección

    void Update()
    {
        MoveBetweenPoints();
    }

    void MoveBetweenPoints()
    {
        if (movingToB)
        {
            // Mover la plataforma hacia el punto B
            transform.position = Vector3.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);

            // Si la plataforma alcanza el punto B, cambiar la dirección
            if (transform.position == pointB.position)
            {
                movingToB = false;
            }
        }
        else
        {
            // Mover la plataforma hacia el punto A
            transform.position = Vector3.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);

            // Si la plataforma alcanza el punto A, cambiar la dirección
            if (transform.position == pointA.position)
            {
                movingToB = true;
            }
        }
    }
}

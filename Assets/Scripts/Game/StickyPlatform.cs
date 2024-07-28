using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{

   

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
        Vector3 newPosition = rotatingCube.position + rotatingCube.rotation * initialOffset;
        newPosition.y = transform.position.y; // Preserve the original Y-axis position

        transform.position = newPosition;
        transform.rotation = rotatingCube.rotation * initialRotationOffset;
    }

    
}

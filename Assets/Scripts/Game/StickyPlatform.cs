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
        transform.position = rotatingCube.position + rotatingCube.rotation * initialOffset;
        transform.rotation = rotatingCube.rotation * initialRotationOffset;
    }

    
}

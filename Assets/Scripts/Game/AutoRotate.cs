using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour {

    [SerializeField] float _rotationSpeed = 0.5f;
    [SerializeField] bool _isRock = false;

    void Update() {
        transform.Rotate((_isRock ? Vector3.forward: Vector3.up) * _rotationSpeed);
    }
}

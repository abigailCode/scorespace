using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour {

    [SerializeField] float _rotationSpeed = 0.5f;

    void Update() {
        transform.Rotate(Vector3.up * _rotationSpeed);
    }
}

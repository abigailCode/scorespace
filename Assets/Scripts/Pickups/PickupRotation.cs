using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupRotation : MonoBehaviour
{
    [SerializeField] float _rotationSpeed = 5f;
    [SerializeField] float _rotationAmount = 2f; // La cantidad de rotaci�n m�xima en grados
    [SerializeField] float _verticalSpeed = 2f;
    [SerializeField] float _verticalAmount = 0.5f; // La cantidad de movimiento vertical m�xima en unidades
    Vector3 _originalPosition;

    void Start()
    {
        _originalPosition = transform.position;
    }

    void Update()
    {
        // Animaci�n de rotaci�n
        float angle = Mathf.Sin(Time.time * _rotationSpeed) * _rotationAmount;
        transform.rotation = Quaternion.Euler(angle - 90, 90, -90);

        // Animaci�n de movimiento vertical
        float verticalMovement = Mathf.Sin(Time.time * _verticalSpeed) * _verticalAmount;
        transform.position = new Vector3(_originalPosition.x, _originalPosition.y + verticalMovement, _originalPosition.z);
    }
}

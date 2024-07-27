using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {
    [SerializeField] LevelController levelController;

    void OnTriggerEnter(Collider other) {
        levelController.SetLevel(-1);
    }

    void OnCollisionEnter(Collision collision) {
        levelController.SetLevel(-1);
    }
}

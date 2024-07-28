using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPickUp : MonoBehaviour
{
    public GameObject particles;

    void OnDestroy()
    {
        particles.SetActive(true);
    }
}

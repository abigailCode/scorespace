using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPController : MonoBehaviour
{

    [SerializeField] float hp = 100f;
    public GameObject hpCube;
    [SerializeField] float initialHpCubeHeight;
   
    void Start()
    {
        if (hpCube != null)
        {
            initialHpCubeHeight = hpCube.transform.localScale.y;
        }
    }

    void UpdateHpCubeHeight()
    {
        if (hpCube != null)
        {
            float heightPercentage = hp / 100f; 
            Vector3 newScale = hpCube.transform.localScale;
            newScale.y = initialHpCubeHeight * heightPercentage;
            hpCube.transform.localScale = newScale;
        }
    }


    public void DecrementHp(float damage)
    {
        if (hp > 0)
        {
            hp -= damage;
            UpdateHpCubeHeight();
        }
        else
        {
            hp = 0;
            GameManager.Instance.GameOver();
        }
    }
}

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
            initialHpCubeHeight = hpCube.transform.localScale.x;
        }
    }

    void UpdateHpCubeHeight()
    {
        if (hpCube != null)
        {
            float heightPercentage = hp / 100f;
            Vector3 newScale = hpCube.transform.localScale;
            newScale.x = initialHpCubeHeight * heightPercentage;
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
        if (hp <= 0)
        {
            AudioManager.Instance.PlaySFX("error");
            hp = 0;
            GameManager.Instance.GameOver();
        }
        
    }


    public void IncrementHp(float heal)
    {
        if (hp < 100)
        {
            hp += heal;
            if (hp > 100) hp = 100;
            UpdateHpCubeHeight();
        }
    }

    public float GetHp() { return hp; }
}

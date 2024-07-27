using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    [SerializeField] Transform[] _playerSpawnPoints;
    [SerializeField] GameObject player;
   
   

    public void SetLevel(int level)
    {
        Debug.Log("LEVEL: "+ level);
        Debug.Log("Current player pos -> " + player.transform.position);
        Debug.Log("Next player pos -> " + _playerSpawnPoints[level].position);
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = _playerSpawnPoints[level].position;
        Debug.Log("New player pos -> " + player.transform.position);
        player.GetComponent<CharacterController>().enabled = true;

    }
}

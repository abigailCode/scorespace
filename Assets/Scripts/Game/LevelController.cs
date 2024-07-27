using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    [SerializeField] Transform[] _playerSpawnPoints;
    [SerializeField] GameObject player;
    int currentLevel = 0;
   
   

    public void SetLevel(int level)
    {
        if (level == -1) level = currentLevel; // Comes from DeathZonew
        currentLevel = level;
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = _playerSpawnPoints[level].position;
        player.GetComponent<CharacterController>().enabled = true;

    }
}

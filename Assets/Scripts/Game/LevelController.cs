using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelController : MonoBehaviour
{

    [SerializeField] Transform[] _playerSpawnPoints;
    [SerializeField] GameObject player;
    int currentLevel = 0;

    [SerializeField] TextMeshProUGUI _counterText;
    int counter = 0;
    int maxCount = 300;

    void Start()
    {
        GameObject.Find("Total").GetComponent<TextMeshProUGUI>().text = $"/{maxCount.ToString("D3")}";
    }

    public void SetLevel(int level)
    {
        if (level == -1) level = currentLevel; // Comes from DeathZonew
        currentLevel = level;
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = _playerSpawnPoints[level].position;
        AudioManager.Instance.PlaySFX("spawn");
        player.GetComponent<CharacterController>().enabled = true;

    }

    public void IncrementCounter(int points)
    {
        counter += points;
        _counterText.text = counter.ToString("D3");
    }

    public void SaveCounter()
    {
        PlayerPrefs.SetInt("score", counter);
        GameObject.Find("FinalCount").GetComponent<TextMeshProUGUI>().text = $"{counter.ToString("D3")}/{maxCount.ToString("D3")}";
    }
}

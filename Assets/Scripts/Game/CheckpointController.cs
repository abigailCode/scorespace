using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{

    public GameObject mainCamera;
    public GameObject levelController;
    private string nextLevel; 
 

    void Start() {
        //    if (!AudioManager.Instance.IsPlayingCountDown()) AudioManager.Instance.PlaySFX("portal");
        //mainCamera = GameObject.Find("Main Camera");
        //levelController = GameObject.Find("Level Controller");
        PlayerPrefs.SetInt("level", 0);
    }

    private void OnTriggerEnter(Collider other) {
       // if (!AudioManager.Instance.IsPlayingCountDown()) AudioManager.Instance.PlaySFX("portal");
        //if (PlayerPrefs.GetInt == 10) { GameManager.Instance.GameWon(); return; }
        //SCManager.Instance.LoadScene(nextLevel);
        NextLevel();
    }

    //IEnumerator NextLevel()
    //{
    //    int currentLevel = PlayerPrefs.GetInt("level", 1);
    //    if (currentLevel == 10) GameManager.Instance.GameWon();
    //    else
    //    {
    //        PlayerPrefs.SetInt("level", currentLevel + 1);
    //        // Check if level bigger than levels count
    //        if (PlayerPrefs.GetInt("level") > PlayerPrefs.GetInt("levelsPassed", 0))
    //            PlayerPrefs.SetInt("levelsPassed", PlayerPrefs.GetInt("level"));
    //        PlayerPrefs.Save();
    //        yield return new WaitForSeconds(1);
    //        SCManager.Instance.LoadScene("Game");
    //    }
    //}

    void NextLevel()
    {
        int currentLevel = PlayerPrefs.GetInt("level", 0) + 1;
        if (currentLevel == 10) GameManager.Instance.GameWon();
        else
        {
            PlayerPrefs.SetInt("level", currentLevel);
            // Check if level bigger than levels count
            if (PlayerPrefs.GetInt("level") > PlayerPrefs.GetInt("levelsPassed", 0))
            PlayerPrefs.SetInt("levelsPassed", PlayerPrefs.GetInt("level"));
            PlayerPrefs.Save();
            mainCamera.GetComponent<CameraBehaviour>().SetCameraPosition(currentLevel);
            levelController.GetComponent<LevelController>().SetLevel(currentLevel);
            //yield return new WaitForSeconds(1);
            //  SCManager.Instance.LoadScene("Game");

        }
    }

}

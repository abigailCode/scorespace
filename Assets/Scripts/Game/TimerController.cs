using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    TextMeshProUGUI _timerText, _countdownText;
    Coroutine _timerCoroutine;
    [SerializeField] float remainingTime = 0f;

    public void Start()
    {
        _timerText = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
        _timerCoroutine = StartCoroutine(UpdateTimer());
    }

    public void StopTimer()
    {
        AudioManager.Instance.StopSFX();
        StopCoroutine(_timerCoroutine);
    }

    IEnumerator UpdateTimer()
    {
        _timerText.text = FormatTime(remainingTime);
        while (true)
        {
            yield return new WaitForSeconds(1);
            _timerText.text = FormatTime(++remainingTime);
           // if (remainingTime <= 15) ShowCountdown();
        }
        //GameOver();
    }

    string FormatTime(float time)
    {
        string minutes = (Mathf.Floor(Mathf.Round(time) / 60)).ToString();
        string seconds = (Mathf.Round(time) % 60).ToString();

        if (minutes.Length == 1) minutes = "0" + minutes;
        if (seconds.Length == 1) seconds = "0" + seconds;
        return minutes + ":" + seconds;
    }

    public void RestartTime(float time) => remainingTime = time;

}

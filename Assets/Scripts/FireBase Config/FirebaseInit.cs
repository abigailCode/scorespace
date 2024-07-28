using Firebase;
using Firebase.Analytics;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class FirebaseInit : MonoBehaviour {
    public static FirebaseInit Instance;
    FirebaseController firebaseController;
    private int itemsPerPage = 10; // Número de elementos por página
    private int currentPage = 0;
    public Transform contentPanel;
    public GameObject listItemPrefab; // Prefab del panel de cada fila
    public Button nextPageButton;
    public Button prevPageButton;
    List<Ranking> rankings;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    void Start() {
        firebaseController = new FirebaseController();
    }

    public void LoadRanking()
    {
        firebaseController.GetRanking();
    }

    public void SaveRanking(string username, int score, float time)
    {
        firebaseController.UpdateRanking(username, score, time);
    }


    public void ShowPage(int page, List<Ranking> rankingList = null)
    {
        if (rankingList != null) rankings = rankingList;
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject); // Limpiar el contenido actual
        }

        int startItem = page * itemsPerPage;
        int endItem = Mathf.Min(startItem + itemsPerPage, rankings.Count);

        for (int i = startItem; i < endItem; i++)
        {
            Ranking ranking = rankings[i];
            GameObject newItem = Instantiate(listItemPrefab, contentPanel);
            newItem.transform.Find("UserText").GetComponent<TextMeshProUGUI>().text = ranking.username;
            newItem.transform.Find("TimeText").GetComponent<TextMeshProUGUI>().text = FormatTime(ranking.time);
            newItem.transform.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = ranking.score.ToString("D3");
            if (i == 0) newItem.transform.Find("Gold").gameObject.SetActive(true);
            if (i == 1) newItem.transform.Find("Silver").gameObject.SetActive(true);
            if (i == 2) newItem.transform.Find("Bronze").gameObject.SetActive(true);
        }

        //prevPageButton.interactable = page > 0;
        //nextPageButton.interactable = endItem < rankings.Count;
    }

    private void NextPage()
    {
        currentPage++;
        ShowPage(currentPage);
    }

    private void PrevPage()
    {
        currentPage--;
        ShowPage(currentPage);
    }

    public string FormatTime(float time)
    {
        string minutes = (Mathf.Floor(Mathf.Round(time) / 60)).ToString();
        string seconds = (Mathf.Round(time) % 60).ToString();

        if (minutes.Length == 1) minutes = "0" + minutes;
        if (seconds.Length == 1) seconds = "0" + seconds;
        return minutes + ":" + seconds;
    }
}
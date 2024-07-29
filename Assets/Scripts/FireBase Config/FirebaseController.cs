using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;
using UnityEngine;
using System.Collections.Generic;
using TMPro;


public class FirebaseController {
    FirebaseApp _firebaseApp = null;
    DatabaseReference _database = null;

    private List<Ranking> rankings = new List<Ranking>();

    public FirebaseController() {
        Firebase.FirebaseApp.CheckDependenciesAsync().ContinueWith(checkTask =>
        {
            Firebase.DependencyStatus status = checkTask.Result;
            if (status == DependencyStatus.Available)
            {
                _firebaseApp = FirebaseApp.Create(new AppOptions()
                {
                    ApiKey = "AIzaSyCGm-or8IQGT8heO8FQ91bG4iOVHZ87M9w",
                    DatabaseUrl = new Uri("https://speedrunning-jam-default-rtdb.europe-west1.firebasedatabase.app/"),
                    ProjectId = "speedrunning-jam",
                    StorageBucket = "speedrunning-jam.appspot.com",
                    AppId = "1:411173312812:android:5674d12b238e0e9847f489",
                    MessageSenderId = ""
                });

                _database = FirebaseDatabase.GetInstance(_firebaseApp).RootReference;
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + status);
            }
        });
    }

    public void UpdateRanking(string username, int score, float time) {
        if (_database == null) return;

        string key = _database.Child("ranking").Push().Key;
        Ranking ranking = new Ranking(username, score, time);
        string json = JsonUtility.ToJson(ranking);
        _database.Child("ranking").Child(key).SetRawJsonValueAsync(json).ContinueWithOnMainThread(task => {
            if (task.IsCompleted) Debug.Log("Score written successfully.");
            else Debug.LogError("Failed to write score: " + task.Exception);
        });
    }

    public void GetRanking() {
        if (_database == null) return;
        rankings.Clear();
        _database.Child("ranking").GetValueAsync().ContinueWithOnMainThread(task => {
            if (task.IsCompleted) {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot childSnapshot in snapshot.Children) {
                    string json = childSnapshot.GetRawJsonValue();
                    Ranking ranking = JsonUtility.FromJson<Ranking>(json);
                    rankings.Add(ranking);
                    Debug.Log(ranking);
                }
                
            } else {
                Debug.LogError("Failed to read scores: " + task.Exception);
            }
            rankings.Sort((x, y) => {
                int timeComparison = x.time.CompareTo(y.time);
                if (timeComparison == 0)
                {
                    // If times are equal, compare by score (higher score is better, so we reverse the comparison)
                    return y.score.CompareTo(x.score);
                }
                return timeComparison;
            });
            FirebaseInit.Instance.ShowPage(0, rankings);
        });
    }

    public List<Ranking> GetRankingList() => rankings;
}

[System.Serializable]
public class Ranking {
    public string username;
    public int score;
    public float time;

    public Ranking() { }

    public Ranking(string username, int score, float time) {
        this.username = username;
        this.score = score;
        this.time = time;
    }
}

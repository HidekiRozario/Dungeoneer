using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreSenderHTTP : MonoBehaviour
{

    Token tokenText;
    public PlayerInfo playerStats;
    public Top10 top10;

    public bool isFinnished = false;

    void Start()
    {
    }


    public IEnumerator CallLogin(int score, string name)
    {
        /*gets new token*/
        string url = "https://api.globalstats.io/oauth/access_token";
        string logindataFormString = "grant_type=client_credentials&scope=endpoint_client&client_id=nF1bQiSJvTRnkbJeie0OuADtnAepVWfjBgurznb4&client_secret=2rBebPwqQlUqYnOzBduytOaJjBjGNnXQPy40oAqe";
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(logindataFormString);

        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);

        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        yield return request.SendWebRequest();

        if (request.error != null)
        {
        }
        else
        {
            tokenText = Token.CreateFromJSON(request.downloadHandler.text);
        }





        /*sends data*/
        url = "https://api.globalstats.io/v1/statistics/";
        logindataFormString = "{\"name\":\"" + name + "\",\"values\":{\"score\":" + score + "}}";
         request = new UnityWebRequest(url, "POST");
        bodyRaw = Encoding.UTF8.GetBytes(logindataFormString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        request.SetRequestHeader("Authorization", "Bearer " + tokenText.access_token);
        yield return request.SendWebRequest();

        if (request.error != null)
        {
        }
        else
        {
            playerStats = PlayerInfo.CreateFromJSON(request.downloadHandler.text);
        }



        
        /*gets LeaderBoard*/
        url = "https://api.globalstats.io/v1/gtdleaderboard/score";
        logindataFormString = "{\"limit\":10}";
        request = new UnityWebRequest(url, "POST");
        bodyRaw = Encoding.UTF8.GetBytes(logindataFormString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        request.SetRequestHeader("Authorization", "Bearer " + tokenText.access_token);
        yield return request.SendWebRequest();

        if (request.error != null)
        {
        }
        else
        {
            top10 = Top10.CreateFromJSON(request.downloadHandler.text);
            
        }
        isFinnished = true;
    }


    private bool AllRequestsDone(List<UnityWebRequestAsyncOperation> requests)
    {
        foreach (var r in requests)
        {
            if (!r.isDone) return false;
        }
        return true;
    }

    [System.Serializable]
    public class PlayerInfo
    {

        public string name;
        public string _id;
        public data[] values;
        public static PlayerInfo CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<PlayerInfo>(jsonString);
        }

    }

    [System.Serializable]
    public struct data
    {
        public string key;
        public int value;
        public string sorting;
        public int rank;
    }

    [System.Serializable]
    public class Token
    {
        public string access_token;
        public static Token CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<Token>(jsonString);
        }

    }
   
    [System.Serializable]
    public class Top10
    {
        
        public BoardData[] data;
        public static Top10 CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<Top10>(jsonString);
        }

    }

    [System.Serializable]
    public struct BoardData
    {
        public string name;
        public string user_profile;
        public string user_icon;
        public int rank;
        public int value;
    }

}

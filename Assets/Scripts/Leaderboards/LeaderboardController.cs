using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardController : MonoBehaviour
{
    class Player {
        public Player(int position, string playerName, int score)
        {
            this.position = position;
            this.playerName = playerName;
            this.score = score;
        }

        public int position { get; set; }
        public string playerName { get; set; }
        public int score { get; set; }
    }

    [Header("LeaderBoard")]
    public Text[] playersText;
    Player[] players;
    private int score;

    [Header("Submit")]
    public Button submit;
    public InputField playerName;
    public Animator panelClose;
    public Animator leaderOpen;
    public Text errorTXT;

    [Header("Container")]
    LevelContainer container;

    private bool LBoardWasSet = false;


    [Header("Networking")]
    public ScoreSenderHTTP requestSender;//TODO everything else

    private void Start()
    {
        submit.onClick.AddListener(SubmitName);
        container = GameObject.FindWithTag("Container").GetComponent<LevelContainer>();
        score = container.score;
        players = new Player[10];
    }

    void SetPlayer(Player player, Text playerText)
    {
        playerText.text = player.position.ToString() + " " + player.playerName.ToString() + " " + player.score.ToString();
    }

    void SubmitName()
    {
        if (playerName.text.Length > 2)
        {
            errorTXT.text = "";
            panelClose.SetBool("close", true);
            leaderOpen.SetBool("open", true);

            StartCoroutine(requestSender.CallLogin(score, playerName.text));

            players[0] = new Player(0, playerName.text, score);
            SetPlayer(players[0], playersText[0]);
        }
        else
        {
            errorTXT.text = "Player name must have atleast 3 characters";
        }
    }


    private void Update()
    {

        if (!LBoardWasSet && requestSender.isFinnished)
        {
            LBoardWasSet = true;
            bool localPlayerWasPlaced = false;
            for (int i = 0; i < requestSender.top10.data.Length; i++){
                if(!localPlayerWasPlaced && requestSender.playerStats.values[0].rank == requestSender.top10.data[i].rank)
                {
                    localPlayerWasPlaced = true;
                    SetPlayer(new Player(requestSender.playerStats.values[0].rank, playerName.text, requestSender.playerStats.values[0].value), playersText[i]);
                    playersText[i].color = new Color32(94, 235, 52, 255);
                }
                else
                {
                    SetPlayer(new Player(requestSender.top10.data[i].rank, requestSender.top10.data[i].name, requestSender.top10.data[i].value), playersText[i]);
                }
            }
            if (!localPlayerWasPlaced)
            {
                SetPlayer(new Player(requestSender.playerStats.values[0].rank, playerName.text, requestSender.playerStats.values[0].value), playersText[10]);
                playersText[10].color = new Color32(94, 235, 52, 255);
            }
        }
    }
}

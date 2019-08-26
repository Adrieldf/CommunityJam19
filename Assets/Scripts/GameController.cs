using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    #region Help Variables
    public enum HandType
    {
        None = -1,
        Rock = 0,
        Paper = 1,
        Scissors = 2
    }

    public List<string> Tips = new List<string>()
    {
        "I think he is going to play SCISSORS, so you should play ROCK",
        "I think he is going to play PAPER, so you should play SCISSORS",
        "I think he is going to play ROCK, so you should play PAPER",
        "Maybe he will go with SCISSORS, you should go with ROCK",
        "Maybe he will go with PAPER, you should go with SCISSORS",
        "Maybe he will go with ROCK, you should go with PAPER",
        "Go for SCISSORS, trust me",
        "Go for PAPER, trust me",
        "Go for ROCK, trust me",
        "I actually don't know this turn",
        "Maybe you should trust yourself more",
        "This turn is on yours",
        "The other guy is really good at this, maybe ROCK now?",
        "The other guy is really good at this, maybe PAPER now?",
        "The other guy is really good at this, maybe SCISSORS now?",
        "You definitely should go with SCISSORS",
        "You definitely should go with PAPER",
        "You definitely should go with ROCK",
        "I don't know, ROCK maybe?",
        "I don't know, PAPER maybe?",
        "I don't know, SCISSORS maybe?",
    };
    #endregion

    #region Player
    [SerializeField]
    private GameObject PlayerRockSpeak = null;
    [SerializeField]
    private GameObject PlayerPaperSpeak = null;
    [SerializeField]
    private GameObject PlayerScissorsSpeak = null;
    #endregion

    #region Enemy
    [SerializeField]
    private GameObject EnemyRockSpeak = null;
    [SerializeField]
    private GameObject EnemyPaperSpeak = null;
    [SerializeField]
    private GameObject EnemyScissorsSpeak = null;
    #endregion

    #region UI
    [SerializeField]
    private Button RockButton = null;
    [SerializeField]
    private Button PaperButton = null;
    [SerializeField]
    private Button ScissorsButton = null;
    [SerializeField]
    private GameObject TipPanel = null;
    [SerializeField]
    private TextMeshProUGUI TipText = null;
    [SerializeField]
    private TextMeshProUGUI CountDownText = null;
    #endregion

    private float TimeRemaining = 0f;
    private bool TimerRunning = false;
    private HandType PlayerSelected = HandType.None;
    private HandType EnemySelected = HandType.None;


    void Start()
    {
        ShowPlayerSelected(HandType.None);
        ShowEnemySelected(HandType.None);
        SetButtons(true);
        StartCounter(10f);
    }
    void Update()
    {
        if (TimerRunning)
        {
            if (TimeRemaining > 0.1f)
            {
                TimeRemaining -= Time.deltaTime;
                CountDownText.text = Mathf.FloorToInt(TimeRemaining).ToString();
            }
            else
            {
                TipPanel.SetActive(false);
                CountDownText.gameObject.SetActive(false);
                TimerRunning = false;
            }
        }
    }
    public void SetRandomTip()
    {
        TipText.text = Tips[Random.Range(0, 2 /*Tips.Count*/)];
    }
    public void SetButtons(bool active)
    {
        RockButton.interactable = PaperButton.interactable = ScissorsButton.interactable = active;
    }
    public void ShowPlayerSelected(HandType hand)
    {
        PlayerRockSpeak.SetActive(hand == HandType.Rock);
        PlayerPaperSpeak.SetActive(hand == HandType.Paper);
        PlayerScissorsSpeak.SetActive(hand == HandType.Scissors);
    }
    public void ShowEnemySelected(HandType hand)
    {
        EnemyRockSpeak.SetActive(hand == HandType.Rock);
        EnemyPaperSpeak.SetActive(hand == HandType.Paper);
        EnemyScissorsSpeak.SetActive(hand == HandType.Scissors);
    }

    public void StartCounter(float time)
    {
        TimeRemaining = time;
        TimerRunning = true;
        CountDownText.gameObject.SetActive(true);
        if (time > 9)
        {
            TipPanel.SetActive(true);
            SetRandomTip();
        }
    }
    public void OnRockClick()
    {
        PlayerSelected = HandType.Rock;
    }
    public void OnPaperClick()
    {
        PlayerSelected = HandType.Paper;
    }
    public void OnScissorsClick()
    {
        PlayerSelected = HandType.Scissors;
    }

    public void EndRound()
    {
        ShowEnemySelected(EnemySelected);
        ShowPlayerSelected(PlayerSelected);
    }

    public void CalculateEnemyPick()
    {
        if(PlayerSelected == HandType.None)
        {
            //game over, I think?

        }
        //Well, the jam theme was "The Game Is A Liar", it's not my fault ¯\_(ツ)_/¯
        int chance = Random.Range(0, 20);
        if (chance == 19) //5%
        {
            //victory
            if (PlayerSelected == HandType.Paper)
                EnemySelected = HandType.Rock;
            else if (PlayerSelected == HandType.Rock)
                EnemySelected = HandType.Scissors;
            else //PlayerSelected == HandType.Scissors
                EnemySelected = HandType.Paper;

        }
        else if (chance < 19 && chance > 6) //60%
        {
            //tie
            EnemySelected = PlayerSelected;
        }
        else
        {
            //loose
            if (PlayerSelected == HandType.Paper)
                EnemySelected = HandType.Scissors;
            else if (PlayerSelected == HandType.Rock)
                EnemySelected = HandType.Paper;
            else //PlayerSelected == HandType.Scissors
                EnemySelected = HandType.Rock;
        }

        EndRound();
    }

}

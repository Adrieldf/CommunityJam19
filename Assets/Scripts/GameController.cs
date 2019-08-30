using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    /* DISCLAIMER:
     * I know, I know,
     * it's all a big one god script and that's bad (really bad),
     * but I ain't got that much time to make this game so for now it goes as this.
     * Just a reminder if you look at this and please don't judge me like that LOL.
     * Thanks for checking the code btw.
     * 
     * May the force be with you
     */

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
    [SerializeField]
    private Animator PlayerAnimator = null;
    #endregion

    #region Enemy
    [SerializeField]
    private GameObject EnemyRockSpeak = null;
    [SerializeField]
    private GameObject EnemyPaperSpeak = null;
    [SerializeField]
    private GameObject EnemyScissorsSpeak = null;
    [SerializeField]
    private Animator EnemyAnimator = null;

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
    [SerializeField]
    private TextMeshProUGUI ResultText = null;
    [SerializeField]
    private TextMeshProUGUI PickYourHand = null;
    #endregion

    [SerializeField]
    private float RoundTime = 10f;
    private float TimeRemaining = 0f;
    private bool TimerRunning = false;
    private HandType PlayerSelected = HandType.None;
    private HandType EnemySelected = HandType.None;
    private string resultText = string.Empty;
    void Start()
    {
        StartRound();
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
                if (PlayerSelected == HandType.None)
                    CalculateEnemyPick();
            }
        }
    }
    public void SetRandomTip()
    {
        TipText.text = Tips[Random.Range(0, Tips.Count)];
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
            ResultText.gameObject.SetActive(false);
            PickYourHand.gameObject.SetActive(true);
            TipPanel.SetActive(true);
            SetRandomTip();
        }
    }
    public void OnRockClick()
    {
        PlayerSelected = HandType.Rock;
        CalculateEnemyPick();
    }
    public void OnPaperClick()
    {
        PlayerSelected = HandType.Paper;
        CalculateEnemyPick();
    }
    public void OnScissorsClick()
    {
        PlayerSelected = HandType.Scissors;
        CalculateEnemyPick();
    }

    public void EndRound()
    {
        TimeRemaining = 0f;
        ShowEnemySelected(EnemySelected);
        ShowPlayerSelected(PlayerSelected);
        PickYourHand.gameObject.SetActive(false);
        ResultText.text = resultText;
        ResultText.gameObject.SetActive(true);
        SetButtons(false);
        StartCoroutine(Wait(2));
    }
    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        StartRound();

    }
    public void StartRound()
    {
        PlayerSelected = HandType.None;
        EnemySelected = HandType.None;
        ShowPlayerSelected(HandType.None);
        ShowEnemySelected(HandType.None);
        SetButtons(true);
        StartCounter(RoundTime);

    }
    public void CalculateEnemyPick()
    {
        ResultText.gameObject.SetActive(false);
        if (PlayerSelected == HandType.None)
        {
            //pega um aleatorio caso nao tenha selecionado nenhum
            int pick = Random.Range(0, 3);
            switch (pick)
            {
                default:
                case 0:
                    PlayerSelected = HandType.Rock;
                    break;
                case 1:
                    PlayerSelected = HandType.Paper;
                    break;
                case 2:
                    PlayerSelected = HandType.Scissors;
                    break;
            }
        }
        //Well, the jam theme was "The Game Is A Liar", it's not my fault ¯\_(ツ)_/¯
        int chance = Random.Range(0, 20);
        if (chance == 19 || chance == 18 || chance == 17) //15%
        {
            //victory
            if (PlayerSelected == HandType.Paper)
                EnemySelected = HandType.Rock;
            else if (PlayerSelected == HandType.Rock)
                EnemySelected = HandType.Scissors;
            else //PlayerSelected == HandType.Scissors
                EnemySelected = HandType.Paper;
            resultText = "Victory!!";
            EnemyAnimator.SetTrigger("mad");
            PlayerAnimator.SetTrigger("happy");

        }
        else if (chance < 17 && chance > 6) //45%
        {
            //tie
            EnemySelected = PlayerSelected;
            resultText = "It's a tie!";
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
            resultText = "Try again";
            EnemyAnimator.SetTrigger("happy");
            PlayerAnimator.SetTrigger("mad");
        }

        EndRound();
    }
    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}

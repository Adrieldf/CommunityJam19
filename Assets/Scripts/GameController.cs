using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public enum HandType
    {
        None = -1,
        Rock = 0,
        Paper = 1,
        Scissors = 2
    }
    #region Enemy
    [SerializeField]
    private GameObject RockSpeak = null;
    [SerializeField]
    private GameObject PaperSpeak = null;
    [SerializeField]
    private GameObject ScissorsSpeak = null;
    #endregion

    #region UI
    [SerializeField]
    private GameObject RockButton = null;
    [SerializeField]
    private GameObject PaperButton = null;
    [SerializeField]
    private GameObject ScissorsButton = null;
    #endregion


    private HandType Selected = HandType.None;

    void Start()
    {
        ShowSelected(HandType.None);
    }

    public void ShowSelected(HandType hand)
    {
        RockSpeak.SetActive(hand == HandType.Rock);
        PaperSpeak.SetActive(hand == HandType.Paper);
        ScissorsSpeak.SetActive(hand == HandType.Scissors);
    }

    public void OnRockClick()
    {
        Selected = HandType.Rock;
    }
    public void OnPaperClick()
    {
        Selected = HandType.Paper;
    }
    public void OnScissorsClick()
    {
        Selected = HandType.Scissors;
    }
}

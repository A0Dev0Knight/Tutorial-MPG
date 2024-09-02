using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Search;

public class GameStartCountdownUI : MonoBehaviour
{
    private const string NUMBER_POPUP = "NumberPopup";

    [SerializeField] private TextMeshProUGUI countdownText;

    private Animator animator;
    private int previousCountdownNumber;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        GameHandler.Instance.OnStateChange += GameHandler_OnStateChange;
        countdownText.gameObject.SetActive(false);
    }

    private void Update()
    {
        int countdownNumber = Mathf.CeilToInt(GameHandler.Instance.GetCountdownToStartTimer());
        countdownText.text = countdownNumber.ToString();

        if (countdownNumber != previousCountdownNumber )
        {
            previousCountdownNumber = countdownNumber;
            animator.SetTrigger(NUMBER_POPUP);
            SoundManager.Instance.PlayCountdownSound();
        }

    }
    private void GameHandler_OnStateChange(object sender, System.EventArgs e)
    {
        if (GameHandler.Instance.IsCountdownToStartActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        countdownText.gameObject.SetActive(true);
    }

    private void Hide()
    {
        countdownText.gameObject.SetActive(false);

    }
}

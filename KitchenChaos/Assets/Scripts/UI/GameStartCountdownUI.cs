using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Search;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;

    private void Start()
    {
        GameHandler.Instance.OnStateChange += GameHandler_OnStateChange;
        countdownText.gameObject.SetActive(false);
    }

    private void Update()
    {
        countdownText.text = Mathf.Ceil(GameHandler.Instance.GetCountdownToStartTimer()).ToString();

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

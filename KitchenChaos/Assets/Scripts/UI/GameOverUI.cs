using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;

    private void Awake()
    {
        Hide();
    }
    private void Start()
    {
        GameHandler.Instance.OnStateChange += GameHandler_OnStateChange;
    }

    private void GameHandler_OnStateChange(object sender, System.EventArgs e)
    {
        if (GameHandler.Instance.IsGameOver())
        {
            Show();
            recipesDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRecipesAmount().ToString();

        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

}

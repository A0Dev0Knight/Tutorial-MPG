using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;

    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button retryButton;

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        retryButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.GameScene);
        });
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
        mainMenuButton.Select();

    }

    private void Hide()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

}

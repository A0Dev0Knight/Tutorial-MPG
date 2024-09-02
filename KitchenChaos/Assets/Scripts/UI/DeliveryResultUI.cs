using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{

    private const string POP_UP = "PopUp";

    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI messageText;

    [SerializeField] private Color successColor;
    [SerializeField] private Color failColor;

    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failedSprite;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;

        Hide();
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        Show();
        animator.SetTrigger(POP_UP);
        backgroundImage.color = failColor;
        iconImage.sprite = failedSprite;
        messageText.text = "DELIVERY\nFAILED";

    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        Show();
        animator.SetTrigger(POP_UP);
        backgroundImage.color = successColor;
        iconImage.sprite = successSprite;
        messageText.text = "DELIVERY\nSUCCESS";

    }

    private void Show()
    {
        this.gameObject.SetActive(true);
    }

    private void Hide()
    {
        this.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keyoard_moveUpText;
    [SerializeField] private TextMeshProUGUI keyoard_moveDownText;
    [SerializeField] private TextMeshProUGUI keyoard_moveLeftText;
    [SerializeField] private TextMeshProUGUI keyoard_moveRightText;
    [SerializeField] private TextMeshProUGUI keyoard_interactText;
    [SerializeField] private TextMeshProUGUI keyoard_interactAlternateText;
    [SerializeField] private TextMeshProUGUI keyoard_PauseText;

    /*[SerializeField] private TextMeshProUGUI gamepad_moveText;*/
    [SerializeField] private TextMeshProUGUI gamepad_interactText;
    [SerializeField] private TextMeshProUGUI gamepad_interactAlternateText;
    [SerializeField] private TextMeshProUGUI gamepad_PauseText;

    private void Start()
    {
        UpdateVisuals();
        GameInput.Instance.OnBindingRebind += GameInput_OnBindingRebind;
        GameHandler.Instance.OnStateChange += GameHandler_OnStateChange;
        Show();
    }

    private void GameHandler_OnStateChange(object sender, System.EventArgs e)
    {
        if (GameHandler.Instance.IsCountdownToStartActive())
        {
            Hide();
        }
    }

    private void GameInput_OnBindingRebind(object sender, System.EventArgs e)
    {
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        keyoard_moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up); 
        keyoard_moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down); 
        keyoard_moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left); 
        keyoard_moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right); 
        keyoard_interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact); 
        keyoard_interactAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact_Alternate); 
        keyoard_PauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause); 
        
        /*gamepad_moveText.text = GameInput.Instance.GetBindingText(GameInput.Binding.);*/ 
        gamepad_interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact); 
        gamepad_interactAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact_Alternate);
        gamepad_PauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause); 

    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}

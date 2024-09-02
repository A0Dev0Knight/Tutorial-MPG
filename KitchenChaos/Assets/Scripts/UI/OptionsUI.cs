using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance {  get; private set; }

    [SerializeField] private Button soundEffectsBtn;
    [SerializeField] private TextMeshProUGUI soundEffectsBtnText;
    [SerializeField] private Button musicBtn;
    [SerializeField] private TextMeshProUGUI musicBtnText;
    [SerializeField] private Button backButton;

    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAlternateText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private TextMeshProUGUI gamepadInteractText;
    [SerializeField] private TextMeshProUGUI gamepadInteractAlternateText;
    [SerializeField] private TextMeshProUGUI gamepadPauseText;

    [SerializeField] private Button moveUpBtn;
    [SerializeField] private Button moveDownBtn;
    [SerializeField] private Button moveLeftBtn;
    [SerializeField] private Button moveRightBtn;
    [SerializeField] private Button interactBtn;
    [SerializeField] private Button interactAlternateBtn;
    [SerializeField] private Button pauseBtn;
    [SerializeField] private Button gamepadInteractBtn;
    [SerializeField] private Button gamepadInteractAlternateBtn;
    [SerializeField] private Button gamepadPauseBtn;


    [SerializeField] private Transform pressToRebindKeyTransform;


    private Action onCloseButtonAction;
    private void Awake()
    {
        Instance = this;

        soundEffectsBtn.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisuals();
        });

        musicBtn.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisuals();
        });

        backButton.onClick.AddListener(() =>
        {
            Hide();
            onCloseButtonAction();
        });

        moveUpBtn.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Up);
        });

        moveDownBtn.onClick.AddListener(() => {
            
            RebindBinding(GameInput.Binding.Move_Down);
        });

        moveLeftBtn.onClick.AddListener(() => {
            
            RebindBinding(GameInput.Binding.Move_Left);
        });

        moveRightBtn.onClick.AddListener(() => {
            
            RebindBinding(GameInput.Binding.Move_Right);
        });
        interactBtn.onClick.AddListener(() => {
            
            RebindBinding(GameInput.Binding.Interact);
        });
        interactAlternateBtn.onClick.AddListener(() => {
            
            RebindBinding(GameInput.Binding.Interact_Alternate);
        });
        pauseBtn.onClick.AddListener(() =>
        {

            RebindBinding(GameInput.Binding.Pause);
        });

        gamepadInteractBtn.onClick.AddListener(() => {

            RebindBinding(GameInput.Binding.Gamepad_Interact);
        });
        gamepadInteractAlternateBtn.onClick.AddListener(() => {

            RebindBinding(GameInput.Binding.Gamepad_Interact_Alternate);
        });
        gamepadPauseBtn.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Gamepad_Pause);
        });

    }

    private void Start()
    {
        GameHandler.Instance.OnGameUnpaused += GameHandler_OnGameUnpaused;
        UpdateVisuals();
        Hide();
        HidePressToRebindKey();
    }

    private void GameHandler_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisuals()
    {
        soundEffectsBtnText.text = "Sound effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f).ToString();
        musicBtnText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10).ToString();

        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact_Alternate);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);

        gamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        gamepadInteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact_Alternate);
        gamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);


    }

    public void Show(Action onCloseButtonAction)
    {
        this.onCloseButtonAction = onCloseButtonAction;
        this.gameObject.SetActive(true);
        soundEffectsBtn.Select();
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
    public void ShowPressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }
    public void HidePressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Binding binding)
    {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBinding(binding, () => {
            HidePressToRebindKey();
            UpdateVisuals();

        });
    }
}

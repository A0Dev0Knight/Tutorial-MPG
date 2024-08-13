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
        });
    }

    private void Start()
    {
        GameHandler.Instance.OnGameUnpaused += GameHandler_OnGameUnpaused;
        UpdateVisuals();
        Hide();
    }

    private void GameHandler_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisuals()
    {
        soundEffectsBtnText.text = "Sound effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f).ToString();
        musicBtnText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10).ToString();
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}

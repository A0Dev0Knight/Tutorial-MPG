using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonQuit;

    private void Awake()
    {
        buttonPlay.Select();
        buttonPlay.onClick.AddListener( () =>
        {
            Loader.Load(Loader.Scene.GameScene);
        });
        buttonQuit.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    
        Time.timeScale = 1.0f;
    }

}

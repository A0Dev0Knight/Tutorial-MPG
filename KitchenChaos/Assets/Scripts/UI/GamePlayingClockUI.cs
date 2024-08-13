using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] private Image timerVisual;

    private void Update()
    {
        timerVisual.fillAmount = GameHandler.Instance.GetPlayingTimerNormalised();
    }
}

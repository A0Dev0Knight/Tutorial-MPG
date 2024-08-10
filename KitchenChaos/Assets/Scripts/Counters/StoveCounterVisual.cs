using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{

    [SerializeField] private GameObject stoveOnVisual;
    [SerializeField] private GameObject particlesOnVisual;
    [SerializeField] private StoveCounter stoveCounter;


    private void Start()
    {
        stoveCounter.OnStateChange += StoveCounter_OnStateChange;
    }

    private void StoveCounter_OnStateChange(object sender, StoveCounter.OnStateChangeEventArgs e)
    {
        if (e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried)
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
        stoveOnVisual.SetActive(true); 
        particlesOnVisual.SetActive(true);
    }
    private void Hide()
    {
        stoveOnVisual.SetActive(false);
        particlesOnVisual.SetActive(false);
    }

}

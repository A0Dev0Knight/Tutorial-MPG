using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{

    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] selectedCounterVisualArray;

    void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == baseCounter)
        {
            foreach (GameObject visualObject in selectedCounterVisualArray)
            {
                visualObject.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject visualObject in selectedCounterVisualArray)
            {
                visualObject.SetActive(false);
            }
        }
    }
}

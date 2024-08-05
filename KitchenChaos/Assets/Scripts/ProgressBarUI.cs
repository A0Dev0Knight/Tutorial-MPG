using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private BaseCounter counterRefference;
    [SerializeField] private Image progressBarVisual;

    // Start is called before the first frame update
    void Start()
    {
        switch (counterRefference)
        {
            case CuttingCounter cuttingCounter:
                cuttingCounter.OnCuttingKitchenObject += CuttingCounter_OnCuttingKitchenObject;
                break;
            
            case TrashCounter trashCounter:
                trashCounter.OnTrashCounterAccessed += TrashCounter_OnTrashCounterAccessed;
                break;
            default:
                break;
        }
        progressBarVisual.fillAmount = 0f;
        Hide();
    }

    private void TrashCounter_OnTrashCounterAccessed(object sender, TrashCounter.OnTrashCounterAccessedEventArgs e)
    {
        progressBarVisual.fillAmount = e.nrOfItemsInTrashCounterNormalised;

        if (e.nrOfItemsInTrashCounterNormalised == 0)
        {
            Hide();
        }
        else
        {
            Show();
        }


    }

    private void CuttingCounter_OnCuttingKitchenObject(object sender, CuttingCounter.OnCuttingKitchenObjectEventArgs e)
    {
        progressBarVisual.fillAmount = e.ProgressNormalised;

        if (e.ProgressNormalised == 0 || e.ProgressNormalised == 1)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Hide()
    {
        this.gameObject.SetActive(false);
    }

    private void Show()
    {
        this.gameObject.SetActive(true);
    }
}

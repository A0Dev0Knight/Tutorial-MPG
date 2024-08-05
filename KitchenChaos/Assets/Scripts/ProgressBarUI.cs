using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;
    [SerializeField] private Image progressBarVisual;

    // Start is called before the first frame update
    void Start()
    {
        cuttingCounter.OnCuttingKitchenObject += CuttingCounter_OnCuttingKitchenObject;
        progressBarVisual.fillAmount = 0f;
        Hide();
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

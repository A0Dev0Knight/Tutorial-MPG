using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO KitchenObjectSO;
        public GameObject gameObject;
    }


    [SerializeField] private PlateKitckenObject plateKitckenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjectsList;


    private void Start()
    {
        plateKitckenObject.OnIngredientAdded += PlateKitckenObject_OnIngredientAdded;

        foreach (KitchenObjectSO_GameObject kitchenObjectSO_gameObject in kitchenObjectSOGameObjectsList)
        {
            kitchenObjectSO_gameObject.gameObject.SetActive(false);
        }

    }

    private void PlateKitckenObject_OnIngredientAdded(object sender, PlateKitckenObject.OnIngredientAddedEventArgs e)
    {
        foreach(KitchenObjectSO_GameObject kitchenObjectSO_gameObject in kitchenObjectSOGameObjectsList)
        {
            if (kitchenObjectSO_gameObject.KitchenObjectSO == e.KitchenObjectSO)
            {
                kitchenObjectSO_gameObject.gameObject.SetActive(true);
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;

    public event EventHandler OnRecipeFailed;
    public event EventHandler OnRecipeSuccess;

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;
    
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimerMax = 4f;
    private float spawnRecipeTimer = 0f;
    private int waitingRecipesMax = 5;


    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }
    private void Update()
    {
        spawnRecipeTimer += Time.deltaTime;
        if (spawnRecipeTimer >= spawnRecipeTimerMax)
        {
            spawnRecipeTimer = 0f;

            if (waitingRecipeSOList.Count < waitingRecipesMax)
            {
                // get a random recipe and spawn it
                RecipeSO waitingRecipeSO = recipeListSO.possibleRecieSOList[UnityEngine.Random.Range(0, recipeListSO.possibleRecieSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);
                
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
                Debug.Log(waitingRecipeSO.recipeName);
            }


        }
        
    }

    public void DeliverRecipe(PlateKitckenObject plateKitckenObject)
    {
        foreach( RecipeSO order in waitingRecipeSOList )
        {
            if (order.kitchenObjectSOList.Count == plateKitckenObject.GetKitchenObjectSOList().Count)
            {
                // plate has the same nr of elements as an order

                bool plateHasCorrectOrder = true;
                foreach(KitchenObjectSO kitchenObjectSO in order.kitchenObjectSOList)
                {
                    bool ingredientFound = plateKitckenObject.GetKitchenObjectSOList().Contains(kitchenObjectSO);
                    if (!ingredientFound)
                    {
                        plateHasCorrectOrder = false;
                        break;
                    }
                }

                if (plateHasCorrectOrder) 
                {
                    waitingRecipeSOList.Remove(order);

                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);

                    return;
                }
            }
        }

        Debug.Log("Plate cannot be delivered!");
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }
}

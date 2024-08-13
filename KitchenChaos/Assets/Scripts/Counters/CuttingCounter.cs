using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class CuttingCounter : BaseCounter, IHasProgress
{
    new public static void ResetStaticData()
    {
        OnAnyCut = null;
    }
    public static event EventHandler OnAnyCut;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // no kitchen object present
            if (player.HasKitchenObject())
            {
                // player holds an object
                if (HasCuttingRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;

                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        ProgressNormalised = (float)cuttingProgress / cuttingRecipeSO.CuttingProgressMax
                    });

                }
            }
            else
            {
                // player does not hold an object
                Debug.LogError("You hold nothing to be placed!!");
            }
        }
        else
        {
            // there is a kitchen object here
            if (!player.HasKitchenObject())
            {
                // player does not holds an object
                this.GetKitchenObject().SetKitchenObjectParent(player);
            }
            else
            {
                // player does already have an object in his hands
                Debug.LogError("You already have an object in your hands");
                if (player.GetKitchenObject().TryGetPlate(out PlateKitckenObject plateKitckenObject))
                {
                    // player holds a plate

                    if (plateKitckenObject.TryAddIngredientToPlate(this.GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }

            }


        }

    }

    public override void InteractAlternate(Player player)
    {
        if(HasKitchenObject() && HasCuttingRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            cuttingProgress++;
            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);

            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                ProgressNormalised = (float)cuttingProgress/ cuttingRecipeSO.CuttingProgressMax
            });

            if (cuttingProgress >= GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO()).CuttingProgressMax)
            {
                KitchenObjectSO kitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(kitchenObjectSO, this);

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    ProgressNormalised = 0f,
                });

            }
        }
        else
        {
            Debug.Log("Cutting counter is empty or there is no recipe for it");
        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        
        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }
        else
        {
            return null;
        }


    }

    private bool HasCuttingRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);

        return cuttingRecipeSO != null;
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }

        return null;
    }
}

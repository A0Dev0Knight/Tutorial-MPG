using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // no kitchen object present
            if (player.HasKitchenObject())
            {
                // player holds an object
                player.GetKitchenObject().SetKitchenObjectParent(this);
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
            }


        }
    }

}

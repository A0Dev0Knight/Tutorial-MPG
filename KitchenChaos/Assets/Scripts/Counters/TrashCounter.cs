using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public event EventHandler<OnTrashCounterAccessedEventArgs> OnTrashCounterAccessed;

    public class OnTrashCounterAccessedEventArgs : EventArgs
    {
        public float nrOfItemsInTrashCounterNormalised;
    }

    [SerializeField] private int maxTrashedItems = 10;

    private int totalTrashedItems = 0;
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject() && totalTrashedItems < maxTrashedItems)
        {
            player.GetKitchenObject().DestroySelf();
            totalTrashedItems++;
            OnTrashCounterAccessed?.Invoke(this, new OnTrashCounterAccessedEventArgs
            {
                nrOfItemsInTrashCounterNormalised = (float)totalTrashedItems / maxTrashedItems,
            });
        }
    }

    public override void InteractAlternate(Player player)
    {
        totalTrashedItems = 0;
        OnTrashCounterAccessed?.Invoke(this, new OnTrashCounterAccessedEventArgs
        {
            nrOfItemsInTrashCounterNormalised = (float)totalTrashedItems / maxTrashedItems,
        });

    }
}

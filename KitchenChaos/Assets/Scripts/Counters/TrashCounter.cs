using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter, IHasProgress
{
    public static event EventHandler OnAnyObjectTrashed;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    [SerializeField] private int maxTrashedItems = 10;

    private int totalTrashedItems = 0;
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject() && totalTrashedItems < maxTrashedItems)
        {
            player.GetKitchenObject().DestroySelf();
            OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);

            totalTrashedItems++;
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                ProgressNormalised = (float)totalTrashedItems / maxTrashedItems,
            });
        }
    }

    public override void InteractAlternate(Player player)
    {
        totalTrashedItems = 0;
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
        {
            ProgressNormalised = (float)totalTrashedItems / maxTrashedItems,
        });

        OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
    }
}

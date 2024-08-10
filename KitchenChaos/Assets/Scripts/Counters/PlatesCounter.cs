using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlatesSpawned;
    public event EventHandler OnPlatesRemoved;

    public class OnPlatesSpawnedEventArgs : EventArgs
    {
        public int numberOfPlates;
    }

    [SerializeField] private KitchenObjectSO plateSO;

    private float spawnPlatesTimer;
    private float spawnPlatesTimerMax = 4f;
    private int platesSpawnedAmount;
    private int platesSpawnedAmountMax = 4;
    private int platesToSpwanPerTimerCycle = 1;

    void Update()
    {
        spawnPlatesTimer += Time.deltaTime;
        
        if (spawnPlatesTimer >= spawnPlatesTimerMax)
        {
            spawnPlatesTimer = 0;
            
            // spawn some plates
            if (platesSpawnedAmount < platesSpawnedAmountMax)
            {
                platesSpawnedAmount += platesToSpwanPerTimerCycle;
                OnPlatesSpawned?.Invoke(this, EventArgs.Empty);
            }

        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            // empty handed
            if (platesSpawnedAmount > 0)
            {
                platesSpawnedAmount--;
                KitchenObject.SpawnKitchenObject(plateSO, player);
                OnPlatesRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}

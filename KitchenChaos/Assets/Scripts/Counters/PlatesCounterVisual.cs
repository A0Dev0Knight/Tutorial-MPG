using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform platePrefab;


    private List<GameObject> platesVisualList;

    private void Awake()
    {
        platesVisualList = new List<GameObject>();
    }

    private void Start()
    {
        platesCounter.OnPlatesSpawned += PlatesCounter_OnPlatesSpawned;
        platesCounter.OnPlatesRemoved += PlatesCounter_OnPlatesRemoved;
    }

    private void PlatesCounter_OnPlatesRemoved(object sender, EventArgs e)
    {
        GameObject gameObject = platesVisualList[platesVisualList.Count - 1];
        platesVisualList.Remove(gameObject);
        Destroy(gameObject);
    }

    private void PlatesCounter_OnPlatesSpawned(object sender, EventArgs e)
    {
        float plateSpawnOffset = 0.1f;

        Transform singlePlate = Instantiate(platePrefab);

        singlePlate.parent = counterTopPoint;
        singlePlate.localPosition = Vector3.zero + platesVisualList.Count * plateSpawnOffset * Vector3.up;
        platesVisualList.Add(singlePlate.gameObject);
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour {

    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform platePrefab;

    private List<GameObject> platesVisual;

    private void Awake() {
        platesVisual = new List<GameObject>();
    }

    private void Start() {
        platesCounter.OnPlatesSpawned += PlatesCounter_OnPlatesSpawned;
        platesCounter.OnPlatesRemoved += PlatesCounter_OnPlatesRemoved;

        float plateVisualHeight = 0.12f;
        for(int i = 0; i < platesCounter.GetPlastesAmountMax(); ++i) {
            Transform plateVisualTransform = Instantiate(platePrefab, counterTopPoint);
            plateVisualTransform.position += new Vector3(0, plateVisualHeight * i, 0);

            platesVisual.Add(plateVisualTransform.gameObject);
            platesVisual[i].SetActive(false);
        }
    }

    private void PlatesCounter_OnPlatesRemoved(object sender, PlatesCounter.OnPlateSpawnedEventArgs e) {
        platesVisual[e.platesAmount].SetActive(false);
    }

    private void PlatesCounter_OnPlatesSpawned(object sender, PlatesCounter.OnPlateSpawnedEventArgs e) {
        platesVisual[e.platesAmount - 1].SetActive(true);
    }
}

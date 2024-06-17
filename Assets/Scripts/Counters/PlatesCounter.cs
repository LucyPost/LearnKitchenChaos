using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter {

    public event EventHandler<OnPlateSpawnedEventArgs> OnPlatesSpawned;
    public event EventHandler<OnPlateSpawnedEventArgs> OnPlatesRemoved;

    public class OnPlateSpawnedEventArgs : EventArgs {
        public int platesAmount;
    }

    [SerializeField] private KitchenObjectSO plateKitechObjectSO;

    private float spawnPlateTimer = 0.0f;
    private float spawnPlateTimerMax = 2.0f;
    private int platesSpawnAmount = 0;
    private int platesSpawnAmountMax = 4;

    private void Update() {
        if(platesSpawnAmount < platesSpawnAmountMax) {
            spawnPlateTimer += Time.deltaTime;
        }
        if (GameManager.Instance.IsGamePlaying() && spawnPlateTimer >= spawnPlateTimerMax && platesSpawnAmount < platesSpawnAmountMax) {
            spawnPlateTimer = 0.0f;

            ++platesSpawnAmount;
            OnPlatesSpawned?.Invoke(this, new OnPlateSpawnedEventArgs { platesAmount = platesSpawnAmount });
        }
    }

    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            if(platesSpawnAmount > 0) {
                KitchenObject.SpawnKitchenObject(plateKitechObjectSO, player);

                --platesSpawnAmount;
                OnPlatesRemoved?.Invoke(this, new OnPlateSpawnedEventArgs { platesAmount = platesSpawnAmount });
            }
        }
    }

    public int GetPlastesAmountMax() {
        return platesSpawnAmountMax;
    }
}

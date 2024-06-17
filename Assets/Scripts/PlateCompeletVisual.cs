using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompeletVisual : MonoBehaviour {

    [Serializable]
    public struct KitchenObjectSO_GameOject {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameOject> kitchenObjectSO_GameOjectList;

    private void Start() {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;

        foreach (KitchenObjectSO_GameOject kitchenObjectSO_GameOject in kitchenObjectSO_GameOjectList) {
            kitchenObjectSO_GameOject.gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
        foreach (KitchenObjectSO_GameOject kitchenObjectSO_GameOject in kitchenObjectSO_GameOjectList) {
            if (kitchenObjectSO_GameOject.kitchenObjectSO == e.kitchenObjectSO) {
                kitchenObjectSO_GameOject.gameObject.SetActive(true);
            }
        }
    }
}

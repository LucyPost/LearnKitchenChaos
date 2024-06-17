using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent {

    public static event EventHandler OnAnyObjectplaceHere;

    public static void ResetStaticData() {
        OnAnyObjectplaceHere = null;
    }

    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;
    public virtual void Interact(Player player) {
        Debug.LogError("Interact method not implemented");
    }

    public virtual void InteractAlternate(Player player) {
        //Debug.LogError("InteractAlternate method not implemented");
    }

    public void ClearKitchenObject() {
        this.kitchenObject = null;
    }

    public KitchenObject GetKitchenObject() {
        return this.kitchenObject;
    }

    public Transform GetkitechenObjectFollowtransform() {
        return counterTopPoint;
    }

    public bool HasKitchenObject() {
        return this.kitchenObject != null;
    }

    public void SetKitechenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
        OnAnyObjectplaceHere?.Invoke(this, EventArgs.Empty);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IKitchenObjectParent, IHasProgress {

    public static event EventHandler OnAnyCut;

    new public static void ResetStaticData() {
        OnAnyCut = null;
    }

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public event EventHandler OnCut;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress = 0;
    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {

                KitchenObject kitchenObject = player.GetKitchenObject();
                if (HasRecipeWithInput(kitchenObject.GetKitchenObjectSO())) {
                    kitchenObject.SetKitchenObjectparent(this);

                    cuttingProgress = 0;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { 
                        progressNormalized = 0.0f 
                    });
                }
            }
        } else {
            if (player.HasKitchenObject()) {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    if (plateKitchenObject.TryAddIndredient(this.GetKitchenObject().GetKitchenObjectSO())) {
                        this.GetKitchenObject().DestroySelf();
                    }
                }
            } else {
                KitchenObject kitchenObject = this.GetKitchenObject();
                kitchenObject.SetKitchenObjectparent(player);

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { 
                    progressNormalized = 0.0f 
                });
            }
        }
    }
    public override void InteractAlternate(Player player) {
        if (HasKitchenObject()) {
            KitchenObject kitchenObject = GetKitchenObject();
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(kitchenObject.GetKitchenObjectSO());

            if(cuttingRecipeSO != null) {

                ++cuttingProgress;

                OnCut?.Invoke(this, EventArgs.Empty);
                OnAnyCut?.Invoke(this, EventArgs.Empty);

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { 
                    progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax 
                });

                if(cuttingProgress >= cuttingRecipeSO.cuttingProgressMax) {
                    kitchenObject.DestroySelf();
                    KitchenObject.SpawnKitchenObject(cuttingRecipeSO.output, this);
                }
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO input) {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(input);
        return cuttingRecipeSO != null;
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO input) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.input == input) {
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player) {
        if(!HasKitchenObject()) {
            if(player.HasKitchenObject()) {
                KitchenObject kitchenObject = player.GetKitchenObject();
                kitchenObject.SetKitchenObjectparent(this);
            }
        } else {
            if(player.HasKitchenObject()) {
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    if(plateKitchenObject.TryAddIndredient(this.GetKitchenObject().GetKitchenObjectSO())) {
                        this.GetKitchenObject().DestroySelf();
                    }
                } else if (this.GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                    if (plateKitchenObject.TryAddIndredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                        player.GetKitchenObject().DestroySelf();
                    }
                }
            } else {
                KitchenObject kitchenObject = this.GetKitchenObject();
                kitchenObject.SetKitchenObjectparent(player);
            }
        }
    }
}

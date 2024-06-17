using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KitchenObject : MonoBehaviour {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSO;
    }

    public void SetKitchenObjectparent(IKitchenObjectParent parent) {

        if(this.kitchenObjectParent != null) {
            this.kitchenObjectParent.ClearKitchenObject();
        }

        this.kitchenObjectParent = parent;
        parent.SetKitechenObject(this);

        this.transform.SetParent(parent.GetkitechenObjectFollowtransform());
        this.transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetApplicationVariable() {
        return this.kitchenObjectParent;
    }

    public void DestroySelf() {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(this.gameObject);
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO KitchenObjectSO, IKitchenObjectParent KitchenObjectParent) {
        if (KitchenObjectParent.HasKitchenObject()) {
            Debug.LogError("KitchenObjectParent already has a KitchenObject");
            return null;
        }
        Transform KitchenObjectTransform = Instantiate(KitchenObjectSO.prefab);
        KitchenObject KitchenObject = KitchenObjectTransform.GetComponent<KitchenObject>();
        KitchenObject.SetKitchenObjectparent(KitchenObjectParent);

        return KitchenObject;
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject) {
        plateKitchenObject = null;
        if (this is PlateKitchenObject) {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        return false;
    }
}

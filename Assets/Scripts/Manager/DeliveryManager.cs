using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour {

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;

    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    [SerializeField] private RecipeListSO recipeListSO;

    public static DeliveryManager Instance { get; private set; }

    private List<RecipeSO> waitingRecipeSoList;
    private float spawnRecipeTimer = 0.0f;
    private float spawnRecipeTimerMax = 4.0f;
    private int waitingRecipesMax = 4;
    private int successfulRecipesAmount;

    private void Awake() {
        Instance = this;
        successfulRecipesAmount = 0;
        waitingRecipeSoList = new List<RecipeSO>();
    }

    private void Update() {
        spawnRecipeTimer += Time.deltaTime;
        if(GameManager.Instance.IsGamePlaying() && spawnRecipeTimer > spawnRecipeTimerMax) {

            spawnRecipeTimer = 0.0f;

            if(waitingRecipeSoList.Count < waitingRecipesMax) {

                RecipeSO recipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingRecipeSoList.Add(recipeSO);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public bool DeliverRecipe(PlateKitchenObject plateKitchenObject) {
        for(int i = 0; i<waitingRecipeSoList.Count; i++) {

            RecipeSO waitingRecipeSO = waitingRecipeSoList[i];
            
            if(waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count) {
                //has the same number of kitchen objects
                bool plateContentsMatchesRecipe = true;
                foreach(KitchenObjectSO kitchenObjectSO in waitingRecipeSO.kitchenObjectSOList) {
                    //check if the plate has the same kitchen objects
                    if(!plateKitchenObject.GetKitchenObjectSOList().Contains(kitchenObjectSO)) {
                        plateContentsMatchesRecipe = false;
                        break;
                    }
                }
                if(plateContentsMatchesRecipe) {
                    waitingRecipeSoList.RemoveAt(i);

                    ++successfulRecipesAmount;

                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);

                    return true;
                }
            }
        }
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        return false;
    }

    public List<RecipeSO> GetWaitingRecipeSoList() {
        return waitingRecipeSoList;
    }

    public int GetSuccessfulRecipesAmount() {
        return successfulRecipesAmount;
    }
}

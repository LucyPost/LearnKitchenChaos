using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoaderCallback : MonoBehaviour {
    [SerializeField] TextMeshProUGUI levelNameText;

    private bool isFirstUpdate = true;

    private void Update() {
        if (isFirstUpdate) {
            levelNameText.text = Loader.GetLevelName(Loader.targetScene);
            isFirstUpdate = false;
            Loader.LoaderCallback();
        }
    }
}

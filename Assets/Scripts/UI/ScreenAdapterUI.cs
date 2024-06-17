using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenAdapterUI : MonoBehaviour {

    private RectTransform rectTransform;

    private float targetWidth = 1920;
    private float targetHeight = 1080;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
    }

    void Start() {
        rectTransform = GetComponent<RectTransform>();
        AdaptScreen();
    }

    void AdaptScreen() {

        float targetaspect = 16.0f / 9.0f;

        float windowaspect = (float)Screen.width / (float)Screen.height;

        float scaleheight = windowaspect / targetaspect;

        if ((float)Screen.width < targetWidth || (float)Screen.height < targetHeight) {
            if (scaleheight < 1.0f) {
                float targetScale = (float)Screen.width / targetWidth;
                rectTransform.localScale = new Vector2(targetScale, targetScale);
            } else {
                float targetScale = (float)Screen.height / targetHeight;

                rectTransform.localScale = new Vector2(targetScale, targetScale);
            }
        }
    }
}

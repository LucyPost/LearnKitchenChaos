using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMapUI : MonoBehaviour
{
    public static MainMapUI Instance;
    public EventHandler OnBackButtonPressed;

    [SerializeField] private MainMenuUI mainMenuUI;

    [SerializeField] private GameObject mainMenuObjects;
    [SerializeField] private GameObject chooseLevelObjects;

    [SerializeField] private Button backButton;
    [SerializeField] private Button level1Button;
    [SerializeField] private Button level2Button;
    [SerializeField] private Button level3Button;
    [SerializeField] private Button level4Button;
    [SerializeField] private Button level5Button;

    private void Awake() {
        Instance = this;
        backButton.onClick.AddListener(() => {
            OnBackButtonPressed?.Invoke(this, EventArgs.Empty);
        });
        level1Button.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.Level1);
        });
        level2Button.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.Level2);
        });
        level3Button.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.Level3);
        });
        level4Button.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.Level4);
        });
        level5Button.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.Level5);
        });
    }

    private void Start() {
        mainMenuUI.OnBackgroundTurnBlack += MainMenuUI_OnBackgroundTurnBlack;
    }

    private void MainMenuUI_OnBackgroundTurnBlack(object sender, EventArgs e) {
        mainMenuObjects.SetActive(true);
        chooseLevelObjects.SetActive(false);
    }
}

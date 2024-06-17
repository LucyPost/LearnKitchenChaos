using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    [SerializeField] private MainMenuUI mainMenuUI;

    private Animator animator;
    private MainMapUI mainMapUI;
    private Material material;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        mainMapUI = MainMapUI.Instance;
        mainMenuUI.OnPlayeButtonPressed += MainMenuUI_OnPlayeButtonPressed;
        mainMenuUI.OnBackgroundTurnBlack += MainMenuUI_OnBackgroundTurnBlack;
    }

    private void MainMenuUI_OnBackgroundTurnBlack(object sender, EventArgs e) {
        animator.SetTrigger("Reset");
    }

    private void MainMenuUI_OnPlayeButtonPressed(object sender, EventArgs e) {
        animator.SetTrigger("PlayButtonPreesed");
    }
}

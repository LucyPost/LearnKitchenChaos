using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneCamera : MonoBehaviour
{
    [SerializeField] private MainMenuUI mainMenuUI;

    private MainMapUI mainMapUI;
    private DressingRoomUI dressingRoomUI;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        mainMapUI = MainMapUI.Instance;
        dressingRoomUI = DressingRoomUI.Instance;

        mainMenuUI.OnPlayeButtonPressed += MainMenuUI_OnPlayeButtonPressed;
        mainMenuUI.OnDressingRoomButtonPressed += MainMenuUI_OnDressingRoomButtonPressed;
        mainMenuUI.OnBackgroundTurnBlack += MainMenuUI_OnBackgroundTurnBlack;

        dressingRoomUI.OnBackButtonPressed += DressingRoomUI_OnBackButtonPressed;
    }

    private void DressingRoomUI_OnBackButtonPressed(object sender, EventArgs e) {
        animator.SetTrigger("ReturnFromDressingRoom");
    }

    private void MainMenuUI_OnDressingRoomButtonPressed(object sender, EventArgs e) {
        animator.SetTrigger("DressingRoomButtonPressed");
    }

    private void MainMenuUI_OnBackgroundTurnBlack(object sender, EventArgs e) {
        animator.SetTrigger("Reset");
    }

    private void MainMenuUI_OnPlayeButtonPressed(object sender, EventArgs e) {
        animator.SetTrigger("PlayButtonPreesed");
    }
}

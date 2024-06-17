using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DressingRoomObjects : MonoBehaviour
{
    [SerializeField] private MainMenuUI mainMenuUI;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        mainMenuUI.OnPlayeButtonPressed += MainMenuUI_OnPlayeButtonPressed;
        mainMenuUI.OnDressingRoomButtonPressed += MainMenuUI_OnDressingRoomButtonPressed;
        gameObject.SetActive(false);
    }

    private void MainMenuUI_OnPlayeButtonPressed(object sender, EventArgs e) {
        gameObject.SetActive(false);
    }

    private void MainMenuUI_OnDressingRoomButtonPressed(object sender, EventArgs e) {
        gameObject.SetActive(true);
    }
}

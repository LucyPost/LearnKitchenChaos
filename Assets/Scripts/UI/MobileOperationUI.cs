using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileOperationUI : MonoBehaviour {

    [SerializeField] private Joystick joystick;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAlternateButton;
    [SerializeField] private Button pauseButton;

    private void Awake() {
        interactButton.onClick.AddListener(() => {
            GameInput.Instance.MobileIneractionPreformed();
        });
        interactAlternateButton.onClick.AddListener(() => {
            GameInput.Instance.MobileIneractionAlternatePreformed();
        });
        pauseButton.onClick.AddListener(() => {
            GameManager.Instance.TogglePauseGame();
        });
    }
    private void Start() {
        joystick.gameObject.SetActive(false);

        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e) {
        if(GameManager.Instance.IsGamePlaying()) {
            joystick.gameObject.SetActive(true);
        } else {
            joystick.gameObject.SetActive(false);
        }
    }
}

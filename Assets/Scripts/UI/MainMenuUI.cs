using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {

    [SerializeField] private Button playButton;
    [SerializeField] private Button DressingRoomButton;

    public event EventHandler OnPlayeButtonPressed;
    public event EventHandler OnDressingRoomButtonPressed;
    public event EventHandler OnBackgroundTurnBlack;

    private MainMapUI mainMapUI;
    private Animator animator;
    private AnimationEventHandler animationEventHandler;

    private bool isDressingRoomActive = false;

    private void Awake() {
        animator = GetComponent<Animator>();
        playButton.onClick.AddListener(() => {
            //Loader.Load(Loader.Scene.Level1);
            OnPlayeButtonPressed?.Invoke(this, EventArgs.Empty);
            gameObject.SetActive(false);
        });
        DressingRoomButton.onClick.AddListener(() => {
            OnDressingRoomButtonPressed?.Invoke(this, EventArgs.Empty);
            gameObject.SetActive(false);
        });

        Time.timeScale = 1f;
    }

    private void Start() {
        mainMapUI = MainMapUI.Instance;
        animationEventHandler = AnimationEventHandler.Instance;
        mainMapUI.OnBackButtonPressed += MainMapUI_OnBackButtonPressed;
        animationEventHandler.OnCameraBackFromDressingRoomEnd += AnimationEventHandler_OnCameraBackFromDressingRoomEnd;
    }

    private void AnimationEventHandler_OnCameraBackFromDressingRoomEnd(object sender, EventArgs e) {
        isDressingRoomActive = !isDressingRoomActive;
        if (!isDressingRoomActive) {
            gameObject.SetActive(true);
        }
    }

    private void MainMapUI_OnBackButtonPressed(object sender, EventArgs e) {
        gameObject.SetActive(true);
        animator.SetTrigger("BackFromMap");
    }

    private void OnBackgroundTurnBlackAnimationEvent() {
        OnBackgroundTurnBlack?.Invoke(this, EventArgs.Empty);
    }
}

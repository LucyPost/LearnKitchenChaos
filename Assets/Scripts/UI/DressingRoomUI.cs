using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DressingRoomUI : MonoBehaviour
{
    public static DressingRoomUI Instance;

    public EventHandler OnBackButtonPressed;

    [SerializeField] private Button backButton;

    private AnimationEventHandler animationEventHandler;
    private bool isDressingRoomActive = false;

    private void Awake() {
        Instance = this;
        backButton.onClick.AddListener(() => {
            OnBackButtonPressed?.Invoke(this, EventArgs.Empty);
            gameObject.SetActive(false);
        });
    }
    private void Start() {
        animationEventHandler = AnimationEventHandler.Instance;
        animationEventHandler.OnCameraTurntoDressingRoomEnd += AnimationEventHandler_OnCameraTurntoDressingRoomEnd;
        gameObject.SetActive(false);
    }

    private void AnimationEventHandler_OnCameraTurntoDressingRoomEnd(object sender, EventArgs e) {
        isDressingRoomActive = !isDressingRoomActive;
        if (isDressingRoomActive) {
            gameObject.SetActive(true);
        }
    }
}

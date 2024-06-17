using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChooseLevelObjects : MonoBehaviour
{
    private AnimationEventHandler animationEventHandler;

    private MainMapUI mainMapUI;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        mainMapUI = MainMapUI.Instance;
        animationEventHandler = AnimationEventHandler.Instance;
        animationEventHandler.OnCameraFocuseOnMapHalfWay += AnimationEventHandler_OnCameraAnimationWay;
        animationEventHandler.OnCameraFocuseOnMapEnd += AnimationEventHandler_OnCameraAnimationEnd;
        mainMapUI.OnBackButtonPressed += MainMapUI_OnBackButtonPressed;
        gameObject.SetActive(false);
    }

    private void MainMapUI_OnBackButtonPressed(object sender, EventArgs e) {
        animator.SetTrigger("Reset");
    }

    private void AnimationEventHandler_OnCameraAnimationEnd(object sender, EventArgs e) {
        animator.SetTrigger("CameraAnimationEnd");
    }

    private void AnimationEventHandler_OnCameraAnimationWay(object sender, EventArgs e) {
        gameObject.SetActive(true);
    }
}

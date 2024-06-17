using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationSkiper : MonoBehaviour
{
    [SerializeField] private Animator[] animators;

    private Button button;
    private AnimationEventHandler animationEventHandler;

    private void Awake() {
        button = GetComponent<Button>();

        button.onClick.AddListener(() => {
            SpeedupAnimation();
        });
    }

    private void Start() {
        animationEventHandler = AnimationEventHandler.Instance;
        animationEventHandler.OnCameraFocuseOnMapStart += AnimationEventHandler_OnCameraFocuseOnMapStart;
        animationEventHandler.OnCameraFocuseOnMapEnd += AnimationEventHandler_OnCameraFocuseOnMapEnd;
    }

    private void AnimationEventHandler_OnCameraFocuseOnMapEnd(object sender, EventArgs e) {
        gameObject.SetActive(false);
        DialbackAnimationSpeed();
    }

    private void AnimationEventHandler_OnCameraFocuseOnMapStart(object sender, EventArgs e) {
        gameObject.SetActive(true);
    }

    private void SpeedupAnimation() {
        foreach (var animator in animators) {
            animator.speed = 100;
        }
    }

    private void DialbackAnimationSpeed() {
        foreach (var animator in animators) {
            animator.speed = 1;
        }
    }
}
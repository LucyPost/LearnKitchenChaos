using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostVolumeController : MonoBehaviour
{
    [SerializeField] private Color initialColor = Color.white;
    [SerializeField] private Color targetColor = Color.black;
    [SerializeField] private Color secondTargetColor = Color.white;
    [SerializeField] private float initialChromaticAberration = 0.4f;
    [SerializeField] private float targetChromaticAberration = 0.0f;

    [SerializeField] private float CountdownToChangeColorTimmerMax = 0.0f;
    [SerializeField] private float CountdownToChangeChromaticAberrationTimmerMax = 0.0f;

    [SerializeField] private float colorChangeSpeed = 0.1f;
    [SerializeField] private float secondColorChangeSpeed = 0.1f;
    [SerializeField] private float chromaticAberrationChangeSpeed = 0.1f;

    private Volume volume;
    private ChromaticAberration chromaticAberration;
    private ColorAdjustments colorAdjustments;

    private bool shouldStartCountdownToChangeColor = false;
    private bool shouldStartCountdownToChangeChromaticAberration = false;
    private bool shouleStartChangeColor = false;
    private bool shouldStartChangeColorAgain = false;
    private bool shouldStartChangeChromaticAberration = false;

    private float CountdownToChangeColorTimmer = 0.0f;
    private float CountdownToChangeChromaticAberrationTimmer = 0.0f;

    private float startChangeColorTime = 0.0f;
    private float starChangeColorAgainTime = 0.0f;
    private float startChangeChromaticAberrationTime = 0.0f;

    private void Awake() {
        volume = GetComponent<Volume>();
    }

    private void Start() {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        SetColorAdjustments(initialColor);
        SetChromaticAberration(initialChromaticAberration);
    }

    private void Update() {
        if(shouldStartCountdownToChangeColor) {
            CountdownToChangeColorTimmer += Time.deltaTime;
            if(CountdownToChangeColorTimmer >= CountdownToChangeColorTimmerMax) {
                shouleStartChangeColor = true;
                shouldStartCountdownToChangeColor = false;
                CountdownToChangeColorTimmer = 0.0f;
                startChangeColorTime = Time.time;
            }
        }
        if(shouldStartCountdownToChangeChromaticAberration) {
            CountdownToChangeChromaticAberrationTimmer += Time.deltaTime;
            if(CountdownToChangeChromaticAberrationTimmer >= CountdownToChangeChromaticAberrationTimmerMax) {
                shouldStartChangeChromaticAberration = true;
                shouldStartCountdownToChangeChromaticAberration = false;
                CountdownToChangeChromaticAberrationTimmer = 0.0f;
                startChangeChromaticAberrationTime = Time.time;
            }
        }

        if(shouleStartChangeColor) {
            Color currentColor = colorAdjustments.colorFilter.value;
            if(currentColor != targetColor) {
                currentColor = Color.Lerp(initialColor, targetColor, (Time.time - startChangeColorTime) * colorChangeSpeed);
                SetColorAdjustments(currentColor);
            }
            else {
                shouleStartChangeColor = false;
                shouldStartChangeColorAgain = true;
                starChangeColorAgainTime = Time.time;
            }
        }
        if(shouldStartChangeChromaticAberration) {
            float currentChromaticAberration = chromaticAberration.intensity.value;
            if(currentChromaticAberration != targetChromaticAberration) {
                currentChromaticAberration = Mathf.Lerp(initialChromaticAberration, targetChromaticAberration, (Time.time - startChangeChromaticAberrationTime) * chromaticAberrationChangeSpeed);
                SetChromaticAberration(currentChromaticAberration);
            }
            else {
                shouldStartChangeChromaticAberration = false;
            }
        }
        if (shouldStartChangeColorAgain) {
            Color currentColor = colorAdjustments.colorFilter.value;
            if(currentColor != secondTargetColor) {
                currentColor = Color.Lerp(targetColor, secondTargetColor, (Time.time - starChangeColorAgainTime) * secondColorChangeSpeed);
                SetColorAdjustments(currentColor);
            }
            else {
                shouldStartChangeColorAgain = false;
            }
        }
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e) {
        if (GameManager.Instance.IsCountdownToStartActive()) {
            shouldStartCountdownToChangeColor = true;
            shouldStartCountdownToChangeChromaticAberration = true;
        }
    }

    private void SetColorAdjustments(Color color) {
        volume.profile.TryGet(out colorAdjustments);
        colorAdjustments.colorFilter.value = color;
    }

    private void SetChromaticAberration(float intensity) {
        volume.profile.TryGet(out chromaticAberration);
        chromaticAberration.intensity.value = intensity;
    }
}

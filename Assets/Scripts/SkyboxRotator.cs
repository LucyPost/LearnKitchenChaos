using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 0.5f;
    [SerializeField] private float initialRotation = 260.0f;
    [SerializeField] private float targetRotation = 180.0f;

    private Material skyboxMaterial;

    private bool shouldStartRotate = false;

    private void Awake() {
        skyboxMaterial = RenderSettings.skybox;
    }

    private void Start() {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        skyboxMaterial.SetFloat("_Rotation", initialRotation);
        if(initialRotation < targetRotation) {
            Debug.Log("Warnig -- Initial rotation is less than target rotation");
        }
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e) {
        if(GameManager.Instance.IsCountdownToStartActive()) {
            shouldStartRotate = true;
        }
    }

    private void Update() {
        float currentRotation = RenderSettings.skybox.GetFloat("_Rotation");
        if(shouldStartRotate) {
            if(currentRotation > targetRotation) {
                currentRotation -= rotationSpeed * Time.deltaTime;
                skyboxMaterial.SetFloat("_Rotation", currentRotation);
            }
            else {
                shouldStartRotate = false;
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudLauncher : MonoBehaviour
{
    [SerializeField] private float CountdownToLaunchCloudTimmerMax = 0.0f;
    [SerializeField] private float LaunchCloudTimmerMax = 0.0f;

    private ParticleSystem cloudLauncher;

    private bool shouldCountdownToLaunchCloud = false;
    private bool shouldLaunchCloud = false;
    private bool isLaunchingCloud = false;

    private float CountdownToLaunchCloudTimmer = 0.0f;
    private float LaunchCloudTimmer = 0.0f;

    private void Awake() {
        cloudLauncher = GetComponent<ParticleSystem>();
    }

    private void Start() {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void Update() {
        if(shouldCountdownToLaunchCloud) {
            CountdownToLaunchCloudTimmer += Time.deltaTime;
            if(CountdownToLaunchCloudTimmer >= CountdownToLaunchCloudTimmerMax) {
                shouldLaunchCloud = true;
                shouldCountdownToLaunchCloud = false;
            }
        }
        if(isLaunchingCloud) {
            LaunchCloudTimmer += Time.deltaTime;
            if(LaunchCloudTimmer >= LaunchCloudTimmerMax) {
                cloudLauncher.Stop();
                isLaunchingCloud = false;
                LaunchCloudTimmer = 0.0f;
            }
        }

        if(shouldLaunchCloud) {
            cloudLauncher.Play();
            shouldLaunchCloud = false;
            isLaunchingCloud = true;
        }
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e) {
        if(GameManager.Instance.IsCountdownToStartActive()) {
            shouldCountdownToLaunchCloud = true;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class CameraFollowPoint : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform cameraOverlookFocusPointTransform;
    [SerializeField] private float moveSpeed;

    private bool shouldCameraaZoomIn;
    private bool shouldCameraaZoomOut;

    private bool isfollowingPlayer;

    private void Start() {
        isfollowingPlayer = true;
    }

    private void Update() {
        if(shouldCameraaZoomIn) {
            ZoomIn();
        } else if(shouldCameraaZoomOut) {
            ZoomOut();
        }
        if(isfollowingPlayer) {
            FollowPosition(playerTransform.position);
        }
    }

    private void ZoomOut() {
        transform.position = Vector3.Lerp(transform.position, cameraOverlookFocusPointTransform.position, moveSpeed * Time.deltaTime);
        if(transform.position == cameraOverlookFocusPointTransform.position) {
            shouldCameraaZoomOut = false;
        }
    }

    private void ZoomIn() {
        transform.position = Vector3.Lerp(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
        if(transform.position == playerTransform.position) {
            shouldCameraaZoomIn = false;
        }
    }

    private void FollowPosition(Vector3 position) {
        transform.position = position;
    }

    public void SetShouldCameraZoomIn(bool value) {
        shouldCameraaZoomIn = value;
        shouldCameraaZoomOut = !value;
    }

    public void SetShouldCameraZoomOut(bool value) {
        shouldCameraaZoomOut = value;
        shouldCameraaZoomIn = !value;
    }

    public void SetIsFollowingPlayer(bool value) {
        isfollowingPlayer = value;
    }

}

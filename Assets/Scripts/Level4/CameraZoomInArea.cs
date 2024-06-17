using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class CameraZoomInArea : MonoBehaviour
{
    [SerializeField] private FloatingCamera floatingCamera;
    [SerializeField] private Transform playerTransform;

    private bool isPlayerInside;
    private bool isPlayerInSideLastFrame;
    private Renderer itsRenderer;

    private void Awake() {
        itsRenderer = GetComponent<Renderer>();
    }

    private void Start() {
        isPlayerInside = CheckIfPlayerInside();
        isPlayerInSideLastFrame = true;
    }

    private void Update() {
        isPlayerInSideLastFrame = isPlayerInside;
        isPlayerInside = CheckIfPlayerInside();
        if(isPlayerInside != isPlayerInSideLastFrame) {
            if(isPlayerInside) {
                floatingCamera.OnPlayerEnterZoomInArea();
            } else {
                floatingCamera.OnPlayerExitZoomInArea();
            }
        }
    }

    private bool CheckIfPlayerInside() {
        Vector3 playerPosition = playerTransform.position;
        Vector3 position = transform.position;
        if(Mathf.Abs(playerPosition.x - position.x) < itsRenderer.bounds.size.x * 0.5f) {
            if(Mathf.Abs(playerPosition.z - position.z) < itsRenderer.bounds.size.z * 0.5f) {
                return true;
            }
        }
        return false;
    }
}

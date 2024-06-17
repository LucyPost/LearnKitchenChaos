using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class FloatingCamera : MonoBehaviour
{
    [SerializeField] private Vector3 cameraOverlookOffset;
    [SerializeField] private Vector3 cameraInitalOffset;
    [SerializeField] private float CameraOverlookSpeed;
    [SerializeField] private CameraFollowPoint cameraFollowPoint;
    [SerializeField] private Player player;

    private Cinemachine.CinemachineVirtualCamera cinemachineVirtualCamera;

    private int cameraZoomInAreaCount;
    private bool shouldZoomIn;
    
    private void Awake() {
        cinemachineVirtualCamera = GetComponent<Cinemachine.CinemachineVirtualCamera>();
        cinemachineVirtualCamera.Follow = cameraFollowPoint.transform;
    }

    private void Start() {
        cameraZoomInAreaCount = 1;
        shouldZoomIn = false;
    }

    private void Update() {
        if (cameraZoomInAreaCount == 0) {
            cinemachineVirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineTransposer>().m_FollowOffset = Vector3.Lerp(cinemachineVirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineTransposer>().m_FollowOffset, cameraOverlookOffset, CameraOverlookSpeed * Time.deltaTime);
            transform.LookAt(cameraFollowPoint.transform);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);
        } else if (shouldZoomIn) {
            cinemachineVirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineTransposer>().m_FollowOffset = Vector3.Lerp(cinemachineVirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineTransposer>().m_FollowOffset, cameraInitalOffset, CameraOverlookSpeed * Time.deltaTime);
            transform.LookAt(cameraFollowPoint.transform);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);
        }
    }

    public void OnPlayerExitZoomInArea() {
        --cameraZoomInAreaCount;
        if(cameraZoomInAreaCount == 0) {
            cameraFollowPoint.SetShouldCameraZoomOut(true);
            cameraFollowPoint.SetIsFollowingPlayer(false);
            player.Run();
        }
    }

    public void OnPlayerEnterZoomInArea() {
        if(cameraZoomInAreaCount == 0) {
            cameraFollowPoint.SetShouldCameraZoomIn(true);
            cameraFollowPoint.SetIsFollowingPlayer(true);
            shouldZoomIn = true;
            player.StopRun();
        }
        ++cameraZoomInAreaCount;
    }
}

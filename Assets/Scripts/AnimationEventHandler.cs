using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimationEventHandler : MonoBehaviour
{
    public static AnimationEventHandler Instance;

    public event EventHandler OnCameraFocuseOnMapHalfWay;
    public event EventHandler OnCameraFocuseOnMapStart;
    public event EventHandler OnCameraFocuseOnMapEnd;
    public event EventHandler OnCameraFocusOnDressingRoomWay;
    public event EventHandler OnCameraBackFromDressingRoomEnd;
    public event EventHandler OnCameraTurntoDressingRoomEnd;
    
    private void Awake() {
        Instance = this;
    }

    private void OnCameraAnimationWayAnimationEvent() {
        OnCameraFocuseOnMapHalfWay?.Invoke(this, EventArgs.Empty);
    }

    private void OnCameraAnimationStartAnimationEvent() {
        OnCameraFocuseOnMapStart?.Invoke(this, EventArgs.Empty);
    }

    private void OnCameraAnimationEndAnimationEvent() {
        OnCameraFocuseOnMapEnd?.Invoke(this, EventArgs.Empty);
    }

    private void OnCameraFocusOnDressingRoomWayAnimationEvent() {
        OnCameraFocusOnDressingRoomWay?.Invoke(this, EventArgs.Empty);
    }

    private void OnCameraBackFromDressingRoomEndAnimationEvent() {
        OnCameraBackFromDressingRoomEnd?.Invoke(this, EventArgs.Empty);
    }
    private void OnCameraTurntoDressingRoomEndAnimationEvent() {
        OnCameraTurntoDressingRoomEnd?.Invoke(this, EventArgs.Empty);
    }
}

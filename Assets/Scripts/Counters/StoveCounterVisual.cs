using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour {

    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject StoveOnGameobject;
    [SerializeField] private GameObject particalGameObject;

    private void Start() {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
        bool isStoveOn = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;

        StoveOnGameobject.SetActive(isStoveOn);
        particalGameObject.SetActive(isStoveOn);
    }
}

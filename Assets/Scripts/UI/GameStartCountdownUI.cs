using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour {

    private const string NUMBER_POPUP = "NumberPopup";

    [SerializeField] private TextMeshProUGUI countdownText;

    private Animator animator;
    private int previousCountdownNumber = 3;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        GameManager.Instance.OnStateChanged += Instance_OnStateChanged;

        Hide();
    }

    private void Instance_OnStateChanged(object sender, EventArgs e) {
        if(GameManager.Instance.IsCountdownToStartActive()) {
            Show();
            animator.SetTrigger(NUMBER_POPUP);
        } else {
            Hide();
        }
    }

    private void Update() {
        int countdownNumber = Mathf.CeilToInt(GameManager.Instance.GetCountdownToStartTimer());
        countdownText.text = countdownNumber.ToString();

        if(countdownNumber != previousCountdownNumber) {
            animator.SetTrigger(NUMBER_POPUP);
            previousCountdownNumber = countdownNumber;

            SoundManager.Instance.PlayCountdownSound();
        }

    }

    private void Show() {
        countdownText.gameObject.SetActive(true);
    }

    private void Hide() {
        countdownText.gameObject.SetActive(false);
    }
}

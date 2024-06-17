using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisguiseManager : MonoBehaviour
{
    [SerializeField] private Color[] colors;

    [SerializeField] private Transform headDisguiseParent;
    [SerializeField] private Transform bodyDisguiseParent;
    [SerializeField] private Material playerMaterial;
    [SerializeField] private Disguise[] headDisguises;
    [SerializeField] private Disguise[] bodyDisguises;

    private int colorIndex = -1;
    private int headDisguiseIndex = -1;
    private int bodyDisguiseIndex = -1;

    private void Start() {
        for(int i = 0; i < headDisguises.Length; i++) {
            headDisguises[i].transform.SetParent(headDisguiseParent);
            headDisguises[i].gameObject.SetActive(false);
        }
        for(int i = 0; i < bodyDisguises.Length; i++) {
            bodyDisguises[i].transform.SetParent(bodyDisguiseParent);
            bodyDisguises[i].gameObject.SetActive(false);
        }
        if(PlayerPrefs.HasKey("ColorIndex")) {
            colorIndex = PlayerPrefs.GetInt("ColorIndex");
        }
        if(PlayerPrefs.HasKey("HeadDisguiseIndex")) {
            headDisguiseIndex = PlayerPrefs.GetInt("HeadDisguiseIndex");
        }
        if(PlayerPrefs.HasKey("BodyDisguiseIndex")) {
            bodyDisguiseIndex = PlayerPrefs.GetInt("BodyDisguiseIndex");
        }
        UpdateVisual();
    }

    private void UpdateVisual() {
        if (colorIndex != -1) {
            playerMaterial.color = colors[colorIndex];
        }
        for (int i = 0; i < headDisguises.Length; i++) {
            if (i == headDisguiseIndex) {
                headDisguises[i].gameObject.SetActive(true);
            } else {
                headDisguises[i].gameObject.SetActive(false);
            }
        }
        for (int i = 0; i < bodyDisguises.Length; i++) {
            if (i == bodyDisguiseIndex) {
                bodyDisguises[i].gameObject.SetActive(true);
            } else {
                bodyDisguises[i].gameObject.SetActive(false);
            }
        }
    }

    private void SavePlayerPrefs() {
        PlayerPrefs.SetInt("ColorIndex", colorIndex);
        PlayerPrefs.SetInt("HeadDisguiseIndex", headDisguiseIndex);
        PlayerPrefs.SetInt("BodyDisguiseIndex", bodyDisguiseIndex);
    }

    public void PickColor(int index) {
        colorIndex = index;
        UpdateVisual();
        SavePlayerPrefs();
    }

    public void PickHeadDisguise(int index) {
        headDisguiseIndex = index;
        UpdateVisual();
        SavePlayerPrefs();
    }

    public void PickBodyDisguise(int index) {
        bodyDisguiseIndex = index;
        UpdateVisual();
        SavePlayerPrefs();
    }

    public Disguise[] GetHeadDisguises() {
        return headDisguises;
    }
    public Disguise[] GetBodyDisguises() {
        return bodyDisguises;
    }
    public Color[] GetColors() {
        return colors;
    }
}

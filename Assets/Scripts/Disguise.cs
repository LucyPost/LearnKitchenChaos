using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Disguise : MonoBehaviour
{
    [SerializeField] private Sprite sprite;

    public Sprite GetSprite() {
        return sprite;
    }
    public void SetDisguiseActice(bool value) {
        gameObject.SetActive(value);
    }
}

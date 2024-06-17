using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Fade : MonoBehaviour
{
    [SerializeField] private float fadeTime;

    private Material material;
    
    private float fadeStart;

    private void Awake() {
        material = GetComponent<Renderer>().material;
        material.SetColor("_Color", new Color(1, 1, 1, 1));
    }

    private void Start() {
        fadeStart = Time.time;
    }
    private void Update() {
        float alpha = Mathf.Lerp(1, 0, (Time.time - fadeStart) / fadeTime);
        material.color = new Color(1, 1, 1, alpha);
        if(alpha <= 0.01) {
            gameObject.SetActive(false);
        }
    }
}

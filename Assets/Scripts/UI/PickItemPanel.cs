using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickItemPanel : MonoBehaviour
{
    [SerializeField] private Button pickColorButton;
    [SerializeField] private Button pickHeadDisguiseButton;
    [SerializeField] private Button pickBodyDisguiseButton;
    [SerializeField] private PickDisguiseButton resetDisguiseButton;

    [SerializeField] private Button pickColorButtonTemplete;
    [SerializeField] private Button pickItemButtonTemplete;

    [SerializeField] private DisguiseManager disguiseManager;
    
    private void Awake() {
        pickColorButton.onClick.AddListener(() => {
            ShowPickColorPanel();
        });
        pickHeadDisguiseButton.onClick.AddListener(() => {
            ShowPickHeadDisguisePanel();
        });
        pickBodyDisguiseButton.onClick.AddListener(() => {
            ShowPickBodyDisguisePanel();
        });
        resetDisguiseButton.disguiseID = -1;
        ShowPickColorPanel();
    }

    private void ShowPickColorPanel() {
        ClearPanel();

        resetDisguiseButton.gameObject.SetActive(false);

        Color[] colors = disguiseManager.GetColors();

        for(int i = 0; i < colors.Length; i++) {
            Color color = colors[i];
            GameObject colorButton = Instantiate(pickColorButtonTemplete.gameObject, pickColorButtonTemplete.transform.parent);
            colorButton.SetActive(true);

            colorButton.GetComponent<Outline>().effectColor = color;
            colorButton.GetComponentInChildren<TextMeshProUGUI>().text = color.ToString();
            PickDisguiseButton pickDisguiseButton = colorButton.GetComponent<PickDisguiseButton>();
            pickDisguiseButton.disguiseID = i;

            colorButton.GetComponent<Button>().onClick.AddListener(() => {
                disguiseManager.PickColor(pickDisguiseButton.disguiseID);
            });
        }
    }
    private void ShowPickHeadDisguisePanel() {
        ClearPanel();

        resetDisguiseButton.gameObject.SetActive(true);
        resetDisguiseButton.GetComponent<Button>().onClick.RemoveAllListeners();
        resetDisguiseButton.GetComponent<Button>().onClick.AddListener(() => {
            disguiseManager.PickHeadDisguise(resetDisguiseButton.disguiseID);
        });

        Disguise[] headDisguises = disguiseManager.GetHeadDisguises();

        for(int i = 0; i < headDisguises.Length; i++) {
            Disguise headDisguise = headDisguises[i];
            GameObject headDisguiseButton = Instantiate(pickItemButtonTemplete.gameObject, pickItemButtonTemplete.transform.parent);
            headDisguiseButton.SetActive(true);

            headDisguiseButton.GetComponent<Image>().sprite = headDisguise.GetSprite();
            PickDisguiseButton pickDisguiseButton = headDisguiseButton.GetComponent<PickDisguiseButton>();
            pickDisguiseButton.disguiseID = i;

            headDisguiseButton.GetComponent<Button>().onClick.AddListener(() => {
                disguiseManager.PickHeadDisguise(pickDisguiseButton.disguiseID);
            });
        }
    }

    private void ShowPickBodyDisguisePanel() {
        ClearPanel();

        resetDisguiseButton.gameObject.SetActive(true);
        resetDisguiseButton.GetComponent<Button>().onClick.RemoveAllListeners();
        resetDisguiseButton.GetComponent<Button>().onClick.AddListener(() => {
            disguiseManager.PickBodyDisguise(resetDisguiseButton.disguiseID);
        });

        Disguise[] bodyDisguises = disguiseManager.GetBodyDisguises();

        for(int i = 0; i < bodyDisguises.Length; i++) {
            Disguise bodyDisguise = bodyDisguises[i];
            GameObject bodyDisguiseButton = Instantiate(pickItemButtonTemplete.gameObject, pickItemButtonTemplete.transform.parent);
            bodyDisguiseButton.SetActive(true);

            bodyDisguiseButton.GetComponent<Image>().sprite = bodyDisguise.GetSprite();
            PickDisguiseButton pickDisguiseButton = bodyDisguiseButton.GetComponent<PickDisguiseButton>();
            pickDisguiseButton.disguiseID = i;

            bodyDisguiseButton.GetComponent<Button>().onClick.AddListener(() => {
                disguiseManager.PickBodyDisguise(pickDisguiseButton.disguiseID);
            });
        }
    }

    private void ClearPanel() {
        foreach (Transform child in gameObject.transform) {
            if(child == pickColorButtonTemplete.transform || child == pickItemButtonTemplete.transform) continue;
            Destroy(child.gameObject);
        }
    }
}

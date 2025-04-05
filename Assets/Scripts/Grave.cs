using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Grave : MonoBehaviour {
    [SerializeField] private Sprite[] gravestone;
    [SerializeField] private GraveDescription[] description;
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private TMPro.TextMeshProUGUI[] descriptionText;
    [SerializeField] private GameObject soulObject;

    private bool hasSoul = true;
    private SpriteRenderer spriteRender;
    private GraveDescription currentDescription;
    
    // Showing the description is lagging so preloading to avoid the lag spike on the first time
    private void Start() {
        uiPanel.SetActive(true);
        Canvas.ForceUpdateCanvases();
        uiPanel.SetActive(false);
        spriteRender = GetComponent<SpriteRenderer>();

        spriteRender.sprite = gravestone[Random.Range(0, gravestone.Length)];
    }

    private void OnTriggerEnter2D(Collider2D _other) {
        if (_other.CompareTag("Player")) {
            ChooseRandomDescription();
            ShowDescription();
            Player.instance.canDig = true;
        }
    }
    private void ChooseRandomDescription() {
        if (description.Length > 0) {
            currentDescription = description[Random.Range(0, description.Length)];
        }
    }

    private void OnTriggerExit2D(Collider2D _other) {
        if (_other.CompareTag("Player")) {
            HideDescription();
            Player.instance.canDig = false;
        }
    }

    private void ShowDescription() {
        uiPanel.SetActive(true);
        
        for (int i = 0; i < descriptionText.Length && i < currentDescription.descriptionLines.Length; i++) {
            descriptionText[i].text = currentDescription.descriptionLines[i];
        }
    }
    
    private void HideDescription() {
        uiPanel.SetActive(false);
    }

    public void SpawnSoul() {
        if (hasSoul) {
            hasSoul = false;
            GameObject soulGO = Instantiate(soulObject, transform.position, quaternion.identity);
            Soul soul = soulGO.GetComponent<Soul>();

            if (soul != null) {
                soul.Initilize(currentDescription);
            }
        }
    }
}
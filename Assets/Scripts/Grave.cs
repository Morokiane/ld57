using Unity.Mathematics;
using UnityEngine;

public class Grave : MonoBehaviour {
    [SerializeField] private Sprite[] gravestone;
    [SerializeField] private GraveDescription description;
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private TMPro.TextMeshProUGUI[] descriptionText;
    [SerializeField] private GameObject soulObject;

    private bool hasSoul = true;
    
    // Showing the description is lagging so preloading to avoid the lag spike on the first time
    private void Start() {
        uiPanel.SetActive(true);
        Canvas.ForceUpdateCanvases();
        uiPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D _other) {
        if (_other.CompareTag("Player")) {
            ShowDescription();
            Player.instance.canDig = true;
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
        
        for (int i = 0; i < descriptionText.Length && i < description.descriptionLines.Length; i++) {
            descriptionText[i].text = description.descriptionLines[i];
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
                soul.Initilize(description);
            }
        }
    }
}
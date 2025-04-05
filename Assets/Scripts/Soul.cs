using System.Collections;
using UnityEngine;

public class Soul : MonoBehaviour {
    private bool canPickup;
    private GraveDescription description;

    public void Initilize(GraveDescription data) {
        description = data;
    }

    private void Start() {
        StartCoroutine(PickupWait());
    }

    private IEnumerator PickupWait() {
        yield return new WaitForSeconds(1);
        canPickup = true;
    }

    private void OnTriggerEnter2D(Collider2D _other) {
        if (_other.CompareTag("Player") && canPickup) {
            Controllers.LevelController.soulWeight += description.weight;
            Destroy(gameObject);
        }
    }
}
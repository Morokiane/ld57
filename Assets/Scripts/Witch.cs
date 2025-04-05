using UnityEngine;

public class Witch : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D _other) {
        if (_other.CompareTag("Player")) {
            Player.instance.talkToWitch = true;
            // Grave graveInstance = FindObjectOfType<Grave>();
            Grave.instance.Reset();
        }
    }

    private void OnTriggerExit2D(Collider2D _other) {
        if (_other.CompareTag("Player")) {
            Player.instance.talkToWitch = false;
        }
    }
}
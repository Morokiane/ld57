using System;
using UnityEngine;

namespace Controllers {
    public class Damager : MonoBehaviour {
        [SerializeField] private float damage;
        
        private void OnTriggerEnter2D(Collider2D _other) {
            if (_other.CompareTag("Player")) {
                Player.Player.instance.TakeDamage(damage);    
                // SoundFXController.instance.PlaySoundFXClip(explosionSfx, transform, Data.GameData.instance.sfxVolume);
                // Instantiate(explosion, transform.position, transform.rotation);
                gameObject.SetActive(false);
            }
        }
    }
}

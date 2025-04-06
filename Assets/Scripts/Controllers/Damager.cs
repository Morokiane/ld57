using UnityEngine;

namespace Controllers {
    public class Damager : MonoBehaviour {
        [SerializeField] private float damage;
        [SerializeField] private GameObject explosion;
        
        private void OnTriggerEnter2D(Collider2D _other) {
            if (_other.CompareTag("Player")) {
                Player.Player.instance.TakeDamage(damage);    
                // SoundFXController.instance.PlaySoundFXClip(explosionSfx, transform, Data.GameData.instance.sfxVolume);
                Instantiate(explosion, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
            }
        }
    }
}

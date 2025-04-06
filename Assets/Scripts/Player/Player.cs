using System;
using System.Collections;
using UnityEngine;
using Controllers;

namespace Player {
    public class Player : MonoBehaviour {
        public static Player instance;

        public float playerHP { get; private set; } = 1;
        [SerializeField] private Sprite playerGhost;
        [SerializeField] private Sprite playerNormal;
        public bool takeDamage { get; private set; } = true;

        [HideInInspector] public PlayerMovement playerMovement;
        
        public float currentHP { get; private set; }
        public float currentDepth;

        private CircleCollider2D circleCollider2D;
        private SpriteRenderer spriteRenderer;
        
        private void Start() {
            if (instance == null) {
                instance = this;
            } else {
                Destroy(gameObject);
            }
            
            playerMovement = GetComponent<PlayerMovement>();
            circleCollider2D = GetComponent<CircleCollider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();

            currentHP = playerHP;
        }

        public void TakeDamage(float _damage) {
            if (takeDamage) { 
                currentHP -= _damage;
                    // SoundFXController.instance.PlaySoundFXClip(playerHit, transform, 1f);
            }
            HUDController.instance.PlayerHP();
            
            if (currentHP <= 0 && !LevelController.playerDead) {
                // Instantiate(explode, transform.position, Quaternion.identity);
                LevelController.playerDead = true;
                // HUDController.instance.RemoveLives();
                Ghost();
            }
        }
        private void Ghost() {
            spriteRenderer.sprite = playerGhost;
            takeDamage = false;
            circleCollider2D.enabled = false;
            StartCoroutine(ResetPlayer());
        }
        
        private void UnGhost() {
            spriteRenderer.sprite = playerNormal;
            takeDamage = true;
            circleCollider2D.enabled = true;
        }

        private IEnumerator ResetPlayer() {
            yield return new WaitForSeconds(2);
            UnGhost();
        }
    }
}

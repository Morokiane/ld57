using System;
using System.Collections;
using UnityEngine;
using Controllers;

namespace Player {
    public class Player : MonoBehaviour {
        public static Player instance;

        [SerializeField] private int playerHP;
        [SerializeField] private Sprite playerGhost;
        [SerializeField] private Sprite playerNormal;
        public bool takeDamage { get; private set; } = true;

        [HideInInspector] public PlayerMovement playerMovement;
        
        private int currentHP;

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

        public void TakeDamage(int _damage) {
            if (takeDamage) { 
                currentHP -= _damage;
                    // SoundFXController.instance.PlaySoundFXClip(playerHit, transform, 1f);
            }
            // HUDController.instance.PlayerHp();
            
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

using System;
using System.Collections;
using UnityEngine;
using Controllers;

namespace Player {
    public class Player : MonoBehaviour {
        public static Player instance;

        public float playerHP { get; private set; } = 1;
        public bool takeDamage { get; private set; } = true;
        
        [SerializeField] private Sprite playerGhost;
        [SerializeField] private Sprite playerNormal;
        [SerializeField] private GameObject explosion;

        [HideInInspector] public PlayerMovement playerMovement;
        
        public float currentHP { get; private set; }
        public float currentDepth;

        private int lastDepthMilestone = 0;
        private Coroutine ghostRoutine;
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

        private void Update() {
            int currentMilestone = Mathf.FloorToInt(Mathf.Abs(currentDepth) / 50);

            if (currentMilestone > lastDepthMilestone) {
                LevelController.instance.spawnWait = Mathf.Max(0.08f, LevelController.instance.spawnWait - 0.1f);
                lastDepthMilestone = currentMilestone;
                
                Color c = HUDController.instance.fade.color;
                c.a = Mathf.Lerp(c.a, Mathf.Min(0.8f, LevelController.instance.spawnWait + 0.1f), Time.deltaTime * 5f);
                HUDController.instance.fade.color = c;
            }
        }
        
        public void TakeDamage(float _damage) {
            if (takeDamage) { 
                currentHP -= _damage;
                Ghost();
                // SoundFXController.instance.PlaySoundFXClip(playerHit, transform, 1f);
            }
            
            HUDController.instance.PlayerHP();
            
            if (currentHP <= 0 && !LevelController.playerDead) {
                Instantiate(explosion, transform.position, Quaternion.identity);
                LevelController.playerDead = true;
                HUDController.instance.GameOver();
                StartCoroutine(LevelController.GameOver());
            }
        }
        
        private void Ghost() {
            if (ghostRoutine != null) {
                StopCoroutine(ghostRoutine);
            }

            spriteRenderer.sprite = playerGhost;
            takeDamage = false;
            circleCollider2D.enabled = false;

            ghostRoutine = StartCoroutine(ResetPlayer());
        }

        private IEnumerator ResetPlayer() {
            yield return new WaitForSeconds(2);
            UnGhost();
            ghostRoutine = null;
        }
        
        private void UnGhost() {
            spriteRenderer.sprite = playerNormal;
            takeDamage = true;
            circleCollider2D.enabled = true;
        }
    }
}

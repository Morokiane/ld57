using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Controllers {
    public class HUDController : MonoBehaviour {
        public static HUDController instance;

        [SerializeField] private Image healthBar;
        [SerializeField] private TextMeshProUGUI depth;
        [SerializeField] private TextMeshProUGUI depthFinal;
        [SerializeField] private RectTransform ninePatch;

        public SpriteRenderer fade;
        
        private Camera mainCamera;

        private void Start() {
            instance = this;
            mainCamera = Camera.main;
            
            StartCoroutine(WaitForPlayerAndInit());
        }

        private void Update() {
            depth.text = Mathf.RoundToInt(Player.Player.instance.currentDepth).ToString("0000");

            if (Input.GetButtonDown("Jump")) {
                SceneManager.LoadScene("Title");
            }
        }

        public void GameOver() {
            Debug.Log("Game Over");
            ninePatch.gameObject.SetActive(true);
            depthFinal.text = Mathf.RoundToInt(Player.Player.instance.currentDepth).ToString("0000");
        }

        private IEnumerator WaitForPlayerAndInit() {
            // Wait until the player exists in the scene
            while (Player.Player.instance == null) {
                yield return null; // wait one frame
            }

            if (Player.Player.instance == null) {
                Debug.LogError("Player instance is still null");
                yield break;
            }

            if (healthBar == null) {
                Debug.LogError("healthBar reference is null in HUDController");
                yield break;
            }

            Debug.Log("Player and healthBar exist — accessing playerHP");
            
            healthBar.fillAmount = Player.Player.instance.playerHP;
        }

        public void PlayerHP() {
            healthBar.fillAmount = Player.Player.instance.currentHP;
            StartCoroutine(Shake(0.4f, 0.15f));
        }

        public void Depth() {
        }
        
        private IEnumerator Shake(float _magnitude, float _duration) {
            Vector3 originalPos = mainCamera.transform.position;
            float elapsedTime = 0f;

            while (elapsedTime < _duration) {
                float xOffset = UnityEngine.Random.Range(-1f, 1f) * _magnitude;
                float yOffset = UnityEngine.Random.Range(-1f, 1f) * _magnitude;

                mainCamera.transform.position = new Vector3(xOffset, yOffset, -1.55f);
                elapsedTime += Time.unscaledDeltaTime;
                yield return 0;
            }
            mainCamera.transform.position = originalPos;
        }
    }
}

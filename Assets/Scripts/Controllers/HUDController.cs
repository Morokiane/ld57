using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers {
    public class HUDController : MonoBehaviour {
        public static HUDController instance;

        [SerializeField] private Image healthBar;
        [SerializeField] private TextMeshProUGUI depth;

        private void Start() {
            instance = this;
            StartCoroutine(WaitForPlayerAndInit());
        }

        private void Update() {
            depth.text = Mathf.RoundToInt(Player.Player.instance.currentDepth).ToString("0000");
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
        }

        public void Depth() {
        }
    }
}

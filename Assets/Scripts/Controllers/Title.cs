using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers {
    public class Title : MonoBehaviour {
        private AudioSource audioSource;
        private bool hasStarted = false;

        private void Start() {
            audioSource = GetComponent<AudioSource>();
        }

        private void Update() {
            if (!hasStarted && Input.GetButtonDown("Jump")) {
                hasStarted = true;
                StartCoroutine(PlayAndLoad());
            }
            
            if (Input.GetButtonDown("Cancel")) {
                Application.Quit();
            }
        }

        private IEnumerator PlayAndLoad() {
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length); // or a shorter delay if needed
            SceneManager.LoadScene("Main");
        }
    }
}

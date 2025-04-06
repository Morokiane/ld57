using System;
using UnityEngine;

namespace Controllers {
    public class HUDController : MonoBehaviour {
        public static HUDController hudController;

        private void Start() {
            hudController = this;
        }
    }
}

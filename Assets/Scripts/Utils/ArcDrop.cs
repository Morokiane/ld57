using UnityEngine;

namespace Utils {
    public class ArcDrop : MonoBehaviour {

        [SerializeField] private float arcHeight = 2f;
        [SerializeField] private float dropDuration = 1f;
        [SerializeField] private AnimationCurve dropCurve;

        private Vector2 startPosition;
        private Vector2 targetPosition;
        private Vector2 dropTargetOffset;
        private float elapsedTime;

        private void Start() {
            startPosition = transform.position;

            dropTargetOffset.x = Random.Range(-3, 3);
            arcHeight = Random.Range(1, 3);
            
            targetPosition = startPosition - dropTargetOffset;
            elapsedTime = 0;
        }

        private void FixedUpdate() {
            if (elapsedTime < dropDuration) {
                elapsedTime += Time.deltaTime;
                float progress = Mathf.Clamp01(elapsedTime / dropDuration);
                
                Vector2 horizontalPosition = Vector2.Lerp(startPosition, targetPosition, progress);
                float heightOffset = dropCurve.Evaluate(progress) * arcHeight;

                transform.position = new Vector2(horizontalPosition.x, startPosition.y + heightOffset);
            }
        }
    }
}

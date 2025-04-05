using UnityEngine;

namespace Utils {
    public class CameraFollow : MonoBehaviour {
        [SerializeField] private Transform target;
        [SerializeField] private Vector2 deadZoneSize = new Vector2(1f, 1f); // Width & Height of dead zone
        [SerializeField] private float followSpeed = 5f;
        [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f);

        private void LateUpdate() {
            if (target == null) return;

            Vector3 targetPosition = target.position + offset;
            Vector3 delta = targetPosition - transform.position;

            // Check if outside dead zone
            if (Mathf.Abs(delta.x) > deadZoneSize.x || Mathf.Abs(delta.y) > deadZoneSize.y) {
                Vector3 desiredPosition = transform.position;

                if (Mathf.Abs(delta.x) > deadZoneSize.x)
                    desiredPosition.x = targetPosition.x - Mathf.Sign(delta.x) * deadZoneSize.x;

                if (Mathf.Abs(delta.y) > deadZoneSize.y)
                    desiredPosition.y = targetPosition.y - Mathf.Sign(delta.y) * deadZoneSize.y;

                transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
            }
        }
    }
}

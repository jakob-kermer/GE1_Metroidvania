using UnityEngine;

namespace AI
{
    public class RangeSensor : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float detectionRange = 5f;

        public bool IsTargetInRange()
        {
            if (target == null)
                return false;

            return Vector3.Distance(transform.position, target.position) <= detectionRange;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = IsTargetInRange()
                ? Color.green
                : Color.red;

            Gizmos.DrawWireSphere(
                transform.position,
                detectionRange
            );
        }
    }
}
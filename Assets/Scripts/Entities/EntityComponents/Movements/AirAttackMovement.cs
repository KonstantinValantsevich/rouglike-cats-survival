using UnityEngine;

namespace Entities.EntityComponents.Movements
{
    public class AirAttackMovement : Movement
    {
        private readonly Vector3 targetPosition;
        public float timeToFall;

        private Vector3 scaleSpeed;

        public AirAttackMovement(float timeToFall, Vector3 startOffset, Vector3 startScale,
            Transform movementTransform,
            Transform rotationTransform) : base(
            startOffset.magnitude / timeToFall, movementTransform, rotationTransform)
        {
            this.timeToFall = timeToFall;

            var enemy = SceneHelper.FindRandomEnemy();
            if (enemy == null) {
                targetPosition = Random.insideUnitCircle * 10;
            }
            else {
                targetPosition = enemy.transform.position;
            }

            var endScale = movementTransform.localScale;

            movementTransform.position = targetPosition + new Vector3(startOffset.x, startOffset.y, 0);
            movementTransform.localScale = startScale;
            scaleSpeed = (endScale - startScale) / timeToFall;

            SetLookRotation(targetPosition);
        }

        public override void Tick(float deltaTime)
        {
            Move(movementTransform.right);
            movementTransform.localScale += scaleSpeed * deltaTime;
        }
    }
}
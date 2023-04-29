using UnityEngine;

namespace Entities.EntityComponents.Attacks
{
    public class PartPlayerProjectile : Attack
    {
        private GameObject playerMock;
        private void Awake()
        {
            playerMock = new GameObject("Player Mock");
        }

        private void Update()
        {
            playerMock.transform.position = transform.position;
            playerMock.transform.rotation = transform.rotation;
        }

        public override void PerformAttack(float deltaTime, float baseDamage)
        {
            if (Cooldown(deltaTime)) {
                return;
            }

            var bullet = Instantiate(AttackPrefab, attackerTransform.position, attackerTransform.rotation,
                playerMock.transform);
            InitialiseBullet(bullet, baseDamage);
        }
    }
}
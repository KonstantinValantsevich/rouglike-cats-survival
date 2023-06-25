using DG.Tweening;
using Entities.EntityComponents;
using Entities.Interfaces;
using UnityEngine;

namespace Entities.Collectibles
{
    public class ExperienceOrb : Collectible
    {
        public int experienceAmount = 3;
        public ParticleSystem buff;
        public SpriteRenderer sprite;
        public GameObject aura;

        public override void Initialise(IPlayerState player)
        {
            Player = player;

            InitialiseComponents();

            UpdateTickables();

            Health.Damaged += amount => Debug.Log($"Entity: {name} was damaged by {amount} damage");
        }

        public override void CollectItem(Inventory inventory)
        {
            inventory.AddExperience(experienceAmount);
            transform.SetParent(Player.Transform);
            transform.DOLocalJump(Random.insideUnitCircle * 5, Random.value, 1, 1)
                .Append(transform.DOLocalMove(Vector3.zero, 0.3f))
                .AppendCallback(() => {
                    buff.time = 0;
                    buff.gameObject.SetActive(true);
                    sprite.enabled = false;
                    aura.SetActive(false);
                }).AppendInterval(0.5f)
                .AppendCallback(() => {
                    buff.gameObject.SetActive(false);
                    sprite.enabled = true;
                    aura.SetActive(true);   
                    KillEntity();
                });
        }
    }
}
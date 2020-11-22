﻿using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using Exa.Math;
using Exa.Ships;
using UnityEngine;

namespace Exa.Weapons
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        private float damage;
        private float lifeTime;
        private BlockContext damageMask;
        private float timeAlive;

        public void Setup(Transform transform, float speed, float range, float damage, BlockContext damageMask) {
            this.transform.position = transform.position;
            this.damage = damage;
            this.lifeTime = range / speed;
            this.damageMask = damageMask;

            rb.velocity = transform.right * speed;
        }

        public void Update() {
            var deltaTime = Time.deltaTime;
            timeAlive += deltaTime;

            if (timeAlive > lifeTime) {
                Destroy(gameObject);
            }
        }

        public void OnTriggerEnter2D(Collider2D collider) {
            var block = collider.gameObject.GetComponent<Block>();
            if (!(block && PassesDamageMask(block))) return;

            var absorbedAllDamage = block.PhysicalBehaviour.AbsorbDamage(damage, out var eventData);
            damage -= eventData.absorbedDamage;

            if (absorbedAllDamage) {
                Destroy(gameObject);
            }
        }

        private bool PassesDamageMask(Block block) {
            if (block.Parent == null) {
                Debug.LogError("Block has no parent");
                return false;
            }

            return (block.Parent.BlockContext & damageMask) != 0;
        }
    }
}
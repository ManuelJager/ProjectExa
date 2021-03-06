﻿using System.Collections.Generic;
using System.Diagnostics;
using Exa.Math;
using UnityEngine;
using Debug = UnityEngine.Debug;

#pragma warning disable CS0649

namespace Exa.Gameplay
{
    public class PopupManager : MonoBehaviour
    {
        [SerializeField] private GameObject damagePopupPrefab;
        private Dictionary<object, DamagePopup> popupByDamageSource;
        private int order = 0;

        private void Awake() {
            popupByDamageSource = new Dictionary<object, DamagePopup>();
        }

        public void CreateOrUpdateDamagePopup(Vector2 worldPosition, object damageSource, float damage) {
            order++;

            if (damageSource != null && popupByDamageSource.ContainsKey(damageSource)) {
                SetupPopup(popupByDamageSource[damageSource], worldPosition, damage);
                return;
            }

            var popup = CreateNewDamagePopup(worldPosition, damage);

            if (damageSource != null) {
                popupByDamageSource.Add(damageSource, popup); 
                popup.DestroyEvent.AddListener(() => popupByDamageSource.Remove(damageSource));
            }
        }

        private DamagePopup CreateNewDamagePopup(Vector2 worldPosition, float damage) {
            var popupGO = Instantiate(damagePopupPrefab, transform);
            var popup = popupGO.GetComponent<DamagePopup>();
            SetupPopup(popup, worldPosition, damage);
            return popup;
        }

        private void SetupPopup(DamagePopup popup, Vector2 worldPosition, float damage) {
            var randomizedPosition = worldPosition + MathUtils.RandomVector2(0.5f);
            popup.Setup(randomizedPosition, damage, order);
        }
    }
}

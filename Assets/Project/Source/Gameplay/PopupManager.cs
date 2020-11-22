using Exa.Math;
using UnityEngine;
#pragma warning disable CS0649

namespace Exa.Gameplay
{
    public class PopupManager : MonoBehaviour
    {
        [SerializeField] private GameObject damagePopupPrefab;
         
        public void CreateDamagePopup(Vector2 worldPosition, float value) {
            var popupGO = Instantiate(damagePopupPrefab, transform);
            var randomizedPosition = worldPosition + MathUtils.RandomVector2(0.5f);
            popupGO.GetComponent<DamagePopup>().Setup(randomizedPosition, value.Round().ToString());
        }
    }
}

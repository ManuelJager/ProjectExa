using System.Collections;
using System.Collections.Generic;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Math;
using Exa.Types;
using Exa.Types.Binding;
using Exa.Utils;
using UnityEngine;

namespace Exa.ShipEditor
{
    public class EditorGridTurretLayer : MonoBehaviour
    {
        [SerializeField] private Transform prefabContainer;
        [SerializeField] private Transform instanceContainer;

        private Dictionary<ITurretTemplate, GameObject> turretOverlayPrefabs = new Dictionary<ITurretTemplate, GameObject>();

        public void ClearPrefabs() {
            prefabContainer.DestroyChildren();
            turretOverlayPrefabs.Clear();
        }

        public void GenerateTurretOverlayPrefab(ITurretTemplate template) {
            var go = new GameObject($"{template} overlay");
            go.SetActive(false);
            go.transform.SetParent(prefabContainer);
            go.transform.position = prefabContainer.position;
            var renderer = go.AddComponent<SpriteRenderer>();
            renderer.sprite = GenerateTexture(template).CreateSprite();
            renderer.color = Color.white;
            turretOverlayPrefabs[template] = go;
        }
         
        private Texture2D GenerateTexture(ITurretTemplate template) {
            var pixelRadius = Mathf.RoundToInt(template.TurretRadius * 32);
            var size = pixelRadius * 2;
            var centre = (pixelRadius - 0.5f).ToVector2();
            return new Texture2D(size, size).SetDefaults()
                .DrawCircle(new Color(1, 1, 1, 0.5f), centre, pixelRadius, true)
                .DrawCircle(new Color(1, 1, 1, 0.2f), centre, pixelRadius - 2);
        }
    }
}

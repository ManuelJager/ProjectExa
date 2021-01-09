using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Exa.Utils;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using MathUtils = Exa.Math.MathUtils;

namespace Exa.VFX
{
    public class GaussCannonArcs : MonoBehaviour
    {
        [Header("Arc settings")]
        [SerializeField] private int arcCount;
        [SerializeField] private float arcDistance;
        [SerializeField] private GameObject arcPrefab;
        [SerializeField] private List<GameObject> arcs;

        private float timeAlive;
        private int prevActivatedArcIndex;

        public float ChargeTime { get; set; }

        public void RandomizeMaterials() {
            foreach (var mat in arcs.Select(arc => arc.GetComponent<SpriteRenderer>().CopyMaterial())) {
                mat.SetVector("_NoiseOffset", MathUtils.RandomVector2(10f));
            }
        }

        private void OnEnable() {
            timeAlive = 0f;
            prevActivatedArcIndex = 0;
        }

        private void OnDisable() {
            arcs.ForEach(arc => arc.SetActive(false));
        }

        private void Update() {
            timeAlive += Time.deltaTime;
            SetActiveUpTo(Mathf.FloorToInt(timeAlive / ChargeTime * (arcCount + 1)) + 1);
        }

        private void SetActiveUpTo(int index) {
            if (index > arcCount) {
                return;
            }

            var diff = index - prevActivatedArcIndex;
            if (diff == 0) return;

            for (var i = prevActivatedArcIndex; i < index; i++) {
                arcs[i].gameObject.SetActive(true);
            }

            prevActivatedArcIndex = index;
        }

        [ContextMenu("Create Arcs")]
        private void CreateArcs() {
            foreach (var child in transform.GetChildren().ToList()) {
                DestroyImmediate(child.gameObject);
            }

            arcs = new List<GameObject>(arcCount);
            for (var i = 0; i < arcCount; i++) {
                var go = Instantiate(arcPrefab, transform);
                go.name = $"Arc ({i})";
                go.transform.localPosition = new Vector3(i * arcDistance, 0);
                go.SetActive(false);

                EditorUtility.SetDirty(go);
                EditorSceneManager.MarkSceneDirty(go.scene);

                arcs.Add(go);
            }

            EditorUtility.SetDirty(gameObject);
        }
    }
}


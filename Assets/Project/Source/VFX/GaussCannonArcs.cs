using System.Collections.Generic;
using System.Linq;
using Exa.Audio;
using Exa.Data;
using Exa.Logging;
using Exa.Math;
using Exa.Types.Generics;
using Exa.UI.Tweening;
using Exa.Utils;
using UnityEditor;
using UnityEngine;
using MathUtils = Exa.Math.MathUtils;
using Random = System.Random;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;

#endif

namespace Exa.VFX {
    public class GaussCannonArcs : MonoBehaviour {
        private static readonly int NoiseOffsetID = Shader.PropertyToID("_NoiseOffset");
        private static readonly int FlickeringOffsetID = Shader.PropertyToID("_FlickeringOffset");
        private static readonly float StepSize = 1f / 3f;

        [Header("References")]
        [SerializeField] private SerializableDictionary<int, Sound> coilChargeSoundByStep;
        [SerializeField] private LocalAudioPlayerProxy audioPlayer;
        
        [Header("Arc settings")]
        [SerializeField] private ExaEase progressMap;
        [SerializeField] private int arcCount;
        [SerializeField] private float arcDistance;
        [SerializeField] private GameObject arcPrefab;
        [SerializeField] private List<GameObject> arcs;
        
        private int prevCoilStep = -1;
        private int prevActiveIndex = -1;

        public void SetChargeProgress(Scalar progress, bool isCharging) {
            if (progress == 0f) {
                Reset();
                return;
            }
            
            var index = Mathf.CeilToInt(EvaluateProgress(progress, isCharging) * arcCount);
            
            if (prevActiveIndex == index) {
                return;
            }
            
            prevActiveIndex = index;
            
            SetActiveUpTo(index);
        }

        public void Reset() {
            prevActiveIndex = -1;
            prevCoilStep = -1;
            
            SetActiveUpTo(0);
        }

        public void RandomizeMaterials() {
            var random = new Random();
            var propertyBlock = new MaterialPropertyBlock();

            foreach (var spriteRenderer in arcs.Select(arc => arc.GetComponent<SpriteRenderer>())) {
                propertyBlock.SetVector(NoiseOffsetID, MathUtils.RandomVector2(10f));
                propertyBlock.SetFloat(FlickeringOffsetID, random.Next() % 1000);
                spriteRenderer.SetPropertyBlock(propertyBlock);
            }
        }

        private float EvaluateProgress(float progress, bool isCharging) {
            var coilStep = progress.DivRem(StepSize, out var rem);

            if (isCharging && prevCoilStep < coilStep) {
                audioPlayer.Play(coilChargeSoundByStep[coilStep]);
            }

            prevCoilStep = coilStep;

            return coilStep * StepSize + progressMap.Evaluate(rem / StepSize) * StepSize;
        }

        private void SetActiveUpTo(int index) {
            if (index > arcCount) {
                Debug.LogWarning($"Index out of range {index}");

                return;
            }

            for (var i = 0; i < arcCount; i++) {
                arcs[i].SetActive(i < index);
            }
        }

    #if UNITY_EDITOR
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
    #endif
    }
}
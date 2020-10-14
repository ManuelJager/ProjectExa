using System;
using DG.Tweening;
using Exa.Data;
using Exa.Grids.Blueprints;
using Exa.Ships;
using Exa.UI.Tweening;
using Exa.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI
{
    public class FleetView : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform mothershipContainer;
        [SerializeField] private Transform unitsContainer;
        [SerializeField] private Text unitCountText;

        [Header("Settings")] 
        [SerializeField] private AnimationArgs animationArgs;
        private TweenRef<int> fontSizeTween;
        private TweenRef<Color> colorTween;
        private Fleet fleet;
        private Func<BlueprintContainer, FleetBlueprintView> viewFactory;

        private void Awake()
        {
            fontSizeTween = new TweenWrapper<int>(unitCountText.DOFontSize);
            colorTween = new TweenWrapper<Color>(unitCountText.DOColor);
        }

        public Fleet Export()
        {
            return fleet;
        }

        public void Create(int capacity, Func<BlueprintContainer, FleetBlueprintView> viewFactory)
        {
            this.fleet = new Fleet(capacity);
            this.viewFactory = viewFactory;
        }

        public void Toggle(BlueprintContainer blueprint)
        {
            if (viewFactory(blueprint).Selected)
                Remove(blueprint);
            else
                Insert(blueprint);
        }

        public void Insert(BlueprintContainer blueprint)
        {
            if (blueprint.Data.BlueprintType.IsMothership)
                AddMothership(blueprint);
            else
                AddUnit(blueprint);
        }

        public void Remove(BlueprintContainer blueprint)
        {
            if (blueprint.Data.BlueprintType.IsMothership)
                fleet.mothership = null;
            else
            {
                fleet.units.Remove(blueprint);
                UpdateUnitCountText();
            }

            SetSelected(blueprint, false);
        }

        private void AddMothership(BlueprintContainer blueprint)
        {
            if (fleet.mothership != null)
                SetSelected(fleet.mothership, false);

            fleet.mothership = blueprint;
            SetSelected(blueprint, true, mothershipContainer);
        }

        private void AddUnit(BlueprintContainer blueprint)
        {
            if (fleet.units.Count == fleet.units.Capacity)
            {
                OnUnitsFull(animationArgs);
                return;
            }
            
            fleet.units.Add(blueprint);
            UpdateUnitCountText();
            SetSelected(blueprint, true, unitsContainer);
        }

        private void SetSelected(BlueprintContainer blueprint, bool active, Transform container = null)
        {
            var view = viewFactory(blueprint);
            view.Selected = active;
            view.transform.SetParent(container != null 
                ? container 
                : view.NormalContainer);
            this.DelayOneFrame(view.hoverable.Refresh);
        }

        private void OnUnitsFull(AnimationArgs args)
        {
            unitCountText.fontSize = args.fontSize.active;
            fontSizeTween.To(args.fontSize.inactive, args.animTime);

            unitCountText.color = args.color.active;
            colorTween.To(args.color.inactive, args.animTime);
        }

        private void UpdateUnitCountText()
        {
            unitCountText.text = $"{fleet.units.Count}/{fleet.units.Capacity}";
        }

        [Serializable]
        public struct AnimationArgs
        {
            public float animTime;
            public ActivePair<int> fontSize;
            public ActivePair<Color> color;
        }
    }
}
using DG.Tweening;
using Exa.Grids.Blueprints;
using Exa.UI.Components;
using UnityEngine;
using UnityEngine.UI;
using Exa.Math;
using Exa.UI.Tweening;
using Exa.Utils;

#pragma warning disable CS0649

namespace Exa.UI
{
    public class BlueprintTypeButton : Navigateable
    {
        public int order;

        [SerializeField] private Button button;
        [SerializeField] private Text text;
        [SerializeField] private LayoutElement layoutElement;
        private BlueprintTypeTabContent content;
        private TweenRef<float> sizeTween;

        public BlueprintTypeSelectEvent SelectType = new BlueprintTypeSelectEvent();

        private void Awake()
        {
            sizeTween = new TweenWrapper<float>(layoutElement.DOPreferredHeight)
                .SetDuration(0.2f);
        }

        public void Init(BlueprintType blueprintType, BlueprintTypeTabContent content, int order, NavigateableTabManager tabManager)
        {
            this.content = content;
            this.order = order;
            text.text = blueprintType.displayName;
            button.onClick.AddListener(() => 
            {
                SelectType?.Invoke(blueprintType);
                tabManager.SwitchTo(this);
            });
        }

        public override void HandleExit(Navigateable target)
        {
            sizeTween.To(48f);
            content.HandleExit(target is BlueprintTypeButton button
                    ? Vector2.up * (button.order > order).To1()
                    : Vector2.zero);
        }

        public override void HandleEnter(NavigationArgs args)
        {
            sizeTween.To(72f);
            content.HandleEnter(args?.current is BlueprintTypeButton button
                    ? Vector2.down * (button.order > order).To1()
                    : Vector2.zero);
        }
    }
}
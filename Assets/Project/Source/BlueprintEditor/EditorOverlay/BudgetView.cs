using DG.Tweening;
using Exa.Grids.Blocks;
using Exa.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.ShipEditor
{
    public class BudgetView : MonoBehaviour
    {
        public BlockCostsView view;

        private AnimatableField creditsField;
        private AnimatableField metalsField;
        
        private void Awake() {
            creditsField = new AnimatableField(view.credits, view.creditsBackground);
            metalsField = new AnimatableField(view.metals, view.metalsBackground);
        }
        
        public void SetBudget(BlockCosts budget) {
            view.Refresh(budget);
            
            creditsField.SetActiveColor(budget.creditCost >= 0);
            metalsField.SetActiveColor(budget.metalsCost >= 0);
        }
    }
    
    public class AnimatableField
    {
        private Graphic text;
        private Graphic image;
        
        private Color initialTextColor;
        private Color initialImageColor;

        private Tween textTween;
        private Tween imageTween;
        
        private bool active;

        public AnimatableField(Graphic text, Graphic image) {
            this.text = text;
            this.image = image;
            
            initialTextColor = text.color;
            initialImageColor = image.color;
            
            active = true;
        }

        public void SetActiveColor(bool active) {
            if (active != this.active) {
                if (active) {
                    Reset();
                }
                else {
                    Animate(Colors.RoseVale, Colors.RoseVale.SetAlpha(100f / 256f), 0.5f);
                }
            }

            this.active = active;
        }

        private void Reset() {
            Animate(initialTextColor, initialImageColor, 0.2f);
        }

        private void Animate(Color textColor, Color imageColor, float duration) {
            text.DOColor(textColor, duration).Replace(ref textTween);
            image.DOColor(imageColor, duration).Replace(ref imageTween);
        }
    }
}
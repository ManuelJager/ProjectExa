using Exa.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace Assets.Project.Source.CustomEditors
{
    [CustomEditor(typeof(CanvasGroupInteractibleAdapter))]
    class CanvasGroupInteractibleAdapterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            ((CanvasGroupInteractibleAdapter)target).Interactible = ((CanvasGroupInteractibleAdapter)target).Interactible;
        }
    }
}

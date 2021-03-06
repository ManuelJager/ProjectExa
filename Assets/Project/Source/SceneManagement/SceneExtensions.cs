﻿using UnityEngine;

namespace Exa.SceneManagement
{
    public static class SceneExtensions
    {
        public static bool GetParentSceneIsLoading(this MonoBehaviour monoBehaviour) {
            var name = monoBehaviour.gameObject.scene.name;
            return Systems.Scenes.GetSceneIsLoading(name);
        }

        public static bool GetParentSceneIsUnloading(this MonoBehaviour monoBehaviour) {
            var name = monoBehaviour.gameObject.scene.name;
            return Systems.Scenes.GetSceneIsUnloading(name);
        }
    }
}
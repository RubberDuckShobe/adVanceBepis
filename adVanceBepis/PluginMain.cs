using BepInEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace adVanceBepis
{
    [BepInPlugin("org.rubberduckshobe.advancebepis", "adVanceBepis", "0.1.0")]
    //ensure the game is OLDTV and not... whatever
    //though it'll still work with plasmatv because they're the same
    [BepInProcess("V.exe")]
    public class adVanceBepisMain : BaseUnityPlugin
    {
        void Awake() {
            Logger.LogInfo("adVanceBepis loaded!");

            var harmonyPatchObject = new GameObject("adVanceBepis Patcher Object");
            DontDestroyOnLoad(harmonyPatchObject);
            harmonyPatchObject.AddComponent<HarmonyPatches>();

            SceneManager.activeSceneChanged += OnSceneChange;
        }

        void OnSceneChange(Scene oldScene, Scene newScene) {
            Logger.LogInfo($"Scene switched from {oldScene.name} to {newScene.name}");
        }
    }
}
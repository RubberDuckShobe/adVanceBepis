using BepInEx;
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
            SceneManager.activeSceneChanged += OnSceneChange;

            Logger.LogInfo("adVanceBepis loaded!");

            var harmonyPatchObject = new GameObject("adVanceBepis Patcher Object");
            DontDestroyOnLoad(harmonyPatchObject);
            harmonyPatchObject.AddComponent<HarmonyPatches>();
        }

        //Runs on every scene change.
        //who would've thought
        void OnSceneChange(Scene oldScene, Scene newScene) {
            Logger.LogInfo($"Scene switched from {oldScene.name} to {newScene.name}");
        }
    }
}
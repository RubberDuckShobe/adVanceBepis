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

            //Creates the adVanceBepis Patcher Object and prevents it from being destroyed when the scene changes (OLDTV has a Loading scene before the menu)
            //Then the HarmonyPatches component is added to the object which will then apply the Harmony patches.
            var harmonyPatchObject = new GameObject("adVanceBepis Patcher Object");
            DontDestroyOnLoad(harmonyPatchObject);
            harmonyPatchObject.AddComponent<HarmonyPatches>();

            //Does the same like above but with the Discord Rich Presence stuff
            var richPresenceObject = new GameObject("adVanceBepis DiscordRPC Manager");
            DontDestroyOnLoad(richPresenceObject);
            richPresenceObject.AddComponent<adVanceRichPresence>();
        }

        //Runs on every scene change.
        //who would've thought
        void OnSceneChange(Scene oldScene, Scene newScene) {
            Logger.LogInfo($"Scene switched from {oldScene.name} to {newScene.name}");
        }
    }
}
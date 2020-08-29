using BepInEx;
using BepInEx.Configuration;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace adVanceBepis
{
    [BepInPlugin("org.rubberduckshobe.advancebepis", "adVanceBepis", "0.2.0")]
    //ensure the game is OLDTV and not... whatever
    //though it'll still work with plasmatv because they're the same
    [BepInProcess("V.exe")]
    public class adVanceBepisMain : BaseUnityPlugin
    {
        public static ConfigEntry<bool> configEnableRichPresence;
        public static ConfigEntry<bool> configEnableRazerChroma;
        public static ConfigEntry<bool> configEnableCustomMenuText;
        public static ConfigEntry<string> configCustomMenuText;
        public static ConfigEntry<bool> configEnableCustomMenuTextSize;
        public static ConfigEntry<int> configCustomMenuTextSize;

        void Start() {
            //config things~
            configEnableRichPresence = Config.Bind("adVanceBepis Settings",   // The section under which the option is shown
                                     "EnableRichPresence",  // The key of the configuration option in the configuration file
                                     true, // The default value
                                     "Enable/disable Rich Presence."); // Description of the option to show in the config file

            configEnableRazerChroma = Config.Bind("adVanceBepis Settings",
                                     "EnableRazerChroma",
                                     true,
                                     "Enable/disable Razer Chroma integration.");

            configEnableCustomMenuText = Config.Bind("Customization",
                                     "EnableCustomMenuText",
                                     false,
                                     "Enable/disable custom menu text.");

            configCustomMenuText = Config.Bind("Customization",
                                     "CustomMenuText",
                                     "adVance",
                                     "Custom menu text to use.");

            configEnableCustomMenuTextSize = Config.Bind("Customization",
                                     "EnableCustomMenuTextSize",
                                     false,
                                     "Enable/disable custom menu text size.");

            configCustomMenuTextSize = Config.Bind("Customization",
                                     "CustomMenuTextSize",
                                     200,
                                     "Custom menu text size to use.");

            SceneManager.activeSceneChanged += OnSceneChange;

            Logger.LogInfo("adVanceBepis loaded!");

            //Creates the adVanceBepis Patcher Object and prevents it from being destroyed when the scene changes (OLDTV has a Loading scene before the menu)
            //Then the HarmonyPatches component is added to the object which will then apply the Harmony patches.
            var harmonyPatchObject = new GameObject("adVanceBepis Patcher Object");
            DontDestroyOnLoad(harmonyPatchObject);
            harmonyPatchObject.AddComponent<HarmonyPatches>();

            if (configEnableRichPresence.Value) {
                //Does the same like above but with the Discord Rich Presence stuff
                var richPresenceObject = new GameObject("adVanceBepis DiscordRPC Manager");
                DontDestroyOnLoad(richPresenceObject);
                richPresenceObject.AddComponent<adVanceRichPresence>();
            }

            if (configEnableRazerChroma.Value) {
                //Does the same like above but with the Razer Chroma stuff
                var razerChromaObject = new GameObject("adVanceBepis Chroma Manager");
                DontDestroyOnLoad(razerChromaObject);
                razerChromaObject.AddComponent<adVanceChroma>(); 
            }
        }

        //Runs on every scene change.
        //who would've thought
        void OnSceneChange(Scene oldScene, Scene newScene) {
            Logger.LogInfo($"Scene switched from {oldScene.name} to {newScene.name}");
        }
    }
}
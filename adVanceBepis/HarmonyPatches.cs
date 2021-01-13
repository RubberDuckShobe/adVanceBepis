using System.Collections;
using HarmonyLib;
using UnityEngine;
using BepInEx.Logging;
using static adVanceBepis.OLDTVResources;
using Steamworks;
using GameJolt.API.Objects;
using System;

namespace adVanceBepis
{
    class HarmonyPatches : MonoBehaviour
    {
        public static ManualLogSource patchLogSource = new ManualLogSource("adVanceBepis Patches");
        static Harmony harmonyInstance;

        void Start() {
            //Register BepInEx Logger source
            BepInEx.Logging.Logger.Sources.Add(patchLogSource);
            patchLogSource.LogInfo("HarmonyPatches Start() ran");
            
            //Initialize Harmony
            harmonyInstance = Harmony.CreateAndPatchAll(typeof(HarmonyPatches));
        }

        void OnDestroy() {
            //Unpatch everything when the patches object is destroyed
            patchLogSource.LogInfo("Reverting all patches of Harmony instance with ID " + harmonyInstance.Id);
            harmonyInstance.UnpatchAll(harmonyInstance.Id);
            harmonyInstance = null;
        }

        //Coroutine to wait, like the game
        //This is neccessary for some reason
        public static IEnumerator AfterFailWait() {
            if (currentState == GameState.NormalMode) {
                patchLogSource.LogInfo("Normal mode FailWait started");
            }
            if (currentState == GameState.InfiniteMode) {
                patchLogSource.LogInfo("Infinite mode FailWait started");
            }

            yield return new WaitForSecondsRealtime(2f);
            currentState = GameState.Menu;
            unpausedState = currentState;
            patchLogSource.LogInfo("FailWait ended, Player is now in menu");
            adVanceRichPresence.SetMenuPresence();
            yield break;
        }

        //Coroutine to wait like the game when unpausing
        public static IEnumerator UnPauseWait() {
            yield return new WaitForSeconds(3f);
            currentState = unpausedState;
            patchLogSource.LogInfo("Game unpaused");
            adVanceRichPresence.SetGameplayPresence(currentState, unpausedState, currentContinent);
            yield break;
        }

        //Coroutine for waiting when the game is connecting to a new country/continent
        public static IEnumerator NormalModeConnectWait() {
            yield return new WaitForSeconds(3f);
            currentState = GameState.NormalMode;
            unpausedState = currentState;
            patchLogSource.LogInfo("Normal mode connection done");
            adVanceRichPresence.SetGameplayPresence(currentState, unpausedState, currentContinent);
            yield break;
        }

        //Same thing as above but with infinite mode
        public static IEnumerator InfiniteModeConnectWait() {
            yield return new WaitForSeconds(0.35f);
            patchLogSource.LogInfo("First Infinite mode connect wait done");
            //Select random continent from base game to display on the rich presence, similar to how the game is messing with the continent text on the connect screen
            //adVanceRichPresence.SetGameplayPresence(currentState, unpausedState, (Continent)UnityEngine.Random.Range(0, 5));
            yield return new WaitForSeconds(0.45f);
            patchLogSource.LogInfo("Second Infinite mode connect wait done");
            //adVanceRichPresence.SetGameplayPresence(currentState, unpausedState, Continent.Europe);
            yield return new WaitForSeconds(0.2f);
            patchLogSource.LogInfo("Third Infinite mode connect wait done");
            //adVanceRichPresence.SetGameplayPresence(currentState, unpausedState, Continent.Saturn);
            yield return new WaitForSeconds(1f);
            currentState = GameState.InfiniteMode;
            unpausedState = currentState;
            adVanceRichPresence.SetGameplayPresence(currentState, unpausedState, currentContinent);
            patchLogSource.LogInfo("Infinite mode connection done");
            yield break;
        }

        /*
         * Harmony patches for various game methods
        */

        #region Normal Mode patches
        //Normal mode
        [HarmonyPatch(typeof(GamePlay), "Fail")]
        [HarmonyPostfix]
        static void OnNormalModeFail() {
            patchLogSource.LogInfo("Player failed");
            currentState = GameState.Failing;
            unpausedState = currentState;
        }

        [HarmonyPatch(typeof(GamePlay), "WaitToConnect")]
        [HarmonyPostfix]
        static void OnNormalModeConnect(string con) {
            patchLogSource.LogInfo("Connecting to " + con);
            currentState = GameState.Connecting;
            unpausedState = currentState;
            switch (con) {
                case "Oceania":
                    currentContinent = Continent.Oceania;
                    break;
                case "Asia":
                    currentContinent = Continent.Asia;
                    break;
                case "Europe":
                    currentContinent = Continent.Europe;
                    break;
                case "The Americas":
                    currentContinent = Continent.America;
                    break;
                case "Africa":
                    currentContinent = Continent.Africa;
                    break;
                case "Antarctica":
                    currentContinent = Continent.Antarctica;
                    break;
                case "Abandoned":
                    currentContinent = Continent.AbandonedStations;
                    break;
            }
            adVanceRichPresence.SetConnectingPresence(currentContinent);
            StaticCoroutine.Start(NormalModeConnectWait());
        }

        [HarmonyPatch(typeof(GamePlay), "FailWait")]
        [HarmonyPostfix]
        static void AfterNormalModeFailWait() {
            //Wait for 2 seconds in real time just like the game does
            //This is necessary because the postfix on this just runs instantly for whatever reason
            StaticCoroutine.Start(AfterFailWait());
        }
        #endregion

        #region Infinite Mode patches
        //Infinite mode
        [HarmonyPatch(typeof(InfiniteModeGamePlay), "Fail")]
        [HarmonyPostfix]
        static void OnInfiniteModeFail() {
            patchLogSource.LogInfo("Player failed (Infinite Mode)");
            currentState = GameState.Failing;
            unpausedState = currentState;
        }

        [HarmonyPatch(typeof(InfiniteModeGamePlay), "ConnectTo")]
        [HarmonyPostfix]
        static void OnInfiniteModeConnect(string continent) {
            patchLogSource.LogInfo("Infinite mode connecting");
            currentState = GameState.Connecting;
            unpausedState = currentState;
            currentContinent = Continent.Saturn;
            adVanceRichPresence.SetGameplayPresence(currentState, unpausedState, Continent.Oceania);
            StaticCoroutine.Start(InfiniteModeConnectWait());
        }

        [HarmonyPatch(typeof(InfiniteModeGamePlay), "FailWait")]
        [HarmonyPostfix]
        static void AfterInfiniteModeFailWait() {
            //Wait for 2 seconds in real time just like the game does
            //This is necessary because the postfix on this just runs instantly for whatever reason
            StaticCoroutine.Start(AfterFailWait());
        }
        #endregion

        #region Pause and unpause patches 
        //Pause and unpause
        [HarmonyPatch(typeof(PauseButton), "DoPause")]
        [HarmonyPostfix]
        static void OnPause() {
            currentState = GameState.Paused;
            patchLogSource.LogInfo("Game paused");
            adVanceRichPresence.SetGameplayPresence(currentState, unpausedState, currentContinent);
        }

        [HarmonyPatch(typeof(PauseButton), "UnPause")]
        [HarmonyPostfix]
        static void OnUnpause() {
            //Wait because the game does that too
            StaticCoroutine.Start(UnPauseWait());
        }
        #endregion

        #region Misc. patches
        //Other harmony patches (Higher FPS cap for example)
        [HarmonyPatch(typeof(Option_FPSCap), "SetFPSCap")]
        [HarmonyPrefix]
        static void PatchFPS(ref int f) {
            f = 999;
            patchLogSource.LogInfo("Patched FPS cap at SetFPSCap");
        }

        #region Fade patches
        [HarmonyPatch(typeof(MeshRenderedColorFade), "Start")]
        [HarmonyPrefix]
        //the amount of arguments scare me but they're neccessary
        static bool PatchMeshFade(MeshRenderedColorFade __instance, ref float ___currentFPS, ref bool ___DoneFading,
                              ref Color ___c, ref Color ___ColorStart, ref float ___diff_r, ref float ___diff_b,
                              ref float ___diff_g, ref float ___diff_a, ref Color ___ColorEnd, ref float ___FadeTime) {
            //Fix the fading by making the fading function think that the game is running at 60FPS.
            //Thanks to OLDTV's spaghetti code, this is neccessary because the fading is tied to the frame rate.
            ___currentFPS = 60f;
            ___DoneFading = false;
            ___c = __instance.gameObject.GetComponent<MeshRenderer>().material.color;
            ___c = ___ColorStart;
            __instance.gameObject.GetComponent<MeshRenderer>().material.color = ___c;
            ___diff_r = (___ColorEnd.r - ___ColorStart.r) / ___FadeTime / ___currentFPS;
            ___diff_b = (___ColorEnd.b - ___ColorStart.b) / ___FadeTime / ___currentFPS;
            ___diff_g = (___ColorEnd.g - ___ColorStart.g) / ___FadeTime / ___currentFPS;
            ___diff_a = (___ColorEnd.a - ___ColorStart.a) / ___FadeTime / ___currentFPS;
            //Prevent original method from running after the prefix
            return false;
        }

        [HarmonyPatch(typeof(TextColourFade), "Start")]
        [HarmonyPrefix]
        static bool PatchTextColourFade(TextColourFade __instance,
                                        ref float ___currentFPS,
                                        ref bool ___DoneFading,
                                        ref Color ___c,
                                        ref Color ___ColorStart,
                                        ref float ___Delay) {
            ___currentFPS = 60f;
            ___DoneFading = false;
            if (___Delay == 0f) {
                ___c = __instance.gameObject.GetComponent<TextMesh>().color;
                ___c = ___ColorStart;
                __instance.gameObject.GetComponent<TextMesh>().color = ___c;
            }
            //Prevent original method from running after the prefix
            return false;
        }

        [HarmonyPatch(typeof(TextFade), "Start")]
        [HarmonyPrefix]
        static bool PatchTextFade(TextFade __instance,
                                  ref float ___currentFPS,
                                  ref float ___OpacityStart,
                                  ref Color ___c) {
            ___currentFPS = 60f;
            ___c = __instance.gameObject.GetComponent<TextMesh>().color;
            ___c.a = ___OpacityStart;
            __instance.gameObject.GetComponent<TextMesh>().color = ___c;
            //Prevent original method from running after the prefix
            return false;
        }

        [HarmonyPatch(typeof(TextFade), "FixedUpdate")]
        [HarmonyPatch(typeof(TextColourFade), "FixedUpdate")]
        [HarmonyPatch(typeof(MeshRenderedColorFade), "FixedUpdate")]
        [HarmonyPrefix]
        static void PatchTextFadeFixedUpdate(ref float ___currentFPS) {
            ___currentFPS = 60f;
        }
        #endregion

        [HarmonyPatch(typeof(ColorRandomizer), "randomColour")]
        [HarmonyPostfix]
        static void OnColorChange(ref string ___colorString) {
            currentColorString = ___colorString;
            patchLogSource.LogDebug("New color is " + ___colorString);
        }

        //Custom Text patch
        [HarmonyPatch(typeof(StartGameButton), "FixedUpdate")]
        [HarmonyPostfix]
        static void PatchMenuText(StartGameButton __instance) {
            if (adVanceBepisMain.configEnableCustomMenuText.Value) __instance.gameObject.GetComponent<TextMesh>().text = adVanceBepisMain.configCustomMenuText.Value;
            if (adVanceBepisMain.configEnableCustomMenuTextSize.Value) __instance.gameObject.GetComponent<TextMesh>().fontSize = adVanceBepisMain.configCustomMenuTextSize.Value;
            //Because the custom text still persists when going to space
            if (FindObjectOfType<StartGameButton>().InfiniteMode) __instance.gameObject.GetComponent<TextMesh>().text = "∞";
            if (FindObjectOfType<StartGameButton>().InfiniteMode) __instance.gameObject.GetComponent<TextMesh>().fontSize = 200;
        }
        #endregion
    }
}
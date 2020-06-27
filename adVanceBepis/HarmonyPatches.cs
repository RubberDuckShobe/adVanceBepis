using System.Collections;
using HarmonyLib;
using UnityEngine;
using BepInEx.Logging;
using static adVanceBepis.OLDTVResources;

namespace adVanceBepis
{
    class HarmonyPatches : MonoBehaviour
    {
        public static ManualLogSource patchLogSource = new ManualLogSource("adVanceBepis Patches");
        static Harmony harmonyInstance;

        void Awake() {
            //Register BepInEx Logger source
            BepInEx.Logging.Logger.Sources.Add(patchLogSource);
            patchLogSource.LogInfo("HarmonyPatches Awake ran");

            //Initialize Harmony
            harmonyInstance = Harmony.CreateAndPatchAll(typeof(HarmonyPatches));
        }

        void OnDestroy() {
            //Unpatch everything when the patches object is destroyed
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
            yield break;
        }

        //Coroutine to wait like the game when unpausing
        public static IEnumerator UnPauseWait() {
            yield return new WaitForSeconds(3f);
            currentState = unpausedState;
            patchLogSource.LogInfo("Game unpaused");
            yield break;
        }

        /*
         * Harmony patches for various game events
         * (for example connecting and failing) 
        */

        #region Harmony Patches
        //Normal mode
        [HarmonyPatch(typeof(GamePlay), "Fail")]
        [HarmonyPostfix]
        static void OnNormalModeFail() {
            patchLogSource.LogInfo("Player failed");
            currentState = GameState.Failing;
        }

        [HarmonyPatch(typeof(GamePlay), "WaitToConnect")]
        [HarmonyPostfix]
        static void OnNormalModeConnect(string con) {
            patchLogSource.LogInfo("Connecting to " + con);
            currentState = GameState.NormalMode;
        }

        [HarmonyPatch(typeof(GamePlay), "FailWait")]
        [HarmonyPostfix]
        static void AfterNormalModeFailWait() {
            //Wait for 2 seconds in real time just like the game does
            //This is necessary because the postfix on this just runs instantly for whatever reason
            StaticCoroutine.Start(AfterFailWait());
        }

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
            currentState = GameState.InfiniteMode;
            unpausedState = currentState;
        }

        [HarmonyPatch(typeof(InfiniteModeGamePlay), "FailWait")]
        [HarmonyPostfix]
        static void AfterInfiniteModeFailWait() {
            //Wait for 2 seconds in real time just like the game does
            //This is necessary because the postfix on this just runs instantly for whatever reason
            StaticCoroutine.Start(AfterFailWait());
        }

        //Pause and unpause
        [HarmonyPatch(typeof(PauseButton), "DoPause")]
        [HarmonyPostfix]
        static void OnPause() {
            currentState = GameState.Paused;
            patchLogSource.LogInfo("Game paused");
        }

        [HarmonyPatch(typeof(PauseButton), "UnPause")]
        [HarmonyPostfix]
        static void OnUnpause() {
            //Wait because the game does that too
            StaticCoroutine.Start(UnPauseWait());
        }

        //Other harmony patches (Higher FPS cap for example)
        [HarmonyPatch(typeof(Option_FPSCap), "SetFPSCap")]
        [HarmonyPrefix]
        static void PatchFPS(ref int f) {
            f = 999;
        }

        #endregion
    }
}
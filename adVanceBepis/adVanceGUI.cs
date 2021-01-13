using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using BepInEx.Logging;
using static adVanceBepis.OLDTVResources;

namespace adVanceBepis
{
    class adVanceGUI : MonoBehaviour
    {
        private bool showSettings = false;
        public Rect miscWindowRect = new Rect(20, 20, 300, 150);
        public Rect statsWindowRect = new Rect(525, 20, 220, 80);
        private ManualLogSource guiLogSource = new ManualLogSource("adVanceBepis GUI");

        void Start() {

        }

        void Update() {

        }

        void OnGUI() {
            //Detect if player pressed O button
            if (Event.current.Equals(Event.KeyboardEvent("O"))) {
                guiLogSource.LogInfo("GUI toggled");
                if (!showSettings) showSettings = true;
                else showSettings = false;
            }

            if (showSettings) {
                // Register the window
                miscWindowRect = GUI.Window(0, miscWindowRect, DrawMiscWindow, "adVanceBepis Misc.");
                statsWindowRect = GUI.Window(1, statsWindowRect, DrawStatsWindow, "Game statistics");
            }
        }

        void DrawMiscWindow(int windowID) {
            // Make a very long rect that is 20 pixels tall.
            // This will make the window be resizable by the top
            // title bar - no matter how wide it gets.
            GUI.DragWindow(new Rect(0, 0, 10000, 20));
            GUI.Label(new Rect(5, 25, 450, 20), "Not much here at the moment!");
            if (GUI.Button(new Rect(5, 50, 100, 20), "Report a bug"))
                Application.OpenURL("https://github.com/RubberDuckShobe/adVanceBepis/issues/new");
        }

        void DrawStatsWindow(int windowID) {
            // Make a very long rect that is 20 pixels tall.
            // This will make the window be resizable by the top
            // title bar - no matter how wide it gets.
            GUI.DragWindow(new Rect(0, 0, 10000, 20));
            GUI.Label(new Rect(5, 25, 450, 20), "Current game state: " + currentState + "\nCurrent continent: " + currentContinent);
        }
    }
}

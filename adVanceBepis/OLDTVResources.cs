using UnityEngine;
using Corale.Colore.Core;
using Color = UnityEngine.Color;
using ColoreColor = Corale.Colore.Core.Color;
using System.Xml.Schema;

namespace adVanceBepis
{
    class OLDTVResources
    {
        //Enum of states the game can be in
        public enum GameState
        {
            Loading,
            Menu,
            NormalMode,
            InfiniteMode,
            Failing,
            Connecting,
            Paused
        }

        /*
         * Enum of continents the game has
         * Antarctica and AbandonedStations are DLC continents
         * Saturn is infinite mode
         * Technically SecondChance also counts as a continent in the game's code, but I'll just pretend like I didn't see that for now
        */
        public enum Continents
        {
            Oceania,
            Asia,
            America,
            Europe,
            Africa,
            Antarctica,
            AbandonedStations,
            Saturn
        }

        public static GameState currentState;
        public static GameState unpausedState;

        //Game colors, exactly like they are in the game.
        public static Color32 red = new Color(1f, 0f, 0f);
        public static Color32 blue = new Color(0f, 0f, 1f);
        public static Color32 green = new Color(0f, 1f, 0f);
        public static Color32 purple = new Color(0.35f, 0f, 0.8f);
        public static Color32 yellow = new Color(0.5f, 0.5f, 0f);
        public static Color32 cyan = new Color(0f, 0.6f, 0.7f);
        public static Color32 pink = new Color(1f, 0.2f, 1f);
        public static Color32 orange = new Color(1f, 0.4f, 0.2f);
    }
}

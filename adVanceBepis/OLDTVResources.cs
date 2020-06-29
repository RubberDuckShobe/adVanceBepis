using UnityEngine;
using Corale.Colore.Core;
using Color = UnityEngine.Color;
using ColoreColor = Corale.Colore.Core.Color;
using System.Xml.Schema;
using System.Collections;

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
         * Antarctica and AbandonedStations are COLDTV DLC continents
         * Saturn is infinite mode
         * Technically SecondChance also counts as a continent in the game's code, but I'll just treat it like it isn't because... why would I?
        */
        public enum Continent
        {
            Oceania,
            Asia,
            Europe,
            America,
            Africa,
            Antarctica,
            AbandonedStations,
            Saturn
        }

//Disable warning CS0162 (unreachable code) on this function
#pragma warning disable CS0162
        public static string ContinentToString(Continent continent) {
            switch (continent) {
                case Continent.Oceania:
                    return "Oceania";
                    break;
                case Continent.Asia:
                    return "Asia";
                    break;
                case Continent.Europe:
                    return "Europe";
                    break;
                case Continent.America:
                    return "America";
                    break;
                case Continent.Africa:
                    return "Africa";
                    break;
                case Continent.Antarctica:
                    return "Antarctica";
                    break;
                case Continent.AbandonedStations:
                    return "Abandoned Stations";
                    break;
                case Continent.Saturn:
                    return "Saturn";
                    break;
                default:
                    return string.Empty;
                    break;
            }
        }
#pragma warning restore CS0162

        public static GameState currentState;
        public static GameState unpausedState;

        public static Continent currentContinent;

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

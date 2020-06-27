using UnityEngine;

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
        public Color Red = new Color(1f, 0f, 0f);
        public Color Blue = new Color(0f, 0f, 1f);
        public Color Green = new Color(0f, 1f, 0f);
        public Color Purple = new Color(0.35f, 0f, 0.8f);
        public Color Yellow = new Color(0.5f, 0.5f, 0f);
        public Color Cyan = new Color(0f, 0.6f, 0.7f);
        public Color Pink = new Color(1f, 0.2f, 1f);
        public Color Orange = new Color(1f, 0.4f, 0.2f);
    }
}

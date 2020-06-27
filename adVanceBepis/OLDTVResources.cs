using System;
using System.Collections.Generic;
using System.Linq;

namespace adVanceBepis
{
    class OLDTVResources
    {
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
    }
}

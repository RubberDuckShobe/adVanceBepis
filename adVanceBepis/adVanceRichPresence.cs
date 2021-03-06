using BepInEx.Logging;
using Corale.Colore.Core;
using System;
using UnityEngine;
using static adVanceBepis.OLDTVResources;
using Steamworks;

namespace adVanceBepis
{
    class adVanceRichPresence : MonoBehaviour
    {
        public static ManualLogSource rpcLogSource = new ManualLogSource("adVanceBepis Rich Presence");
        public static readonly DiscordRpc.RichPresence Presence = new DiscordRpc.RichPresence();
        
        void Awake() {
            rpcLogSource.LogInfo("adVanceRichPresence Awake() ran");

            //Initialize Discord RPC
            //wow
            var handlers = new DiscordRpc.EventHandlers();
            DiscordRpc.Initialize(
                "604588957208150016",
                ref handlers,
                false,
                "643270");

            rpcLogSource.LogInfo("Setting menu Rich Presence");
            SetMenuPresence();
        }

        public static void SetMenuPresence() {
            if (adVanceBepisMain.configEnableRichPresence.Value) {
                Presence.details = "In menu";
                Presence.state = string.Empty;
                Presence.startTimestamp = (long)0;
                Presence.largeImageKey = "oldtv_logo";
                Presence.largeImageText = "OLDTV";
                Presence.smallImageKey = string.Empty;
                Presence.smallImageText = string.Empty;
                DiscordRpc.UpdatePresence(Presence);
                SteamFriends.SetRichPresence("steam_display", Presence.details);
            }
        }

        public static void SetConnectingPresence(Continent continent) {
            if (adVanceBepisMain.configEnableRichPresence.Value) {
                string continentString = ContinentToString(continent);
                Presence.details = $"Connecting to {continentString}...";
                Presence.state = string.Empty;
                Presence.startTimestamp = (long)0;
                Presence.largeImageKey = "oldtv_logo";
                Presence.largeImageText = "OLDTV";
                Presence.smallImageKey = string.Empty;
                Presence.smallImageText = string.Empty;
                DiscordRpc.UpdatePresence(Presence);
                SteamFriends.SetRichPresence("steam_display", Presence.details);
            }
        }

        public static void SetGameplayPresence(GameState gameState, GameState unpausedState, Continent currentContinent) {
            if (adVanceBepisMain.configEnableRichPresence.Value) {
                string continentString = ContinentToString(currentContinent);

                switch (gameState) {
                    case GameState.NormalMode:
                        Presence.details = "Normal mode";
                        Presence.state = "Connected to " + continentString;
                        Presence.startTimestamp = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                        Presence.largeImageKey = "oldtv_logo";
                        Presence.largeImageText = "OLDTV";
                        Presence.smallImageKey = string.Empty;
                        Presence.smallImageText = string.Empty;
                        break;
                    case GameState.InfiniteMode:
                        Presence.details = "Infinite mode";
                        Presence.state = "Connected to Saturn";
                        Presence.startTimestamp = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                        Presence.largeImageKey = "infinity";
                        Presence.largeImageText = "Infinite mode";
                        Presence.smallImageKey = string.Empty;
                        Presence.smallImageText = string.Empty;
                        break;
                    case GameState.Paused:
                        Presence.details = "Game paused";
                        if (unpausedState == GameState.NormalMode) {
                            Presence.state = "Normal mode";
                        }
                        if (unpausedState == GameState.InfiniteMode) {
                            Presence.state = "Infinite mode";
                        }
                        Presence.startTimestamp = (long)0;
                        Presence.largeImageKey = "oldtv_logo";
                        Presence.largeImageText = "OLDTV";
                        Presence.smallImageKey = string.Empty;
                        Presence.smallImageText = string.Empty;
                        break;
                }
                DiscordRpc.UpdatePresence(Presence);
                SteamFriends.SetRichPresence("steam_display", $"{Presence.details}, {Presence.state}");
            }
        }

        void OnApplicationQuit() {
            DiscordRpc.Shutdown();
            SteamFriends.ClearRichPresence();
        }
    }
}
using Corale.Colore.Core;
using UnityEngine;
//Using Corale 5.2.0 because 6.0.0 doesn't work on this .NET framework version or something, idk
using CoraleColor = Corale.Colore.Core.Color;
using static adVanceBepis.OLDTVResources;

namespace adVanceBepis
{
    class adVanceChroma : MonoBehaviour
    {
        //Convert the game text colors to colors usable by Colore
        public static CoraleColor coraleRed = new CoraleColor(OLDTVResources.red.r, OLDTVResources.red.g, OLDTVResources.red.b);
        public static CoraleColor coraleBlue = new CoraleColor(OLDTVResources.blue.r, OLDTVResources.blue.g, OLDTVResources.blue.b);
        public static CoraleColor coraleGreen = new CoraleColor(OLDTVResources.green.r, OLDTVResources.green.g, OLDTVResources.green.b);
        public static CoraleColor coralePurple = new CoraleColor(OLDTVResources.purple.r, OLDTVResources.purple.g, OLDTVResources.purple.b);
        public static CoraleColor coraleYellow = new CoraleColor(OLDTVResources.yellow.r, OLDTVResources.yellow.g, OLDTVResources.yellow.b);
        public static CoraleColor coraleCyan = new CoraleColor(OLDTVResources.cyan.r, OLDTVResources.cyan.g, OLDTVResources.cyan.b);
        public static CoraleColor coralePink = new CoraleColor(OLDTVResources.pink.r, OLDTVResources.pink.g, OLDTVResources.pink.b);
        public static CoraleColor coraleOrange = new CoraleColor(OLDTVResources.orange.r, OLDTVResources.orange.g, OLDTVResources.orange.b);

        //i literally do not know why exactly i did this
        public static void ChromaSetAll(CoraleColor color) {
            Chroma.Instance.SetAll(color);
        }

        void Update() {
            if (currentState == GameState.Failing) {
                ChromaSetAll(CoraleColor.Black);
            }
            if (currentState == GameState.Menu || currentState == GameState.Connecting) {
                ChromaSetAll(CoraleColor.White);
            }

            if (currentState == GameState.NormalMode || currentState == GameState.InfiniteMode)
                switch (currentColorString) {
                    case "Red":
                        ChromaSetAll(coraleRed);
                        break;
                    case "Orange":
                        ChromaSetAll(coraleOrange);
                        break;
                    case "Yellow":
                        ChromaSetAll(coraleYellow);
                        break;
                    case "Green":
                        ChromaSetAll(coraleGreen);
                        break;
                    case "Blue":
                        ChromaSetAll(coraleBlue);
                        break;
                    case "Cyan":
                        ChromaSetAll(coraleCyan);
                        break;
                    case "Purple":
                        ChromaSetAll(coralePurple);
                        break;
                    case "Pink":
                        ChromaSetAll(coralePink);
                        break;
                }
        }
    }
}
# adVanceBepis
![adVanceBepis Logo](https://github.com/RubberDuckShobe/adVanceBepis/blob/master/images/adVanceBepis_transparentbg.png?raw=true)
[adVance](https://github.com/RubberDuckShobe/adVance) fixed, enhanced and ported to [BepInEx](https://github.com/BepInEx/BepInEx).

## Installation
1. Install the x86 version of [BepInEx](https://bepinex.github.io/bepinex_docs/master/articles/user_guide/installation/index.html?tabs=tabid-win)
2. (Optional but recommended) Change the entrypoint type (**not** the function!) in [BepInEx's config file](https://bepinex.github.io/bepinex_docs/master/articles/user_guide/configuration.html) to MonoBehaviour
3. [Download the latest release of adVanceBepis](https://github.com/RubberDuckShobe/adVanceBepis/releases/latest)
4. Copy the .dll files from the release's .zip to BepInEx' plugin folder.
5. Start the game and play!

## Features
- Actually works properly, unlike adVance
- Doesn't use crusty old IPA (ew)
- FPS cap was removed to allow monitors with over 120hz to be used to their full extent (technically it's set to 999 but there are no monitors that crazy fast anyway)
- Razer Chroma integration to allow for the game's color to be shown on the keyboard. (If you have smart lighting in your room and are willing to try adVance's Chroma features with it, feel free to send me a video of it in action on Discord: Rubber Duck Shobe#8332)
- Discord Rich Presence (shows state of game in main menu, when paused and when in-game)

![RPC Image 1](https://github.com/RubberDuckShobe/adVanceBepis/blob/master/images/adVanceRPC_1.png?raw=true) ![RPC Image 2](https://github.com/RubberDuckShobe/adVanceBepis/blob/master/images/adVanceRPC_2.png?raw=true)

- Config for disabling/configuring features (config file located at BepInEx\config in game's folder)
- Custom menu text! (configurable with the config file)
- ~~(Soon™)~~ (Eventually™) Steam Rich Presence

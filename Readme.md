# Unity 3D / C# / Visual Studio - Original game development - 'A Forest' aka 'Grab the cans' (demo)

*November 2019 - Development time: 2 weeks*

> 🔨 Game made with Unity 3D. In this game you walk in a forest and you decided to clean the rubbish to preserve nature. So pick up all the soda  cans in the allotted time. It's an original game, not inspired by a totorial. By the way I used tons of tutorials and websites to help during the developpement.

* * *

## 1. Installation

Download the zip with the game [here](https://xxx) and unzip it on your computer. Then launch xxx.exe.

Or watch the gameplay video on [here](https://xxx).

Some scripts used are in the [script folder](https://github.com/Raigyo/unity-3d-game-forest/tree/master/scripts) on this repository.

*-------*

## 2. About

This game was made to show my skills using Unity 3D and what's it's possible to do with it. This game has a peaceful atmosphere and educational virtues. It's usually the kind of game that inspires institutionnal or non-profit organizations as clients.

It's a desktop version. It would be possible to make that kind of game for mobiles or web browsers (WegGL), but the quality should be downgraded.

This game has been tested again and again and again... But if you have advises or if you see bugs/problems, please contact me using the issue button on this repository.

### 2.1. How to play

#### 2.1.1 Playing game (Game mode)

Walk in the forest and grap the 30 cans within 10 minutes.

Use the zqsd/arrows key (see Settings about that part) to move and your mouse to look around. You can help yoursel with the minimap that displays can that are near you.

If you win or fail, a menu is displayed asking you if you want to try again or go back to menu.

Other features:

- F2: show/hide Minimap

- Escape/P: show/hide pause menu with continue or back to menu.

#### 2.1.2 Walking mode (Free mode)

No cans to pickup or timer, just walk and discover the landscape using the same keys than in game mode.

- F2: show/hide pannel that allows you to choose the time of the day (uses Enviro demo pannel)

- Escape/P: show/hide pause menu with continue or back to menu.

#### 2.1.3 Menu: Settings

Not implemented yet. It would allow to change keys. Numerous tests have been made to create a remapping/rebinding feature in game, but without finding a solution.

It's a native feature that lacks in Unity for years. I will try later using the asset [Rewired](https://assetstore.unity.com/packages/tools/utilities/rewired-21676) that is more powerful thant the input management of Unity, even the new one in preview.

*-------*

## 3. Techniques / Assets / Scripts

### 3.1 Intro scene (scene name: intro)

#### 3.1.1 Menu

You can select between game mode or walking mode.

The game selected is managed by Scriptable Objects (that can be sused as 'global' variables between scenes).

The two versions of the game a just one scene with some GameObjects displayed or not according to the boolean value in the Scriptable Objects. This value is reset each time we comme back to the ontro scene.

#### 3.1.2 Cinematic

This scene has been made using the Unity additionnal package [Cinemachine](https://unity.com/fr/unity/features/editor/art-and-design/cinemachine).

Cinemachine allows to use several virtual cameras to make cut-scenes, trailers and so on. In this intro I use the 'dolly track' feature.

The lanscape is the same the the one used in the game. The result is displayed as an animation made with the components *Animator* and *Animation*.

To make quality cinematics can be a long process... Here it's far to be perfect, you could see some speed variation during the cinematic...

#### 3.1.3 Canvas

A basic canvas is used as overlay to display buttons and informations, with some scripting and EventSystem components, including tooltips.

There are also some canvas in the forest/game scene.

#### 3.1.4 Loader

The muscic is fade out with a coroutine then another coroutine is launched to display the loader asynchronously. The same loader is used on the two scenes. There aren't specic script for this scene, it uses the same than in the next so they will be detailled there.

### 3.2 Game scene (scene name: forest)

#### 3.2.1 The terrain

This game uses [Forest Environment - Dynamic Nature](https://assetstore.unity.com/packages/3d/vegetation/forest-environment-dynamic-nature-150668) from [NatureManufacture](https://assetstore.unity.com/publishers/6887).

And yes, it uses the demo provided with the asset. To speak franckly, this game is just for demonstration purpose. To make that kind of map takes about two weeks for a professionnal level designer (according to NatureManufacture). Moreover, everything already uses LOD (Level of Detail according the position of the camera), that increases performances.

BTW I've used the new Terrain Tools package (preview) to make some changes in the terrain. You should not be able able to fall from it now...

The terrain is surrounded by moutain and there is a slope detection to avoid player falling outside the map.

By the way, I made like all developers, I added... invisible walls! (just in case).

#### 3.2.1 Minimap

The game use a minimap in the GTA style to display the rubbish to collect. The minimap is made with a ortographic camera above the player.

In the properties of this camera I target a render texture previously made. So I can use this texture in the overlay in a raw image with the content displayed with the camera in it as texture.

To improve performances, this camera uses occlusion culling (so what is not in front of the camera is not calculated by the computer) and displays some layers only in culling mask.

Markers are GameObjects in the prefab of cans. The main camera displays the layer with the rubbish and the minimap only these markers.

#### 3.2.2 Script: [HitBehaviour.cs](scripts/HitBehaviour.cs)

//HitBehaviour: Raycast on GO that can be picked-up / Highlight GO

Script that uses raycast to find if we hit an object on the 'rubbish' layer (cans). It calls an other script when we click on an grabbable object.

It's also used to change the material on grabble object when the raycast hits it.

It's also in this script that I cound the cans grabbed. I can define if all the cans have been picked up. It could be managed in another script, but It was  the easiest way for me to avoid to increase the score if the player would click several times... The rubbish are children of a GameObject (used like a kind of folder). I don't have to specify how many GO there are in it because I count them in the script. BTW It's divided by 2 because each prefab has two GO, the rubbish and its icon (for the minimap)...

#### 3.2.3 Script: [GrabObject.cs](scripts/GrabObject.cs)

// GrabObject: animation / sound / set the object unactive

Script used to grap the object following a guide put on the FPSPlayer component.

When the mouse button is released the script set inactive the icon on minimap immediately (otherwise it moves on the minimap following the can asset). The can disapear after a few time using a coroutine and the IEnumerator method.

It's also in that script that the sound is played.

#### 3.2.4 Script: [ScoreAndTimeController.cs](scripts/ScoreAndTimeController.cs)

//ScoreAndTimeController : counter and score management / detect if lost or win**

Coundown that converts deltaTime in minutes / seconds. Delta time can be specified in the inspector.

Score displaying according to the total of GO contained in the rubbish GO.

It's also that calss that controls if the player has won or lost.

#### 3.2.3 Script: [GameController.cs](scripts/GameController.cs)

//GameController : counter and score management / display messages and pannels /show hide some elements

This script manage many things: display or hide elements according to the situation.

- Manage GUI buttons on intro

- Displays elements that are Game mode / Free mode specific

- Displays pannels and messages when we lost or win

- Manage loading scene and progress bar for the two scenes

- Manage the launch of sound fade in / fade out

#### 3.2.4 Script: [ActionKeysManager.cs](scripts/ActionKeysManager.cs)

//ActionKeysManager: Manage the keyboard shortcuts ingame and what's displayed / hidden then**

Used for the pause menu, the displaying of the minimap or the time selector pannel.

#### 3.2.5 Script: [SlopeDetection.cs](scripts/SlopeDetection.cs)

//SlopeDetection: Finds the slope/grade/incline angle of ground underneath a CharacterController**

I used and modified this [script](https://gist.github.com/jawinn/f466b237c0cdc5f92d96) to detect the slope using a raycast behind the player. so i can set the high of jump according to the slope/ incline angle of ground underneath.

#### 3.2.6 [ToolTips.cs](scripts/ToolTips.cs)

//ToolTips: displays tooltip on menu when a button is hovered**

Script to display tooltips. The content (title+content) is managed in a component on the GO.

#### 3.2.6 Footstep sounds

I use the asset [Dynamic Footsteps for first person games](https://assetstore.unity.com/packages/tools/dynamic-footsteps-for-first-person-games-85052) to manage footstep sounds. Not expensive and very useful.

With that asset you can define sounds played randomly for some layers only. BTW I had to adjust or create some colliders (primitive / mesh) on some GameObjects (water and bridges). According to me it's more easy to add colliders on a layer than define texture that will interact with a raycast  (textures can be numerous or be changed by the designer / raycast could... not work on every cases) . I've tried several solutions, this one convinced me.

#### 3.2.7 Time of the day

During the game, the time goes by...

I use the following asset for that: [Hendrik Haupt - Enviro - Sky and Weather](https://assetstore.unity.com/packages/tools/particles-effects/enviro-sky-and-weather-33963)

### 3.3 Optimization

Scripts avoid to use *GameObject.FindGameObjectsWithTag* or *GameObject.Find* methods but use public variables that can be accessed from the inspector.

TextMeshPro is used for text that's more efficient than regular text.

There is occlusion culling on the FPS camera (the computer calculate only what's shown by the camera) and on the camera used on minimap.

A lot of asset in hierarchy use LOD (Level of Detail) that *reduces the load on the hardware and improves the rendering performance*.

### 3.4 Sound design

Excepted in the profesionnal gaming industry, I've noticed that the sound design is often neglected.

However, it's very important in what you feel when you play game, watch movies, series and so on... Like in the real life, real silence is quite rare... The sound and music, it's 90% of the atmosphere...

So I've bought this [soundtrack](https://freetousesounds.bandcamp.com/track/forest-sounds-germany-crows-birds-soft-wind-royalty-free) and paid attention of the sound in this game.

This royalty free song from [Whitesand](https://www.youtube.com/watch?v=_hSyRMyqFOM) is used for the intro.

*-------*

## 4. To do

- [] Menu to allow to change keys binding (querty/azerty - wasd/zqsd)

*-------*

## 5. Music & Sounds credits

- Whitesand - Drops (Royalty and Copyright Free) - [Youtube](https://www.youtube.com/watch?v=_hSyRMyqFOM) / [Facebook](https://www.facebook.com/martynlaur) / [Twitter](https://twitter.com/MartynLaur) / [Instagram](https://www.instagram.com/martynas_lau/) / [SoundCloud](https://soundcloud.com/martynaslau)

- Forest Sounds Germany ! Crows, Birds & Soft Wind! Royalty Free - [freetousesounds](https://freetousesounds.bandcamp.com/track/forest-sounds-germany-crows-birds-soft-wind-royalty-free)

- [Free SFX / Free Sound Effects](https://www.freesfx.co.uk/Default.aspx)

- [Zapsplat / Free sound effects & royalty free music](https://www.zapsplat.com/)

*-------*

## 6. Assets used

- [NatureManufacture  - Forest Environment - Dynamic Nature](https://assetstore.unity.com/packages/3d/vegetation/forest-environment-dynamic-nature-150668)

- [John's Art - Dynamic Footsteps for first person games](https://assetstore.unity.com/packages/tools/dynamic-footsteps-for-first-person-games-85052)

- [Unluck Software - Bird Flock Bundle](https://assetstore.unity.com/packages/3d/characters/animals/bird-flock-bundle-25576)

- [Unluck Software - Firefly Particles](https://assetstore.unity.com/packages/vfx/particles/environment/firefly-particles-89299)

- [YGS Assets - Cola Can](https://assetstore.unity.com/packages/3d/cola-can-96659)

- [Unity Technologies - 3D Game Kit](https://assetstore.unity.com/packages/templates/tutorials/3d-game-kit-115747)

- [Hendrik Haupt - Enviro - Sky and Weather](https://assetstore.unity.com/packages/tools/particles-effects/enviro-sky-and-weather-33963)

*-------*

## 7. Useful links & Greetings

- [Unity Learn](https://learn.unity.com/)

- [Brackeys Game Dev Tutorials](https://www.youtube.com/channel/UCYbK_tjZ2OrIZFBvU6CCMiA)

- [Jason Weimann - Unity 3D College](https://www.youtube.com/channel/UCX_b3NNQN5bzExm-22-NVVg)

- [Raywenderlich](https://www.raywenderlich.com/)

- [IMERSITY](https://www.youtube.com/channel/UCCCf8Z1iY3yXQUxcnarA0Ag)

- [Epitome](https://www.youtube.com/channel/UCsaXQNLxeHvwJdDUrICGufA)

- [Sylvain - Créateur 3D](https://www.youtube.com/channel/UC8BM2xQlXcK4Vt3OqfOmj9g)

- [Unity Pour les nuls](https://www.youtube.com/channel/UCuU8cONIgZ182KheI1s6HqQ)

- [inScope Studios](https://www.youtube.com/channel/UCyVsCcTte38YC9CxJtw3hBQ)

- [Info Gamer](https://www.youtube.com/channel/UCyoayn_uVt2I55ZCUuBVRcQ)

- [Learn Everything Fast](https://www.youtube.com/channel/UCG5XadFg6icC2TcF0I5DIig)

- [DitzelGames](https://www.youtube.com/channel/UCdedu-nAwMACE5WbVcmp3Bg)

- [TUTO UNITY FR](https://www.youtube.com/channel/UCJRwb5W4ZzG43J5_dViL6Fw)

*-------*

## 8.Contact (Github / Linked In)

- [My Github](https://github.com/Raigyo)
- [My LinkedIn](https://www.linkedin.com/in/vincent-chilot/)

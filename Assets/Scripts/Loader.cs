using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader {

    public enum Scene {
        MainMenuScene,
        LoadingScene,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5
    }

    public static Scene targetScene;

    public static void Load(Scene targetScene) {
        Loader.targetScene = targetScene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    internal static void LoaderCallback() {
        SceneManager.LoadScene(targetScene.ToString());
    }

    public static String GetLevelName(Scene scene) {
        switch (scene) {
            case Scene.MainMenuScene:
                return "MAIN MENU";
            case Scene.Level1:
                return "EVERYDAY LIFE";
            case Scene.Level2:
                return "IN THE MIRROR";
            case Scene.Level3:
                return "HIGH IN THE CLOUDS";
            case Scene.Level4:
                return "MAZE";
            case Scene.Level5:
                return "ICE LAND";
            default:
                return "Unknown";
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static int CurrLevel { get; set; } = 13;
    public static int max_level = 32;
    public static bool[] completedLevels = new bool[Constants.N_LEVELS];

    private static bool tutorialShown = false;
    public static string getNextScene() {

        if (tutorialShown) {
            tutorialShown = false;
            return "LevelScene";
	    }
        int offset = 0;
        tutorialShown = true;
        switch (CurrLevel - offset) {
            case 3:
                return "DialogC-01";
            case 6:
                return "DialogC-02";
            case 9:
                return "DialogC-02A";
            case 14:
                return "DialogC-03B";
            case 18:
                return "DialogC-03A";
            case 22:
                return "DialogC-04";
            default:
                tutorialShown = false;
                return "LevelScene";
        }
    }


    //public Scen
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals 
{
    public static int currentLevel = 1;
    public static int currentLevelIndex = 0, LevelCount;
    public static long moneyAmount = 0;

    public static float currentSpawnSpeed = 0; // Upgradeable

    public static bool spawnActive = false;
    public static float clickSpawnSpeed = 1f;








    public static float currrentAnimSpeed = 0; // Upgradeable


    public static int coinPerBall = 1; // Upgradeable
    //public static int brickPerHit = 1; // Upgradeable
    public static float coolDownSpeed = 0; // Upgradeable
    public static float healthDownSpeed = 10; // Upgradeable
    //public static float clickAnimSpeed = 2; // Upgradeable


    public static int ballLevel = 0;
    public static int staminaLevel = 0;
    public static int clickSpeedLevel = 0;


    public static int currentMergeCost = 0;
    public static int currentMergeLevel = 0;
    public static bool nowMergeable = false;
}

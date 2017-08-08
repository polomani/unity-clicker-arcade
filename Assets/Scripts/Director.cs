using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Director {

    private const int ENEMIES_TO_KILL = 5;
    private static int enemiesSummoned = 0;
    private static int enemiesKilled = 0;
    private static int score = 0;
    private static bool windowOpened;
    private static bool paused;

    public static bool Paused
    {
        get { return Director.paused; }
        set { Director.paused = value; }
    }

    public static bool WindowOpened
    {
        get { return Director.windowOpened; }
        set { Director.windowOpened = value; }
    }

    private static HealthBarBehaviour healthBar;

    public static HealthBarBehaviour HealthBar
    {
        get { return healthBar; }
        set { healthBar = value; }
    }


    public static int Score
    {
        get { return Director.score; }
        set { Director.score = value; }
    }

    public static int EnemiesSummoned
    {
        get { return Director.enemiesSummoned; }
        set { Director.enemiesSummoned = value; }
    }

    private static SpawnsBehaviour spawns;

    public static SpawnsBehaviour Spawns
    {
        get { return spawns; }
        set { spawns = value; }
    }
    private static bool bossMode = false;

    public static bool BossMode {
        get
        {
            return bossMode;
        }
        set
        {
            bossMode = value;
        }
    }

    private static HeroBehavior hero;

    public static HeroBehavior Hero
    {
        get { return hero; }
        set { hero = value; }
    }

    public static void BossKilled()
    {
        bossMode = false;
        enemiesKilled = 0;
        enemiesSummoned = 0;
        spawns.StartCoroutine("PrepareForWave");
        healthBar.Hide();
    }

    public static void HeroDied()
    {
        enemiesKilled = 0;
        enemiesSummoned = 0;
        score = 0;
    }

    public static void EnemyKilled()
    {
        enemiesKilled++;
        if (!bossMode && enemiesKilled >= ENEMIES_TO_KILL)
        {
            bossMode = true;
            spawns.SummonBoss();
            healthBar.Show();
            spawns.StopCoroutine("SummonEnemy");
        }
    }

    public static bool NeedMoreEnemies()
    {
        return bossMode || enemiesSummoned < ENEMIES_TO_KILL;
    }

    public static bool canShoot()
    {
        return !windowOpened;
    }
}

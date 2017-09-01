using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class Director {

    private const int ENEMIES_TO_KILL = 5;
    private static int enemiesSummoned = 0;
    private static int enemiesKilled = 0;
    private static int score = 0;
    private static bool windowOpened;
    private static bool paused;
    private static bool bossMode = false;

    private static UIBehavior ui;
    private static Repository repository;

    private static BossBehavior boss;
    private static SpawnsBehaviour spawns;
    private static HeroBehavior hero;

    public static Repository Repository
    {
        get { return Director.repository; }
        set { Director.repository = value; }
    }

    public static UIBehavior UI
    {
        get { return ui; }
        set { ui = value; }
    }

    public static BossBehavior Boss
    {
        get { return Director.boss; }
        set { Director.boss = value; }
    }

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

    public static int Score
    {
        get { return score; }
        set { score = value; }
    }

    public static int EnemiesSummoned
    {
        get { return Director.enemiesSummoned; }
        set { Director.enemiesSummoned = value; }
    } 

    public static SpawnsBehaviour Spawns
    {
        get { return spawns; }
        set { spawns = value; }
    } 

    public static bool BossMode {
        get { return bossMode; }
        set { bossMode = value; }
    }

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
        UI.healthBar.Hide();
        Repository.Data.Coins += 10;
        UI.UpdateCoinsText();
    }

    public static void HeroDied()
    {
        Pause();
        UI.gameOverPanel.Show();
    }

    public static void EnemyKilled()
    {
        enemiesKilled++;
        if (!bossMode && enemiesKilled >= ENEMIES_TO_KILL)
        {
            bossMode = true;
            spawns.SummonBoss();
            UI.healthBar.Show();
            spawns.StopCoroutine("SummonEnemy");
        }
    }

    public static bool NeedMoreEnemies()
    {
        return bossMode || enemiesSummoned < ENEMIES_TO_KILL;
    }

    public static bool CanShoot()
    {
        return !windowOpened && !paused;
    }

    public static void Restart()
    {
        score = enemiesKilled = enemiesSummoned = 0;
        bossMode = windowOpened = false;
        
        DestroyAllEnemiesAndBoss();
        UI.healthBar.Hide();
        Unpause();
        BossBehavior.totalHP = 80;
        if (spawns) spawns.Init();
    }

    private static void DestroyAllEnemiesAndBoss()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag ("Enemy").
                                   Concat(GameObject.FindGameObjectsWithTag ("Boss")).
                                   Concat(GameObject.FindGameObjectsWithTag ("Bullet")).ToArray();

        for (var i = 0; i < gameObjects.Length; i++)
        {
            GameObject.Destroy(gameObjects[i]);
        }
    }

    public static void Pause()
    {
        Paused = true;
        Object[] objects = GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects)
        {
            go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
        }
        Time.timeScale = 0;
    }

    public static void Unpause()
    {
        Paused = false;
        Object[] objects = GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects)
        {
            go.SendMessage("OnResumeGame", SendMessageOptions.DontRequireReceiver);
        }
        Time.timeScale = 1;
    }
}

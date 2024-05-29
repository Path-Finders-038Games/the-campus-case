using UnityEngine;
using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEditor;
using Minigames.Hacking_Minigame;

public class BulletTest
{
    private GameObject hackingGame;
    public GameController controller;

    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene("Scenes/TestingScene");
    }

    public IEnumerator WaitForSceneLoad()
    {
        while (SceneManager.GetActiveScene().name != "TestingScene")
        {
            yield return null;
        }

        SetupLoad();
    }

    private void SetupLoad()
    {
        string prefabPath = "Assets/Art/Hacking Minigame.prefab";

        // Load the prefab
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

        hackingGame = Object.Instantiate(prefab);

        controller = hackingGame.GetComponentInChildren<GameController>();
        Assert.IsNotNull(controller, "GameController component not found on the instantiated prefab.");
    }

    [TearDown]
    public void TearDown()
    {
        if (hackingGame != null)
        {
            Object.Destroy(hackingGame);
        }
    }

    [UnityTest]
    public IEnumerator TestDamage()
    {
        yield return WaitForSceneLoad();
        yield return new WaitForSeconds(1); // Ensure setup is complete
        float timer = 0;
        bool hit = false;
        controller.HideLocationFile();
        controller.SpawnAmount = 1;
        controller.EnemyAlive = 1;
        controller.Health = 3;
        for (int i = 0; i < controller.Lanes.Length; i++)
        {
            controller.Lanes[i] = controller.Lanes[0];
        }
        // Check initial condition
        Assert.AreEqual(3, controller.Health);

        while (timer < 15 && !hit)
        {
            yield return null; // Wait for a frame
            timer += Time.deltaTime; // Increment the timer by the time passed since the last frame

            if (controller.Health < 3)
            {
                hit = true;
            }
        }

        Assert.AreEqual(true, hit);
    }

    [UnityTest]
    public IEnumerator TestShot()
    {
        yield return WaitForSceneLoad();
        yield return new WaitForSeconds(1); // Ensure setup is complete
        float timer = 0;
        bool hit = false;
        controller.SpawnAmount = 1;
        controller.EnemyAlive = 1;
        controller.HideLocationFile();
        for (int i = 0; i < controller.Lanes.Length; i++)
        {
            controller.Lanes[i] = controller.Lanes[controller.CurrentLane];
        }
        // Check initial condition
        Assert.AreEqual(false, hit);
        Assert.AreEqual(1, controller.EnemyAlive);

        while (timer < 10 && controller.EnemyAlive > 0)
        {
            yield return null; // Wait for a frame
            timer += Time.deltaTime; // Increment the timer by the time passed since the last frame

            if (controller.EnemyAlive == 0)
            {
                hit = true;
            }
        }

        Assert.AreEqual(true, hit);
    }
}
using UnityEngine;
using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEditor;
using Minigames.Hacking_Minigame;
using UnityEngine.UI;

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
        yield return new WaitForSeconds(1);
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
        yield return new WaitForSeconds(1);
        Assert.AreEqual(true, hit);
    }

    [UnityTest]
    public IEnumerator TestLost()
    {

        yield return WaitForSceneLoad();
        yield return new WaitForSeconds(1); // Ensure setup is complete
        float timer = 0;
        Transform canvastransform = hackingGame.transform.Find("Canvas");
        GameObject canvas = canvastransform.gameObject;
        Transform losstransform;
        GameObject lost = null;
        controller.SpawnAmount = 1;
        controller.EnemyAlive = 1;
        controller.Health = 1;
        for (int i = 0; i < controller.Lanes.Length; i++)
        {
            controller.Lanes[i] = controller.Lanes[0];
        }
        controller.HideLocationFile();
        // Check initial condition
        Assert.IsTrue(controller.PlayingGame);

        while (timer < 15 && controller.EnemyAlive > 0)
        {
            yield return null; // Wait for a frame
            timer += Time.deltaTime; // Increment the timer by the time passed since the last frame

            if (controller.EnemyAlive == 0)
            {
                yield return new WaitForSeconds(1);
                losstransform = canvas.transform.Find("LoseText(Clone)");
                lost = losstransform.gameObject;
                Debug.Log(lost.name);
            }
        }
        yield return new WaitForSeconds(1);
        Assert.IsTrue(lost != null);
    }

    [UnityTest]
    public IEnumerator TestWon()
    {

        yield return WaitForSceneLoad();
        yield return new WaitForSeconds(1); // Ensure setup is complete
        float timer = 0;
        Transform canvastransform = hackingGame.transform.Find("Canvas");
        GameObject canvas = canvastransform.gameObject;
        Debug.Log(canvas.name);
        Transform wontransform;
        GameObject won = null;
        controller.SpawnAmount = 1;
        controller.EnemyAlive = 1;
        for (int i = 0; i < controller.Lanes.Length; i++)
        {
            controller.Lanes[i] = controller.Lanes[controller.CurrentLane];
        }
        controller.HideLocationFile();
        // Check initial condition
        Assert.IsTrue(controller.PlayingGame);

        while (timer < 10 && controller.EnemyAlive > 0)
        {
            yield return null; // Wait for a frame
            timer += Time.deltaTime; // Increment the timer by the time passed since the last frame

            if (controller.EnemyAlive == 0)
            {
                yield return new WaitForSeconds(1);
                wontransform = canvas.transform.Find("WinText(Clone)");
                won = wontransform.gameObject;
            }
        }
        yield return new WaitForSeconds(1);
        Assert.IsTrue(won != null);
    }

    [UnityTest]
    public IEnumerator CorrectVallues()
    {
        yield return WaitForSceneLoad();
        yield return new WaitForSeconds(1); // Ensure setup is complete
        Bullet bullet = controller.CombatControllerProperty.Bullet_Prefab.GetComponent<Bullet>();
        Enemy enemy_weak = controller.WeakEnemy.GetComponent<Enemy>();
        Enemy enemy_strong = controller.StrongEnemy.GetComponent<Enemy>();

        yield return null;

        float bullet_speed = bullet.Bulletspeed;
        float bullet_distance = bullet.Distance;

        float weak_speed = enemy_weak.Speed;
        float weak_health = enemy_weak.Health;

        float strong_speed = enemy_strong.Speed;
        float strong_health = enemy_strong.Health;

        Assert.That(bullet_speed > 700 && bullet_speed < 1000);
        Assert.That(bullet_distance > 2000 && bullet_distance < 3000);

        Assert.That(weak_speed > 300 && weak_speed < 500);
        Assert.That(weak_health == 1);

        Assert.That(strong_speed > 100 && strong_speed < 300);
        Assert.That(strong_health == 2);

    }

    [UnityTest]
    public IEnumerator HealthbarTest()
    {
        yield return WaitForSceneLoad();
        yield return new WaitForSeconds(1); // Ensure setup is complete

        Transform canvastransform = hackingGame.transform.Find("Canvas");
        GameObject canvas = canvastransform.gameObject;

        Transform healthbartransform = canvas.transform.Find("HackingBase");
        GameObject healthbar = healthbartransform.gameObject;

        Transform healthtransform = healthbar.transform.Find("Health");
        GameObject health = healthtransform.gameObject;

        Image image = health.GetComponent<Image>();
        yield return null;
        controller.Health = 3;
        Assert.IsTrue(image.sprite.name == "HackMinigameSprites_9");
        yield return null;
        controller.Health = 2;
        Assert.IsTrue(image.sprite.name == "HackMinigameSprites_10");
        yield return null;
        controller.Health = 1;
        Assert.IsTrue(image.sprite.name == "HackMinigameSprites_11");
    }
}
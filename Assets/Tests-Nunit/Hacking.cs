using System.Collections;
using System.Collections.Generic;
using Minigames.Hacking_Minigame;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class Hacking
{
    public GameController Controller;
    [SetUp]
    public void Setup()
    {

    }
    // A Test behaves as an ordinary method
    [SetUp]
    public void setup()
    {
        GameObject _instantiatedPrefab;
        string prefabPath = "Assets/Art/Hacking Minigame.prefab";

        // Load the prefab
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

        // Instantiate the prefab as a game object
        _instantiatedPrefab = Object.Instantiate(prefab);

        // Get all children of the instantiated prefab
        Controller = _instantiatedPrefab.GetComponentInChildren<GameController>();
    }


    [Test]
    public void Test()
    {
        Assert.IsTrue(Controller.SpawnAmount > 0);
    }

    [Test]
    public void Test1()
    {
        Assert.IsTrue(Controller.CombatControllerProperty != null);
    }

    [Test]
    public void Test2()
    {
        Assert.IsTrue(Controller.HealthControllerProperty != null);
    }
}

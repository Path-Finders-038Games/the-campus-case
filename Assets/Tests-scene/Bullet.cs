using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEditor;

//TODO
public class BulletTest
{
    
    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene("Scenes/TestingScene");

        string prefabPath = "Assets/Art/Hacking Minigame.prefab";

        // Load the prefab
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

        HackingGame = Object.Instantiate(prefab);
    }
    private GameObject HackingGame;
    // A Test behaves as an ordinary method
    [Test]
    public void NewTestScriptSimplePasses()
    {
        
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}

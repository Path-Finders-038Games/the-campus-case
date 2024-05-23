using System.Collections;
using System.Collections.Generic;
using Minigames.Hacking_Minigame;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EnemyTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void Variables()
    {
        Enemy enemy = new Enemy();
        enemy.Speed = 1;
        enemy.Health = 1;
        Assert.That(enemy.Speed, Is.EqualTo(1));
        Assert.That(enemy.Health, Is.EqualTo(1));
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [Test]
    public void IncorrectVariables()
    {
        Enemy enemy = new Enemy();
        enemy.Speed = 1;
        enemy.Health = 1;
        enemy.Speed = 0;
        enemy.Health = -6;
        Assert.That(enemy.Speed, Is.EqualTo(1));
        Assert.That(enemy.Health, Is.EqualTo(1));
    }
}

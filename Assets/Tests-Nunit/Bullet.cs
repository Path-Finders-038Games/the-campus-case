using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BulletTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void BulletVariables()
    {
        Bullet bullet = new Bullet();
        bullet.Bulletspeed = 1;
        bullet.Distance = 1;
        Assert.That(bullet.Bulletspeed, Is.EqualTo(1));
        Assert.That(bullet.Distance, Is.EqualTo(1));
    }

    [Test]
    public void IncorrectVariables()
    {
        Bullet bullet = new Bullet();
        bullet.Bulletspeed = 1;
        bullet.Distance = 1;
        bullet.Bulletspeed = -5;
        bullet.Distance = 0;
        Assert.That(bullet.Bulletspeed, Is.EqualTo(1));
        Assert.That(bullet.Distance, Is.EqualTo(1));
    }
}

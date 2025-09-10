using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assets.Scripts.Dinosaur;

public class DinoFunction
{
    GameObject dinosaur;
    // A Test behaves as an ordinary method
    [SetUp]
    public void SetUp()
    {

        dinosaur = new();
        dinosaur.AddComponent<DinoController>();
        dinosaur.AddComponent<DinoMovement>();
        dinosaur.AddComponent<DinoSensors>();
    }
    [Test]
    public void StateWaitsTillAsyncDone()
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


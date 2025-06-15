using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

// Dummy replacement for JumpScareScript
public class DummyJumpScareScript : JumpScareScript
{
    public bool shown = false;

    public override void Start() {}

    public override void ShowJumpscare()
    {
        shown = true;
    }
}

public class JumpscareTest
{
    private GameObject player;
    private GameObject yandere;
    private JumpscareTrigger trigger;

    private DummyJumpScareScript dummy;

    [SetUp]
    public void SetUp()
    {
        // Create player with Rigidbody and Collider
        player = new GameObject("Player");
        player.tag = "Player";
        player.AddComponent<BoxCollider2D>();
        player.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        player.transform.position = Vector2.zero;

        // Create yandere with Collider and Rigidbody
        yandere = new GameObject("Yandere");
        yandere.AddComponent<BoxCollider2D>().isTrigger = true;
        yandere.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        trigger = yandere.AddComponent<JumpscareTrigger>();

        // Add dummy jumpscare manager and inject via reflection
        dummy = yandere.AddComponent<DummyJumpScareScript>();

        trigger.InjectJumpScareManager(dummy);


        yandere.transform.position = Vector2.right;
    }

    [UnityTest]
    public IEnumerator PlayerTriggersJumpscareOnCollision()
    {
        player.transform.position = Vector2.right;

        yield return new WaitForFixedUpdate();

        Assert.IsTrue(trigger.wasTriggered, "Jumpscare is triggered.");
        Assert.IsTrue(dummy.shown, "Jumpscare is shown");
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(player);
        Object.Destroy(yandere);
    }
}

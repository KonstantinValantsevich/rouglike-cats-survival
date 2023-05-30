using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class ModelChanger : MonoBehaviour, IAgeChangeable
{
    public SpriteRenderer bodyRenderer;
    public Transform rotator;
    [FormerlySerializedAs("bodies")]
    public List<Body> bodyList;
    public List<Body> bodyListModern;
    public List<Body> bodyListFuture;
    private Dictionary<BodyType, Sprite> bodies;

    private void Start()
    {
        bodies = bodyList.ToDictionary(v => v.type, v => v.sprite);
    }

    private void Update()
    {
        bool Between(float value, float a, float b)
        {
            if (a > 180 && b < 180) {
                return value >= a || value < b;
            }

            return (value >= a && value < b);
        }

        var rotation = rotator.localEulerAngles.z;

        if (Between(rotation, 247.5F, 292.5F)) {
            bodyRenderer.sprite = bodies[BodyType.Front];
            bodyRenderer.flipX = false;
        }
        else if (Between(rotation, 292.5F, 0)) {
            bodyRenderer.sprite = bodies[BodyType.Profile];
            bodyRenderer.flipX = false;
        }
        else if (Between(rotation, 0, 67.5f)) {
            bodyRenderer.sprite = bodies[BodyType.HalfBack];
            bodyRenderer.flipX = false;
        }
        else if (Between(rotation, 67.5f, 112.5f)) {
            bodyRenderer.sprite = bodies[BodyType.Behind];
            bodyRenderer.flipX = false;
        }
        else if (Between(rotation, 112.5f, 180F)) {
            bodyRenderer.sprite = bodies[BodyType.HalfBack];
            bodyRenderer.flipX = true;
        }
        else if (Between(rotation, 180F, 247.5F)) {
            bodyRenderer.sprite = bodies[BodyType.Profile];
            bodyRenderer.flipX = true;
        }
    }

    public void ChangeAge(AgeType age)
    {
        bodies = age switch {
            AgeType.Ancient => bodyList.ToDictionary(v => v.type, v => v.sprite),
            AgeType.Modern => bodyListModern.ToDictionary(v => v.type, v => v.sprite),
            AgeType.Future => bodyListFuture.ToDictionary(v => v.type, v => v.sprite),
            _ => throw new ArgumentOutOfRangeException(nameof(age), age, null)
        };
    }
}

[Serializable]
public class Body
{
    public BodyType type;
    public Sprite sprite;
}

public enum BodyType
{
    Front,
    Behind,
    Profile,
    HalfBack
}
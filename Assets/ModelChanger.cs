using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class ModelChanger : MonoBehaviour
{
    public SpriteRenderer bodyRenderer;
    public Transform rotator;
    [FormerlySerializedAs("bodies")]
    public List<Body> bodyList;
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
            bodyRenderer.flipX = true;
        }
        else if (Between(rotation, 292.5F, 0)) {
            bodyRenderer.sprite = bodies[BodyType.Profile];
            bodyRenderer.flipX = true;
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
            bodyRenderer.flipX = false;
        }
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
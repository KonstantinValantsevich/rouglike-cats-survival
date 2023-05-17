using System.Collections.Generic;
using UnityEngine;

public class DynamicBackground : MonoBehaviour, IAgeChangeable
{
    public List<SpriteRenderer> tiles;
    public List<Sprite> tileSprites;

    [ContextMenuItem("Change Type", nameof(UpdateTiles))]
    [SerializeField]
    private AgeType ageType = AgeType.Ancient;

    public void ChangeAge(AgeType age)
    {
        ageType = age;
        UpdateTiles();
    }

    private void UpdateTiles()
    {
        var newSprite = tileSprites[(int) ageType];
        foreach (var tile in tiles) {
            tile.sprite = newSprite;
        }
    }
}

public interface IAgeChangeable
{
    public void ChangeAge(AgeType age);
}

public enum AgeType
{
    Ancient,
    Modern,
    Future
}
using System.Linq;
using Entities;
using UnityEngine;

public class SceneHelper
{
    public static Enemy FindClosestEnemy(Vector3 point)
    {
        var list = Object.FindObjectsOfType<Enemy>();
        var closestEnemy = list.FirstOrDefault();
        if (closestEnemy == null) {
            return null;
        }
        var closestDistance = Vector2.Distance(closestEnemy.transform.position, point);
        foreach (var enemy in list) {
            var distance = Vector2.Distance(enemy.transform.position, point);
            if (distance > closestDistance) {
                continue;
            }
            closestDistance = distance;
            closestEnemy = enemy;
        }
        return closestEnemy;
    }

    private static readonly Camera camera = Camera.main;

    public static Enemy FindRandomEnemy()
    {
        var enemy = Object.FindObjectsOfType<Enemy>();
        return (from e in enemy
            let pos = camera.WorldToViewportPoint(e.transform.position)
            where pos.x is >= 0 and <= 1 && pos.y is >= 0 and <= 1
            select e).FirstOrDefault();
    }
}
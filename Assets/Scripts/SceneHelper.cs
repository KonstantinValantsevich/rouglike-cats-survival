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
    
    public static Enemy FindRandomEnemy()
    {
        return Object.FindObjectOfType<Enemy>();
    }
}
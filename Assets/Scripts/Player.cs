using UnityEngine;

public class Player : Entity
{
    public float health;
    public float baseDamageMultiplier;
    public float regenMultiplier;

    private Camera cameraMain;

    private void Start()
    {
        cameraMain = Camera.main;
    }

    private void Update()
    {
        var mousePosition = Input.mousePosition;
        var position = cameraMain.ScreenToWorldPoint(mousePosition);
        position.z = 0;
        SetLookRotation(position.normalized);
        
        Move(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
    }
}
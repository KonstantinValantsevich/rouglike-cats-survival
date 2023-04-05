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
        mousePosition = cameraMain.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;
        SetLookRotation(mousePosition);
        
        Move(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
    }
}
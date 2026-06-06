using UnityEngine;

public class AttackModule : BaseModule
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform crosshair;
    [SerializeField] float speed;

    public void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab);
        ICrosshairAction action = projectile.GetComponent<ICrosshairAction>();
        action.UseCrosshairAction(this, crosshair);        
    }

    public void SetInput(ButtonState button)
    {
        if (button.IsFirstDown()) { FireProjectile(); }
    }
}

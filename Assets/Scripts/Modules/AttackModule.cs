using UnityEngine;

public class AttackModule : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform crosshair;
    [SerializeField] float speed;

    public void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab);
        ICrosshairAction action = projectile.GetComponent<ICrosshairAction>();
        action.SetCrosshairTarget(crosshair);
        projectile.transform.position = transform.position;
        Vector3 direction = (crosshair.position - transform.position).normalized;
        projectile.GetComponent<Rigidbody>().linearVelocity = direction * speed;
        
    }

    public void SetInput(ButtonState button)
    {
        if (button.IsFirstDown()) { FireProjectile(); }
    }
}

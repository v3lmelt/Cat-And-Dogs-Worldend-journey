using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject projectilePrefab;
    public int MagicDamage;
    private Damageable launcherDamageable;

    private void Start()
    {
        launcherDamageable = GetComponent<Damageable>();
        projectilePrefab.GetComponent<Projectile>().damage = MagicDamage;
    }

    public void FireProjectile()
    {
        if (launcherDamageable != null && launcherDamageable.Fir())
        {
            GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, projectilePrefab.transform.rotation);
            Vector3 origScale = projectile.transform.localScale;
            projectile.transform.localScale = new Vector3(
                origScale.x * launchPoint.parent.transform.localScale.x > 0 ? 1 : -1,
                origScale.y,
                origScale.z
                );
        }
        else
        {
            Debug.Log("Not enough MP to fire!");
        }

    }
}

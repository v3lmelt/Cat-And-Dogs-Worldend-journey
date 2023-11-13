using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    // 发射点的Transform
    public Transform launchPoint;

    // 抛射物的预制体
    public GameObject projectilePrefab;

    private Damageable launcherDamageable;

    // 发射抛射物的方法
    public void FireProjectile()
    {
        if (launcherDamageable != null && launcherDamageable.Fir())
        {
            // 实例化抛射物对象，在发射点位置，并使用抛射物预制体的旋转
            GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, projectilePrefab.transform.rotation);

            // 获取抛射物原始的缩放
            Vector3 origScale = projectile.transform.localScale;

            // 根据发射点的方向设置抛射物的缩放，确保抛射物朝向正确的方向
            projectile.transform.localScale = new Vector3(
                origScale.x * transform.localScale.x > 0 ? 1 : -1,// 如果发射点方向朝右，则保持原始缩放，否则翻转抛射物
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

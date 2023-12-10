using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewTentacle : Tentacle
{
    [Header("泡泡")] 
    public float foamGravityMax = 1.1f;
    public float foamGravityMin = 0.8f;

    public int foamGenerateMax = 40;
    public int foamGenerateMin = 27;

    public float foamXForceMax = 4f;
    public float foamXForceMin = 2f;

    public float foamYForceMax = 16f;
    public float foamYForceMin = 2f;
    public new void CreateFoam()
    {
        for (var i = 0; i < Random.Range(foamGenerateMin, foamGenerateMax); i++)
        {
            var foamRigid = Instantiate(FoamPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            
            foamRigid.AddForce(new Vector2(Random.Range(foamXForceMin, foamXForceMax) * transform.localScale.x, 
                Random.Range(foamYForceMin, foamYForceMax)), ForceMode2D.Impulse);
            
            foamRigid.gravityScale = Random.Range(foamGravityMin, foamGravityMax);
        }
    }
}

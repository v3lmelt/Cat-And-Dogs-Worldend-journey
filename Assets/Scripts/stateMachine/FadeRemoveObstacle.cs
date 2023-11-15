using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FadeRemoveObstacle : StateMachineBehaviour
{
    public float fadetime = 0.5f;
    public float fadeDelay = 0.0f;
    private float timeElapsed = 0f;
    private float fadeDelayElapsed = 0f;
    TilemapRenderer tilemapRenderer;
    GameObject objToRemove;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed = 0f;
        tilemapRenderer = animator.GetComponent<TilemapRenderer>();
        objToRemove = animator.gameObject;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (fadeDelay > fadeDelayElapsed)
        {
            fadeDelayElapsed += Time.deltaTime;
        }
        else
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed > fadetime)
            {
                Destroy(objToRemove);
            }
        }
    }
}

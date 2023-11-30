using UnityEngine;

public static class TagUtil
    {
        public static bool ComparePlayerTag(GameObject go)
        {
            return go.CompareTag("Player") || go.CompareTag("Player_cat");
        } 
    }
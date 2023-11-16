using UnityEngine;

public static class Util
    {
        public static bool ComparePlayerTag(GameObject go)
        {
            return go.CompareTag("Player") || go.CompareTag("Player_cat");
        } 
    }
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests.EditorTest
{
    public class SceneBasicSetupTest
    {
        [UnityTest]
        public IEnumerator SceneBasicSetupTestWithEnumeratorPasses()
        {
            // 获取所有Scene，并放在一个List中
            var sceneList = new List<string>();
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                var sceneName =
                    System.IO.Path.GetFileName(SceneUtility
                        .GetScenePathByBuildIndex(i));
                if (!EditorTestConst.SceneExcludeFromSetupTest.Contains(sceneName)) sceneList.Add(sceneName);
            }
            if (sceneList == null) throw new ArgumentNullException(nameof(sceneList));

            foreach (var scene in sceneList)
            {
                EditorSceneManager.OpenScene("Assets/Scenes/" + scene);
                yield return null;
                // 每个关卡都要有的物品
                // TextManager, 主要负责创建状态字体
                var textManager = GameObject.Find("TextManager");
                Assert.IsTrue(textManager != null, "Can't find textManager in " + scene);
                // UIManager，主要负责伤害字体的创建
                var uiManager = GameObject.Find("UIManager");
                Assert.IsTrue(uiManager != null, "Can't find uiManager in " + scene);
                // Canvas_1, 负责真正的UI绘制
                var canvas1 = GameObject.Find("Canvas_1");
                Assert.IsTrue(canvas1 != null, "Can't find canvas1 in " + scene);
                // TeleportPoint, 传送点
                var teleportPoint = GameObject.Find("Tp");
                Assert.IsNotNull(teleportPoint, "Can't find teleport point in " + scene);
                // GroundCollision, 地面
                var groundCollision = GameObject.Find("GroundCollision");
                Assert.IsNotNull(groundCollision, "Can't find groundCollision in " + scene);
                Assert.IsNotNull(GameObject.Find("LevelBorderLeft"), "Can't find LevelBorderLeft in " + scene);
                Assert.IsNotNull(GameObject.Find("LevelBorderRight"), "Can't find LevelBorderRight in " + scene);
            }
        }

    }
}

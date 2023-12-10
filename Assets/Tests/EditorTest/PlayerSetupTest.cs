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
    public class PlayerSetupTest
    {
        [UnityTest]
        public IEnumerator PlayerSetupTestPasses()
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
                // 查找猫狗是否被正确设置，并且存在对应的Tag
                var cat = GameObject.Find("Cat");
                var dog = GameObject.Find("Dog");
                
                // 首先猫狗不能为空对象!
                Assert.IsNotNull(cat);
                Assert.IsNotNull(dog);
                
                Assert.IsTrue(cat.CompareTag("Player_cat"));
                Assert.IsTrue(dog.CompareTag("Player"));
            }
        }
    }
}

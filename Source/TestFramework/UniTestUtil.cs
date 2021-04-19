
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UniDi.Internal
{
    public static class UniDiTestUtil
    {
        public const string UnitTestRunnerGameObjectName = "Code-based tests runner";

        public static void DestroyEverythingExceptTestRunner(bool immediate)
        {
            var testRunner = GameObject.Find(UnitTestRunnerGameObjectName);
            Assert.IsNotNull(testRunner);
            GameObject.DontDestroyOnLoad(testRunner);

            // We want to clear all objects across all scenes to ensure the next test is not affected
            // at all by previous tests
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                foreach (var obj in SceneManager.GetSceneAt(i).GetRootGameObjects())
                {
                    GameObject.DestroyImmediate(obj);
                }
            }

            if (ProjectContext.HasInstance)
            {
                var dontDestroyOnLoadRoots = ProjectContext.Instance.gameObject.scene
                    .GetRootGameObjects();

                foreach (var rootObj in dontDestroyOnLoadRoots)
                {
                    if (rootObj.name != UnitTestRunnerGameObjectName)
                    {
                        if (immediate)
                        {
                            GameObject.DestroyImmediate(rootObj);
                        }
                        else
                        {
                            GameObject.Destroy(rootObj);
                        }
                    }
                }
            }
        }
    }
}

#if !NOT_UNITY3D

using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace UniDi.Internal
{
    public static class UniDiMenuItems
    {
        [MenuItem("Edit/UniDi/Validate Current Scenes #&v")]
        public static void ValidateCurrentScene()
        {
            ValidateCurrentSceneInternal();
        }

        [MenuItem("Edit/UniDi/Validate Then Run #&r")]
        public static void ValidateCurrentSceneThenRun()
        {
            if (ValidateCurrentSceneInternal())
            {
                EditorApplication.isPlaying = true;
            }
        }

        [MenuItem("Edit/UniDi/Help...")]
        public static void OpenDocumentation()
        {
            Application.OpenURL("https://github.com/svermeulen/UniDi");
        }

        [MenuItem("GameObject/UniDi/Scene Context", false, 9)]
        public static void CreateSceneContext(MenuCommand menuCommand)
        {
            var root = new GameObject("SceneContext").AddComponent<SceneContext>();
            Selection.activeGameObject = root.gameObject;

            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }

        [MenuItem("GameObject/UniDi/Decorator Context", false, 9)]
        public static void CreateDecoratorContext(MenuCommand menuCommand)
        {
            var root = new GameObject("DecoratorContext").AddComponent<SceneDecoratorContext>();
            Selection.activeGameObject = root.gameObject;

            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }

        [MenuItem("GameObject/UniDi/Game Object Context", false, 9)]
        public static void CreateGameObjectContext(MenuCommand menuCommand)
        {
            var root = new GameObject("GameObjectContext").AddComponent<GameObjectContext>();
            Selection.activeGameObject = root.gameObject;

            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }

        [MenuItem("Edit/UniDi/Create Project Context")]
        public static void CreateProjectContextInDefaultLocation()
        {
            var fullDirPath = Path.Combine(Application.dataPath, "Resources");

            if (!Directory.Exists(fullDirPath))
            {
                Directory.CreateDirectory(fullDirPath);
            }

            CreateProjectContextInternal("Assets/Resources");
        }

        [MenuItem("Assets/Create/UniDi/Default Scene Contract Config", false, 80)]
        public static void CreateDefaultSceneContractConfig()
        {
            var folderPath = UniDiUnityEditorUtil.GetCurrentDirectoryAssetPathFromSelection();

            if (!folderPath.EndsWith("/Resources"))
            {
                EditorUtility.DisplayDialog("Error",
                    "UniDiDefaultSceneContractConfig objects must be placed directly underneath a folder named 'Resources'.  Please try again.", "Ok");
                return;
            }

            var config = ScriptableObject.CreateInstance<DefaultSceneContractConfig>();

            UniDiUnityEditorUtil.SaveScriptableObjectAsset(
                Path.Combine(folderPath, DefaultSceneContractConfig.ResourcePath + ".asset"), config);
        }

        [MenuItem("Assets/Create/UniDi/Installer", false, 1)]
        public static void CreateInstaller()
        {
            AddCSharpClassTemplate("Installer", "UntitledInstaller",
                  "using UnityEngine;"
                + "\nusing UniDi;"
                + "\n"
                + "\npublic class CLASS_NAME : Installer<CLASS_NAME>"
                + "\n{"
                + "\n    public override void InstallBindings()"
                + "\n    {"
                + "\n    }"
                + "\n}");
        }

        [MenuItem("Assets/Create/UniDi/Mono Installer", false, 2)]
        public static void CreateMonoInstaller()
        {
            AddCSharpClassTemplate("Mono Installer", "UntitledInstaller",
                  "using UnityEngine;"
                + "\nusing UniDi;"
                + "\n"
                + "\npublic class CLASS_NAME : MonoInstaller"
                + "\n{"
                + "\n    public override void InstallBindings()"
                + "\n    {"
                + "\n    }"
                + "\n}");
        }

        [MenuItem("Assets/Create/UniDi/Scriptable Object Installer", false, 3)]
        public static void CreateScriptableObjectInstaller()
        {
            AddCSharpClassTemplate("Scriptable Object Installer", "UntitledInstaller",
                  "using UnityEngine;"
                + "\nusing UniDi;"
                + "\n"
                + "\n[CreateAssetMenu(fileName = \"CLASS_NAME\", menuName = \"Installers/CLASS_NAME\")]"
                + "\npublic class CLASS_NAME : ScriptableObjectInstaller<CLASS_NAME>"
                + "\n{"
                + "\n    public override void InstallBindings()"
                + "\n    {"
                + "\n    }"
                + "\n}");
        }

        [MenuItem("Assets/Create/UniDi/Composite Scriptable Object Installer", false, 4)]
        public static void CreateCompositeScriptableObjectInstaller()
        {
            ProjectWindowUtil.CreateAsset(
                asset: ScriptableObject.CreateInstance<CompositeScriptableObjectInstaller>(),
                pathName: "CompositeScriptableObjectInstaller.asset");
        }

        [MenuItem("Assets/Create/UniDi/Editor Window", false, 20)]
        public static void CreateEditorWindow()
        {
            AddCSharpClassTemplate("Editor Window", "UntitledEditorWindow",
                  "using UnityEngine;"
                + "\nusing UnityEditor;"
                + "\nusing UniDi;"
                + "\n"
                + "\npublic class CLASS_NAME : UniDiEditorWindow"
                + "\n{"
                + "\n    [MenuItem(\"Window/CLASS_NAME\")]"
                + "\n    public static CLASS_NAME GetOrCreateWindow()"
                + "\n    {"
                + "\n        var window = EditorWindow.GetWindow<CLASS_NAME>();"
                + "\n        window.titleContent = new GUIContent(\"CLASS_NAME\");"
                + "\n        return window;"
                + "\n    }"
                + "\n"
                + "\n    public override void InstallBindings()"
                + "\n    {"
                + "\n        // TODO"
                + "\n    }"
                + "\n}");
        }

        [MenuItem("Assets/Create/UniDi/Project Context", false, 40)]
        public static void CreateProjectContext()
        {
            var absoluteDir = UniDiUnityEditorUtil.TryGetSelectedFolderPathInProjectsTab();

            if (absoluteDir == null)
            {
                EditorUtility.DisplayDialog("Error",
                    "Could not find directory to place the '{0}.prefab' asset.  Please try again by right clicking in the desired folder within the projects pane."
                    .Fmt(ProjectContext.ProjectContextResourcePath), "Ok");
                return;
            }

            var parentFolderName = Path.GetFileName(absoluteDir);

            if (parentFolderName != "Resources")
            {
                EditorUtility.DisplayDialog("Error",
                    "'{0}.prefab' must be placed inside a directory named 'Resources'.  Please try again by right clicking within the Project pane in a valid Resources folder."
                    .Fmt(ProjectContext.ProjectContextResourcePath), "Ok");
                return;
            }

            CreateProjectContextInternal(absoluteDir);
        }

        static void CreateProjectContextInternal(string absoluteDir)
        {
            var assetPath = UniDiUnityEditorUtil.ConvertFullAbsolutePathToAssetPath(absoluteDir);
            var prefabPath = (Path.Combine(assetPath, ProjectContext.ProjectContextResourcePath) + ".prefab").Replace("\\", "/");

            var gameObject = new GameObject();

            try
            {
                gameObject.AddComponent<ProjectContext>();

#if UNITY_2018_3_OR_NEWER
                var prefabObj = PrefabUtility.SaveAsPrefabAsset(gameObject, prefabPath);
#else
                var prefabObj = PrefabUtility.ReplacePrefab(gameObject, PrefabUtility.CreateEmptyPrefab(prefabPath));
#endif

                Selection.activeObject = prefabObj;
            }
            finally
            {
                GameObject.DestroyImmediate(gameObject);
            }

            Debug.Log("Created new ProjectContext at '{0}'".Fmt(prefabPath));
        }

        public static string AddCSharpClassTemplate(
            string friendlyName, string defaultFileName, string templateStr)
        {
            return AddCSharpClassTemplate(
                friendlyName, defaultFileName, templateStr, UniDiUnityEditorUtil.GetCurrentDirectoryAssetPathFromSelection());
        }

        public static string AddCSharpClassTemplate(
            string friendlyName, string defaultFileName,
            string templateStr, string folderPath)
        {
            var absolutePath = EditorUtility.SaveFilePanel(
                "Choose name for " + friendlyName,
                folderPath,
                defaultFileName + ".cs",
                "cs");

            if (absolutePath == "")
            {
                // Dialog was cancelled
                return null;
            }

            if (!absolutePath.ToLower().EndsWith(".cs"))
            {
                absolutePath += ".cs";
            }

            var className = Path.GetFileNameWithoutExtension(absolutePath);
            File.WriteAllText(absolutePath, templateStr.Replace("CLASS_NAME", className));

            AssetDatabase.Refresh();

            var assetPath = UniDiUnityEditorUtil.ConvertFullAbsolutePathToAssetPath(absolutePath);

            EditorUtility.FocusProjectWindow();
            Selection.activeObject = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(assetPath);

            return assetPath;
        }

        [MenuItem("Edit/UniDi/Validate All Active Scenes")]
        public static void ValidateAllActiveScenes()
        {
            UniDiUnityEditorUtil.SaveThenRunPreserveSceneSetup(() =>
                {
                    var numValidated = UniDiUnityEditorUtil.ValidateAllActiveScenes();
                    Log.Info("Validated all '{0}' active scenes successfully", numValidated);
                });
        }

        static bool ValidateCurrentSceneInternal()
        {
            return UniDiUnityEditorUtil.SaveThenRunPreserveSceneSetup(() =>
                {
                    SceneParentAutomaticLoader.ValidateMultiSceneSetupAndLoadDefaultSceneParents();
                    UniDiUnityEditorUtil.ValidateCurrentSceneSetup();
                    Log.Info("All scenes validated successfully");
                });
        }

        [MenuItem("Assets/Create/UniDi/Unit Test", false, 60)]
        public static void CreateUnitTest()
        {
            AddCSharpClassTemplate("Unit Test", "UntitledUnitTest",
                  "using UniDi;"
                + "\nusing NUnit.Framework;"
                + "\n"
                + "\n[TestFixture]"
                + "\npublic class CLASS_NAME : UniDiUnitTestFixture"
                + "\n{"
                + "\n    [Test]"
                + "\n    public void RunTest1()"
                + "\n    {"
                + "\n        // TODO"
                + "\n    }"
                + "\n}");
        }

        [MenuItem("Assets/Create/UniDi/Integration Test", false, 60)]
        public static void CreateIntegrationTest()
        {
            AddCSharpClassTemplate("Integration Test", "UntitledIntegrationTest",
                  "using UniDi;"
                + "\nusing System.Collections;"
                + "\nusing UnityEngine.TestTools;"
                + "\n"
                + "\npublic class CLASS_NAME : UniDiIntegrationTestFixture"
                + "\n{"
                + "\n    [UnityTest]"
                + "\n    public IEnumerator RunTest1()"
                + "\n    {"
                + "\n        // Setup initial state by creating game objects from scratch, loading prefabs/scenes, etc"
                + "\n"
                + "\n        PreInstall();"
                + "\n"
                + "\n        // Call Container.Bind methods"
                + "\n"
                + "\n        PostInstall();"
                + "\n"
                + "\n        // Add test assertions for expected state"
                + "\n        // Using Container.Resolve or [Inject] fields"
                + "\n        yield break;"
                + "\n    }"
                + "\n}");
        }

        [MenuItem("Assets/Create/UniDi/Scene Test", false, 60)]
        public static void CreateSceneTest()
        {
            AddCSharpClassTemplate("Scene Test Fixture", "UntitledSceneTest",
                  "using UniDi;"
                + "\nusing System.Collections;"
                + "\nusing UnityEngine;"
                + "\nusing UnityEngine.TestTools;"
                + "\n"
                + "\npublic class CLASS_NAME : SceneTestFixture"
                + "\n{"
                + "\n    [UnityTest]"
                + "\n    public IEnumerator TestScene()"
                + "\n    {"
                + "\n        yield return LoadScene(\"InsertSceneNameHere\");"
                + "\n"
                + "\n        // TODO: Add assertions here now that the scene has started"
                + "\n        // Or you can just uncomment to simply wait some time to make sure the scene plays without errors"
                + "\n        //yield return new WaitForSeconds(1.0f);"
                + "\n"
                + "\n        // Note that you can use SceneContainer.Resolve to look up objects that you need for assertions"
                + "\n    }"
                + "\n}");
        }
    }
}
#endif

﻿using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

namespace ArenaUnity
{
    // Tagging a class with the EditorTool attribute and no target type registers a global tool. Global tools are valid for any selection, and are accessible through the top left toolbar in the editor.
    [EditorTool("ARENA Size Tool")]
    class SizeTool : EditorTool
    {
        // Serialize this value to set a default value in the Inspector.
        [SerializeField]
        Texture2D m_ToolIcon;
        GUIContent m_IconContent;

        void OnEnable()
        {
            m_ToolIcon = (Texture2D)AssetDatabase.LoadAssetAtPath("Packages/io.conix.arena.unity/Editor/Resources/Images/xr-logo.png", typeof(Texture2D));
            m_IconContent = new GUIContent()
            {
                image = m_ToolIcon,
                text = "ARENA Size Tool",
                tooltip = "ARENA Size Tool"
            };
        }

        public override GUIContent toolbarIcon
        {
            get { return m_IconContent; }
        }

        // This is called for each window that your tool is active in. Put the functionality of your tool here.
        public override void OnToolGUI(EditorWindow window)
        {
            // must be an arena object
            GameObject go = Selection.activeGameObject;
            if (go == null) return;
            ArenaObject aobj = go.GetComponent<ArenaObject>();
            if (aobj == null) return;
            ArenaMeshBase am = aobj.GetComponent<ArenaMeshBase>();
            if (am == null) return;
            switch (am.GetType().ToString())
            {
                case "ArenaUnity.ArenaMeshCube":
                    HandleSizeCube(go.GetComponent<ArenaMeshCube>());
                    break;
                case "ArenaUnity.ArenaMeshCone":
                    HandleSizeCone(go.GetComponent<ArenaMeshCone>());
                    break;
                case "ArenaUnity.ArenaMeshCylinder":
                    HandleSizeCylinder(go.GetComponent<ArenaMeshCylinder>());
                    break;
                case "ArenaUnity.ArenaMeshIcosahedron":
                    HandleSizeIcosahedron(go.GetComponent<ArenaMeshIcosahedron>());
                    break;
                case "ArenaUnity.ArenaMeshOctahedron":
                    HandleSizeOctahedron(go.GetComponent<ArenaMeshOctahedron>());
                    break;
                case "ArenaUnity.ArenaMeshPlane":
                    HandleSizePlane(go.GetComponent<ArenaMeshPlane>());
                    break;
                case "ArenaUnity.ArenaMeshSphere":
                    HandleSizeSphere(go.GetComponent<ArenaMeshSphere>());
                    break;
            }
        }

        private static void HandleSizeCube(ArenaMeshCube cube)
        {
            float size = HandleUtility.GetHandleSize(cube.transform.position) * 1f;
            float snap = 0.5f;

            EditorGUI.BeginChangeCheck();
            float width = cube.width;
            float height = cube.height;
            float depth = cube.depth;
            using (new Handles.DrawingScope(Color.magenta))
            {
                width = Handles.ScaleSlider(cube.width, cube.transform.position, cube.transform.right, cube.transform.rotation, size, snap);
            }
            using (new Handles.DrawingScope(Color.green))
            {
                height = Handles.ScaleSlider(cube.height, cube.transform.position, cube.transform.up, cube.transform.rotation, size, snap);
            }
            using (new Handles.DrawingScope(Color.cyan))
            {
                depth = Handles.ScaleSlider(cube.depth, cube.transform.position, cube.transform.forward, cube.transform.rotation, size, snap);
            }
            if (EditorGUI.EndChangeCheck())
            {
                //Undo.RecordObjects(Selection.gameObjects, "Size Arena Cube");
                foreach (var o in Selection.gameObjects)
                {
                    var amesh = o.GetComponent<ArenaMeshCube>();
                    amesh.width = width;
                    amesh.height = height;
                    amesh.depth = depth;
                    amesh.rebuild = true;
                }
            }
        }

        private static void HandleSizePlane(ArenaMeshPlane plane)
        {
            float size = HandleUtility.GetHandleSize(plane.transform.position) * 1f;
            float snap = 0.5f;

            EditorGUI.BeginChangeCheck();
            float width = plane.width;
            float height = plane.height;
            using (new Handles.DrawingScope(Color.magenta))
            {
                width = Handles.ScaleSlider(plane.width, plane.transform.position, plane.transform.right, plane.transform.rotation, size, snap);
            }
            using (new Handles.DrawingScope(Color.cyan))
            {
                height = Handles.ScaleSlider(plane.height, plane.transform.position, plane.transform.up, plane.transform.rotation, size, snap);
            }
            if (EditorGUI.EndChangeCheck())
            {
                //Undo.RecordObjects(Selection.gameObjects, "Size Arena Plane");
                foreach (var o in Selection.gameObjects)
                {
                    var amesh = o.GetComponent<ArenaMeshPlane>();
                    amesh.width = width;
                    amesh.height = height;
                    amesh.rebuild = true;
                }
            }
        }

        private static void HandleSizeCylinder(ArenaMeshCylinder cylinder)
        {
            float size = HandleUtility.GetHandleSize(cylinder.transform.position) * 1f;
            float snap = 0.5f;

            EditorGUI.BeginChangeCheck();
            float radius = cylinder.radius;
            float height = cylinder.height;
            using (new Handles.DrawingScope(Color.magenta))
            {
                radius = Handles.ScaleSlider(cylinder.radius, cylinder.transform.position, cylinder.transform.right, cylinder.transform.rotation, size, snap);
            }
            using (new Handles.DrawingScope(Color.green))
            {
                height = Handles.ScaleSlider(cylinder.height, cylinder.transform.position, cylinder.transform.up, cylinder.transform.rotation, size, snap);
            }
            if (EditorGUI.EndChangeCheck())
            {
                //Undo.RecordObjects(Selection.gameObjects, "Size Arena Cylinder");
                foreach (var o in Selection.gameObjects)
                {
                    var amesh = o.GetComponent<ArenaMeshCylinder>();
                    amesh.radius = radius;
                    amesh.height = height;
                    amesh.rebuild = true;
                }
            }
        }

        private static void HandleSizeCone(ArenaMeshCone cone)
        {
            float size = HandleUtility.GetHandleSize(cone.transform.position) * 1f;
            float snap = 0.5f;

            EditorGUI.BeginChangeCheck();
            float radius = cone.radius;
            float height = cone.height;
            using (new Handles.DrawingScope(Color.magenta))
            {
                radius = Handles.ScaleSlider(cone.radius, cone.transform.position, cone.transform.right, cone.transform.rotation, size, snap);
            }
            using (new Handles.DrawingScope(Color.green))
            {
                height = Handles.ScaleSlider(cone.height, cone.transform.position, cone.transform.up, cone.transform.rotation, size, snap);
            }
            if (EditorGUI.EndChangeCheck())
            {
                //Undo.RecordObjects(Selection.gameObjects, "Size Arena Cone");
                foreach (var o in Selection.gameObjects)
                {
                    var amesh = o.GetComponent<ArenaMeshCone>();
                    amesh.radius = radius;
                    amesh.height = height;
                    amesh.rebuild = true;
                }
            }
        }

        private static void HandleSizeSphere(ArenaMeshSphere sphere)
        {
            float size = HandleUtility.GetHandleSize(sphere.transform.position) * 1f;
            float snap = 0.5f;

            EditorGUI.BeginChangeCheck();
            float radius = sphere.radius;
            using (new Handles.DrawingScope(Color.magenta))
            {
                radius = Handles.ScaleSlider(sphere.radius, sphere.transform.position, sphere.transform.right, sphere.transform.rotation, size, snap);
            }
            if (EditorGUI.EndChangeCheck())
            {
                //Undo.RecordObjects(Selection.gameObjects, "Size Arena Sphere");
                foreach (var o in Selection.gameObjects)
                {
                    var amesh = o.GetComponent<ArenaMeshSphere>();
                    amesh.radius = radius;
                    amesh.rebuild = true;
                }
            }
        }

        private static void HandleSizeIcosahedron(ArenaMeshIcosahedron icosahedron)
        {
            float size = HandleUtility.GetHandleSize(icosahedron.transform.position) * 1f;
            float snap = 0.5f;

            EditorGUI.BeginChangeCheck();
            float radius = icosahedron.radius;
            using (new Handles.DrawingScope(Color.magenta))
            {
                radius = Handles.ScaleSlider(icosahedron.radius, icosahedron.transform.position, icosahedron.transform.right, icosahedron.transform.rotation, size, snap);
            }
            if (EditorGUI.EndChangeCheck())
            {
                //Undo.RecordObjects(Selection.gameObjects, "Size Arena Icosahedron");
                foreach (var o in Selection.gameObjects)
                {
                    var amesh = o.GetComponent<ArenaMeshIcosahedron>();
                    amesh.radius = radius;
                    amesh.rebuild = true;
                }
            }
        }

        private static void HandleSizeOctahedron(ArenaMeshOctahedron octahedron)
        {
            float size = HandleUtility.GetHandleSize(octahedron.transform.position) * 1f;
            float snap = 0.5f;

            EditorGUI.BeginChangeCheck();
            float radius = octahedron.radius;
            using (new Handles.DrawingScope(Color.magenta))
            {
                radius = Handles.ScaleSlider(octahedron.radius, octahedron.transform.position, octahedron.transform.right, octahedron.transform.rotation, size, snap);
            }
            if (EditorGUI.EndChangeCheck())
            {
                //Undo.RecordObjects(Selection.gameObjects, "Size Arena Octahedron");
                foreach (var o in Selection.gameObjects)
                {
                    var amesh = o.GetComponent<ArenaMeshOctahedron>();
                    amesh.radius = radius;
                    amesh.rebuild = true;
                }
            }
        }

    }
}

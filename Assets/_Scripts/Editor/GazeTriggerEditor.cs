using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GazeTrigger))]
public class GazeTriggerEditor : Editor
{
    private GazeTrigger gazeTrigger;

    private void OnEnable()
    {
        gazeTrigger = target as GazeTrigger;
        gazeTrigger.sphere = GameObject.Find("Sphere");
        gazeTrigger.SphereRadius = gazeTrigger.sphere.GetComponent<MeshFilter>().sharedMesh.bounds.size.x / 2.0f * gazeTrigger.sphere.transform.localScale.x;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (gazeTrigger.sphere.transform.hasChanged)
        {
            gazeTrigger.SphereRadius = gazeTrigger.sphere.GetComponent<MeshFilter>().sharedMesh.bounds.size.x / 2.0f * gazeTrigger.sphere.transform.localScale.x;
        }

        if (gazeTrigger.transform.hasChanged)
        {
            if (!gazeTrigger.IsOnSphere())
            {
                gazeTrigger.SetOnSphere();
            }
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }

}

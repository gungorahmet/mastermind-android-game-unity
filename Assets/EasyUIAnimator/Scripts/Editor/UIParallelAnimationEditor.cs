using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using EasyUIAnimator;

[CustomEditor(typeof(UIParallelAnimation))]
public class UIParallelAnimationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        UIParallelAnimation parallelAnim = (UIParallelAnimation)target;

        //MOVE ANIMATION
        if (parallelAnim.moveAnimation)
        {
            if (GUILayout.Button("- Move Animation", GUILayout.Height(30)))   parallelAnim.moveAnimation = false;
            if (GUILayout.Button("Use Current Value"))  UseCurrentValue(AnimationType.MOVE, parallelAnim);
            parallelAnim.start[0] = EditorGUILayout.Vector3Field("Start Position", parallelAnim.start[0]);
            parallelAnim.final[0] = EditorGUILayout.Vector3Field("Final Position", parallelAnim.final[0]);
            parallelAnim.useScreenValues = EditorGUILayout.Toggle("Use Screen Values", parallelAnim.useScreenValues);

            parallelAnim.moveModifier   = (Modifiers)EditorGUILayout.EnumPopup("Modifier", parallelAnim.moveModifier);
            parallelAnim.moveEffect     = (Effects)EditorGUILayout.EnumPopup("Effect", parallelAnim.moveEffect);
            if (parallelAnim.moveEffect != Effects.NONE)
            {
                parallelAnim.max[0] = EditorGUILayout.FloatField("Max", parallelAnim.max[0]);
                if (parallelAnim.moveEffect != Effects.EXPLOSION)
                {
                    parallelAnim.bounce[0] = EditorGUILayout.IntField("Bounce", parallelAnim.bounce[0]);
                    parallelAnim.randomDirection = false;
                }
                else
                {
                    parallelAnim.randomDirection = EditorGUILayout.Toggle("Random Direction", parallelAnim.randomDirection);
                }
                if (!parallelAnim.randomDirection)
                {
                    parallelAnim.effectRotation = EditorGUILayout.Vector3Field("Effect Rotation", parallelAnim.effectRotation);
                }
            }
        }
        else
            if (GUILayout.Button("+ Move Animation", GUILayout.Height(30))) parallelAnim.moveAnimation = true;

        //SCALE ANIMATION
        if (parallelAnim.scaleAnimation)
        {
            if (GUILayout.Button("- Scale Animation", GUILayout.Height(30))) parallelAnim.scaleAnimation = false;
            if (GUILayout.Button("Use Current Value")) UseCurrentValue(AnimationType.SCALE, parallelAnim);
            parallelAnim.start[1] = EditorGUILayout.Vector3Field("Start Scale", parallelAnim.start[1]);
            parallelAnim.final[1] = EditorGUILayout.Vector3Field("Final Scale", parallelAnim.final[1]);

            parallelAnim.scaleModifier  = (Modifiers)EditorGUILayout.EnumPopup("Modifier", parallelAnim.scaleModifier);
            parallelAnim.scaleEffect    = (Effects)EditorGUILayout.EnumPopup("Effect", parallelAnim.scaleEffect);
            if (parallelAnim.scaleEffect != Effects.NONE)
            {
                parallelAnim.max[1] = EditorGUILayout.FloatField("Max", parallelAnim.max[1]);
                if (parallelAnim.scaleEffect != Effects.EXPLOSION)
                {
                    parallelAnim.bounce[1] = EditorGUILayout.IntField("Bounce", parallelAnim.bounce[1]);
                }
            }
        }
        else
            if (GUILayout.Button("+ Scale Animation", GUILayout.Height(30))) parallelAnim.scaleAnimation = true;

        //ROTATION ANIMATION
        if (parallelAnim.rotationAnimation)
        {
            if (GUILayout.Button("- Rotation Animation", GUILayout.Height(30))) parallelAnim.rotationAnimation = false;
            if (GUILayout.Button("Use Current Value")) UseCurrentValue(AnimationType.ROTATION, parallelAnim);
            parallelAnim.start[2] = EditorGUILayout.Vector3Field("Start Rotation", parallelAnim.start[2]);
            parallelAnim.final[2] = EditorGUILayout.Vector3Field("Final Rotation", parallelAnim.final[2]);

            parallelAnim.rotationModifier   = (Modifiers)EditorGUILayout.EnumPopup("Modifier", parallelAnim.rotationModifier);
            parallelAnim.rotationEffect     = (Effects)EditorGUILayout.EnumPopup("Effect", parallelAnim.rotationEffect);
            if (parallelAnim.rotationEffect != Effects.NONE)
            {
                parallelAnim.max[2] = EditorGUILayout.FloatField("Max", parallelAnim.max[2]);
                if (parallelAnim.rotationEffect != Effects.EXPLOSION)
                {
                    parallelAnim.bounce[2] = EditorGUILayout.IntField("Bounce", parallelAnim.bounce[2]);
                }
            }
        }
        else
            if (GUILayout.Button("+ Rotation Animation", GUILayout.Height(30))) parallelAnim.rotationAnimation = true;

        //GRAPH ANIMATION
        if (parallelAnim.graphicAnimation)
        {
            if (GUILayout.Button("- Graphic Animation", GUILayout.Height(30))) parallelAnim.graphicAnimation = false;
            if (GUILayout.Button("Use Current Value")) UseCurrentValue(AnimationType.IMAGE, parallelAnim);
            parallelAnim.startColor = EditorGUILayout.ColorField("Start Color", parallelAnim.startColor);
            parallelAnim.finalColor = EditorGUILayout.ColorField("Final Color", parallelAnim.finalColor);

            parallelAnim.graphicModifier = (Modifiers)EditorGUILayout.EnumPopup("Modifier", parallelAnim.graphicModifier);
        }
        else
            if (GUILayout.Button("+ Graphic Animation", GUILayout.Height(30))) parallelAnim.graphicAnimation = true;

        GUIStyle style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
        EditorGUILayout.LabelField("-Common Values-", style);
        parallelAnim.disableAfter   = EditorGUILayout.Toggle("Disable On Finish", parallelAnim.disableAfter);
        if (!parallelAnim.disableAfter)
            parallelAnim.loop       = (Loop)EditorGUILayout.EnumPopup("Loop Options", parallelAnim.loop);
        parallelAnim.delay          = EditorGUILayout.FloatField("Delay", parallelAnim.delay);
        parallelAnim.duration       = EditorGUILayout.FloatField("Duration", parallelAnim.duration);
        parallelAnim.playOnStart    = EditorGUILayout.Toggle("Play On Start", parallelAnim.playOnStart);
        if (GUILayout.Button("Play"))
            parallelAnim.PlayReverse();
    }

    private void UseCurrentValue(AnimationType type, UIParallelAnimation anim)
    {
        switch (type)
        {
            case AnimationType.MOVE:
                if (anim.useScreenValues)
                    anim.start[0] = anim.transform.position;
                else
                    anim.start[0] = Vector3.Scale(anim.transform.position, EasyUIAnimator.UIAnimator.InvertedScreenDimension);
                break;
            case AnimationType.SCALE:
                anim.start[1] = anim.transform.localScale;
                break;
            case AnimationType.ROTATION:
                anim.start[2] = anim.transform.localRotation.eulerAngles;
                break;
            case AnimationType.IMAGE:
                anim.startColor = anim.GetComponent<UnityEngine.UI.Graphic>().color;
                break;
        }
    }
}

using ThreeD;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EllenPlayerController))]
public class PlayerControllerEditor : Editor {

    public override void OnInspectorGUI(){
        EllenPlayerController playerController = (EllenPlayerController)target;
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Ellen Player", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        switch (playerController.PlayerState){
            case PlayerController.EPlayerState.None:
                GUI.backgroundColor = Color.black;
                break;
            case PlayerController.EPlayerState.Idle:
                GUI.backgroundColor = Color.red;
                break;
            case PlayerController.EPlayerState.Move:
                GUI.backgroundColor = Color.blue;
                break;
            case PlayerController.EPlayerState.Jump:
                GUI.backgroundColor = Color.green;
                break;
        }
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.LabelField($"Current State: {playerController.PlayerState}", EditorStyles.boldLabel);
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();
    }

    private void OnEnable(){
        EditorApplication.update += OnEditorUpdate;
    }

    private void OnDisable(){
        EditorApplication.update -= OnEditorUpdate;
    }

    private void OnEditorUpdate(){
        if (target) Repaint();
    }

}
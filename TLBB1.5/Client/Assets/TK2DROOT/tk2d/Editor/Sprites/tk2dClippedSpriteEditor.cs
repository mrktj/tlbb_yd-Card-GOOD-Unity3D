using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CanEditMultipleObjects]
[CustomEditor(typeof(tk2dClippedSprite))]
class tk2dClippedSpriteEditor : tk2dSpriteEditor
{
	tk2dClippedSprite[] targetClippedSprites = new tk2dClippedSprite[0];

	new void OnEnable() {
		base.OnEnable();
		targetClippedSprites = GetTargetsOfType<tk2dClippedSprite>( targets );
	}

	public override void OnInspectorGUI()
    {
        tk2dClippedSprite sprite = (tk2dClippedSprite)target;
		base.OnInspectorGUI();

		bool newCreateBoxCollider = EditorGUILayout.Toggle("Create Box Collider", sprite.CreateBoxCollider);
		if (newCreateBoxCollider != sprite.CreateBoxCollider) {
			Undo.RegisterUndo(targetClippedSprites, "Create Box Collider");
			sprite.CreateBoxCollider = newCreateBoxCollider;
		}

		Rect newClipRect = EditorGUILayout.RectField("Clip Region", sprite.ClipRect);
		if (newClipRect != sprite.ClipRect) {
			Undo.RegisterUndo(targetClippedSprites, "Clipped Sprite Rect");
			foreach (tk2dClippedSprite spr in targetClippedSprites) {
				spr.ClipRect = newClipRect;
			}
		}
		EditorGUI.indentLevel--;

		if (GUI.changed) {
			foreach (tk2dClippedSprite spr in targetClippedSprites) {
				EditorUtility.SetDirty(spr);
			}
		}
    }

    [MenuItem("GameObject/Create Other/tk2d/Clipped Sprite", false, 12901)]
    static void DoCreateClippedSpriteObject()
    {
		tk2dSpriteCollectionData sprColl = null;
		if (sprColl == null)
		{
			// try to inherit from other Sprites in scene
			tk2dSprite spr = GameObject.FindObjectOfType(typeof(tk2dSprite)) as tk2dSprite;
			if (spr)
			{
				sprColl = spr.Collection;
			}
		}

		if (sprColl == null)
		{
			tk2dSpriteCollectionIndex[] spriteCollections = tk2dEditorUtility.GetOrCreateIndex().GetSpriteCollectionIndex();
			foreach (var v in spriteCollections)
			{
				GameObject scgo = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(v.spriteCollectionDataGUID), typeof(GameObject)) as GameObject;
				var sc = scgo.GetComponent<tk2dSpriteCollectionData>();
				if (sc != null && sc.spriteDefinitions != null && sc.spriteDefinitions.Length > 0)
				{
					sprColl = sc;
					break;
				}
			}

			if (sprColl == null)
			{
				EditorUtility.DisplayDialog("Create Clipped Sprite", "Unable to create clipped sprite as no SpriteCollections have been found.", "Ok");
				return;
			}
		}

		GameObject go = tk2dEditorUtility.CreateGameObjectInScene("Clipped Sprite");
		tk2dClippedSprite sprite = go.AddComponent<tk2dClippedSprite>();
		sprite.SwitchCollectionAndSprite(sprColl, sprColl.FirstValidDefinitionIndex);
		sprite.Build();

		Selection.activeGameObject = go;
		Undo.RegisterCreatedObjectUndo(go, "Create Clipped Sprite");
    }
}


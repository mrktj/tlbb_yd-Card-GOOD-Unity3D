using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CanEditMultipleObjects]
[CustomEditor(typeof(tk2dAnimatedSprite))]
class tk2dAnimatedSpriteEditor : tk2dSpriteEditor
{
	tk2dGenericIndexItem[] animLibs = null;
	string[] animLibNames = null;
	bool initialized = false;

	tk2dAnimatedSprite[] targetAnimSprites = new tk2dAnimatedSprite[0];
	
	new void OnEnable() {
		base.OnEnable();
		targetAnimSprites = GetTargetsOfType<tk2dAnimatedSprite>( targets );
	}

	void Init()
	{
		if (!initialized)
		{
			animLibs = tk2dEditorUtility.GetOrCreateIndex().GetSpriteAnimations();
			if (animLibs != null)
			{
				animLibNames = new string[animLibs.Length];
				for (int i = 0; i < animLibs.Length; ++i)
				{
					animLibNames[i] = animLibs[i].AssetName;
				}
			}
			initialized = true;
		}
	}
	
	static bool spriteUiVisible = false;
    public override void OnInspectorGUI()
    {
		spriteUiVisible = EditorGUILayout.Foldout(spriteUiVisible, "Sprite");
		if (spriteUiVisible)
			base.OnInspectorGUI();
		
		Init();
		if (animLibs == null)
		{
			GUILayout.Label("no libraries found");
			if (GUILayout.Button("Refresh"))
			{
				initialized = false;
				Init();
			}
		}
		else
		{
	        tk2dAnimatedSprite sprite = (tk2dAnimatedSprite)target;
			
			EditorGUIUtility.LookLikeInspector();
			EditorGUI.indentLevel = 1;

			if (sprite.anim == null)
			{
				sprite.anim = animLibs[0].GetAsset<tk2dSpriteAnimation>();
				GUI.changed = true;
			}
			
			// Display animation library
			int selAnimLib = 0;
			string selectedGUID = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(sprite.anim));
			for (int i = 0; i < animLibs.Length; ++i)
			{
				if (animLibs[i].assetGUID == selectedGUID)
				{
					selAnimLib = i;
					break;
				}
			}
		
			int newAnimLib = EditorGUILayout.Popup("Anim Lib", selAnimLib, animLibNames);
			if (newAnimLib != selAnimLib)
			{
				Undo.RegisterUndo(targetAnimSprites, "Sprite Anim Lib");
				foreach (tk2dAnimatedSprite spr in targetAnimSprites) {
					spr.anim = animLibs[newAnimLib].GetAsset<tk2dSpriteAnimation>();
					spr.clipId = 0;
					
					if (spr.anim.clips.Length > 0)
					{
						// automatically switch to the first frame of the new clip
						spr.SwitchCollectionAndSprite(spr.anim.clips[spr.clipId].frames[0].spriteCollection,
						                              spr.anim.clips[spr.clipId].frames[0].spriteId);
					}
				}
			}
			
			// Everything else
			if (sprite.anim && sprite.anim.clips.Length > 0)
			{
				int clipId = sprite.clipId;

				// Sanity check clip id
				clipId = Mathf.Clamp(clipId, 0, sprite.anim.clips.Length - 1);
				if (clipId != sprite.clipId)
				{
					sprite.clipId = clipId;
					GUI.changed = true;
				}
				
				string[] clipNames = new string[sprite.anim.clips.Length];
				// fill names (with ids if necessary)
				if (tk2dPreferences.inst.showIds)
				{
					for (int i = 0; i < sprite.anim.clips.Length; ++i)
					{
						if (sprite.anim.clips[i].name != null && sprite.anim.clips[i].name.Length > 0)
							clipNames[i] = sprite.anim.clips[i].name + "\t[" + i.ToString() + "]";
						else
							clipNames[i] = sprite.anim.clips[i].name;
					}
				}
				else
				{
					for (int i = 0; i < sprite.anim.clips.Length; ++i)
						clipNames[i] = sprite.anim.clips[i].name;
				}
				
				int newClipId = EditorGUILayout.Popup("Clip", sprite.clipId, clipNames);
				if (newClipId != sprite.clipId)
				{
					Undo.RegisterUndo(targetAnimSprites, "Sprite Anim Clip");
					foreach (tk2dAnimatedSprite spr in targetAnimSprites) {
						spr.clipId = newClipId;
						// automatically switch to the first frame of the new clip
						spr.SwitchCollectionAndSprite(spr.anim.clips[spr.clipId].frames[0].spriteCollection,
						                              spr.anim.clips[spr.clipId].frames[0].spriteId);
					}
				}
			}

			// Play automatically
			bool newPlayAutomatically = EditorGUILayout.Toggle("Play automatically", sprite.playAutomatically);
			if (newPlayAutomatically != sprite.playAutomatically) {
				Undo.RegisterUndo(targetAnimSprites, "Sprite Anim Play Automatically");
				foreach (tk2dAnimatedSprite spr in targetAnimSprites) {
					spr.playAutomatically = newPlayAutomatically;
				}
			}

			bool newCreateCollider = EditorGUILayout.Toggle("Create collider", sprite.createCollider);
			if (newCreateCollider != sprite.createCollider)
			{
				Undo.RegisterUndo(targetAnimSprites, "Sprite Anim Create Collider");
				foreach (tk2dAnimatedSprite spr in targetAnimSprites) {
					spr.createCollider = newCreateCollider;
					spr.EditMode__CreateCollider();
				}
			}
			
			if (GUI.changed)
			{
				foreach (tk2dAnimatedSprite spr in targetAnimSprites) {
					EditorUtility.SetDirty(spr);
				}
			}
		}
    }

    [MenuItem("GameObject/Create Other/tk2d/Animated Sprite", false, 12901)]
    static void DoCreateSpriteObject()
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
				EditorUtility.DisplayDialog("Create Sprite", "Unable to create sprite as no SpriteCollections have been found.", "Ok");
				return;
			}
		}		
		
		tk2dGenericIndexItem[] animIndex = tk2dEditorUtility.GetOrCreateIndex().GetSpriteAnimations();
		tk2dSpriteAnimation anim = null;
		int clipId = -1;
		foreach (var animIndexItem in animIndex)
		{
			tk2dSpriteAnimation a = animIndexItem.GetAsset<tk2dSpriteAnimation>();
			if (a != null && a.clips != null && a.clips.Length > 0)
			{
				for (int i = 0; i < a.clips.Length; ++i) {
					if (!a.clips[i].Empty &&
						a.clips[i].frames[0].spriteCollection != null &&
						a.clips[i].frames[0].spriteId >= 0) {
						clipId = i;
						break;
					}
				}

				if (clipId != -1) {
					anim = a;
					break;
				}
			}
		}
		
		if (anim == null || clipId == -1)
		{
			EditorUtility.DisplayDialog("Create Animated Sprite", "Unable to create animated sprite as no SpriteAnimations have been found.", "Ok");
			return;
		}
		
		GameObject go = tk2dEditorUtility.CreateGameObjectInScene("AnimatedSprite");
		tk2dAnimatedSprite sprite = go.AddComponent<tk2dAnimatedSprite>();
		sprite.SwitchCollectionAndSprite(anim.clips[clipId].frames[0].spriteCollection, anim.clips[clipId].frames[0].spriteId);
		sprite.anim = anim;
		sprite.clipId = clipId;
		sprite.Build();
		
		Selection.activeGameObject = go;
		Undo.RegisterCreatedObjectUndo(go, "Create AnimatedSprite");
    }
}


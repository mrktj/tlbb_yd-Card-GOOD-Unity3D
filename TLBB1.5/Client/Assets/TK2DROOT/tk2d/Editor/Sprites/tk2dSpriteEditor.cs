using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CanEditMultipleObjects]
[CustomEditor(typeof(tk2dSprite))]
class tk2dSpriteEditor : Editor
{
	tk2dSpriteThumbnailCache thumbnailCache;
	
	// Serialized properties are going to be far too much hassle
	private tk2dBaseSprite[] targetSprites = new tk2dBaseSprite[0];

    public override void OnInspectorGUI()
    {
		DrawSpriteEditorGUI();
    }

    protected T[] GetTargetsOfType<T>( Object[] objects ) where T : UnityEngine.Object {
    	List<T> ts = new List<T>();
    	foreach (Object o in targets) {
    		T s = o as T;
    		if (s != null)
    			ts.Add(s);
    	}
    	return ts.ToArray();
    }

    protected void OnEnable()
    {
    	thumbnailCache = new tk2dSpriteThumbnailCache();

    	List<tk2dBaseSprite> sprites = new List<tk2dBaseSprite>();
    	foreach (Object o in targets) {
    		tk2dBaseSprite s = o as tk2dBaseSprite;
    		if (s != null)
    			sprites.Add(s);
    	}
    	targetSprites = GetTargetsOfType<tk2dBaseSprite>( targets );
    }
	
	void OnDestroy()
	{
		targetSprites = new tk2dBaseSprite[0];
		thumbnailCache.Destroy();
		tk2dGrid.Done();
	}
	
	// Callback and delegate
	void SpriteChangedCallbackImpl(tk2dSpriteCollectionData spriteCollection, int spriteId, object data)
	{
		Undo.RegisterUndo(targetSprites, "Sprite Change");
		foreach (tk2dBaseSprite s in targetSprites) {
			s.SwitchCollectionAndSprite(spriteCollection, spriteId);
			s.EditMode__CreateCollider();
			EditorUtility.SetDirty(s);
		}
	}
	tk2dSpriteGuiUtility.SpriteChangedCallback _spriteChangedCallbackInstance = null;
	tk2dSpriteGuiUtility.SpriteChangedCallback spriteChangedCallbackInstance {
		get {
			if (_spriteChangedCallbackInstance == null) {
				_spriteChangedCallbackInstance = new tk2dSpriteGuiUtility.SpriteChangedCallback( SpriteChangedCallbackImpl );
			}
			return _spriteChangedCallbackInstance;
		}
	}

	protected void DrawSpriteEditorGUI()
	{
		Event ev = Event.current;
		tk2dSpriteGuiUtility.SpriteSelector( targetSprites[0].Collection, targetSprites[0].spriteId, spriteChangedCallbackInstance, null );

        if (targetSprites[0].Collection != null)
        {
        	if (tk2dPreferences.inst.displayTextureThumbs) {
        		tk2dBaseSprite sprite = targetSprites[0];
				tk2dSpriteDefinition def = sprite.GetCurrentSpriteDef();
				if (sprite.Collection.version < 1 || def.texelSize == Vector2.zero)
				{
					string message = "";
					
					message = "No thumbnail data.";
					if (sprite.Collection.version < 1)
						message += "\nPlease rebuild Sprite Collection.";
					
					tk2dGuiUtility.InfoBox(message, tk2dGuiUtility.WarningLevel.Info);
				}
				else
				{
					GUILayout.BeginHorizontal();
					EditorGUILayout.PrefixLabel(" ");

					int tileSize = 128;
					Rect r = GUILayoutUtility.GetRect(tileSize, tileSize, GUILayout.ExpandWidth(false));
					tk2dGrid.Draw(r);
					thumbnailCache.DrawSpriteTextureInRect(r, def, Color.white);

					GUILayout.EndHorizontal();

					r = GUILayoutUtility.GetLastRect();
					if (ev.type == EventType.MouseDown && ev.button == 0 && r.Contains(ev.mousePosition)) {
						tk2dSpriteGuiUtility.SpriteSelectorPopup( sprite.Collection, sprite.spriteId, spriteChangedCallbackInstance, null );
					}
				}
			}

            Color newColor = EditorGUILayout.ColorField("Color", targetSprites[0].color);
            if (newColor != targetSprites[0].color) {
            	Undo.RegisterUndo(targetSprites, "Sprite Color");
            	foreach (tk2dBaseSprite s in targetSprites) {
            		s.color = newColor;
            	}
            }
			Vector3 newScale = EditorGUILayout.Vector3Field("Scale", targetSprites[0].scale);
			if (newScale != targetSprites[0].scale)
			{
				Undo.RegisterUndo(targetSprites, "Sprite Scale");
				foreach (tk2dBaseSprite s in targetSprites) {
					s.scale = newScale;
					s.EditMode__CreateCollider();
				}
			}
			
			EditorGUILayout.BeginHorizontal();
			
			if (GUILayout.Button("HFlip"))
			{
				Undo.RegisterUndo(targetSprites, "Sprite HFlip");
				foreach (tk2dBaseSprite sprite in targetSprites) {
					sprite.EditMode__CreateCollider();
					Vector3 scale = sprite.scale;
					scale.x *= -1.0f;
					sprite.scale = scale;
				}
				GUI.changed = true;
			}
			if (GUILayout.Button("VFlip"))
			{
				Undo.RegisterUndo(targetSprites, "Sprite VFlip");
				foreach (tk2dBaseSprite sprite in targetSprites) {
					Vector3 s = sprite.scale;
					s.y *= -1.0f;
					sprite.scale = s;
					GUI.changed = true;
				}
			}
			
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			
			if (GUILayout.Button(new GUIContent("Reset Scale", "Set scale to 1")))
			{
				Undo.RegisterUndo(targetSprites, "Sprite Reset Scale");
				foreach (tk2dBaseSprite sprite in targetSprites) {
					Vector3 s = sprite.scale;
					s.x = Mathf.Sign(s.x);
					s.y = Mathf.Sign(s.y);
					s.z = Mathf.Sign(s.z);
					sprite.scale = s;
					GUI.changed = true;
				}
			}
			
			if (GUILayout.Button(new GUIContent("Bake Scale", "Transfer scale from transform.scale -> sprite")))
			{
				Undo.RegisterSceneUndo("Bake Scale");
				foreach (tk2dBaseSprite sprite in targetSprites) {
					tk2dScaleUtility.Bake(sprite.transform);
				}
				GUI.changed = true;
			}
			
			GUIContent pixelPerfectButton = new GUIContent("1:1", "Make Pixel Perfect");
			if ( GUILayout.Button(pixelPerfectButton ))
			{
				if (tk2dPixelPerfectHelper.inst) tk2dPixelPerfectHelper.inst.Setup();
				Undo.RegisterUndo(targetSprites, "Sprite Pixel Perfect");
				foreach (tk2dBaseSprite sprite in targetSprites) {
					sprite.MakePixelPerfect();
				}
				GUI.changed = true;
			}
			
			bool newPixelPerfect = GUILayout.Toggle(targetSprites[0].pixelPerfect, new GUIContent("Always", "Always keep pixel perfect"), GUILayout.Width(60.0f));
			if (newPixelPerfect != targetSprites[0].pixelPerfect) {
				Undo.RegisterUndo(targetSprites, "Sprite Pixel Perfect");
				foreach (tk2dBaseSprite sprite in targetSprites) {
					sprite.pixelPerfect = newPixelPerfect;
				}
			}

			EditorGUILayout.EndHorizontal();
        }
        else
        {
            EditorGUILayout.IntSlider("Need a collection bound", 0, 0, 1);
        }
		
		bool needUpdatePrefabs = false;
		if (GUI.changed)
		{
			foreach (tk2dBaseSprite sprite in targetSprites) {
#if !(UNITY_3_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4)
			if (PrefabUtility.GetPrefabType(sprite) == PrefabType.Prefab)
				needUpdatePrefabs = true;
#endif
				EditorUtility.SetDirty(sprite);
			}
		}
		
		// This is a prefab, and changes need to be propagated. This isn't supported in Unity 3.4
		if (needUpdatePrefabs)
		{
#if !(UNITY_3_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4)
			// Rebuild prefab instances
			tk2dBaseSprite[] allSprites = Resources.FindObjectsOfTypeAll(typeof(tk2dBaseSprite)) as tk2dBaseSprite[];
			foreach (var spr in allSprites)
			{
				if (PrefabUtility.GetPrefabType(spr) == PrefabType.PrefabInstance)
				{
					Object parent = PrefabUtility.GetPrefabParent(spr.gameObject);
					bool found = false;
					foreach (tk2dBaseSprite sprite in targetSprites) {
						if (sprite.gameObject == parent) {
							found = true;
							break;
						}
					}

					if (found) {
						// Reset all prefab states
						var propMod = PrefabUtility.GetPropertyModifications(spr);
						PrefabUtility.ResetToPrefabState(spr);
						PrefabUtility.SetPropertyModifications(spr, propMod);
						
						spr.ForceBuild();
					}
				}
			}
#endif
		}
	}
	
    [MenuItem("GameObject/Create Other/tk2d/Sprite", false, 12900)]
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
				if (sc != null && sc.spriteDefinitions != null && sc.spriteDefinitions.Length > 0 && !sc.managedSpriteCollection)
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

		GameObject go = tk2dEditorUtility.CreateGameObjectInScene("Sprite");
		tk2dSprite sprite = go.AddComponent<tk2dSprite>();
		sprite.SwitchCollectionAndSprite(sprColl, sprColl.FirstValidDefinitionIndex);
		sprite.renderer.material = sprColl.FirstValidDefinition.material;
		sprite.Build();
		
		Selection.activeGameObject = go;
		Undo.RegisterCreatedObjectUndo(go, "Create Sprite");
    }
}


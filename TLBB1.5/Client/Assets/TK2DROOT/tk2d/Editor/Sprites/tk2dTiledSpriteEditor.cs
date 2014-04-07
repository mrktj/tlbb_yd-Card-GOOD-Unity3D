using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(tk2dTiledSprite))]
class tk2dTiledSpriteEditor : tk2dSpriteEditor
{
	tk2dTiledSprite[] targetTiledSprites = new tk2dTiledSprite[0];

	new void OnEnable() {
		base.OnEnable();
		targetTiledSprites = GetTargetsOfType<tk2dTiledSprite>( targets );
	}

	public override void OnInspectorGUI()
    {
        tk2dTiledSprite sprite = (tk2dTiledSprite)target;
		base.OnInspectorGUI();
		
		if (sprite.Collection == null)
			return;

		
		EditorGUILayout.BeginVertical();
		
		var spriteData = sprite.GetCurrentSpriteDef();
		
		// need raw extents (excluding scale)
		Vector3 extents = spriteData.boundsData[1];

		bool newCreateBoxCollider = EditorGUILayout.Toggle("Create Box Collider", sprite.CreateBoxCollider);
		if (newCreateBoxCollider != sprite.CreateBoxCollider) {
			Undo.RegisterUndo(targetTiledSprites, "Create Box Collider");
			sprite.CreateBoxCollider = newCreateBoxCollider;
		}
		
		// if either of these are zero, the division to rescale to pixels will result in a
		// div0, so display the data in fractions to avoid this situation
		bool editBorderInFractions = true;
		if (spriteData.texelSize.x != 0.0f && spriteData.texelSize.y != 0.0f && extents.x != 0.0f && extents.y != 0.0f) {
			editBorderInFractions = false;
		}
		
		if (!editBorderInFractions)
		{
			Vector2 newDimensions = EditorGUILayout.Vector2Field("Dimensions (Pixel Units)", sprite.dimensions);
			if (newDimensions != sprite.dimensions) {
				Undo.RegisterUndo(targetTiledSprites, "Tiled Sprite Dimensions");
				foreach (tk2dTiledSprite spr in targetTiledSprites) {
					spr.dimensions = newDimensions;
				}
			}
			
			tk2dTiledSprite.Anchor newAnchor = (tk2dTiledSprite.Anchor)EditorGUILayout.EnumPopup("Anchor", sprite.anchor);
			if (newAnchor != sprite.anchor) {
				Undo.RegisterUndo(targetTiledSprites, "Tiled Sprite Anchor");
				foreach (tk2dTiledSprite spr in targetTiledSprites) {
					spr.anchor = newAnchor;
				}
			}
		}
		else
		{
			GUILayout.Label("Border (Displayed as Fraction).\nSprite Collection needs to be rebuilt.", "textarea");
		}

		Mesh mesh = sprite.GetComponent<MeshFilter>().sharedMesh;
		if (mesh != null) {
			GUILayout.Label(string.Format("Triangles: {0}", mesh.triangles.Length / 3));
		}

		// One of the border valus has changed, so simply rebuild mesh data here		
		if (GUI.changed)
		{
			foreach (tk2dTiledSprite spr in targetTiledSprites) {
				spr.Build();
				EditorUtility.SetDirty(spr);
			}
		}

		EditorGUILayout.EndVertical();
    }


    [MenuItem("GameObject/Create Other/tk2d/Tiled Sprite", false, 12901)]
    static void DoCreateSlicedSpriteObject()
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
				EditorUtility.DisplayDialog("Create Tiled Sprite", "Unable to create tiled sprite as no SpriteCollections have been found.", "Ok");
				return;
			}
		}

		GameObject go = tk2dEditorUtility.CreateGameObjectInScene("Tiled Sprite");
		tk2dTiledSprite sprite = go.AddComponent<tk2dTiledSprite>();
		sprite.SetSprite(sprColl, sprColl.FirstValidDefinitionIndex);
		sprite.Build();
		Selection.activeGameObject = go;
		Undo.RegisterCreatedObjectUndo(go, "Create Tiled Sprite");
    }
}


using UnityEngine;
using System.Collections;

[AddComponentMenu("2D Toolkit/Sprite/tk2dTiledSprite")]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[ExecuteInEditMode]
/// <summary>
/// Sprite implementation that implements 9-slice scaling. Doesn't support diced sprites.
/// The interface takes care of sprite unit conversions for border[Top|Bottom|Left|Right]
/// </summary>
public class tk2dTiledSprite : tk2dBaseSprite
{
	Mesh mesh;
	Vector2[] meshUvs;
	Vector3[] meshVertices;
	Color32[] meshColors;
	int[] meshIndices;
	
	[SerializeField]
	Vector2 _dimensions = new Vector2(50.0f, 50.0f);
	[SerializeField]
	Anchor _anchor = Anchor.LowerLeft;
	
	/// <summary>
	/// Gets or sets the dimensions.
	/// </summary>
	/// <value>
	/// Use this to change the dimensions of the sliced sprite in pixel units
	/// </value>
	public Vector2 dimensions
	{ 
		get { return _dimensions; } 
		set
		{
			if (value != _dimensions)
			{
				_dimensions = value;
				UpdateVertices();
#if UNITY_EDITOR
				EditMode__CreateCollider();
#endif
				UpdateCollider();
			}
		}
	}
	
	/// <summary>
	/// The anchor position for this tiled sprite
	/// </summary>
	public Anchor anchor
	{
		get { return _anchor; }
		set
		{
			if (value != _anchor)
			{
				_anchor = value;
				UpdateVertices();
#if UNITY_EDITOR
				EditMode__CreateCollider();
#endif
				UpdateCollider();
			}
		}
	}

	[SerializeField]
	protected bool _createBoxCollider = false;

	/// <summary>
	/// Create a trimmed box collider for this sprite
	/// </summary>
	public bool CreateBoxCollider {
		get { return _createBoxCollider; }
		set {
			if (_createBoxCollider != value) {
				_createBoxCollider = value;
				UpdateCollider();
			}
		}
	}
	
	new void Awake()
	{
		base.Awake();
		
		// Create mesh, independently to everything else
		mesh = new Mesh();
		mesh.hideFlags = HideFlags.DontSave;
		GetComponent<MeshFilter>().mesh = mesh;
		
		// This will not be set when instantiating in code
		// In that case, Build will need to be called
		if (Collection)
		{
			// reset spriteId if outside bounds
			// this is when the sprite collection data is corrupt
			if (_spriteId < 0 || _spriteId >= Collection.Count)
				_spriteId = 0;
			
			Build();
			
			if (boxCollider == null)
				boxCollider = GetComponent<BoxCollider>();
		}
	}
	
	protected void OnDestroy()
	{
		if (mesh)
		{
#if UNITY_EDITOR
			DestroyImmediate(mesh);
#else
			Destroy(mesh);
#endif
		}
	}
	
	new protected void SetColors(Color32[] dest)
	{
		Color c = _color;
        if (collectionInst.premultipliedAlpha) { c.r *= c.a; c.g *= c.a; c.b *= c.a; }
        Color32 c32 = c;
		for (int i = 0; i < dest.Length; ++i)
			dest[i] = c32;
	}
	
	// Calculated center and extents
	Vector3 boundsCenter = Vector3.zero, boundsExtents = Vector3.zero;
	
	public override void Build()
	{
		var sprite = collectionInst.spriteDefinitions[spriteId];
		int numTilesX = (int)Mathf.Ceil( (_dimensions.x * sprite.texelSize.x) / sprite.untrimmedBoundsData[1].x );
		int numTilesY = (int)Mathf.Ceil( (_dimensions.y * sprite.texelSize.y) / sprite.untrimmedBoundsData[1].y );
		int numVertices = numTilesX * numTilesY * 4;
		int numIndices = numTilesX * numTilesY * 6;
		Vector2 totalMeshSize = new Vector2( _dimensions.x * sprite.texelSize.x * _scale.x, _dimensions.y * sprite.texelSize.y * _scale.y );

		if (meshUvs == null || meshUvs.Length != numVertices) {
			meshUvs = new Vector2[numVertices];
			meshVertices = new Vector3[numVertices];
			meshColors = new Color32[numVertices];
		}
		if (meshIndices == null || meshIndices.Length != numIndices) {
			meshIndices = new int[numIndices];
		}

		int baseIndex = 0;
		for (int i = 0; i < numIndices; i += 6) {
			meshIndices[i + 0] = sprite.indices[0] + baseIndex;
			meshIndices[i + 1] = sprite.indices[1] + baseIndex;
			meshIndices[i + 2] = sprite.indices[2] + baseIndex;
			meshIndices[i + 3] = sprite.indices[3] + baseIndex;
			meshIndices[i + 4] = sprite.indices[4] + baseIndex;
			meshIndices[i + 5] = sprite.indices[5] + baseIndex;
			baseIndex += 4;
		}

		// Anchor tweaks
		Vector3 anchorOffset = Vector3.zero;
		switch (anchor)
		{
		case Anchor.LowerLeft: case Anchor.MiddleLeft: case Anchor.UpperLeft: 
			break;
		case Anchor.LowerCenter: case Anchor.MiddleCenter: case Anchor.UpperCenter: 
			anchorOffset.x = -(totalMeshSize.x / 2.0f); break;
		case Anchor.LowerRight: case Anchor.MiddleRight: case Anchor.UpperRight: 
			anchorOffset.x = -(totalMeshSize.x); break;
		}
		switch (anchor)
		{
		case Anchor.LowerLeft: case Anchor.LowerCenter: case Anchor.LowerRight:
			break;
		case Anchor.MiddleLeft: case Anchor.MiddleCenter: case Anchor.MiddleRight:
			anchorOffset.y = -(totalMeshSize.y / 2.0f); break;
		case Anchor.UpperLeft: case Anchor.UpperCenter: case Anchor.UpperRight:
			anchorOffset.y = -totalMeshSize.y; break;
		}
		Vector3 colliderAnchor = anchorOffset;
		anchorOffset -= Vector3.Scale( sprite.positions[0], _scale );

		float colliderOffsetZ = ( boxCollider != null ) ? ( boxCollider.center.z ) : 0.0f;
		float colliderExtentZ = ( boxCollider != null ) ? ( boxCollider.size.z * 0.5f ) : 0.5f;
		boundsCenter.Set(totalMeshSize.x * 0.5f + colliderAnchor.x, totalMeshSize.y * 0.5f + colliderAnchor.y, colliderOffsetZ );
		boundsExtents.Set(totalMeshSize.x * 0.5f, totalMeshSize.y * 0.5f, colliderExtentZ);

		int vert = 0;
		Vector3 bounds = Vector3.Scale(  sprite.untrimmedBoundsData[1], _scale );
		Vector3 baseOffset = Vector3.zero;
		Vector3 offset = baseOffset;
		for (int y = 0; y < numTilesY; ++y) {
			offset.x = baseOffset.x;
			for (int x = 0; x < numTilesX; ++x) {
				float xClipFrac = 1;
				float yClipFrac = 1;
				if (Mathf.Abs(offset.x + bounds.x) > Mathf.Abs(totalMeshSize.x) ) {
					xClipFrac = ((totalMeshSize.x % bounds.x) / bounds.x);
				}
				if (Mathf.Abs(offset.y + bounds.y) > Mathf.Abs(totalMeshSize.y)) {
					yClipFrac = ((totalMeshSize.y % bounds.y) / bounds.y);
				}

				Vector3 geomOffset = offset + anchorOffset;

				if (xClipFrac != 1 || yClipFrac != 1) {
					Vector2 fracBottomLeft = Vector2.zero;
					Vector2 fracTopRight = new Vector2(xClipFrac, yClipFrac);

					Vector3 bottomLeft = new Vector3(Mathf.Lerp(sprite.positions[0].x, sprite.positions[3].x, fracBottomLeft.x) * _scale.x,
												  	 Mathf.Lerp(sprite.positions[0].y, sprite.positions[3].y, fracBottomLeft.y) * _scale.y,
												 	 sprite.positions[0].z * _scale.z);
					Vector3 topRight = new Vector3(Mathf.Lerp(sprite.positions[0].x, sprite.positions[3].x, fracTopRight.x) * _scale.x,
												   Mathf.Lerp(sprite.positions[0].y, sprite.positions[3].y, fracTopRight.y) * _scale.y,
												   sprite.positions[0].z * _scale.z);

					meshVertices[vert + 0] = geomOffset + new Vector3(bottomLeft.x, bottomLeft.y, bottomLeft.z);
					meshVertices[vert + 1] = geomOffset + new Vector3(topRight.x, bottomLeft.y, bottomLeft.z);
					meshVertices[vert + 2] = geomOffset + new Vector3(bottomLeft.x, topRight.y, bottomLeft.z);
					meshVertices[vert + 3] = geomOffset + new Vector3(topRight.x, topRight.y, bottomLeft.z);

					// find the fraction of UV
					// This can be done without a branch, but will end up with loads of unnecessary interpolations
					if (sprite.flipped == tk2dSpriteDefinition.FlipMode.Tk2d)
					{
						Vector2 v0 = new Vector2(Mathf.Lerp(sprite.uvs[0].x, sprite.uvs[3].x, fracBottomLeft.y),
		  									     Mathf.Lerp(sprite.uvs[0].y, sprite.uvs[3].y, fracBottomLeft.x));
						Vector2 v1 = new Vector2(Mathf.Lerp(sprite.uvs[0].x, sprite.uvs[3].x, fracTopRight.y),
												 Mathf.Lerp(sprite.uvs[0].y, sprite.uvs[3].y, fracTopRight.x));
						
						meshUvs[vert + 0] = new Vector2(v0.x, v0.y);
						meshUvs[vert + 1] = new Vector2(v0.x, v1.y);
						meshUvs[vert + 2] = new Vector2(v1.x, v0.y);
						meshUvs[vert + 3] = new Vector2(v1.x, v1.y);
					}
					else if (sprite.flipped == tk2dSpriteDefinition.FlipMode.TPackerCW)
					{
						Vector2 v0 = new Vector2(Mathf.Lerp(sprite.uvs[0].x, sprite.uvs[3].x, fracBottomLeft.y),
												 Mathf.Lerp(sprite.uvs[0].y, sprite.uvs[3].y, fracBottomLeft.x));
						Vector2 v1 = new Vector2(Mathf.Lerp(sprite.uvs[0].x, sprite.uvs[3].x, fracTopRight.y),
												 Mathf.Lerp(sprite.uvs[0].y, sprite.uvs[3].y, fracTopRight.x));
			
						meshUvs[vert + 0] = new Vector2(v0.x, v0.y);
						meshUvs[vert + 2] = new Vector2(v1.x, v0.y);
						meshUvs[vert + 1] = new Vector2(v0.x, v1.y);
						meshUvs[vert + 3] = new Vector2(v1.x, v1.y);
					}
					else
					{
						Vector2 v0 = new Vector2(Mathf.Lerp(sprite.uvs[0].x, sprite.uvs[3].x, fracBottomLeft.x),
												 Mathf.Lerp(sprite.uvs[0].y, sprite.uvs[3].y, fracBottomLeft.y));
						Vector2 v1 = new Vector2(Mathf.Lerp(sprite.uvs[0].x, sprite.uvs[3].x, fracTopRight.x),
												 Mathf.Lerp(sprite.uvs[0].y, sprite.uvs[3].y, fracTopRight.y));
			
						meshUvs[vert + 0] = new Vector2(v0.x, v0.y);
						meshUvs[vert + 1] = new Vector2(v1.x, v0.y);
						meshUvs[vert + 2] = new Vector2(v0.x, v1.y);
						meshUvs[vert + 3] = new Vector2(v1.x, v1.y);
					}
				}
				else {
					meshVertices[vert + 0] = geomOffset + Vector3.Scale( sprite.positions[0], _scale );
					meshVertices[vert + 1] = geomOffset + Vector3.Scale( sprite.positions[1], _scale );
					meshVertices[vert + 2] = geomOffset + Vector3.Scale( sprite.positions[2], _scale );
					meshVertices[vert + 3] = geomOffset + Vector3.Scale( sprite.positions[3], _scale );
					meshUvs[vert + 0] = sprite.uvs[0];
					meshUvs[vert + 1] = sprite.uvs[1];
					meshUvs[vert + 2] = sprite.uvs[2];
					meshUvs[vert + 3] = sprite.uvs[3];
				}

				vert += 4;
				offset.x += bounds.x;
			}
			offset.y += bounds.y;
		}
		
		SetColors(meshColors);
		
		if (mesh == null)
		{
			mesh = new Mesh();
			mesh.hideFlags = HideFlags.DontSave;
		}
		else
		{
			mesh.Clear();
		}
		mesh.vertices = meshVertices;
		mesh.colors32 = meshColors;
		mesh.uv = meshUvs;
		mesh.triangles = meshIndices;
		mesh.RecalculateBounds();
		
		GetComponent<MeshFilter>().mesh = mesh;
		
		UpdateCollider();
		UpdateMaterial();
	}
	
	protected override void UpdateGeometry() { UpdateGeometryImpl(); }
	protected override void UpdateColors() { UpdateColorsImpl(); }
	protected override void UpdateVertices() { UpdateGeometryImpl(); }
	
	
	protected void UpdateColorsImpl()
	{
#if UNITY_EDITOR
		// This can happen with prefabs in the inspector
		if (meshColors == null || meshColors.Length == 0)
			return;
#endif
		if (meshColors == null || meshColors.Length == 0) {
			Build();
		}
		else {
			SetColors(meshColors);
			mesh.colors32 = meshColors;
		}
	}

	protected void UpdateGeometryImpl()
	{
#if UNITY_EDITOR
		// This can happen with prefabs in the inspector
		if (mesh == null)
			return;
#endif
		Build();
	}
	
#region Collider
	protected override void UpdateCollider()
	{
		if (CreateBoxCollider) {
			if (boxCollider == null) {
				boxCollider = GetComponent<BoxCollider>();
				if (boxCollider == null) {
					boxCollider = gameObject.AddComponent<BoxCollider>();
				}
			}
			boxCollider.extents = boundsExtents;
			boxCollider.center = boundsCenter;
		} else {
#if UNITY_EDITOR
			boxCollider = GetComponent<BoxCollider>();
			if (boxCollider != null) {
				DestroyImmediate(boxCollider);
			}
#else
			if (boxCollider != null) {
				Destroy(boxCollider);
			}
#endif
		}
	}

	protected override void CreateCollider() {
		UpdateCollider();
	}

#if UNITY_EDITOR
	public override void EditMode__CreateCollider() {
		UpdateCollider();
	}
#endif
#endregion	
	
	protected override void UpdateMaterial()
	{
		if (renderer.sharedMaterial != collectionInst.spriteDefinitions[spriteId].materialInst)
			renderer.material = collectionInst.spriteDefinitions[spriteId].materialInst;
	}
	
	protected override int GetCurrentVertexCount()
	{
#if UNITY_EDITOR
		if (meshVertices == null)
			return 0;
#endif
		return 16;
	}
}

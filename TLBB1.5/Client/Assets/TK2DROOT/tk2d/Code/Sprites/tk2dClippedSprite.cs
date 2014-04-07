using UnityEngine;
using System.Collections;

[AddComponentMenu("2D Toolkit/Sprite/Clipped Sprite")]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[ExecuteInEditMode]
public class tk2dClippedSprite : tk2dBaseSprite
{
	Mesh mesh;
	Vector2[] meshUvs;
	Vector3[] meshVertices;
	Color32[] meshColors;
	int[] meshIndices;
	
	public Vector2 _clipBottomLeft = new Vector2(0, 0);
	public Vector2 _clipTopRight = new Vector2(1, 1);

	// Temp cached variables
	Rect _clipRect = new Rect(0, 0, 0, 0);

	/// <summary>
	/// Sets the clip rectangle
	/// 0, 0, 1, 1 = display the entire sprite
	/// </summary>
	public Rect ClipRect {
		get {
			_clipRect.Set( _clipBottomLeft.x, _clipBottomLeft.y, _clipTopRight.x - _clipBottomLeft.x, _clipTopRight.y - _clipBottomLeft.y );
			return _clipRect;
		}
		set {
			Vector2 v = new Vector2( value.x, value.y );
			clipBottomLeft = v;
			v.x += value.width;
			v.y += value.height;
			clipTopRight = v;
		}
	}
	
	
	/// <summary>
	/// Sets the bottom left clip area.
	/// 0, 0 = display full sprite
	/// </summary>
	public Vector2 clipBottomLeft
	{
		get { return _clipBottomLeft; }
		set 
		{ 
			if (value != _clipBottomLeft) 
			{
				_clipBottomLeft = new Vector2(value.x, value.y);
				Build();
				UpdateCollider();
			}
		}
	}

	/// <summary>
	/// Sets the top right clip area
	/// 1, 1 = display full sprite
	/// </summary>
	public Vector2 clipTopRight
	{
		get { return _clipTopRight; }
		set 
		{ 
			if (value != _clipTopRight) 
			{
				_clipTopRight = new Vector2(value.x, value.y);
				Build();
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

	protected void SetGeometry(Vector3[] vertices, Vector2[] uvs)
	{
		var sprite = collectionInst.spriteDefinitions[spriteId];
		
		// Only do this when there are exactly 4 polys to a sprite (i.e. the sprite isn't diced, and isnt a more complex mesh)
		if (sprite.positions.Length == 4)
		{
			// This is how the default quad is set up
			// Indices are 0, 3, 1, 2, 3, 0
			
			// 2--------3
			// |        |
			// |        |
			// |        |
			// |        |
			// 0--------1
			
			// index 0 = top left
			// index 3 = bottom right
			
			// clipBottomLeft is the fraction to start from the bottom left (0,0 - full sprite)
			// clipTopRight is the fraction to start from the top right (1,1 - full sprite)
			Vector2 fracBottomLeft = new Vector2( Mathf.Clamp01( _clipBottomLeft.x ), Mathf.Clamp01( _clipBottomLeft.y ) );
			Vector2 fracTopRight = new Vector2( Mathf.Clamp01( _clipTopRight.x ), Mathf.Clamp01( _clipTopRight.y ) );

			// find the fraction of positions, but fold in the scale multiply as well
			Vector3 bottomLeft = new Vector3(Mathf.Lerp(sprite.positions[0].x, sprite.positions[3].x, fracBottomLeft.x) * _scale.x,
										  	 Mathf.Lerp(sprite.positions[0].y, sprite.positions[3].y, fracBottomLeft.y) * _scale.y,
										 	 sprite.positions[0].z * _scale.z);
			Vector3 topRight = new Vector3(Mathf.Lerp(sprite.positions[0].x, sprite.positions[3].x, fracTopRight.x) * _scale.x,
										   Mathf.Lerp(sprite.positions[0].y, sprite.positions[3].y, fracTopRight.y) * _scale.y,
										   sprite.positions[0].z * _scale.z);

			float colliderOffsetZ = ( boxCollider != null ) ? ( boxCollider.center.z ) : 0.0f;
			float colliderExtentZ = ( boxCollider != null ) ? ( boxCollider.size.z * 0.5f ) : 0.5f;
			boundsCenter.Set( bottomLeft.x + (topRight.x - bottomLeft.x) * 0.5f, bottomLeft.y + (topRight.y - bottomLeft.y) * 0.5f, colliderOffsetZ );
			boundsExtents.Set( (topRight.x - bottomLeft.x) * 0.5f, (topRight.y - bottomLeft.y) * 0.5f, colliderExtentZ );
			
			// The z component only needs to be consistent
			meshVertices[0] = new Vector3(bottomLeft.x, bottomLeft.y, bottomLeft.z);
			meshVertices[1] = new Vector3(topRight.x, bottomLeft.y, bottomLeft.z);
			meshVertices[2] = new Vector3(bottomLeft.x, topRight.y, bottomLeft.z);
			meshVertices[3] = new Vector3(topRight.x, topRight.y, bottomLeft.z);
			
			// find the fraction of UV
			// This can be done without a branch, but will end up with loads of unnecessary interpolations
			if (sprite.flipped == tk2dSpriteDefinition.FlipMode.Tk2d)
			{
				Vector2 v0 = new Vector2(Mathf.Lerp(sprite.uvs[0].x, sprite.uvs[3].x, fracBottomLeft.y),
  									     Mathf.Lerp(sprite.uvs[0].y, sprite.uvs[3].y, fracBottomLeft.x));
				Vector2 v1 = new Vector2(Mathf.Lerp(sprite.uvs[0].x, sprite.uvs[3].x, fracTopRight.y),
										 Mathf.Lerp(sprite.uvs[0].y, sprite.uvs[3].y, fracTopRight.x));
				
				meshUvs[0] = new Vector2(v0.x, v0.y);
				meshUvs[1] = new Vector2(v0.x, v1.y);
				meshUvs[2] = new Vector2(v1.x, v0.y);
				meshUvs[3] = new Vector2(v1.x, v1.y);
			}
			else if (sprite.flipped == tk2dSpriteDefinition.FlipMode.TPackerCW)
			{
				Vector2 v0 = new Vector2(Mathf.Lerp(sprite.uvs[0].x, sprite.uvs[3].x, fracBottomLeft.y),
										 Mathf.Lerp(sprite.uvs[0].y, sprite.uvs[3].y, fracBottomLeft.x));
				Vector2 v1 = new Vector2(Mathf.Lerp(sprite.uvs[0].x, sprite.uvs[3].x, fracTopRight.y),
										 Mathf.Lerp(sprite.uvs[0].y, sprite.uvs[3].y, fracTopRight.x));
	
				meshUvs[0] = new Vector2(v0.x, v0.y);
				meshUvs[2] = new Vector2(v1.x, v0.y);
				meshUvs[1] = new Vector2(v0.x, v1.y);
				meshUvs[3] = new Vector2(v1.x, v1.y);
			}
			else
			{
				Vector2 v0 = new Vector2(Mathf.Lerp(sprite.uvs[0].x, sprite.uvs[3].x, fracBottomLeft.x),
										 Mathf.Lerp(sprite.uvs[0].y, sprite.uvs[3].y, fracBottomLeft.y));
				Vector2 v1 = new Vector2(Mathf.Lerp(sprite.uvs[0].x, sprite.uvs[3].x, fracTopRight.x),
										 Mathf.Lerp(sprite.uvs[0].y, sprite.uvs[3].y, fracTopRight.y));
	
				meshUvs[0] = new Vector2(v0.x, v0.y);
				meshUvs[1] = new Vector2(v1.x, v0.y);
				meshUvs[2] = new Vector2(v0.x, v1.y);
				meshUvs[3] = new Vector2(v1.x, v1.y);
			}
		}
		else
		{
			// Only supports normal sprites
			for (int i = 0; i < vertices.Length; ++i)
				vertices[i] = Vector3.zero;
		}
	}
	
	public override void Build()
	{
		meshUvs = new Vector2[4];
		meshVertices = new Vector3[4];
		meshColors = new Color32[4];
		
		SetGeometry(meshVertices, meshUvs);
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
		mesh.triangles = new int[6] { 0, 3, 1, 2, 3, 0 };
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
		if (meshVertices == null || meshVertices.Length == 0) {
			Build();
		}
		else {
			SetGeometry(meshVertices, meshUvs);
			mesh.vertices = meshVertices;
			mesh.uv = meshUvs;
			mesh.RecalculateBounds();
		}
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
		if (renderer.sharedMaterial != collectionInst.spriteDefinitions[spriteId].material)
			renderer.material = collectionInst.spriteDefinitions[spriteId].material;
	}
	
	protected override int GetCurrentVertexCount()
	{
#if UNITY_EDITOR
		if (meshVertices == null)
			return 0;
#endif
		return 4;
	}
}

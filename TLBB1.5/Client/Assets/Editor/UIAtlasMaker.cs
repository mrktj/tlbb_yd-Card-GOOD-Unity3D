using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public partial class UIAtlasMaker :EditorWindow{
	
	static public void UpdateAtlasEX (UIAtlas atlas, List<Texture> textures)
	{
		
		if (atlas != null && textures != null)
		{
			List<SpriteEntry> sprites = CreateSprites(textures);
			SpriteEntry[] TEMPSPS = sprites.ToArray();
			ExtractSprites(atlas, sprites);
			UpdateAtlas(atlas, sprites);
		}
	}
	
	static public List<UIAtlasMaker.SpriteEntry> CreateSpritesEX(List<Texture> temp){
		
		return  CreateSprites(temp);

	}
	
	static public void ReleaseSpritesEX (List<SpriteEntry> sprites){
		
		ReleaseSprites(sprites);
	}
}

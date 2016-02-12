using CatdogEngine.Playground.Object;
using CatdogEngine.Playground.Object.Component;
using Microsoft.Xna.Framework;

namespace MyGame
{
	public class GojamChan : Behavior
	{
		public GojamChan()
		{
			// 이미지를 붙이기 위한 SpriteRenderer
			SpriteRenderer renderer = new SpriteRenderer("Gojam_Chan_00");
			AddComponent(renderer);

			// 스케일
			Transform.Scale = new Vector2(0.5f, 0.5f);
		}

		public override void Start()
		{
			Transform.Velocity = new Vector2(-20f, 0f);
		}

		public override void Update(GameTime gameTime)
		{
			
		}
	}
}

using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;

namespace _Project.Develop.Runtime.Gameplay.Common
{
	public class TransformEntityRegistrator : MonoEntityRegistrator
	{
		public override void Register(Entity entity)
		{
			TransformComponent transformComponent = new TransformComponent();
			transformComponent.Value = transform;
			entity.AddComponent(transformComponent);
		}
	}
}
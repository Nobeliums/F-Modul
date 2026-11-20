using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Gameplay.Features.TeamsFeutere;
using _Project.Develop.Runtime.Utilities;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Gameplay.Features.Sensors
{
	public class AnotherTeamTouchDetectorSystem : IInitializableSystem, IUpdatableSystem
	{
		private Buffer<Entity> _contacts;
		private ReactiveVariable<bool> _isTouchAnootherTeam;
		private ReactiveVariable<Teams> _sourceTeam;
		
		
		public void OnInit(Entity entity)
		{
			_contacts = entity.ContactEntitiesBuffer;
			_isTouchAnootherTeam = entity.IsTouchAnotherTeam;
			_sourceTeam = entity.Team;
		}

		public void OnUpdate(float dt)
		{
			for (int i = 0; i < _contacts.Count; i++)
			{
				Entity contact = _contacts.Items[i];

				if (contact.TryGetTeam(out ReactiveVariable<Teams> contactTeam))
				{
					if (_sourceTeam.Value != contactTeam.Value)
					{
						_isTouchAnootherTeam.Value = true;
						return;
					}
				}
			}
			
			_isTouchAnootherTeam.Value = false;
		}
	}
}
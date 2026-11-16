using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.AI.States
{
	public interface ITargetSelector
	{
		Entity SelectTargetFrom(IEnumerable<Entity> targets);
	}
}
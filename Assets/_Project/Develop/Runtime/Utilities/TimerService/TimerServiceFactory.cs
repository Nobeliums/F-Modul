using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.CoroutineManagement;

namespace _Project.Develop.Runtime.Utilities.TimerService
{
	public class TimerServiceFactory
	{
		private DIContainer _container;

		public TimerServiceFactory(DIContainer container)
		{
			_container = container;
		}

		public TimerService Create(float cooldown) => new TimerService(cooldown, _container.Resolve<ICoroutinePerformer>());
	}
}
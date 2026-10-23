using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.Utilities.WalletService;
using _Project.Develop.Runtime.Meta.Configs;
using _Project.Develop.Runtime.UI.View;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.UI.Presenters
{
	public class CurrencyPresenter
	{
		private IconTextView _iconTextView;
		
		private IReadOnlyReactiveVariable<int> _value;
		private WalletCurrencyType _currencyType;
		private StartWalletViewConfig _viewConfig;
		
		private IDisposable _subscription;

		public CurrencyPresenter(IconTextView iconTextView,
			WalletCurrencyType currencyType,
			IReadOnlyReactiveVariable<int> value,
			StartWalletViewConfig viewConfig)
		{
			_iconTextView = iconTextView;
			_currencyType = currencyType;
			_value = value;
			_viewConfig = viewConfig;
		}
		
		public void Initialize()
		{
			_iconTextView.SetText(_value.Value.ToString());
			_iconTextView.SetSprite(_viewConfig.GetViewBy(_currencyType));

			_subscription = _value.Subscribe(OnValueChanged);
		}

		public void Dispose()
		{
			_subscription.Dispose();
		}

		private void OnValueChanged(int oldValue, int newValue)
		{
			_iconTextView.SetText(newValue.ToString());
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Utilities.DataManagment;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Gameplay.Utilities.WalletService
{
	public class WalletService : IDataReader<PlayerData>, IDataWriter<PlayerData>
	{
		private Dictionary<WalletCurrencyType, ReactiveVabiable<int>> _walletCurrencies;

		public WalletService(Dictionary<WalletCurrencyType,
			ReactiveVabiable<int>> walletCurrencies,
			PlayerDataProvider playerDataProvider)
		{
			_walletCurrencies = new(walletCurrencies);

			playerDataProvider.RegisterDataReader(this);
			playerDataProvider.RegisterDataWriter(this);
		}

		public List<WalletCurrencyType> AvailableCurrencies => _walletCurrencies.Keys.ToList();
		
		public IReadOnlyReactiveVariable<int> GetCurrency(WalletCurrencyType currency) => _walletCurrencies[currency];

		public void Add(int amount, WalletCurrencyType currency)
		{
			if (amount < 0)
				throw new IndexOutOfRangeException("amount must be >= 0");
			
			_walletCurrencies[currency].Value += amount;
		}
		
		public void Debit(int amount, WalletCurrencyType currency)
		{
			if (amount < 0)
				throw new ArgumentOutOfRangeException("amount must be >= 0");
			
			if (IsEnough(amount, currency) == false)
				throw new InvalidOperationException("Amount is not enough to Debit currency");
			
			_walletCurrencies[currency].Value -= amount;
		}

		public bool IsEnough(int amount, WalletCurrencyType currency) => _walletCurrencies[currency].Value >= amount;

		public void ReadFrom(PlayerData data)
		{
			foreach (KeyValuePair<WalletCurrencyType, int> walletData in data.WalletData)
			{
				_walletCurrencies[walletData.Key].Value = walletData.Value;
			}
		}

		public void WriteTo(PlayerData data)
		{
			foreach (KeyValuePair<WalletCurrencyType, ReactiveVabiable<int>> wallet in _walletCurrencies)
			{
				data.WalletData[wallet.Key] = wallet.Value.Value;
			}
		}
	}
}
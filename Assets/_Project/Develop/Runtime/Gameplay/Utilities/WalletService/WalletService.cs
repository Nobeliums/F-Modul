using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Gameplay.Utilities.WalletService
{
	public class WalletService
	{
		private Dictionary<WalletCurrencyType, ReactiveVabiable<int>> _walletCurrencies;

		public WalletService(Dictionary<WalletCurrencyType, ReactiveVabiable<int>> walletCurrencies)
		{
			_walletCurrencies = walletCurrencies;
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

		public bool IsEnough(int amount, WalletCurrencyType currency) => amount >= _walletCurrencies[currency].Value;
	}
}
using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.UI.View;
using _Project.Develop.Runtime.Utilities.ResourcesManagement;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Develop.Runtime.UI.Core
{
	public class ViewFactory
	{
		private ResourceLoadService _resourceLoadService;

		private readonly Dictionary<string, string> _viewIdToResourcesPath = new()
		{
			{ViewIds.CurrencyView, "Prefabs/CurrencyView"},
			{ViewIds.MainMenuView, "Prefabs/UI/MainMenuView"},
			{ViewIds.GameStatTextView, "Prefabs/UI/TextBoxView"},
			{ViewIds.SelectCharsGameButtonView, "Prefabs/UI/SelectGameCharsTypeView"},
			{ViewIds.SelectGameCharsPopupView, "Prefabs/UI/SelectCharsTypePopup"},
			{ViewIds.ShopItemBuyButtonView, "Prefabs/UI/ShopItemBuy"},
			{ViewIds.ShopPopupView, "Prefabs/UI/ShopPopup"},
			{ViewIds.GameplayView, "Prefabs/UI/GameplayView"},
			{ViewIds.GameResultPopupView, "Prefabs/UI/GameResultPopup"}
		};

		public ViewFactory(ResourceLoadService resourceLoadService)
		{
			_resourceLoadService = resourceLoadService;
		}

		public TView Create<TView>(string viewId, Transform parent = null) where TView : MonoBehaviour, IView
		{
			if (_viewIdToResourcesPath.TryGetValue(viewId, out var resourcePath))
			{
				TView prefab = _resourceLoadService.Load<TView>(resourcePath);
				Debug.Log(prefab);
				
				TView viewIntance = Object.Instantiate(prefab, parent);
				
				return viewIntance;
			}
			
			throw new ArgumentException($"Unknown view id '{viewId}'");
		}

		public void Release<TView>(TView view) where TView : MonoBehaviour, IView
		{
			Object.Destroy(view.gameObject);
		}
	}
}

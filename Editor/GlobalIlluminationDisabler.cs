using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UniGlobalIlluminationDisabler
{
	/// <summary>
	/// 新規のシーンが作成された時に Global Illumination をオフにするエディタ拡張
	/// </summary>
	[InitializeOnLoad]
	public static class GlobalIlluminationDisabler
	{
		//================================================================================
		// デリゲート（static）
		//================================================================================
		/// <summary>
		/// Global Illumination をオフにするシーンかどうかを確認する時に呼び出されます
		/// </summary>
		public static Func<Scene, NewSceneSetup, NewSceneMode, bool> Predicate { private get; set; }
		
		//================================================================================
		// 関数（static）
		//================================================================================
		/// <summary>
		/// コンストラクタ
		/// </summary>
		static GlobalIlluminationDisabler()
		{
			EditorSceneManager.newSceneCreated += OnCreated;
		}
		
		/// <summary>
		/// 新規のシーンが作成された時に Global Illumination をオフにします
		/// </summary>
		private static void OnCreated( Scene scene, NewSceneSetup setup, NewSceneMode mode )
		{
			if ( Predicate == null ) return;
			if ( !Predicate( scene, setup, mode ) ) return;

			Lightmapping.realtimeGI     = false;
			Lightmapping.bakedGI        = false;
			Lightmapping.giWorkflowMode = Lightmapping.GIWorkflowMode.OnDemand;
			RenderSettings.skybox       = null;
		}
	}
}
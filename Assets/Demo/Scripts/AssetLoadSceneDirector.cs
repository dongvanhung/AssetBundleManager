﻿using UnityEngine;
using System.Collections;

public class AssetLoadSceneDirector : MonoBehaviour {

	AssetBundleManager bundleMng;
	private bool isButtonEnabled = false;
	private GameObject model = null;

	// Use this for initialization
	void Start () {
		// アセットバンドルマネージャーインスタンス取得
		// キャッシュから読み込み
		string[] bundleNames = { "unitychan_std", "unitychan_crs", "unitychan_baseassets" };
		AssetBundleManager.LoadAssetBundle (bundleNames, ((bool isSuccess, string error) => {
			if (isSuccess) {
				isButtonEnabled = true;
				Debug.Log("ロード成功");
			}
			else {
				Debug.Log("ロード失敗 : "+error);
			}
		}));
	}
	
	// Update is called once per frame
	void Update () {

	}

	// ボタンタップ時の挙動
	public void OnButton(string assetName) {
		if (isButtonEnabled) {
			if (model != null) {
				Destroy (model);
			}

			if (assetName == "UnityChan_Std") {
				// 同期でモデルロード
				GameObject obj = AssetBundleManager.GetAsset<GameObject> ("unitychan_std", assetName);
				// モデル表示
				model = Instantiate<GameObject> (obj, Vector3.zero, Quaternion.identity);
			}
			else if (assetName == "UnityChan_Crs") {
				// 非同期でモデルをロード
				AssetBundleManager.GetAssetAsync ("unitychan_crs", assetName, ((Object asset, bool isSuccess) => {
					if (isSuccess) {
						// モデル表示
						model = Instantiate ((GameObject)asset, Vector3.zero, Quaternion.identity);
					}
				}));
			}
			else {
				Debug.LogError ("ロードするアセット名が指定されていません。");
			}
		}
	}
}

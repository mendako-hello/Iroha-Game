using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using System;

/// <summary>
/// スプラッシュ画面を読み込み
/// ゲームの初期化を行います
/// </summary>
public class GameInitializer : MonoBehaviour {
  // disable "never assigned warning"
  #pragma warning disable 649

  [SerializeField, HeaderAttribute("スプラッシュ画面のシーン名")]
  private String SplashSceneName;

  // restore "never assigned warning"
  #pragma warning restore 649

  void Start() {
    // 現在のシーンの名前
    var currentName = SceneManager.GetActiveScene().name;
    var count = SceneManager.sceneCount;
    var ops = new List<IObservable<AsyncOperation>>();
    for (var i = 0; i < count; i++) {
      var scene = SceneManager.GetSceneAt(i);
      if (scene.name != currentName && scene.name != SplashSceneName) {
        // 固定シーンとスプラッシュ画面以外をアンロードする
        ops.Add(SceneManager.UnloadSceneAsync(scene).AsObservable());
        Debug.Log("[GameInitializer] Unload \"" + scene.name + "\"");
      }
    }

    // スプラッシュ画面がロードされているか判定
    if (!SceneManager.GetSceneByName(SplashSceneName).isLoaded) {
      Observable.WhenAll(ops).Subscribe(o => {
        // すべてのアンロードが完了後
        // スプラッシュ画面をロードする
        SceneManager.LoadSceneAsync(SplashSceneName, LoadSceneMode.Additive);
        Debug.Log("[GameInitializer] Load \"" + SplashSceneName + "\"");
      });
    }
  }
}

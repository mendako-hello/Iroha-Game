using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniRx;
using System;

/// <summary>
/// タイトル画面を表示します
/// 各ボタンに対応した動作を行い
/// 開始時にはゲーム画面へ遷移します
/// </summary>
public class TitleScreen : MonoBehaviour {
  // disable "never assigned warning"
  #pragma warning disable 649

  [SerializeField, HeaderAttribute("タイトル画面のシーン名")]
  private String TitleSceneName;
  [SerializeField, HeaderAttribute("ゲーム画面のシーン名")]
  private String GameSceneName;

  [HeaderAttribute("コンポーネント")]
  [SerializeField, TooltipAttribute("「はじめる」ボタン")]
  private Button StartButton;
  [SerializeField, TooltipAttribute("「つづきから」ボタン")]
  private Button ContinueButton;
  [SerializeField, TooltipAttribute("「チャプター」ボタン")]
  private Button ChapterButton;
  [SerializeField, TooltipAttribute("「終了」ボタン")]
  private Button ExitButton;

  // restore "never assigned warning"
  #pragma warning restore 649

  void Start() {
    // 「設定」ボタンを表示
    GameSettings.showSettingButton = true;

    // 「はじめる」ボタンが押されたときの処理
    StartButton.onClick.AsObservable().Subscribe(_ => {
      // 最初からゲーム画面を再生
      // ゲーム画面をロードする
      var op = SceneManager.LoadSceneAsync(GameSceneName, LoadSceneMode.Additive);

      op.AsObservable().Subscribe(o => {
        // タイトル画面をアンロードする
        SceneManager.UnloadSceneAsync(TitleSceneName);
      });

      Debug.Log("[TitleScreen] Click \"StartButton\"");
    });

    // 「つづきから」ボタンが押されたときの処理
    ContinueButton.onClick.AsObservable().Subscribe(_ => {
      // 前回の続きからゲーム画面を再生
      // ゲーム画面をロードする
      var op = SceneManager.LoadSceneAsync(GameSceneName, LoadSceneMode.Additive);

      op.AsObservable().Subscribe(o => {
        // タイトル画面をアンロードする
        SceneManager.UnloadSceneAsync(TitleSceneName);
      });

      Debug.Log("[TitleScreen] Click \"ContinueButton\"");
    });

    // 「チャプター」ボタンが押されたときの処理
    ChapterButton.onClick.AsObservable().Subscribe(_ => {
      Debug.Log("[TitleScreen] Click \"ChapterButton\"");
    });

    // 「終了」ボタンが押されたときの処理
    ExitButton.onClick.AsObservable().Subscribe(_ => {
      Debug.Log("[TitleScreen] Click \"ExitButton\"");
    });
  }
}

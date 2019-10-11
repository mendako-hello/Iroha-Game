using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

/// <summary>
/// 設定画面を表示します
/// 普段は隠れており設定が可能な状況で
/// 設定画面を出せるようになります
/// </summary>
public class FixedScreen : MonoBehaviour {
  // disable "never assigned warning"
  #pragma warning disable 649

  [HeaderAttribute("コンポーネント")]
  [SerializeField, TooltipAttribute("「設定」ボタン")]
  private Button SettingButton;
  [SerializeField, TooltipAttribute("設定画面")]
  private GameObject SettingsObject;
  [SerializeField, TooltipAttribute("「音量」スライダー")]
  private Slider VolumeSlider;

  // restore "never assigned warning"
  #pragma warning restore 649

  void ShowSettings() {
    SettingsObject.SetActive(true);
    Vector3 pos = transform.position;
    pos.y = 1080;
    transform.position = pos;
  }

  void HideSettings() {
    Vector3 pos = transform.position;
    pos.y = 1080;
    transform.position = pos;
    SettingsObject.SetActive(false);
  }

  void Awake() {
    // 「設定」ボタンの表示・非表示が変更されたときの処理
    // Start以降で動作する必要があるのでAwakeで定義
    Observable.FromEvent<bool> (
      h => GameSettings.onChangeSettingButton += h,
      h => GameSettings.onChangeSettingButton -= h
    ).Subscribe(b => {
      SettingButton.gameObject.SetActive(b);

      Debug.Log("[FixedScreen] Update \"SettingButton\"");
    });

    // 設定画面を上に隠しておく
    HideSettings();
  }

  void Start() {
    // 「設定」ボタンが押されたときの処理
    SettingButton.onClick.AsObservable().Subscribe(_ => {
      // 設定画面を表示・非表示
      if (SettingsObject.activeInHierarchy) {
        HideSettings();
      } else {
        ShowSettings();
      }

      Debug.Log("[FixedScreen] Click \"SettingButton\"");
    });

    // 「音量」スライダーを初期化
    VolumeSlider.value = GameSettings.soundVolume;
    // 「音量」スライダーが操作されたときの処理
    VolumeSlider.onValueChanged.AsObservable().Subscribe(_ => {
      // 音量をスライダーの値で更新
      GameSettings.soundVolume = VolumeSlider.value;

      Debug.Log("[FixedScreen] Move \"VolumeSlider\"");
    });
  }
}

using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// ゲームのプロパティを管理する
/// プロパティ変更時にはそれぞれ
/// イベントを登録することができる
/// </summary>
public class GameSettings {
  /// <summary>
  /// 設定画面の表示・非表示変更イベント
  /// </summary>
  public static event Action<bool> onChangeSettingButton;

  /// <summary>
  /// 設定画面の表示・非表示
  /// </summary>
  private static bool _showSettingButton = false;
  public static bool showSettingButton {
    set {
      _showSettingButton = value;
      onChangeSettingButton?.Invoke(value);
    }
    get {
      return _showSettingButton;
    }
  }

  /// <summary>
  /// ゲーム内の音量変更イベント
  /// </summary>
  public static event Action<float> onChangeSoundVolume;

  /// <summary>
  /// ゲーム内の音量
  /// </summary>
  private static float _soundVolume = 0.5f;
  public static float soundVolume {
    set {
      _soundVolume = value < 0 ? 0 : (value > 1 ? 1 : value);
      onChangeSoundVolume?.Invoke(value);
    }
    get {
      return _soundVolume;
    }
  }
}

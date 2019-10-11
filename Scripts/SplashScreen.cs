using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UniRx;
using System;

/// <summary>
/// スプラッシュ画面を表示します
/// ロゴの表示、注意の表示を行い
/// 後にタイトル画面へ遷移します
/// </summary>
public class SplashScreen : MonoBehaviour {
  // disable "never assigned warning"
  #pragma warning disable 649

  [SerializeField, HeaderAttribute("スプラッシュ画面のシーン名")]
  private String SplashSceneName;
  [SerializeField, HeaderAttribute("タイトル画面のシーン名")]
  private String TitleSceneName;

  [SerializeField, HeaderAttribute("ロゴ画像")]
  private Sprite LogoSprite;
  [SerializeField, HeaderAttribute("ロゴの表示時間")]
  private float LogoDuration;
  [SerializeField, HeaderAttribute("注意の表示時間")]
  private float CautionDuration;
  [SerializeField, TextAreaAttribute(2, 100), HeaderAttribute("注意文章")]
  private String CautionMessage;
  [SerializeField, HeaderAttribute("背景画像")]
  private Sprite BackSprite;

  [HeaderAttribute("コンポーネント")]
  [SerializeField, TooltipAttribute("ロゴ")]
  private Image LogoImage;
  [SerializeField, TooltipAttribute("注意")]
  private TextMeshProUGUI CautionText;
  [SerializeField, TooltipAttribute("背景")]
  private Image BackImage;

  // restore "never assigned warning"
  #pragma warning restore 649

  void Start() {
    var span0 = TimeSpan.FromSeconds(LogoDuration);
    var span1 = TimeSpan.FromSeconds(CautionDuration);

    // 0秒後の処理
    {
      // 「設定」ボタンを非表示
      GameSettings.showSettingButton = false;
      // BackSpriteを表示
      BackImage.enabled = true;
      BackImage.sprite = BackSprite;
      // LogoSpriteを表示
      LogoImage.enabled = true;
      LogoImage.sprite = LogoSprite;
      // CautionMessageを非表示
      CautionText.enabled = false;
    }

    // LogoDuration秒後の処理
    Observable.Timer(span0).Subscribe(_ => {
      // LocoSpriteを非表示
      LogoImage.enabled = false;
      // CautionMessageを表示
      CautionText.enabled = true;
      CautionText.text = CautionMessage;
    });

    // LogoDuration+CautionDuration秒後の処理
    Observable.Timer(span0.Add(span1)).Subscribe(_ => {
      // タイトル画面をロードする
      var op = SceneManager.LoadSceneAsync(TitleSceneName, LoadSceneMode.Additive);

      op.AsObservable().Subscribe(o => {
        // CautionMessageを非表示
        CautionText.enabled = false;
        // BackSpriteを非表示
        BackImage.enabled = false;
        // スプラッシュ画面をアンロードする
        SceneManager.UnloadSceneAsync(SplashSceneName);
      });
    });
  }
}

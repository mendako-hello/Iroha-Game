using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲーム画面を表示します
/// テキストファイルを読み
/// ゲーム画面を生成します
/// </summary>
public class GameScreen : MonoBehaviour {
  // disable "never assigned warning"
  #pragma warning disable 649

  [HeaderAttribute("コンポーネント")]
  [SerializeField, TooltipAttribute("「次へ」ボタン")]
  private Button NextButton;

  // restore "never assigned warning"
  #pragma warning restore 649
}

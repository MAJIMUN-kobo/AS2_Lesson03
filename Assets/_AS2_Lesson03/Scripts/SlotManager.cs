using UnityEngine;
using UnityEngine.InputSystem;

// === スロット管理クラス === //
public class SlotManager : MonoBehaviour
{
    // === Inspector Parameter
    [Header("* * * Reel Object.")]
    [SerializeField] private Transform[] _reels;

    // === Private Value === //
    private int[,] _slot;           // スロットの行列（二次元配列）
    private int _reelStoped;        // 停止リール番号
    private float[] _reelAngles;    // リールの角度

    void Start()
    {
        // 一次元配列では1行のみのデータだが...
        //　　👉 int[] array = new int[] { 0, 1, 2, 3, 4 };
        // 二次元配列では行列のデータを持つことができる。
        // スロットの初期化
        _slot = new int[,] { { 0, 0, 0 },{ 1, 1, 1 },{ 2, 2, 2 },{ 3, 3, 3 },{ 4, 4, 4 } };

        _reelAngles = new float[ _slot.GetLength(1) ];
    }

    void Update()
    {
        if( Keyboard.current.spaceKey.wasPressedThisFrame
            || Mouse.current.leftButton.wasPressedThisFrame)
        {   // UnityEngine.InputSystem インターフェースのボタン押下処理
            // 👇リールのストップ番号を加算する
            _reelStoped++;
        }

        if(Keyboard.current.enterKey.wasPressedThisFrame)
        {
            if(_reelStoped > 2)
                _reelStoped = 0;
        }

        // スロットの配列を回転させる
        PlayingSlot();

        // オブジェクトの角度を更新
        RotateReelObject();
    }

    // === スロットのメインメソッド === //
    // 2026-07-06 内容にコメントをつけて提出すること
    public void PlayingSlot()
    {
        int reelLength = _slot.GetLength(1);

        for(int x = _reelStoped; x < reelLength; x++)
        {
            ReelLoop(x);
            _reelAngles[x] += 360 / _slot.GetLength(0);
        }
    }

    // === スロットリールのループ処理 === //
    // ※ 配列の中身を入れ替えてループを再現する
    // 引数１：回転させるリール番号(int型)
    // 戻り値：ない(void)
    public void ReelLoop (int reelIndex)
    {
        int length = _slot.GetLength(0);            // 配列の行数を取得
        
        int temp = _slot[length - 1, reelIndex];    // 最後の行の値を一時的に保存

        // ※
        // ①　int型のyを宣言します。
        // ②　(行数 - 1)をyに代入します。
        // ③　( y > 0)--- yが0より大きいなら繰り返す。
        // ④　yから1減らしながら繰り返します。
        for (int y = length - 1; y > 0; y--)
        {
            // スロット行列の[y番目, リール番目]に、
            // スロット行列の[y - 1番目, リール番目]の値を代入する。
            _slot[y, reelIndex] = _slot[y - 1, reelIndex];
        }

        // 保存しておいた最後の値を、
        // 配列の最初[0番目]に代入する
        _slot[0, reelIndex] = temp;
    }

    // === 各リールオブジェクトの角度を更新 === //
    private void RotateReelObject()
    {
        for (int x = 0; x < _slot.GetLength(1); x++)
        {
            // eulerAngles の x軸を リールの角度に更新する
            _reels[x].eulerAngles = Vector3.right * _reelAngles[x];
        }
    }

    // === そろったリールをチェックする === //
    // 2026-07-27 まで宿題！！
    private bool GetHitReel(int[,] slot, int row)
    {
        bool hit = false;

        // ヒット判定


        return hit;
    }
}
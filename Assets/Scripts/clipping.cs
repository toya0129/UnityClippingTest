using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class clipping : MonoBehaviour {

  // FukuwaraiのMouseController.cs参照
  private Camera mainCamera = null;
  private Transform mainCameraTransform = null;

  private GameObject canvas;

  // mask image : ここを変えればクリック時に表示する領域を変えれる
  [SerializeField]
  GameObject mask;

  // picture: main picture
  [SerializeField]
  GameObject picture;

  // click Position
  private Vector3 clickPos = Vector3.zero;

  private void Awake()
  {
      //FukuwaraiのMouseController.cs参照
      this.mainCamera = Camera.main;
      this.mainCameraTransform = this.mainCamera.transform;

      //resetするための初期化
      canvas = this.gameObject;
  }

  private void Update(){
    // 左クリックした時にクリック位置のみを表示させる
    if (Input.GetMouseButtonDown(0)){
      Debug.Log("MouseDown");
      Debug.Log(clickPos);
      SetMask();
    }
    // Spaceを押した時にリセット
    if(Input.GetKeyDown(KeyCode.Space)){
      reset();
    }
  }

  // FukuwaraiのMouseController.cs参照
  private Vector3 GetMousePosition()
  {
      // マウスから取得できないZ座標を補完する
      var position = Input.mousePosition;
      position.z = this.mainCameraTransform.position.z;
      position = this.mainCamera.ScreenToWorldPoint(position);
      position.z = 0;

      return position;
  }

  // pictureの特定の領域を表示
  private void SetMask(){
    // Get Click Position
    clickPos = GetMousePosition();
    // 調整 :これを取ると上下左右反転
    clickPos.x *= -1;
    clickPos.y *= -1;
    // maskの座標をクリック位置へ
    mask.transform.position = clickPos;
    // pictureの親要素をmaskに指定
    picture.transform.SetParent(mask.transform);
  }

  // 移動した座標を全てリセット
  private void reset(){
    // click position を　0にする
    clickPos = Vector3.zero;
    // mask Position を　0 へ
    mask.transform.localPosition = clickPos;
    // pictureとmaskの親子関係解除: pictureがCanvasの子になる
    picture.transform.SetParent(canvas.transform);
    picture.transform.position = clickPos;
  }
}



// 下記ページ参照
// http://kan-kikuchi.hatenablog.com/entry/Mask

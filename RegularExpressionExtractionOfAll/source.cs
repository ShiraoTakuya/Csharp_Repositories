﻿using System;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Collections;
using System.Drawing;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Threading;
using System.Collections.Generic;

//メイン
class program{
	[STAThreadAttribute]
	static void Main(){
		var iForm = new Window();
		Application.Run(iForm);
	}
}

//ウィンドウクラス
class Window: Form{

	//変数宣言
	public Dictionary<int, TextBoxEx> arTextBox;
	public const int WINDOW_SIZE_X = 640;
	public const int WINDOW_SIZE_Y = 480;
	public const int TEXTBOX_SIZE_HEIGHT = 20;

	//コンストラクタ
	public Window(){

		//ウィンドウ初期化
		WindowInitialize();

		//テキストボックス初期化
		TextboxInitialize();
	}

	//ウィンドウ初期化処理
	private void WindowInitialize(){
		Icon = new Icon("favicon.ico");
		ClientSize = new Size(WINDOW_SIZE_X, WINDOW_SIZE_Y);
		Text = "正規表現多抽出";

		//KeyDownのイベントハンドラを追加
		KeyPreview = true;
		KeyDown += new KeyEventHandler(Form1_KeyDown);
	}

	//テキストボックス初期化処理
	private void TextboxInitialize(){
		//テキストボックスのサイズ情報を格納、最後の一つはKey
		int[,] SIZE_INFO = new int[,]{
			{0, 0, WINDOW_SIZE_X, TEXTBOX_SIZE_HEIGHT, 0},
			{0, 19, WINDOW_SIZE_X, 0, 1}
		};

		//可変テキストボックスの大きさ調整
		SIZE_INFO[1, 3] = WINDOW_SIZE_Y - (SIZE_INFO[0, 1] + SIZE_INFO[0, 3] - 1);

		//テキストボックスを生成
		arTextBox = new Dictionary<int, TextBoxEx>();
		for(var i = 0; i < SIZE_INFO.GetLength(0); i++){
			arTextBox.Add(i, new TextBoxEx());
		}

		//テキストボックスをウィンドウに追加
		for(int i = 0; i < arTextBox.Count; i++){
			var tb = arTextBox[SIZE_INFO[i,4]];
			tb.Font = new Font("ＭＳ Ｐゴシック", 9);
			tb.Size = new Size(SIZE_INFO[i,2], SIZE_INFO[i,3]);
			tb.Location = new Point(SIZE_INFO[i,0], SIZE_INFO[i,1]);
			tb.Text = "";
			tb.Multiline = true;
			tb.BorderStyle = BorderStyle.FixedSingle;
			tb.WatermarkText = "Press F5 for extraction";
			tb.Anchor = (AnchorStyles)(AnchorStyles.Top|AnchorStyles.Left|AnchorStyles.Right);
			Controls.Add(tb);
		}
//		arTextBox[0].KeyDown += new KeyEventHandler(Form1_KeyDown);
		arTextBox[1].ScrollBars = ScrollBars.Vertical;
		arTextBox[1].Anchor = (AnchorStyles)(AnchorStyles.Top|AnchorStyles.Bottom|AnchorStyles.Left|AnchorStyles.Right);
	}

	//KeyDown動作
	void Form1_KeyDown(object sender, KeyEventArgs e){
		if(e.KeyCode == Keys.F5){
			ExtRegex();
		}
	}

	//正規表現抽出
	private void ExtRegex(){
		var stText = Clipboard.GetText(TextDataFormat.Text);
		var stPattern = arTextBox[0].Text;
		foreach(Match m in Regex.Matches(stText, stPattern, RegexOptions.Singleline)){
			for(int i = 1; i < m.Groups.Count; i++){
				arTextBox[1].Text += m.Groups[i].Value + "\r\n";
			}
		}
	}

}

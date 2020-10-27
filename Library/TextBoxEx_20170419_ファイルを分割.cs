using System;
using System.Windows.Forms;
using System.Drawing;

//テキストボックスにウォーターマークを追加したクラス
class TextBoxEx: TextBox{
	public String WatermarkText = "";
	protected override void WndProc(ref Message m){
		const int WM_PAINT = 0x000F;
		base.WndProc(ref m);
		if (m.Msg == WM_PAINT && string.IsNullOrEmpty(this.Text) && string.IsNullOrEmpty(WatermarkText) == false){
		using (Graphics g = Graphics.FromHwnd(this.Handle)){
				Rectangle rect = this.ClientRectangle;
				rect.Offset(1, 1);
				TextRenderer.DrawText(g, WatermarkText, this.Font, rect, SystemColors.ControlDark, TextFormatFlags.Top | TextFormatFlags.Left);
			}
		}
	}
}

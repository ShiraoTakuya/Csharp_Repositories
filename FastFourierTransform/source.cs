using System;
using System.Windows.Forms;
using System.IO;
using System.Numerics;
using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;

//メイン
class program{
	static void Main(string[] args){
		if(args.Length < 1){
			MessageBox.Show("ファイルをドロップしてください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			Environment.Exit(0);
		}

		// FFT
		for(int i = 0; i < args.Length; i++){
			Console.Write("処理中: " + args[i] + "\r\n");
			FFTs(args[i]);
		}

		MessageBox.Show("完了しました。", "メッセージ", MessageBoxButtons.OK, MessageBoxIcon.None);
	}

	public static void FFTs(string filepath){
		// ファイル読み込み
		string[] lines = File.ReadAllLines(filepath);
		int N = lines.Length;

		// ドロップファイルのデータを複素数データに変換
		var x = new Complex[N];
		for (int i = 0; i < N; i++){
			x[i] = new Complex(Convert.ToDouble(lines[i]), 0);
		}

		// FFT
		Fourier.Forward(x, FourierOptions.NoScaling);

		// 出力
		string basename = System.IO.Path.GetFileNameWithoutExtension(filepath);
		string output = "";
		for (int i = 0; i < N; i++){
			output += (x[i].Real.ToString("e") + "," + x[i].Imaginary.ToString("e") + "," + Math.Sqrt(Math.Pow(x[i].Real,2)+Math.Pow(x[i].Imaginary,2)).ToString("e") + "\r\n");
		}
		File.WriteAllText("(FFT)" + basename + ".csv", output);
	}

}

/*
※使用ライブラリ: MathNet.Numerics4.7.0
*/
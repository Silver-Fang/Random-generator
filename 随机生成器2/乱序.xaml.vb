' https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板
Imports 随机生成器2.App
Imports System.Text
Imports MathNet.Numerics.Combinatorics

''' <summary>
''' 可用于自身或导航至 Frame 内部的空白页。
''' </summary>
Public NotInheritable Class 乱序
	Inherits Page
	Sub New()

		' 此调用是设计器所必需的。
		InitializeComponent()

		' 在 InitializeComponent() 调用之后添加任何初始化。
		输入字符串.Text = If(漫游设置("乱序.输入字符串"), "")
		保留首尾字符.IsChecked = If(漫游设置("乱序.保留首尾字符"), False)
	End Sub

	Private Sub 打乱顺序_Click(sender As Object, e As RoutedEventArgs) Handles 打乱顺序.Click
		漫游设置("乱序.保留首尾字符") = 保留首尾字符.IsChecked
		Static 编码器 As UTF32Encoding = Encoding.UTF32
		Dim 全串字节 As Byte() = 编码器.GetBytes(输入字符串.Text)
		Dim 全串整数(全串字节.Length / 4 - 1) As UInteger
		Buffer.BlockCopy(全串字节, 0, 全串整数, 0, 全串字节.Length)
		If 保留首尾字符.IsChecked Then
			If 全串整数.Length < 2 Then
				输出.Text = 输入字符串.Text
				Exit Sub
			Else
				SelectPermutation(全串整数.Skip(1).SkipLast(1)).ToArray.CopyTo(全串整数, 1)
			End If
		Else
			SelectPermutationInplace(全串整数)
		End If
		Buffer.BlockCopy(全串整数, 0, 全串字节, 0, 全串字节.Length)
		输出.Text = 编码器.GetString(全串字节)
	End Sub

	Private Sub 复制到剪贴板_Click(sender As Object, e As RoutedEventArgs) Handles 复制到剪贴板.Click
		Dim 数据包 As New DataTransfer.DataPackage
		数据包.SetText(输出.Text)
		DataTransfer.Clipboard.SetContent(数据包)
	End Sub
End Class

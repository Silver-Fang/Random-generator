' https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板
Imports 随机生成器2.App
Imports Windows.Storage
Imports MathNet.Numerics.Combinatorics
Imports System.Text
''' <summary>
''' 可用于自身或导航至 Frame 内部的空白页。
''' </summary>
Public NotInheritable Class 汉字
	Inherits Page
	Sub New()

		' 此调用是设计器所必需的。
		InitializeComponent()

		' 在 InitializeComponent() 调用之后添加任何初始化。
		一级规范字.IsChecked = If(漫游设置("汉字.一级规范字"), False)
		二级规范字.IsChecked = If(漫游设置("汉字.二级规范字"), False)
		三级规范字.IsChecked = If(漫游设置("汉字.三级规范字"), False)
		中日韩统一表意文字.IsChecked = If(漫游设置("汉字.中日韩统一表意文字"), False)
		扩展A.IsChecked = If(漫游设置("汉字.扩展A"), False)
		扩展B.IsChecked = If(漫游设置("汉字.扩展B"), False)
		扩展C.IsChecked = If(漫游设置("汉字.扩展C"), False)
		扩展D.IsChecked = If(漫游设置("汉字.扩展D"), False)
		生成个数.Text = If(漫游设置("汉字.生成个数"), 1)
		无重复.IsChecked = If(漫游设置("汉字.无重复"), False)
	End Sub

	Private Shared 一级规范字符集 As Stream
	Private Shared 二级规范字符集 As Stream
	Private Shared 三级规范字符集 As Stream
	Private Shared 中日韩统一表意文字符集 As Stream
	Private Shared 扩展A字符集 As Stream
	Private Shared 扩展B字符集 As Stream
	Private Shared 扩展C字符集 As Stream
	Private Shared 扩展D字符集 As Stream
	Shared ReadOnly 编码器 As UTF32Encoding = Encoding.UTF32

	Private Async Sub 生成_Click(sender As Object, e As RoutedEventArgs) Handles 生成.Click
		Dim 生成个数Integer As Integer
		Try
			生成个数Integer = UInteger.Parse(生成个数.Text)
		Catch ex As Exception
			错误消息.Text = ex.Message
			错误提示.ShowAt(生成)
			Exit Sub
		End Try
		漫游设置("汉字.生成个数") = 生成个数Integer
		Dim 全集 As New MemoryStream
		漫游设置("汉字.一级规范字") = 一级规范字.IsChecked
		If 一级规范字.IsChecked Then
			If 一级规范字符集 Is Nothing Then
				一级规范字符集 = (Await (Await StorageFile.GetFileFromApplicationUriAsync(New Uri("ms-appx:///字符集/一级规范字.u32"))).OpenSequentialReadAsync).AsStreamForRead
			End If
			一级规范字符集.Position = 0
			一级规范字符集.CopyTo(全集)
		End If
		漫游设置("汉字.二级规范字") = 二级规范字.IsChecked
		If 二级规范字.IsChecked Then
			If 二级规范字符集 Is Nothing Then
				二级规范字符集 = (Await (Await StorageFile.GetFileFromApplicationUriAsync(New Uri("ms-appx:///字符集/二级规范字.u32"))).OpenSequentialReadAsync).AsStreamForRead
			End If
			二级规范字符集.Position = 0
			二级规范字符集.CopyTo(全集)
		End If
		漫游设置("汉字.三级规范字") = 三级规范字.IsChecked
		If 三级规范字.IsChecked Then
			If 三级规范字符集 Is Nothing Then
				三级规范字符集 = (Await (Await StorageFile.GetFileFromApplicationUriAsync(New Uri("ms-appx:///字符集/三级规范字.u32"))).OpenSequentialReadAsync).AsStreamForRead
			End If
			三级规范字符集.Position = 0
			三级规范字符集.CopyTo(全集)
		End If
		漫游设置("汉字.中日韩统一表意文字") = 中日韩统一表意文字.IsChecked
		If 中日韩统一表意文字.IsChecked Then
			If 中日韩统一表意文字符集 Is Nothing Then
				中日韩统一表意文字符集 = (Await (Await StorageFile.GetFileFromApplicationUriAsync(New Uri("ms-appx:///字符集/中日韩统一表意文字.u32"))).OpenSequentialReadAsync).AsStreamForRead
			End If
			中日韩统一表意文字符集.Position = 0
			中日韩统一表意文字符集.CopyTo(全集)
		End If
		漫游设置("汉字.扩展A") = 扩展A.IsChecked
		If 扩展A.IsChecked Then
			If 扩展A字符集 Is Nothing Then
				扩展A字符集 = (Await (Await StorageFile.GetFileFromApplicationUriAsync(New Uri("ms-appx:///字符集/扩展A.u32"))).OpenSequentialReadAsync).AsStreamForRead
			End If
			扩展A字符集.Position = 0
			扩展A字符集.CopyTo(全集)
		End If
		漫游设置("汉字.扩展B") = 扩展B.IsChecked
		If 扩展B.IsChecked Then
			If 扩展B字符集 Is Nothing Then
				扩展B字符集 = (Await (Await StorageFile.GetFileFromApplicationUriAsync(New Uri("ms-appx:///字符集/扩展B.u32"))).OpenSequentialReadAsync).AsStreamForRead
			End If
			扩展B字符集.Position = 0
			扩展B字符集.CopyTo(全集)
		End If
		漫游设置("汉字.扩展C") = 扩展C.IsChecked
		If 扩展C.IsChecked Then
			If 扩展C字符集 Is Nothing Then
				扩展C字符集 = (Await (Await StorageFile.GetFileFromApplicationUriAsync(New Uri("ms-appx:///字符集/扩展C.u32"))).OpenSequentialReadAsync).AsStreamForRead
			End If
			扩展C字符集.Position = 0
			扩展C字符集.CopyTo(全集)
		End If
		漫游设置("汉字.扩展D") = 扩展D.IsChecked
		If 扩展D.IsChecked Then
			If 扩展D字符集 Is Nothing Then
				扩展D字符集 = (Await (Await StorageFile.GetFileFromApplicationUriAsync(New Uri("ms-appx:///字符集/扩展D.u32"))).OpenSequentialReadAsync).AsStreamForRead
			End If
			扩展D字符集.Position = 0
			扩展D字符集.CopyTo(全集)
		End If
		Dim 字符数 As UInteger = 全集.Length / 4
		If 字符数 Then
			If 无重复.IsChecked AndAlso 生成个数Integer > 字符数 Then
				错误消息.Text = "从如此小的字符集内取出这么多个汉字，不可能不重复"
				错误提示.ShowAt(生成)
			Else
				Dim UTF32集(字符数 - 1) As UInteger
				Dim 生成字节数 As UInteger = 生成个数Integer * 4
				Dim 字节集(生成字节数 - 1) As Byte
				Buffer.BlockCopy(全集.ToArray, 0, UTF32集, 0, 全集.Length)
				Buffer.BlockCopy(If(无重复.IsChecked, SelectVariation(UTF32集.Distinct, 生成个数Integer).ToArray, SelectVariationWithRepetition(UTF32集, 生成个数Integer).ToArray), 0, 字节集, 0, 生成字节数)
				输出.Text = 编码器.GetString(字节集)
			End If
		Else
			错误消息.Text = "没有选择任何字符集"
			错误提示.ShowAt(生成)
		End If
	End Sub

	Private Sub 复制到剪贴板_Click(sender As Object, e As RoutedEventArgs) Handles 复制到剪贴板.Click
		Dim 数据包 As New DataTransfer.DataPackage
		数据包.SetText(输出.Text)
		DataTransfer.Clipboard.SetContent(数据包)
	End Sub
End Class

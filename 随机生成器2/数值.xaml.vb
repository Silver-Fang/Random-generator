' https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板
Imports 随机生成器2.App

''' <summary>
''' 可用于自身或导航至 Frame 内部的空白页。
''' </summary>
Public NotInheritable Class 数值
	Inherits Page
	Sub New()

		' 此调用是设计器所必需的。
		InitializeComponent()

		' 在 InitializeComponent() 调用之后添加任何初始化。
		设置控件(最小值, 0)
		设置控件(最大值, 100)
		设置控件(浮点, False)
		设置控件(规定平均值, False)
		设置控件(平均值, 50)
		设置控件(规定标准差, False)
		设置控件(标准差, 25)
		设置控件(数值_生成个数, 1)
		设置控件(数值_无重复, False)
	End Sub

	Private Function TryParse(Of T)(文本框 As TextBox, Parser As Func(Of String, T), ByRef 返回值 As T) As Boolean
		Try
			返回值 = Parser(文本框.Text)
		Catch ex As Exception
			错误消息.Text = ex.Message
			错误提示.ShowAt(数值_生成)
			Return True
		End Try
		保存控件(文本框)
		Return False
	End Function

	Private Sub 数值_生成_Click(sender As Object, e As RoutedEventArgs) Handles 数值_生成.Click
		Dim 生成个数UInteger As UInteger
		If TryParse(数值_生成个数, AddressOf UInteger.Parse, 生成个数UInteger) Then
			Exit Sub
		End If
		保存控件(浮点)
		If 浮点.IsChecked Then
			Dim 最小值Double As Double
			If TryParse(最小值, AddressOf Double.Parse, 最小值Double) Then
				Exit Sub
			End If
			Dim 最大值Double As Double
			If TryParse(最大值, AddressOf Double.Parse, 最大值Double) Then
				Exit Sub
			End If
			保存控件(规定平均值)
			If 规定平均值.IsChecked Then
				Dim 平均值Double As Double
				If TryParse(平均值, AddressOf Double.Parse, 平均值Double) Then
					Exit Sub
				End If
				平均值Double = (平均值Double - 最小值Double) / (最大值Double - 最小值Double)
				保存控件(规定标准差)
				If 规定标准差.IsChecked Then
					Dim 标准差Double As Double
					If TryParse(标准差, AddressOf Double.Parse, 标准差Double) Then
						Exit Sub
					End If
					标准差Double /= 最大值Double - 最小值Double
					保存控件(数值_无重复)
				End If
			End If
		End If
	End Sub
End Class

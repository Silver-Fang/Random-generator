' https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板
Imports 随机生成器2.App
Imports MathNet.Numerics.Combinatorics
''' <summary>
''' 可用于自身或导航至 Frame 内部的空白页。
''' </summary>
Public NotInheritable Class 密码
	Inherits Page
	Sub New()

		' 此调用是设计器所必需的。
		InitializeComponent()

		' 在 InitializeComponent() 调用之后添加任何初始化。
		数字.IsChecked = If(漫游设置("密码.数字"), True)
		小写字母.IsChecked = If(漫游设置("密码.小写字母"), True)
		大写字母.IsChecked = If(漫游设置("密码.大写字母"), True)
		使用其它字符.IsChecked = If(漫游设置("密码.使用其它字符"), True)
		其它字符.Text = If(漫游设置("密码.其它字符"), "~!@#$%^&*()_+`-={}|[]\:"";'<>?,./")
		每种字符至少一个.IsChecked = If(漫游设置("密码.每种字符至少一个"), True)
		密码长度.Text = If(漫游设置("密码.密码长度"), 6)
	End Sub

	Private Sub 生成_Click(sender As Object, e As RoutedEventArgs) Handles 生成.Click
		Const 所有数字 As String = "0123456789"
		Const 所有小写 As String = "abcdefghijklmnopqrstuvwxyz"
		Const 所有大写 As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
		Dim 密码长度Byte As Byte
		Try
			密码长度Byte = Byte.Parse(密码长度.Text)
		Catch ex As Exception
			错误消息.Text = ex.Message
			错误提示.ShowAt(生成)
			Exit Sub
		End Try
		漫游设置("密码.密码长度") = 密码长度Byte
		Dim 最小长度 As Byte
		Dim 生成密码(密码长度Byte - 1) As Char
		漫游设置("密码.每种字符至少一个") = 每种字符至少一个.IsChecked
		If 每种字符至少一个.IsChecked Then
			最小长度 = -数字.IsChecked - 小写字母.IsChecked - 大写字母.IsChecked - 使用其它字符.IsChecked
			If 最小长度 > 密码长度Byte Then
				错误消息.Text = "指定的密码长度不可能满足每种字符至少一个的要求"
				错误提示.ShowAt(生成)
				Exit Sub
			Else
				最小长度 = 0
				If 数字.IsChecked Then
					生成密码(最小长度) = 所有数字(随机生成器.Next(0, 9))
					最小长度 += 1
				End If
				If 小写字母.IsChecked Then
					生成密码(最小长度) = 所有小写(随机生成器.Next(0, 25))
					最小长度 += 1
				End If
				If 大写字母.IsChecked Then
					生成密码(最小长度) = 所有大写(随机生成器.Next(0, 25))
					最小长度 += 1
				End If
				If 使用其它字符.IsChecked Then

					Dim 其它字符String As String = 其它字符.Text
					生成密码(最小长度) = 其它字符String(随机生成器.Next(0, 其它字符String.Length - 1))
					最小长度 += 1
				End If
			End If
		Else
			最小长度 = 0
		End If
		Dim 所有字符Builder As New Text.StringBuilder
		漫游设置("密码.数字") = 数字.IsChecked
		If 数字.IsChecked Then
			所有字符Builder.Append(所有数字)
		End If
		漫游设置("密码.小写字母") = 小写字母.IsChecked
		If 小写字母.IsChecked Then
			所有字符Builder.Append(所有小写)
		End If
		漫游设置("密码.大写字母") = 大写字母.IsChecked
		If 大写字母.IsChecked Then
			所有字符Builder.Append(所有大写)
		End If
		漫游设置("密码.使用其它字符") = 使用其它字符.IsChecked
		If 使用其它字符.IsChecked Then
			漫游设置("密码.其它字符") = 其它字符.Text
			所有字符Builder.Append(其它字符.Text)
		End If
		Dim 所有字符String As Char() = 所有字符Builder.ToString
		SelectVariationWithRepetition(所有字符String, 密码长度Byte - 最小长度).ToArray.CopyTo(生成密码, 最小长度)
		输出.Text = SelectPermutation(生成密码).ToArray
	End Sub

	Private Sub 复制到剪贴板_Click(sender As Object, e As RoutedEventArgs) Handles 复制到剪贴板.Click
		Dim 数据包 As New DataTransfer.DataPackage
		数据包.SetText(输出.Text)
		DataTransfer.Clipboard.SetContent(数据包)
	End Sub
End Class

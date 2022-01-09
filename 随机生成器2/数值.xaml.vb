' https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板
Imports 随机生成器2.App
Imports MathNet.Numerics.Distributions
Imports System.Numerics
Imports MathNet.Numerics.LinearAlgebra
''' <summary>
''' 可用于自身或导航至 Frame 内部的空白页。
''' </summary>
Public NotInheritable Class 数值
	Inherits Page
	Sub New()

		' 此调用是设计器所必需的。
		InitializeComponent()

		' 在 InitializeComponent() 调用之后添加任何初始化。
		最小值.Text = If(漫游设置("数值.最小值"), 0)
		最大值.Text = If(漫游设置("数值.最大值"), 100)
		均值.Text = If(漫游设置("数值.均值"), 50)
		方差.Text = If(漫游设置("数值.方差"), 25)
		生成个数.Text = If(漫游设置("数值.生成个数"), 1)
		浮点.IsChecked = If(漫游设置("数值.浮点"), False)
		规定均值.IsChecked = If(漫游设置("数值.规定均值"), False)
		规定方差.IsChecked = If(漫游设置("数值.规定方差"), False)
		无重复.IsChecked = If(漫游设置("数值.无重复"), False)
	End Sub

	Private Sub 报错(错误 As String)
		错误消息.Text = 错误
		错误提示.ShowAt(生成)
	End Sub

	Private Function TryParse(Of T)(文本框 As TextBox, Parser As Func(Of String, T), ByRef 返回值 As T) As Boolean
		Try
			返回值 = Parser(文本框.Text)
		Catch ex As Exception
			报错(ex.Message)
			Return True
		End Try
		漫游设置("数值." & 文本框.Name) = 返回值.ToString
		Return False
	End Function

	Private Sub 枚举随机数(Of T)(枚举器 As IEnumerable(Of T))
		Dim 拼接器 As New Text.StringBuilder
		For Each a As T In 枚举器
			拼接器.Append(a).Append(" ")
		Next
		输出.Text = 拼接器.ToString
	End Sub

	Private Sub 生成_Click(sender As Object, e As RoutedEventArgs) Handles 生成.Click
		Dim 生成个数UInteger As UInteger
		If TryParse(生成个数, AddressOf UInteger.Parse, 生成个数UInteger) Then
			Exit Sub
		End If
		If 浮点.IsChecked Then
			Dim 最小值Double As Double
			If TryParse(最小值, AddressOf Double.Parse, 最小值Double) Then
				Exit Sub
			End If
			Dim 最大值Double As Double
			If TryParse(最大值, AddressOf Double.Parse, 最大值Double) Then
				Exit Sub
			End If
			Dim 范围 As Double = 最大值Double - 最小值Double
			Dim 平均值Double As Double
			If 规定均值.IsChecked Then
				If TryParse(均值, AddressOf Double.Parse, 平均值Double) Then
					Exit Sub
				End If
			End If
			Dim 方差Double As Double
			If 规定方差.IsChecked Then
				If TryParse(方差, AddressOf Double.Parse, 方差Double) Then
					Exit Sub
				End If
				方差Double /= 范围 ^ 2
			End If
			If 规定均值.IsChecked Then
				If 规定方差.IsChecked Then
					平均值Double = (平均值Double - 最小值Double) / 范围
					Dim 共轭平均值 = 1 - 平均值Double
					方差Double = 平均值Double * 共轭平均值 / 方差Double - 1
					平均值Double *= 方差Double
					共轭平均值 *= 方差Double
					If 平均值Double < 0 OrElse 共轭平均值 < 0 Then
						报错("指定的范围和均值条件下，不可能实现如此大的方差")
					Else
						枚举随机数(From a As Double In 可靠的β分布(平均值Double, 共轭平均值).Take(生成个数UInteger) Select a * 范围 + 最小值Double)
					End If
				Else
					枚举随机数(连续β指数分布(最小值Double, 最大值Double, 平均值Double).Take(生成个数UInteger))
				End If
			Else
				If 规定方差.IsChecked Then
					方差Double = 1 / (8 * 方差Double) - 0.5
					If 方差Double < 0 Then
						报错("指定的范围和均值条件下，不可能实现如此大的方差")
					Else
						枚举随机数(From a As Double In 可靠的β分布(方差Double, 方差Double).Take(生成个数UInteger) Select a * 范围 + 最小值Double)
					End If
				Else
					枚举随机数(ContinuousUniform.Samples(最小值Double, 最大值Double).Take(生成个数UInteger))
				End If
			End If
		Else
			Dim 最小值BigInteger As BigInteger
			If TryParse(最小值, AddressOf BigInteger.Parse, 最小值BigInteger) Then
				Exit Sub
			End If
			Dim 最大值BigInteger As BigInteger
			If TryParse(最大值, AddressOf BigInteger.Parse, 最大值BigInteger) Then
				Exit Sub
			End If
			Dim 平均值BigInteger As BigInteger
			If 规定均值.IsChecked Then
				If TryParse(均值, AddressOf BigInteger.Parse, 平均值BigInteger) Then
					Exit Sub
				End If
			End If
			Dim 方差BigInteger As BigInteger
			If 规定方差.IsChecked Then
				If TryParse(方差, AddressOf BigInteger.Parse, 方差BigInteger) Then
					Exit Sub
				End If
			End If
			If 规定均值.IsChecked Then
				Dim n As BigInteger = 最大值BigInteger - 最小值BigInteger
				If 规定方差.IsChecked Then
					平均值BigInteger -= 最小值BigInteger
					If n > Integer.MaxValue Then
						n += 1
						Dim 平均值Double As Double = CDbl(平均值BigInteger + 0.5) / CDbl(n)
						Dim 共轭平均值 As Double = 1 - 平均值Double
						Dim 方差Double As Double = 平均值Double * 共轭平均值 * CDbl(n * n) / CDbl(方差BigInteger) - 1
						平均值Double *= 方差Double
						共轭平均值 *= 方差Double
						If 平均值Double < 0 OrElse 共轭平均值 < 0 Then
							报错("指定的范围和均值条件下，不可能实现如此大的方差")
						Else
							枚举随机数(From a As Double In 可靠的β分布(平均值Double, 共轭平均值).Take(生成个数UInteger) Select a * CDbl(n) - 0.5 + 最小值BigInteger)
						End If
					Else
						Dim 中间量BigInteger As BigInteger = 平均值BigInteger * (n - 平均值BigInteger)
						Dim 中间量Double As Double = CDbl(中间量BigInteger - 方差BigInteger) / CDbl(中间量BigInteger - n * 方差BigInteger)
						Dim a As Double = CDbl(-平均值BigInteger) * 中间量Double
						Dim b As Double = CDbl(平均值BigInteger - n) * 中间量Double
						If a < 0 OrElse b < 0 Then
							报错("指定的范围和均值条件下，不可能实现如此大的方差")
						Else
							枚举随机数(From c As Integer In β二项分布(a, b, n).Take(生成个数UInteger) Select c + 最小值BigInteger)
						End If
					End If
				Else
					枚举随机数(From a As BigInteger In 连续β指数分布(CDbl(最小值BigInteger - 0.5), CDbl(最大值BigInteger) + 0.5, 平均值BigInteger).Take(生成个数UInteger))
				End If
			Else
				If 规定方差.IsChecked Then
					Dim n As BigInteger = 最大值BigInteger - 最小值BigInteger
					If n > Integer.MaxValue Then
						n += 1
						Dim 方差Double As Double = CDbl(n * n) / CDbl(8 * 方差BigInteger) - 0.5
						If 方差Double < 0 Then
							报错("指定的范围和均值条件下，不可能实现如此大的方差")
						Else
							枚举随机数(From a As Double In 可靠的β分布(方差Double, 方差Double).Take(生成个数UInteger) Select a * CDbl(n) - 0.5 + 最小值BigInteger)
						End If
					Else
						方差BigInteger *= 4
						Dim 方差Double As Double = CDbl(方差BigInteger - n * n) / CDbl(2 * (n - 方差BigInteger))
						If 方差Double < 0 Then
							报错("指定的范围和均值条件下，不可能实现如此大的方差")
						Else
							枚举随机数(From a As Integer In β二项分布(方差Double, 方差Double, n).Take(生成个数UInteger) Select a + 最小值BigInteger)
						End If
					End If
				Else
					If 无重复.IsChecked Then
						If 生成个数UInteger > 最大值BigInteger - 最小值BigInteger + 1 Then
							报错("在如此小范围内生成这么多个随机数，不可能不重复")
						Else
							枚举随机数(随机排列(最小值BigInteger, 最大值BigInteger, 生成个数UInteger))
						End If
					Else
						枚举随机数(离散均匀分布(最小值BigInteger, 最大值BigInteger).Take(生成个数UInteger))
					End If
				End If
			End If
		End If
	End Sub

	Private Sub 禁止无重复(sender As Object, e As RoutedEventArgs) Handles 浮点.Checked, 规定均值.Checked, 规定方差.Checked
		无重复.IsEnabled = False
		Dim 来源 As CheckBox = sender
		漫游设置("数值." & 来源.Name) = 来源.IsChecked
	End Sub

	Private Sub 允许无重复(sender As Object, e As RoutedEventArgs) Handles 浮点.Unchecked, 规定均值.Unchecked, 规定方差.Unchecked
		If Not (浮点.IsChecked OrElse 规定均值.IsChecked OrElse 规定方差.IsChecked) Then
			无重复.IsEnabled = True
		End If
		Dim 来源 As CheckBox = sender
		漫游设置("数值." & 来源.Name) = 来源.IsChecked
	End Sub

	Private Sub 无重复_Checked(sender As Object, e As RoutedEventArgs) Handles 无重复.Checked
		浮点.IsEnabled = False
		规定均值.IsEnabled = False
		规定方差.IsEnabled = False
		漫游设置("数值.无重复") = 无重复.IsChecked
	End Sub

	Private Sub 无重复_Unchecked(sender As Object, e As RoutedEventArgs) Handles 无重复.Unchecked
		浮点.IsEnabled = True
		规定均值.IsEnabled = True
		规定方差.IsEnabled = True
		漫游设置("数值.无重复") = 无重复.IsChecked
	End Sub
End Class

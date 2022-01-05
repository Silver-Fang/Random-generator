Public Class 浮点随机数
	Inherits 随机数生成器(Of Double)
	ReadOnly 斜率 As Double
	ReadOnly 截距 As Double

	Sub New(最小值 As Double, 最大值 As Double)
		斜率 = 最大值 - 最小值
		截距 = 最小值
	End Sub

	Public Overrides Function 生成() As Double
		Return 生成器.NextDouble() * 斜率 + 截距
	End Function
End Class

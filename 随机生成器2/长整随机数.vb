Public Class 长整随机数
	Inherits 随机数生成器(Of Long)
	ReadOnly 斜率 As Double
	ReadOnly 截距 As Long

	Sub New(最小值 As Long, 最大值 As Long)
		斜率 = 最大值 - 最小值 + 1
		截距 = 最小值
	End Sub

	Public Overrides Function 生成() As Long
		Return Math.Ceiling(生成器.NextDouble * 斜率 + 截距)
	End Function
End Class

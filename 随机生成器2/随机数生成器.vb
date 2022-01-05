Public MustInherit Class 随机数生成器(Of T)
	Protected Shared ReadOnly 生成器 As New Random
	MustOverride Function 生成() As T
End Class

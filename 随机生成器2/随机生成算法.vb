Imports System.Numerics
Imports MathNet.Numerics
Imports MathNet.Numerics.Distributions

Module 随机生成算法
	Friend ReadOnly 随机生成器 As New System.Random

	Iterator Function 连续β指数分布(最小值 As Double, 最大值 As Double, 平均值 As Double) As IEnumerable(Of Double)
		Dim 斜率 As Double = 最大值 - 最小值
		Dim 指数 As Double = If(平均值 = 最小值, Double.PositiveInfinity, (最大值 - 平均值) / (平均值 - 最小值))
		Do
			Yield 随机生成器.NextDouble ^ 指数 * 斜率 + 最小值
		Loop
	End Function

	Iterator Function 离散均匀分布(最小值 As BigInteger, 最大值 As BigInteger) As IEnumerable(Of BigInteger)
		Dim 范围 As BigInteger = 最大值 - 最小值 + 1
		Dim 字节数 As UInteger = Math.Ceiling(BigInteger.Log(范围 * 范围, 256))
		Dim 字节序列 As Byte() = Enumerable.Repeat(Of Byte)(255, 字节数 + 1).ToArray
		字节序列(字节数) = 0
		Dim 随机数 As New BigInteger(字节序列)
		Dim 合法范围 As BigInteger = 随机数 - 随机数 Mod 范围
		Do
			Do
				随机生成器.NextBytes(字节序列)
				字节序列(字节数) = 0
				随机数 = New BigInteger(字节序列)
			Loop Until 随机数 < 合法范围
			Yield 随机数 Mod 范围 + 最小值
		Loop
	End Function

	Iterator Function 等差迭代器(初值 As BigInteger, 公差 As BigInteger) As IEnumerable(Of BigInteger)
		Do
			Yield 初值
			初值 += 公差
		Loop
	End Function

	Function 二项式系数(N As BigInteger, K As BigInteger) As BigInteger
		Return 等差迭代器(N, -1).Take(K).Aggregate(AddressOf BigInteger.Multiply) / 等差迭代器(1, 1).Take(K).Aggregate(AddressOf BigInteger.Multiply)
	End Function

	Function 随机排列(最小值 As BigInteger, 最大值 As BigInteger, 生成个数 As UInteger) As IEnumerable(Of BigInteger)
		Dim 排列(生成个数 - 1) As BigInteger
		排列(0) = 离散均匀分布(最小值, 最大值).First
		Dim 比较缓存 As Boolean()
		Dim 通过检查 As Boolean
		Dim 随机数 As BigInteger
		For a As UInteger = 1 To 生成个数 - 1
			随机数 = 离散均匀分布(最小值, 最大值 - a).First
			比较缓存 = Enumerable.Repeat(True, a).ToArray
			Do
				通过检查 = True
				For b As UInteger = 0 To a - 1
					If 比较缓存(b) AndAlso 随机数 >= 排列(b) Then
						比较缓存(b) = False
						通过检查 = False
						随机数 += 1
					End If
				Next
			Loop Until 通过检查
			排列(a) = 随机数
		Next
		Return 排列
	End Function

	Function 离散二项分布(最小值 As BigInteger, 最大值 As BigInteger, 平均值 As BigInteger) As IEnumerable(Of BigInteger)
		Dim n As BigInteger = 最大值 - 最小值
		If n <= Integer.MaxValue Then
			Dim p As Double = CDbl(平均值 - 最小值) / CDbl(n)
			Return From a As BigInteger In Distributions.Binomial.Samples(p, n)
		End If
	End Function

	Iterator Function 可靠的β分布(a As Double, b As Double) As IEnumerable(Of Double)
		If a = b Then
			For Each p As Double In Beta.Samples(a, b)
				Yield If(Double.IsNaN(p), Bernoulli.Sample(0.5), p)
			Next
		Else
			For Each p As Double In Beta.Samples(a, b)
				If Double.IsNaN(p) Then
					Continue For
				Else
					Yield p
				End If
			Next
		End If
	End Function

	Function β二项分布(a As Double, b As Double, n As Integer) As IEnumerable(Of Integer)
		Return From p As Double In 可靠的β分布(a, b) Select Binomial.Sample(p, n)
	End Function
End Module
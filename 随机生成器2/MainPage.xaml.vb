' https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

Imports 随机生成器2.App

''' <summary>
''' 可用于自身或导航至 Frame 内部的空白页。
''' </summary>
Public NotInheritable Class MainPage
    Inherits Page
    Sub New()

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        If 漫游设置("当前页") Is Nothing Then
            漫游设置("当前页") = "数值"
        End If
        Select Case 漫游设置("当前页")
            Case "数值"
                框架.Navigate(GetType(数值))
            Case "密码"
                框架.Navigate(GetType(密码))
            Case "汉字"
                框架.Navigate(GetType(汉字))
            Case "乱序"
                框架.Navigate(GetType(乱序))
            Case "帮助"
                框架.Navigate(GetType(帮助))
        End Select
    End Sub

    Private Sub 主页_数值_Tapped(sender As Object, e As TappedRoutedEventArgs) Handles 主页_数值.Tapped
        漫游设置("当前页") = "数值"
        框架.Navigate(GetType(数值))
    End Sub

    Private Sub 主页_密码_Tapped(sender As Object, e As TappedRoutedEventArgs) Handles 主页_密码.Tapped
        漫游设置("当前页") = "密码"
        框架.Navigate(GetType(密码))
    End Sub

    Private Sub 主页_汉字_Tapped(sender As Object, e As TappedRoutedEventArgs) Handles 主页_汉字.Tapped
        漫游设置("当前页") = "汉字"
        框架.Navigate(GetType(汉字))
    End Sub

    Private Sub 主页_帮助_Tapped(sender As Object, e As TappedRoutedEventArgs) Handles 主页_帮助.Tapped
        漫游设置("当前页") = "帮助"
        框架.Navigate(GetType(帮助))
    End Sub

    Private Sub 主页_乱序_Tapped(sender As Object, e As TappedRoutedEventArgs) Handles 主页_乱序.Tapped
        漫游设置("当前页") = "乱序"
        框架.Navigate(GetType(乱序))
    End Sub
End Class

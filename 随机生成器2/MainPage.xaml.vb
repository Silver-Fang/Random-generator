' https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

''' <summary>
''' 可用于自身或导航至 Frame 内部的空白页。
''' </summary>
Public NotInheritable Class MainPage
    Inherits Page
    Sub New()

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        If App.漫游设置.Values("当前页") Is Nothing Then
            App.漫游设置.Values("当前页") = "数值"
        End If
        Select Case App.漫游设置.Values("当前页")
            Case "数值"
                框架.Navigate(GetType(数值))

        End Select
    End Sub
End Class

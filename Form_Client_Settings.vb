Imports Newtonsoft.Json

Public Class Form_Client_Settings
    Property NameChanged As Boolean
    Property UrlChanged As Boolean
    Property ApikeyChanged As Boolean
    Property UsernameChanged As Boolean
    Property PasswordChanged As Boolean
    Property FormLoaded As Boolean
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UrlChanged = False
        ApikeyChanged = False
        UsernameChanged = False
        PasswordChanged = False
        FormLoaded = False
        With ReferenceOfClient
            Label_IdAndName.Text = .Id + " - " + .Name
        End With
        TextBox_url.Text = ReferenceOfClient.Url
        TextBox_apikey.Text = MaskText(ReferenceOfClient.ApiKey)
        TextBox_username.Text = ReferenceOfClient.Username
        TextBox_password.Text = MaskText(ReferenceOfClient.Password)
        formLoaded = True
    End Sub
    Private Sub Button_Save_Click(sender As Object, e As EventArgs) Handles Button_Save.Click
        UpdateClient(ReferenceOfClient)
        Me.Close()
    End Sub
    Private Sub UpdateClient(ByRef InstanceOfClient As Client)
        If urlChanged Then
            InstanceOfClient.Url = TextBox_url.Text
        End If
        If apikeyChanged Then
            InstanceOfClient.ApiKey = TextBox_apikey.Text
        End If
        If usernameChanged Then
            InstanceOfClient.Username = TextBox_username.Text
        End If
        If passwordChanged Then
            InstanceOfClient.Password = TextBox_password.Text
        End If
    End Sub
    Private Sub TextBox_Url_Changed(sender As Object, e As EventArgs) Handles TextBox_url.TextChanged
        If formLoaded And Not urlChanged Then
            urlChanged = True
            Button_Save.Visible = True
        End If
    End Sub
    Private Sub TextBox_Apikey_Changed(sender As Object, e As EventArgs) Handles TextBox_apikey.TextChanged
        If formLoaded And Not apikeyChanged Then
            apikeyChanged = True
            Button_Save.Visible = True
        End If
    End Sub
    Private Sub TextBox_Username_Changed(sender As Object, e As EventArgs) Handles TextBox_username.TextChanged
        If formLoaded And Not usernameChanged Then
            usernameChanged = True
            Button_Save.Visible = True
        End If
    End Sub
    Private Sub TextBox_Password_Changed(sender As Object, e As EventArgs) Handles TextBox_password.TextChanged
        If formLoaded And Not passwordChanged Then
            passwordChanged = True
            Button_Save.Visible = True
        End If
    End Sub
End Class
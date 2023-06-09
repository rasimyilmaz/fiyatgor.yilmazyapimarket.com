Public Class Form3

    Private Sub Form3_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Form_Main.Load_Menu()
    End Sub
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.TextBoxD1.Text = My.Settings.Dizayn1_yol
        Me.TextBoxT1.Text = My.Settings.Dizayn1_isim
        Me.TextBoxY1.Text = My.Settings.Dizayn1_yazici

        Me.TextBoxD2.Text = My.Settings.Dizayn2_yol
        Me.TextBoxT2.Text = My.Settings.Dizayn2_isim
        Me.TextBoxY2.Text = My.Settings.Dizayn2_yazici

        Me.TextBoxD3.Text = My.Settings.Dizayn3_yol
        Me.TextBoxT3.Text = My.Settings.Dizayn3_isim
        Me.TextBoxY3.Text = My.Settings.Dizayn3_yazici

        Me.TextBoxD4.Text = My.Settings.Dizayn4_yol
        Me.TextBoxT4.Text = My.Settings.Dizayn4_isim
        Me.TextBoxY4.Text = My.Settings.Dizayn4_yazici


        Me.TextBoxD5.Text = My.Settings.Dizayn5_yol
        Me.TextBoxT5.Text = My.Settings.Dizayn5_isim
        Me.TextBoxY5.Text = My.Settings.Dizayn5_yazici


        Me.TextBoxD6.Text = My.Settings.Dizayn6_yol
        Me.TextBoxT6.Text = My.Settings.Dizayn6_isim
        Me.TextBoxY6.Text = My.Settings.Dizayn6_yazici

        Me.TextBoxD7.Text = My.Settings.Dizayn7_yol
        Me.TextBoxT7.Text = My.Settings.Dizayn7_isim
        Me.TextBoxY7.Text = My.Settings.Dizayn7_yazici
    End Sub
    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        If Me.TextBoxT1.Text = "" Or Me.TextBoxD1.Text = "" Or Me.TextBoxY1.Text = "" Then
            MsgBox("Boş değer bırakmayınız.", MsgBoxStyle.OkOnly, "Bilgi")
            Exit Sub
        End If
        My.Settings.Dizayn1_yol = Me.TextBoxD1.Text
        My.Settings.Dizayn1_isim = Me.TextBoxT1.Text
        My.Settings.Dizayn1_yazici = Me.TextBoxY1.Text
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Me.TextBoxT2.Text = "" Or Me.TextBoxY2.Text = "" Or Me.TextBoxD2.Text = "" Then
            MsgBox("Boş değer bırakmayınız.", MsgBoxStyle.OkOnly, "Bilgi")
            Exit Sub
        End If
        My.Settings.Dizayn2_yol = Me.TextBoxD2.Text
        My.Settings.Dizayn2_isim = Me.TextBoxT2.Text
        My.Settings.Dizayn2_yazici = Me.TextBoxY2.Text
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Me.TextBoxT3.Text = "" Or Me.TextBoxY3.Text = "" Or Me.TextBoxD3.Text = "" Then
            MsgBox("Boş değer bırakmayınız.", MsgBoxStyle.OkOnly, "Bilgi")
            Exit Sub
        End If
        My.Settings.Dizayn3_yol = Me.TextBoxD3.Text
        My.Settings.Dizayn3_isim = Me.TextBoxT3.Text
        My.Settings.Dizayn3_yazici = Me.TextBoxY3.Text
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If Me.TextBoxT4.Text = "" Or Me.TextBoxY4.Text = "" Or Me.TextBoxT4.Text = "" Then
            MsgBox("Boş değer bırakmayınız.", MsgBoxStyle.OkOnly, "Bilgi")
            Exit Sub
        End If
        My.Settings.Dizayn4_yol = Me.TextBoxD4.Text
        My.Settings.Dizayn4_isim = Me.TextBoxT4.Text
        My.Settings.Dizayn4_yazici = Me.TextBoxY4.Text
    End Sub
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        My.Settings.Dizayn1_isim = ""
        My.Settings.Dizayn1_yazici = ""
        My.Settings.Dizayn1_yol = ""
        TextBoxD1.Text = ""
        TextBoxT1.Text = ""
        TextBoxY1.Text = ""
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        My.Settings.Dizayn2_isim = ""
        My.Settings.Dizayn2_yazici = ""
        My.Settings.Dizayn2_yol = ""
        TextBoxD2.Text = ""
        TextBoxT2.Text = ""
        TextBoxY2.Text = ""
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        My.Settings.Dizayn3_isim = ""
        My.Settings.Dizayn3_yazici = ""
        My.Settings.Dizayn3_yol = ""
        TextBoxD3.Text = ""
        TextBoxT3.Text = ""
        TextBoxY3.Text = ""
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        My.Settings.Dizayn4_isim = ""
        My.Settings.Dizayn4_yazici = ""
        My.Settings.Dizayn4_yol = ""
        TextBoxD4.Text = ""
        TextBoxT4.Text = ""
        TextBoxY4.Text = ""
    End Sub

    Private Sub ButtonD1_Click(sender As Object, e As EventArgs) Handles ButtonD1.Click
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.TextBoxD1.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub ButtonD2_Click(sender As Object, e As EventArgs) Handles ButtonD2.Click
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.TextBoxD2.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub ButtonD3_Click(sender As Object, e As EventArgs) Handles ButtonD3.Click
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.TextBoxD3.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub ButtonD4_Click(sender As Object, e As EventArgs) Handles ButtonD4.Click
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.TextBoxD4.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub ButtonY1_Click(sender As Object, e As EventArgs) Handles ButtonY1.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.TextBoxY1.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub ButtonY2_Click(sender As Object, e As EventArgs) Handles ButtonY2.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.TextBoxY2.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub ButtonY3_Click(sender As Object, e As EventArgs) Handles ButtonY3.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.TextBoxY3.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub
    Private Sub ButtonY4_Click(sender As Object, e As EventArgs) Handles ButtonY4.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.TextBoxY4.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        My.Settings.Dizayn5_isim = ""
        My.Settings.Dizayn5_yazici = ""
        My.Settings.Dizayn5_yol = ""
        TextBoxD5.Text = ""
        TextBoxT5.Text = ""
        TextBoxY5.Text = ""
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        My.Settings.Dizayn6_isim = ""
        My.Settings.Dizayn6_yazici = ""
        My.Settings.Dizayn6_yol = ""
        TextBoxD6.Text = ""
        TextBoxT6.Text = ""
        TextBoxY6.Text = ""
    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        My.Settings.Dizayn7_isim = ""
        My.Settings.Dizayn7_yazici = ""
        My.Settings.Dizayn7_yol = ""
        TextBoxD7.Text = ""
        TextBoxT7.Text = ""
        TextBoxY7.Text = ""
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        If Me.TextBoxT5.Text = "" Or Me.TextBoxY5.Text = "" Or Me.TextBoxT5.Text = "" Then
            MsgBox("Boş değer bırakmayınız.", MsgBoxStyle.OkOnly, "Bilgi")
            Exit Sub
        End If
        My.Settings.Dizayn5_yol = Me.TextBoxD5.Text
        My.Settings.Dizayn5_isim = Me.TextBoxT5.Text
        My.Settings.Dizayn5_yazici = Me.TextBoxY5.Text
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        If Me.TextBoxT6.Text = "" Or Me.TextBoxY6.Text = "" Or Me.TextBoxT6.Text = "" Then
            MsgBox("Boş değer bırakmayınız.", MsgBoxStyle.OkOnly, "Bilgi")
            Exit Sub
        End If
        My.Settings.Dizayn6_yol = Me.TextBoxD6.Text
        My.Settings.Dizayn6_isim = Me.TextBoxT6.Text
        My.Settings.Dizayn6_yazici = Me.TextBoxY6.Text
    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        If Me.TextBoxT7.Text = "" Or Me.TextBoxY7.Text = "" Or Me.TextBoxT7.Text = "" Then
            MsgBox("Boş değer bırakmayınız.", MsgBoxStyle.OkOnly, "Bilgi")
            Exit Sub
        End If
        My.Settings.Dizayn7_yol = Me.TextBoxD7.Text
        My.Settings.Dizayn7_isim = Me.TextBoxT7.Text
        My.Settings.Dizayn7_yazici = Me.TextBoxY7.Text
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.TextBoxD5.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.TextBoxD6.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.TextBoxD7.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.TextBoxY5.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.TextBoxY6.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.TextBoxY7.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub
End Class
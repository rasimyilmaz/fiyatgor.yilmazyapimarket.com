Public Class Form4
    Private Sub Form4_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        My.Settings.StokKodu = Me.TextBoxStokKodu.Text
        My.Settings.Barkod = Me.TextBoxBarkod.Text
        My.Settings.isim = Me.TextBoxisim.Text
        My.Settings.Kisim = Me.TextBoxkisim.Text
        My.Settings.Fiyat = Me.TextBoxFiyat.Text
        My.Settings.Birim = Me.TextBoxBirim.Text
        My.Settings.Doviz = Me.TextBoxDoviz.Text
        My.Settings.BTarihi = Me.TextBoxBTarihi.Text
        My.Settings.FTarihi = Me.TextBoxFTarihi.Text
        My.Settings.MUrun = Me.TextBoxMUrun.Text
    End Sub
    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles Me.Load
        TextBoxStokKodu.Text = My.Settings.StokKodu
        TextBoxBarkod.Text = My.Settings.Barkod
        TextBoxisim.Text = My.Settings.isim
        TextBoxkisim.Text = My.Settings.Kisim
        TextBoxFiyat.Text = My.Settings.Fiyat
        TextBoxBirim.Text = My.Settings.Birim
        TextBoxDoviz.Text = My.Settings.Doviz
        TextBoxBTarihi.Text = My.Settings.BTarihi
        TextBoxFTarihi.Text = My.Settings.FTarihi
        TextBoxMUrun.Text = My.Settings.MUrun
    End Sub

End Class
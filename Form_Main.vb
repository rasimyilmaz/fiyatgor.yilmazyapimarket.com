Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Text
Imports System.Web.UI.WebControls
Imports Newtonsoft.Json

Public Class Form_Main
    Private Property BackgroundScheduler As TaskScheduler
    Private Property UiScheduler As TaskScheduler
    Private IsListBoxClientInitialized As Boolean
    Private WithEvents B1 As BackGroundWorker1
    Private WithEvents B2 As BackGroundWorker2
    Public WithEvents B3 As BackGroundWorker3

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ResizeMe()
        'SetDefaultStyle(DataGridView1)
        B1 = New BackGroundWorker1
        B1.Start()
        B2 = New BackGroundWorker2
        B3 = New BackGroundWorker3
        'UiScheduler = TaskScheduler.FromCurrentSynchronizationContext()
        If (Not FileExists(FirstUseFilename)) Then
            FirstUse()
        End If
        Me.TextBox_Barcode.Select()
        Me.Load_Menu()
    End Sub

    Public Sub UpdateClientsUI() Handles B1.ClientsReady
        ToolStripMenuItemActionRemove.Visible = Convert.ToBoolean(Clients.Count)
        Dim i As Integer = ClientsToolStripMenuItem.DropDownItems.Count - 1
        For j = i To 0 Step -1
            If (Not ClientsToolStripMenuItem.DropDownItems.Item(j).Name.StartsWith("ToolStripMenuItemAction")) Then
                ClientsToolStripMenuItem.DropDownItems.RemoveAt(j)
            End If
        Next
        i = Clients.Count - 1
        ListBoxClients.DataSource = Clients
        IsListBoxClientInitialized = True
        For j = i To 0 Step -1
            Dim dropdownitem As New ToolStripMenuItem(Clients.Item(j).Name, My.Resources.edit, New EventHandler(AddressOf ToolStripMenuItemClick))
            ClientsToolStripMenuItem.DropDownItems.Insert(0, dropdownitem)
        Next
        RecoverLastState()
        ListBoxClients.SelectedItem = ReferenceOfClient

    End Sub
    Public Sub SelectReferenceOfRestClient() Handles B1.RestClientsReady
        ReferenceOfRestClient = RestClients.Item(ListBoxClients.SelectedIndex).Value
    End Sub

    Private Sub ToolStripMenuItemClick(sender As Object, e As EventArgs)

    End Sub


    Private Sub FillWithProducts() Handles B3.SearchFinished

        If B3.Successed Then
            If B3.InstanceOfProductsResponse.Status <> 200 Then
                MsgBox(B3.InstanceOfProductsResponse.Message, MsgBoxStyle.Critical, "Veri iletim hatası")
                Exit Sub
            End If
        Else
            MsgBox(B3.ExceptionMessage, MsgBoxStyle.Critical, "Veri işleme hatası")
            Exit Sub
        End If
        Try
            DataGridView1.Rows.Add()
            Dim i As Integer
            For Each Product In B3.InstanceOfProductsResponse.Products
                i = 0
                Dim Row As New DataGridViewRow()
                Row.CreateCells(DataGridView1)
                Row.Cells.Item(i).Value = Product.Code
                i += 1
                Row.Cells.Item(i).Value = Product.Barcode
                i += 1
                Row.Cells.Item(2).Value = Product.Name
                i += 1
                Row.Cells.Item(3).Value = Product.Price
                i += 1
                Row.Cells.Item(4).Value = Product.CurrencyName
                For Each item In Product.WarehouseQuantity_Collection
                    i += 1
                    Row.Cells.Item(i).Value = item.Quantity
                Next
                i += 1
                Row.Cells.Item(i).Value = Product.Unit
                i += 1
                Row.Cells.Item(i).Value = Product.LocalCurrencyPrice
                i += 1
                Row.Cells.Item(i).Value = Product.ShortName
                i += 1
                Row.Cells.Item(i).Value = Product.PriceChangeDate
                i += 1
                Row.Cells.Item(i).Value = Product.PriceChangeUser
                DataGridView1.Rows.Add(Row)
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, AcceptButton)
        End Try
    End Sub

    Public Async Sub CheckAuth()
        Try
            Dim response As CheckAuthResponse = Await CheckAuthAsync(ReferenceOfClient, GetInstanceOfRestClient(ReferenceOfClient), ProcessorId)
            If response.AccessGrant Then
                MsgBox("Giriş Başarılı")
            Else
                MsgBox("Giriş Başarısız")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub ChangeDataGridView(ByRef ReferenceOfDataGridView As DataGridView, ByRef ReferenceOfListOfDataGridViewTextBoxColumn As List(Of DataGridViewTextBoxColumn))
        ReferenceOfDataGridView.Columns.Clear()
        ReferenceOfDataGridView.Columns.AddRange(ReferenceOfListOfDataGridViewTextBoxColumn.ToArray)
    End Sub

    Private Sub Form1_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Form_Client_Settings.Close()
        Form3.Close()
        Form4.Close()
    End Sub

    Private Sub MakeSearch()
        If Check() Then
            Dim price As Decimal = 0
            If Not String.IsNullOrEmpty(TextBox_Price.Text) Then
                price = Decimal.Parse(TextBox_Price.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture)
            End If
            Dim InstanceOfProductCriteria As New ProductCriteria With {
                .ProductCode = TextBox_Code.Text,
                .ProductBarcode = TextBox_Barcode.Text,
                .ProductName = TextBox_Name.Text,
                .ProductPrice = price,
                .IsCodeFilled = TextBox_Code.Text.Length,
                .IsBarcodeFilled = TextBox_Barcode.Text.Length,
                .IsNameFilled = TextBox_Name.Text.Length,
                .IsPriceFilled = TextBox_Price.Text.Length
                }
            B3.InstanceOfProductCriteria = InstanceOfProductCriteria
            B3.Start()
        End If
    End Sub

    Private Sub KeyPressCatcher(sender As Object, e As KeyPressEventArgs) Handles TextBox_Barcode.KeyPress, TextBox_Code.KeyPress, TextBox_Name.KeyPress, TextBox_Price.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            MakeSearch()
            If sender.Name = "TextBox_Barcode" Then
                TextBox_Barcode.SelectAll()
            End If
        End If
        If sender.Name = "TextBox_Barcode" Or sender.name = "TextBox_Price" Then
            If Char.IsLetter(e.KeyChar) Then
                e.KeyChar = ""
            End If
            If e.KeyChar = "," Or e.KeyChar = "." Then
                If TextBox_Price.Text.Length = 0 Or TextBox_Price.Text.Contains(".") Then
                    e.KeyChar = ""
                Else
                    e.KeyChar = "."
                End If
            End If
        End If
    End Sub

    Public Sub Clear()
        TextBox_Barcode.Clear()
        TextBox_Code.Clear()
        TextBox_Name.Clear()
        TextBox_Price.Clear()
        TextBox_Barcode.Select()
    End Sub

    Public Function Check() As Boolean
        If TextBox_Barcode.Text = "" And TextBox_Code.Text = "" And TextBox_Name.Text = "" And TextBox_Price.Text = "" Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Sub FiyatTarih()
        If DataGridView1.RowCount = 0 Or DataGridView1.SelectedCells.Count = 0 Then
            Exit Sub
        End If
        Dim index As Integer = DataGridView1.SelectedCells(0).RowIndex
        Dim myDate As DateTime = DataGridView1.Rows(index).Cells("PriceChangeDate").Value
        Dim myUser As String = DataGridView1.Rows(index).Cells("PriceChangeUser").Value
        Dim caption As String = DataGridView1.Rows(index).Cells("Name").Value
        Dim originalCulture As Globalization.CultureInfo = Threading.Thread.CurrentThread.CurrentCulture
        Threading.Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo("tr-TR")
        MsgBox("Fiyat Değiştirme Tarihi : " + myDate.ToShortDateString + " " + myDate.ToShortTimeString + vbNewLine + "Kullanıcı Adı : " + myUser, MsgBoxStyle.Information, caption)
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F1
                Print(My.Settings.Dizayn1_yol, My.Settings.Dizayn1_yazici)
            Case Keys.F2
                Print(My.Settings.Dizayn2_yol, My.Settings.Dizayn2_yazici)
            Case Keys.F3
                Print(My.Settings.Dizayn3_yol, My.Settings.Dizayn3_yazici)
            Case Keys.F4
                Print(My.Settings.Dizayn4_yol, My.Settings.Dizayn4_yazici)
            Case Keys.F5
                Print(My.Settings.Dizayn5_yol, My.Settings.Dizayn5_yazici)
            Case Keys.F6
                Print(My.Settings.Dizayn6_yol, My.Settings.Dizayn6_yazici)
            Case Keys.F7
                Print(My.Settings.Dizayn7_yol, My.Settings.Dizayn7_yazici)
            Case Keys.Escape
                Clear()
            Case Keys.F9
                FiyatTarih()
        End Select
    End Sub

    Private Sub ResizeMe()
        Dim Dx As Integer = Screen.PrimaryScreen.Bounds.Width
        Dim Dy As Integer = Screen.PrimaryScreen.Bounds.Height
        Dim Fx As Integer = Me.Size.Width
        Dim Fy As Integer = Me.Size.Height
        Dim Ix As Integer = (Dx - 1024) * 0.5
        Dim Iy As Integer = (Dy - 576) * 0.75
        Me.Scale(New Drawing.SizeF(((Fx + Ix) / Fx), ((Fy + Iy) / Fy)))
        Me.CenterToScreen()
    End Sub

    Private Function ClientToolStripClick() As EventHandler
        Throw New NotImplementedException()
    End Function

    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        If e.Button = MouseButtons.Right Then
            Me.ContextMenuStrip1.Show(MousePosition)
        End If
    End Sub
    Private Sub Print(yol As String, yazici As String)
        If DataGridView1.Rows.Count = 0 Or DataGridView1.SelectedCells.Count = 0 Or yol = "" Or yazici = "" Then
            Exit Sub
        End If
        Dim PrintString As String
        Dim index As Integer = DataGridView1.SelectedCells(0).RowIndex
        Dim Barkod As String = DataGridView1.Rows(index).Cells("Barcode").Value.ToString
        Dim Kod As String = Ceng(DataGridView1.Rows(index).Cells("Code").Value)
        Dim isim As String = Ceng(DataGridView1.Rows(index).Cells("Name").Value)
        Dim Kisim As String = Ceng(DataGridView1.Rows(index).Cells("ShortName").Value)
        Dim Price = DataGridView1.Rows(index).Cells("Price").Value
        Dim Doviz As String = DataGridView1.Rows(index).Cells("Currency").Value
        Dim Birim = DataGridView1.Rows(index).Cells("Unit").Value
        Dim FTarih As Date
        Try
            FTarih = StringISO8601WithZToDateTime(DataGridView1.Rows(index).Cells("PriceChangeDate").Value)
        Catch ex As Exception
            FTarih = Today
        End Try
        Dim Murun As String
        Dim Quantity As String = "1"
        Select Case Barkod.Count
            Case 13
                'Barkod = Barkod.Substring(0, 12)
                PrintString = My.Computer.FileSystem.ReadAllText(yol)
            Case 7
                PrintString = My.Computer.FileSystem.ReadAllText(yol)
                Dim FillWithZero As Integer = 0
                Quantity = InputBox("Ürün Kaç " + DataGridView1.Rows(index).Cells("Unit").Value + " ?", "Miktarlı Barkod : " + isim, "1", , )
                If Not IsNumeric(Quantity) Then
                    Exit Sub
                End If
                If CInt(Quantity) > 99999 Then
                    Quantity = "1"
                End If
                Price *= CInt(Quantity)
                FillWithZero = 5 - Quantity.Length
                While FillWithZero > 0
                    Quantity = Quantity.Insert(0, "0")
                    FillWithZero -= 1
                End While
                Barkod += Quantity
                ''Barkod += ChecksumEAN13(Barkod).ToString
            Case 8
                Try
                    PrintString = My.Computer.FileSystem.ReadAllText(yol + "8")
                Catch ex As Exception
                    MsgBox(yol + "8 dosyasını hazırlayınız.", MsgBoxStyle.Information, "Dosya Bulunamadı.")
                    Exit Sub
                End Try
            Case Else
                Exit Sub
        End Select
        Dim NumberOfPrint As String
        NumberOfPrint = InputBox("Kaç sıra barkod basalım ?", "Basim Adeti : " + isim, "1", , )
        If Not IsNumeric(NumberOfPrint) Then
            Exit Sub
        End If
        If Kisim = Nothing Then
            Kisim = isim.Substring(0, 25)
        End If
        Murun = CStr(CInt(Quantity)) + " " + Birim
        If My.Settings.StokKodu.Length > 0 Then
            ModifyPrintString(My.Settings.StokKodu, Kod, PrintString)
        End If
        If My.Settings.Barkod.Length > 0 Then
            ModifyPrintString(My.Settings.Barkod, Barkod, PrintString)
        End If
        If My.Settings.isim.Length > 0 Then
            ModifyPrintString(My.Settings.isim, isim, PrintString)
        End If
        If My.Settings.Kisim.Length > 0 Then
            ModifyPrintString(My.Settings.Kisim, Kisim, PrintString)
        End If
        If My.Settings.Fiyat.Length > 0 Then
            ModifyPrintString(My.Settings.Fiyat, Price.ToString(), PrintString)
        End If
        If My.Settings.Doviz.Length > 0 Then
            ModifyPrintString(My.Settings.Doviz, Doviz, PrintString)
        End If
        If My.Settings.Birim.Length > 0 Then
            ModifyPrintString(My.Settings.Birim, Birim, PrintString)
        End If
        If My.Settings.BTarihi.Length > 0 Then
            ModifyPrintString(My.Settings.BTarihi, Today.ToString("dd.MM.yyyy"), PrintString)
        End If
        If My.Settings.FTarihi.Length > 0 Then
            ModifyPrintString(My.Settings.FTarihi, FTarih.ToString("dd.MM.yyyy"), PrintString)
        End If
        If My.Settings.MUrun.Length > 0 Then
            ModifyPrintString(My.Settings.MUrun, Murun, PrintString)
        End If
        My.Computer.FileSystem.WriteAllText("temp.prn", PrintString, False)
        For k As Integer = 1 To CInt(NumberOfPrint)
            RawPrinterHelper.SendFileToPrinter(yazici, "temp.prn")
            'Execute("Insert into StokPrint(Kod) Values('" + Kod + "')")
        Next
    End Sub
    Public Sub ModifyPrintString(ByVal param As String, ByVal val As String, ByRef str As String)
        Dim sPattern As String = "{" + param + "(,\d{0,2})?}"
        Dim result As String = RegularExpressions.Regex.Match(str, sPattern, RegularExpressions.RegexOptions.None).Value
        Dim i As Integer = result.IndexOf(",")
        Dim finalValue As String
        If i <> -1 Then
            Dim k As Integer = result.IndexOf("}")
            Dim l As String = result.Substring(i + 1, k - i - 1)
            Dim intL As Integer = 0
            If Integer.TryParse(l, intL) Then
                finalValue = val.Substring(0, intL)
            Else
                finalValue = val
            End If
        Else
            finalValue = val
        End If
        str = RegularExpressions.Regex.Replace(str, sPattern, finalValue, RegularExpressions.RegexOptions.None)
    End Sub

    Public Sub Load_Menu()
        Me.ToolStripMenuItem1.Text = My.Settings.Dizayn1_isim
        Me.ToolStripMenuItem2.Text = My.Settings.Dizayn2_isim
        Me.ToolStripMenuItem3.Text = My.Settings.Dizayn3_isim
        Me.ToolStripMenuItem4.Text = My.Settings.Dizayn4_isim
        Me.ToolStripMenuItem5.Text = My.Settings.Dizayn5_isim
        Me.ToolStripMenuItem6.Text = My.Settings.Dizayn6_isim
        Me.ToolStripMenuItem7.Text = My.Settings.Dizayn7_isim
        If My.Settings.Dizayn1_isim = "" Then
            Me.ToolStripMenuItem1.Visible = False
        Else
            Me.ToolStripMenuItem1.Visible = True
        End If
        If My.Settings.Dizayn2_isim = "" Then
            Me.ToolStripMenuItem2.Visible = False
        Else
            Me.ToolStripMenuItem2.Visible = True
        End If
        If My.Settings.Dizayn3_isim = "" Then
            Me.ToolStripMenuItem3.Visible = False
        Else
            Me.ToolStripMenuItem3.Visible = True
        End If
        If My.Settings.Dizayn4_isim = "" Then
            Me.ToolStripMenuItem4.Visible = False
        Else
            Me.ToolStripMenuItem4.Visible = True
        End If
        If My.Settings.Dizayn5_isim = "" Then
            Me.ToolStripMenuItem5.Visible = False
        Else
            Me.ToolStripMenuItem5.Visible = True
        End If
        If My.Settings.Dizayn6_isim = "" Then
            Me.ToolStripMenuItem6.Visible = False
        Else
            Me.ToolStripMenuItem6.Visible = True
        End If
        If My.Settings.Dizayn7_isim = "" Then
            Me.ToolStripMenuItem7.Visible = False
        Else
            Me.ToolStripMenuItem7.Visible = True
        End If
    End Sub
    Private Sub YazıcıAyarıToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles YazıcıAyarıToolStripMenuItem.Click
        Form3.Show()
    End Sub
    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Print(My.Settings.Dizayn1_yol, My.Settings.Dizayn1_yazici)
    End Sub
    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        Print(My.Settings.Dizayn2_yol, My.Settings.Dizayn2_yazici)
    End Sub
    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        Print(My.Settings.Dizayn3_yol, My.Settings.Dizayn3_yazici)
    End Sub
    Private Sub ToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem4.Click
        Print(My.Settings.Dizayn4_yol, My.Settings.Dizayn4_yazici)
    End Sub
    Public Class RawPrinterHelper
        ' Yapı ve API bildirimleri:
        <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
        Structure DOCINFOW
            <MarshalAs(UnmanagedType.LPWStr)> Public pDocName As String
            <MarshalAs(UnmanagedType.LPWStr)> Public pOutputFile As String
            <MarshalAs(UnmanagedType.LPWStr)> Public pDataType As String
        End Structure

        <DllImport("winspool.Drv", EntryPoint:="OpenPrinterW",
           SetLastError:=True, CharSet:=CharSet.Unicode,
           ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
        Public Shared Function OpenPrinter(ByVal src As String, ByRef hPrinter As IntPtr, ByVal pd As Long) As Boolean
        End Function
        <DllImport("winspool.Drv", EntryPoint:="ClosePrinter",
           SetLastError:=True, CharSet:=CharSet.Unicode,
           ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
        Public Shared Function ClosePrinter(ByVal hPrinter As IntPtr) As Boolean
        End Function
        <DllImport("winspool.Drv", EntryPoint:="StartDocPrinterW",
           SetLastError:=True, CharSet:=CharSet.Unicode,
           ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
        Public Shared Function StartDocPrinter(ByVal hPrinter As IntPtr, ByVal level As Int32, ByRef pDI As DOCINFOW) As Boolean
        End Function
        <DllImport("winspool.Drv", EntryPoint:="EndDocPrinter",
           SetLastError:=True, CharSet:=CharSet.Unicode,
           ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
        Public Shared Function EndDocPrinter(ByVal hPrinter As IntPtr) As Boolean
        End Function
        <DllImport("winspool.Drv", EntryPoint:="StartPagePrinter",
           SetLastError:=True, CharSet:=CharSet.Unicode,
           ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
        Public Shared Function StartPagePrinter(ByVal hPrinter As IntPtr) As Boolean
        End Function
        <DllImport("winspool.Drv", EntryPoint:="EndPagePrinter",
           SetLastError:=True, CharSet:=CharSet.Unicode,
           ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
        Public Shared Function EndPagePrinter(ByVal hPrinter As IntPtr) As Boolean
        End Function
        <DllImport("winspool.Drv", EntryPoint:="WritePrinter",
           SetLastError:=True, CharSet:=CharSet.Unicode,
           ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
        Public Shared Function WritePrinter(ByVal hPrinter As IntPtr, ByVal pBytes As IntPtr, ByVal dwCount As Int32, ByRef dwWritten As Int32) As Boolean
        End Function

        ' SendBytesToPrinter()
        ' İşleve bir yazıcı adı ve yönetilmeyen bir bayt dizisi verildiğinde,
        ' işlev bu baytları yazdırma sırasına gönderir.
        ' Başarılı olduğunda True, hata durumunda False döndürür.
        Public Shared Function SendBytesToPrinter(ByVal szPrinterName As String, ByVal pBytes As IntPtr, ByVal dwCount As Int32) As Boolean
            Dim hPrinter As IntPtr      ' Yazıcı tanıtıcı.
            Dim dwError As Int32        ' Son hata - bir sorun ortaya çıkarsa.
            Dim di As New DOCINFOW          ' Belgenizi tanımlar (ad, bağlantı noktası, veri türü).
            Dim dwWritten As Int32      ' WritePrinter() tarafından yazılan bayt sayısı.
            Dim bSuccess As Boolean     ' Başarı kodunuz.

            ' DOCINFO yapısını hazırla.
            With di
                .pDocName = "Barkod Basımı"
                .pDataType = "RAW"
            End With
            ' Başarılı olduğu belirlenmedikçe başarısız olduğunu varsay.
            bSuccess = False
            If OpenPrinter(szPrinterName, hPrinter, 0) Then
                If StartDocPrinter(hPrinter, 1, di) Then
                    If StartPagePrinter(hPrinter) Then
                        ' Yazıcıya özel baytları yazıcıya yaz.
                        bSuccess = WritePrinter(hPrinter, pBytes, dwCount, dwWritten)
                        EndPagePrinter(hPrinter)
                    End If
                    EndDocPrinter(hPrinter)
                End If
                ClosePrinter(hPrinter)
            End If
            ' Başarılı olmadıysa, GetLastError neden başarısız olunduğu
            ' hakkında daha fazla bilgi verebilir.
            If bSuccess = False Then
                dwError = Marshal.GetLastWin32Error()
            End If
            Return bSuccess
        End Function
        ' SendBytesToPrinter()
        ' SendFileToPrinter()
        ' İşleve bir dosya adı ve bir yazıcı adı verildiğinde,
        ' işlev dosyanın içeriğini okur ve
        ' içeriği yazıcıya gönderir.
        ' Dosyanın yazdırılmaya hazır veri içerdiğini kabul eder.
        ' SendBytesToPrinter işlevinin nasıl kullanılacağını gösterir.
        ' Başarılı olduğunda True, hata durumunda False döndürür.
        Public Shared Function SendFileToPrinter(ByVal szPrinterName As String, ByVal szFileName As String) As Boolean
            ' Dosyayı aç.
            Dim fs As New FileStream(szFileName, FileMode.Open)
            ' Dosya için bir BinaryReader oluştur.
            Dim br As New BinaryReader(fs)
            ' Dosya içeriğini alacak kadar büyük bir bayt dizisi oluştur.
            'Dim bytes(fs.Length) As Byte
            Dim bytes As Byte()
            Dim bSuccess As Boolean
            ' Yönetilmeyen işaretçiniz.
            Dim pUnmanagedBytes As IntPtr

            ' Dosya içeriğini dizi içine oku.
            bytes = br.ReadBytes(fs.Length)
            ' Bu baytlar için bir miktar yönetilmeyen bellek ayır.
            pUnmanagedBytes = Marshal.AllocCoTaskMem(fs.Length)
            ' Yönetilen bayt dizini yönetilmeyen diziye kopyala.
            Marshal.Copy(bytes, 0, pUnmanagedBytes, fs.Length)
            ' Yönetilmeyen baytları yazıcıya gönder.
            bSuccess = SendBytesToPrinter(szPrinterName, pUnmanagedBytes, fs.Length)
            ' Daha önce ayrılan yönetilmeyen belleği serbest bırak.
            Marshal.FreeCoTaskMem(pUnmanagedBytes)
            fs.Close()
            Return bSuccess
        End Function
        ' SendFileToPrinter()
        ' İşleve bir dize ve bir yazıcı adı verildiğinde,
        ' işlev dizeyi ham bayt olarak yazıcıya gönderir.
        Public Shared Sub SendStringToPrinter(ByVal szPrinterName As String, ByVal szString As String)
            Dim pBytes As IntPtr
            Dim dwCount As Int32
            ' Dizede kaç karakter var?
            dwCount = szString.Length()
            ' Yazıcının ANSI metin beklediğini kabul et ve
            ' dizeyi ANSI metne dönüştür.
            pBytes = Marshal.StringToCoTaskMemAnsi(szString)
            ' Dönüştürülmüş ANSI dizeyi yazıcıya gönder.
            SendBytesToPrinter(szPrinterName, pBytes, dwCount)
            Marshal.FreeCoTaskMem(pBytes)
        End Sub
    End Class

    Function ChecksumEAN13(ByVal barcode As String) As Integer
        Dim multiplier As Integer
        Dim total As Integer
        total = 0
        multiplier = 3
        For Each el As Char In barcode.Reverse()
            If el >= "0" And el <= "9" Then
                total += Val(el) * multiplier
                If multiplier = 3 Then multiplier = 1 Else multiplier = 3
            End If
        Next
        ChecksumEAN13 = 10 - (total Mod 10)
    End Function

    Private Sub BarkodParametreleriToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BarkodParametreleriToolStripMenuItem.Click
        Form4.Show()
    End Sub

    Public Function Ceng(ByVal _String As String) As String
        Dim Source As String = "ığüşöçĞÜŞİÖÇ"
        Dim Destination As String = "igusocGUSIOC"
        For i As Integer = 0 To Source.Length - 1
            _String = _String.Replace(Source(i), Destination(i))
        Next
        Return _String
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '  Fill("select * from StokView where FTarih>dateadd(day,-" + NumericUpDown1.Value.ToString + ",getdate()) and FTarih>dbo.fnBTarih(Kod)", DataGridView1)
    End Sub

    Private Sub SatınAlmaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SatınAlmaToolStripMenuItem.Click
        If Me.SatınAlmaToolStripMenuItem.Text = "Fiyat Gör" Then
            Me.Panel1.Hide()
            Me.SatınAlmaToolStripMenuItem.Text = "Satın Alma"
            Me.TextBoxArama.Select()
        Else
            Me.Panel1.Show()
            Me.SatınAlmaToolStripMenuItem.Text = "Fiyat Gör"
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Panel1.Hide()
        Me.SatınAlmaToolStripMenuItem.Text = "Satın Alma"
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'Dim Query As String = "Select * from dbo.fnStokSiparis(" + Me.NumericUpDownHedef.Value.ToString + ",'%" + Me.TextBoxArama.Text + "%'," + CInt(Me.CheckBoxMiktar.Checked).ToString + ")"
        'Query += " Where Gun>=" + Me.NumericUpDownStokta.Value.ToString
        'Fill(Query, DataGridView2)
    End Sub

    Private Sub TextBoxArama_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxArama.KeyPress
        If Keys.Enter = AscW(e.KeyChar) Then
            Button3_Click(sender, e)
        End If
    End Sub

    Private Sub ToolStripMenuItem5_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem5.Click
        Print(My.Settings.Dizayn5_yol, My.Settings.Dizayn5_yazici)
    End Sub

    Private Sub ToolStripMenuItem6_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem6.Click
        Print(My.Settings.Dizayn6_yol, My.Settings.Dizayn6_yazici)
    End Sub

    Private Sub ToolStripMenuItem7_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem7.Click
        Print(My.Settings.Dizayn7_yol, My.Settings.Dizayn7_yazici)
    End Sub

    Private Sub InitializeDataGridViewColumns() Handles B1.DataGridColumnsReady
        If ListBoxClients.Items.Count Then
            Dim SelectedItem As Client = ListBoxClients.SelectedItem
            If DataGridColumns.Count Then
                ReferenceOfColumns = FindInstanceOfGenericClass(Of List(Of DataGridViewTextBoxColumn))(DataGridColumns, ReferenceOfClient.Id)
                ChangeDataGridView(DataGridView1, ReferenceOfColumns)
            End If
        End If
    End Sub

    Private Sub ListBoxClients_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBoxClients.SelectedIndexChanged
        If Me.IsListBoxClientInitialized Then
            Dim InstanceOfClient As Client = TryCast(sender.SelectedItem, Client)
            If Not InstanceOfClient.Equals(ReferenceOfClient) Then
                ReferenceOfClient = sender.SelectedItem
                ReferenceOfRestClient = RestClients.Item(sender.SelectedIndex).Value
                'ReferenceOfColumns = FindInstanceOfGenericClass(Of List(Of DataGridViewTextBoxColumn))(DataGridColumns, ReferenceOfClient.Id)
                If DataGridColumns.Count > sender.SelectedIndex Then
                    ReferenceOfColumns = DataGridColumns.Item(sender.SelectedIndex).Value
                    ChangeDataGridView(DataGridView1, ReferenceOfColumns)
                    MakeSearch()
                    B2.Start()
                End If

            End If
        End If
    End Sub
End Class

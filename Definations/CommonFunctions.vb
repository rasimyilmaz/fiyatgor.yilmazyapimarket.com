Imports System.Globalization
Imports Newtonsoft.Json
Imports RestSharp
Imports RestSharp.Authenticators

Module CommonFunctions
    Public Function MaskText(ByVal text As String) As String
        Dim alteredText As String = ""
        For i = 0 To text.Length - 1
            If (Rnd(1) = 0) Then
                alteredText += "*"
            Else
                alteredText += text(i)
            End If
        Next
        Return alteredText
    End Function
    Public Function StringISO8601WithZToDateTime(ByVal Value As String) As DateTime
        Dim InstanceOfDateTime As DateTime = DateTime.Now
        Date.TryParse(Value, CultureInfo.CurrentCulture, DateTimeStyles.RoundtripKind, InstanceOfDateTime)
        Return InstanceOfDateTime
    End Function
    Public Function StringToHunderdMultipliedString(ByVal Value As String) As String
        Return Convert.ToString(CInt(Math.Round(Convert.ToDecimal(Value), 2) * 100))
    End Function
    Public Function HunderdMiltipliedStringToDecimal(ByVal Value As String) As Decimal
        Return Convert.ToDecimal(Value) / 100
    End Function
    Public Function DecimalToHunderdMultipliedString(ByVal Value As Decimal) As String
        Return Convert.ToString(CInt(Math.Round(Value, 2) * 100))
    End Function
    Public Function GetFullFileName(ByVal Filename As String) As String
        Dim FullPath As String = GetApplicationFolder()
        Dim FullFileName As String = Path.Combine(FullPath, Filename)
        Return FullFileName
    End Function
    Public Function FileExists(ByVal Filename As String) As Boolean
        Dim FullPath As String = GetApplicationFolder()
        Dim IsExists As Boolean = False
        If Directory.Exists(FullPath) Then
            If File.Exists(Path.Combine(FullPath, Filename)) Then
                IsExists = True
            End If
        End If
        Return IsExists
    End Function
    Public Function ReadFile(ByVal FileName As String) As String
        Dim Out As String = ""
        Dim FullFileName As String = GetFullFileName(FileName)
        If File.Exists(FullFileName) Then
            Using reader As New StreamReader(FullFileName)
                Out = reader.ReadToEnd()
            End Using
        Else
            Throw New Exception(FileModes.FileNotFound)
        End If
        Return Out
    End Function
    Public Function TouchFile(ByVal Filename As String) As Boolean
        Dim ApplicationFolder As String = GetApplicationFolder()
        TouchDirectory(ApplicationFolder)
        Dim FullFileName As String = Path.Combine(ApplicationFolder, Filename)
        Dim executionFault As Boolean = False
        If Not File.Exists(FullFileName) Then
            Try
                Using outFile As New StreamWriter(FullFileName)
                End Using
            Catch ex As Exception
                executionFault = True
            End Try
        End If
        Return executionFault
    End Function
    Public Function GetApplicationFolder() As String
        Dim RootPath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
        Dim FullPath As String = Path.Combine(RootPath, AuthorFolderName, ApplicationName)
        Return FullPath
    End Function
    Public Sub TouchDirectory(ByVal Path As String)
        If Not Directory.Exists(Path) Then
            'TouchDirectory
            Directory.CreateDirectory(Path)
        End If
    End Sub
    Public Sub FirstUse()
        TouchDirectory(GetApplicationFolder)
        TouchFile("clients.json")
        TouchFile(FirstUseFilename)
        Clients = InsertClientsDefaultValues()
        TouchFile(LastStateFilename)
        ReferenceOfClient = Clients.First
        SaveLastState()
    End Sub
    Public Function InsertClientsDefaultValues() As List(Of Client)
        Dim Clients = New List(Of Client) From {
                Default_Client_0901,
                Default_Client_3501
            }
        WriteFile(ClientsJsonFilename, JsonConvert.SerializeObject(Clients))
        Return Clients
    End Function
    Public Function WriteFile(ByVal FileName As String, ByVal Context As String) As Boolean
        Dim ApplicationFolder As String = GetApplicationFolder()
        TouchDirectory(ApplicationFolder)
        Dim FullFileName As String = Path.Combine(ApplicationFolder, FileName)
        Dim ExecutionFault As Boolean = False
        Try
            Using writer As New StreamWriter(FullFileName, False)
                writer.Write(Context)
            End Using
        Catch ex As Exception
            ExecutionFault = True
        End Try
        Return ExecutionFault
    End Function
    Public Async Function LoadClients() As Task(Of List(Of Client))
        Dim FileName As String = "clients.json"
        Dim Clients As List(Of Client)
        Dim clients_json As String = ""
        Try
            clients_json = ReadFile(FileName)
        Catch ex As Exception
            If ex.Message = FileModes.FileNotFound Then
                TouchFile(FileName)
            End If
        End Try

        If String.IsNullOrEmpty(clients_json) Then
            Clients = InsertClientsDefaultValues()
        Else
            Clients = JsonConvert.DeserializeObject(Of List(Of Client))(clients_json)
        End If
        RestClients = GetInstanceOfRestClients(Clients)

        Return Await Task.FromResult(Of List(Of Client))(Clients)
    End Function
    Public Async Function LoadWarehouses(ByVal InstanceOfClients As List(Of Client), InstanceOfRestClients As List(Of KeyValuePair(Of String, RestClient))) As Task(Of List(Of KeyValuePair(Of String, WarehouseInfo)))
        Dim InstanceOfWarehouseInfos As New List(Of KeyValuePair(Of String, WarehouseInfo))
        For i As Integer = 0 To InstanceOfClients.Count - 1
            Try
                Dim InstanceOfClient As Client = InstanceOfClients.Item(i)
                Dim InstanceOfWarehouseInfo As WarehouseInfo = Await GetWarehouseInfoAsync(InstanceOfClient, InstanceOfRestClients.Item(i).Value, ProcessorId)
                Dim Pair As New KeyValuePair(Of String, WarehouseInfo)(InstanceOfClient.Id, InstanceOfWarehouseInfo)
                InstanceOfWarehouseInfos.Add(Pair)
            Catch ex As Exception

            End Try
        Next
        Return Await Task.FromResult(Of List(Of KeyValuePair(Of String, WarehouseInfo)))(InstanceOfWarehouseInfos)
    End Function

    Public Sub RecoverLastState()
        Dim LastState As String = ReadFile(LastStateFilename)
        Try
            ReferenceOfClient = FindInstanceOfClient(Clients, LastState)
            ReferenceOfRestClient = FindInstanceOfGenericClass(Of RestClient)(RestClients, ReferenceOfClient.Id)
        Catch ex As Exception
            MsgBox("İstenilen bağlantı noktası tanımlanamıyor", MsgBoxStyle.OkOnly, "Uyarı")
        End Try
    End Sub
    Public Function SaveLastState()
        Return WriteFile(LastStateFilename, ReferenceOfClient.Id)
    End Function
    Public Function GetOption(ByVal [Option] As String) As String
        Dim Value As String = ""
        Try
            Value = Options.Find(Function(x) x.Key = [Option]).Value
        Catch ex As Exception
            Throw New Exception(OptionModes.NotFound)
        End Try
        Return Value
    End Function
    Public Sub LoadOptions()
        Options = New List(Of KeyValuePair(Of String, String))
        Dim Line As String = ""
        Dim FullFileName As String = GetFullFileName(OptionsFileName)
        If File.Exists(FullFileName) Then
            Using reader As New StreamReader(FullFileName)
                While Not reader.EndOfStream
                    Try
                        Options.Add(ConvertToKeyValuePair(Line))
                    Catch ex As Exception

                    End Try
                End While
            End Using
        End If
    End Sub

    Public Sub SaveOptions()
        Dim FullFileName As String = GetFullFileName(OptionsFileName)
        If File.Exists(FullFileName) Then
            Using Writer As New StreamWriter(FullFileName)
                For Each [Option] In Options
                    Writer.WriteLine(ConvertToLine([Option]))
                Next
            End Using
        End If
    End Sub
    Public Function ConvertToLine(ByVal [Option] As KeyValuePair(Of String, String)) As String
        Return [Option].Key + "=" + [Option].Value
    End Function

    Public Function ConvertToKeyValuePair(ByVal Line As String) As KeyValuePair(Of String, String)
        If (String.IsNullOrEmpty(Line)) Then
            Throw New Exception(OptionLineModes.Incorrect)
        Else
            Dim pair As String() = Strings.Split(Line, "=")
            Dim key, value As String
            If pair.Count = 2 Then
                key = pair(0)
                value = pair(1)
            ElseIf pair.Count = 1 Then
                key = pair(0)
            Else
                Throw New Exception(OptionLineModes.Incorrect)
            End If
            Return New KeyValuePair(Of String, String)(Strings.Trim(0), Strings.Trim(pair(1)))
        End If
    End Function

    Public Async Function GetProcessorId() As Task(Of String)
        'Win32_Processor -> https://docs.microsoft.com/en-us/windows/desktop/cimwin32prov/win32-processor
        Dim strComputer = "."
        Dim Id As String = ""
        Dim objWMIService = GetObject("winmgmts:" _
        & "{impersonationLevel=impersonate}!\\" & strComputer & "\root\cimv2")
        Dim colProcessors = objWMIService.ExecQuery("Select * from Win32_Processor")
        For Each objProcessor In colProcessors
            Id += objProcessor.ProcessorID
        Next
        Return Await Task.FromResult(Of String)(Id)
    End Function

    Public Function GetInstanceOfRestClient(ByVal Instance As Client) As RestClient
        Dim Options As New RestClientOptions(Instance.Url) With {.Authenticator = New HttpBasicAuthenticator(Instance.Username, Instance.Password)}
        Return New RestClient(Options)
    End Function

    Public Function FindInstanceOfClient(collection As List(Of Client), id As String) As Client
        Dim ResultSet As IEnumerable(Of Client) = collection.Where(Function(c) c.Id = id)
        Dim size As Integer = ResultSet.Count
        If size = 0 Then
            Throw New Exception("No releted client found")
        ElseIf size > 1 Then
            Throw New Exception("Too many related clients found")
        Else
            Return ResultSet.First
        End If
    End Function

    Public Function FindInstanceOfGenericClass(Of T)(collection As List(Of KeyValuePair(Of String, T)), id As String) As T
        Dim ResultSet As IEnumerable(Of KeyValuePair(Of String, T)) = collection.Where(Function(c) c.Key = id)
        Dim size As Integer = ResultSet.Count
        If size = 0 Then
            Throw New Exception("No releted " + GetType(T).ToString() + " found")
        ElseIf size > 1 Then
            Throw New Exception("Too many related " + GetType(T).ToString() + "s found")
        Else
            Return ResultSet.First.Value
        End If
    End Function

    Public Function GetInstanceOfRestClients(ByVal Collection As List(Of Client)) As List(Of KeyValuePair(Of String, RestClient))
        Dim Clients As New List(Of KeyValuePair(Of String, RestClient))
        For Each Client In Collection
            Clients.Add(New KeyValuePair(Of String, RestClient)(Client.Id, GetInstanceOfRestClient(Client)))
        Next
        Return Clients
    End Function

    Public Sub SetDefaultStyle(ByRef InstanceOfDataGridView As DataGridView)
        InstanceOfDataGridView.DefaultCellStyle = New DataGridViewCellStyle With {
                .Alignment = DataGridViewContentAlignment.MiddleRight,
                .Padding = New Padding() With {.Left = 0, .Top = 0, .Right = 10, .Bottom = 0}}
    End Sub

    Public Async Function CheckAuthAsync(ByVal InstanceOfClient As Client, ByVal InstanceOfRestClient As RestClient, ByVal ProcessorId As String) As Task(Of CheckAuthResponse)
        Return Await GenericRequestAsync(Of CheckAuthResponse)(InstanceOfClient, InstanceOfRestClient, "/check_auth.php", ProcessorId)
    End Function

    Public Async Function GetWarehouseInfoAsync(ByVal InstanceOfClient As Client, ByVal InstanceOfRestClient As RestClient, ByVal ProcessorId As String) As Task(Of WarehouseInfo)
        Return Await GenericRequestAsync(Of WarehouseInfo)(InstanceOfClient, InstanceOfRestClient, "/get_warehouse_info.php", ProcessorId)
    End Function

    Public Async Function GetProducts(ByVal InstanceOfClient As Client, ByVal InstanceOfRestClient As RestClient, ByVal ProcessorId As String, ByVal InstanceOfProductCriteria As ProductCriteria) As Task(Of ProductsResponse)
        Return Await GenericRequestAsync(Of ProductsResponse)(InstanceOfClient, InstanceOfRestClient, "/products.php", ProcessorId, JsonConvert.SerializeObject(InstanceOfProductCriteria))
    End Function

    Public Async Function GenericRequestAsync(Of T)(ByVal InstanceOfClient As Client, ByVal InstanceOfRestClient As RestClient, ByVal endpoint As String, ByVal ProcessorId As String, Optional [json] As String = "") As Task(Of T)
        Dim response As RestResponse = Await MakeRequestAsync(InstanceOfClient, InstanceOfRestClient, endpoint, ProcessorId, json)
        If response.StatusCode = 200 Then
            Return JsonConvert.DeserializeObject(Of T)(response.Content)
        Else
            Throw New Exception("Server connection is not successfull." + " " + response.StatusCode.ToString() + " : " + response.Content.ToString)
        End If
    End Function

    Public Async Function MakeRequestAsync(ByVal InstanceOfClient As Client, ByVal InstanceOfRestClient As RestClient, ByVal endpoint As String, ByVal ProcessorId As String, Optional ByVal [json] As String = "") As Task(Of RestResponse)
        Dim response As RestResponse
        Dim request As New RestRequest(endpoint, Method.Post)
        request.AddHeader("Content-type", "application/json; charset=utf-8")
        request.AddHeader("X-API-KEY", InstanceOfClient.ApiKey)
        request.AddHeader("X-PROCESSOR-ID", ProcessorId)
        request.Timeout = 5000
        If Not String.IsNullOrEmpty(json) Then
            request.AddBody(json, ContentType.Json)
        End If
        response = Await InstanceOfRestClient.PostAsync(request)
        Return response
    End Function

    Public Function GetInstanceOfRestClient(collection As List(Of KeyValuePair(Of String, RestClient)), id As String)
        Dim ResultSet As IEnumerable(Of KeyValuePair(Of String, RestClient)) = collection.Where(Function(c) c.Key = id)
        Dim size As Integer = ResultSet.Count
        If size = 0 Then
            Throw New Exception("No releted rest client found")

        ElseIf size > 1 Then
            Throw New Exception("Too many related rest clients found")
        Else
            Return ResultSet.First.Value
        End If
    End Function

    Public Function CreateDataGridColumns(ByRef InstanceOfWarehouseInfos As List(Of KeyValuePair(Of String, WarehouseInfo))) As List(Of KeyValuePair(Of String, List(Of DataGridViewTextBoxColumn)))
        Dim InstanceOfDataGridColumns As New List(Of KeyValuePair(Of String, List(Of DataGridViewTextBoxColumn)))
        For Each Item In InstanceOfWarehouseInfos
            Try
                Dim Instance As List(Of DataGridViewTextBoxColumn) = CreateListOfDataGridViewTextBoxColumn(Item.Value)
                Dim Pair As New KeyValuePair(Of String, List(Of DataGridViewTextBoxColumn))(Item.Key, Instance)
                InstanceOfDataGridColumns.Add(Pair)
            Catch ex As Exception

            End Try
        Next
        Return InstanceOfDataGridColumns
    End Function

    Public Function CreateListOfDataGridViewTextBoxColumn(ByVal InstanceOfWarehouseInfo As WarehouseInfo) As List(Of DataGridViewTextBoxColumn)
        Dim collection As New List(Of DataGridViewTextBoxColumn)
        Dim MultiplierPixel As Integer = 7
        Dim Column_Code As New DataGridViewTextBoxColumn With {
            .Name = "Code",
            .HeaderText = "Kod",
            .MaxInputLength = 25,
            .MinimumWidth = .MaxInputLength * (MultiplierPixel - 3),
            .Resizable = DataGridViewTriState.True}
        collection.Add(Column_Code)
        Dim Column_Barcode As New DataGridViewTextBoxColumn With {
            .Name = "Barcode",
            .HeaderText = "Barkod",
            .MaxInputLength = 13,
            .MinimumWidth = .MaxInputLength * (MultiplierPixel - 3),
            .Resizable = DataGridViewTriState.True}
        collection.Add(Column_Barcode)
        Dim Column_Name As New DataGridViewTextBoxColumn With {
            .Name = "Name",
            .HeaderText = "İsim",
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
            .MaxInputLength = 40,
            .MinimumWidth = .MaxInputLength * (MultiplierPixel - 3),
            .Resizable = DataGridViewTriState.True}
        collection.Add(Column_Name)
        Dim Column_Price As New DataGridViewTextBoxColumn With {
            .Name = "Price",
            .HeaderText = "Fiyat",
            .MaxInputLength = 10,
            .MinimumWidth = .MaxInputLength * (MultiplierPixel - 3),
            .Resizable = DataGridViewTriState.True}
        collection.Add(Column_Price)
        Dim Column_Currency As New DataGridViewTextBoxColumn With {
            .Name = "Currency",
            .HeaderText = "Döviz",
            .MaxInputLength = 3,
            .MinimumWidth = .MaxInputLength * (MultiplierPixel - 3),
            .Resizable = DataGridViewTriState.True}
        collection.Add(Column_Currency)
        For i = 0 To InstanceOfWarehouseInfo.Collection.Count - 1
            Dim SelectedWarehouse As Warehouse = InstanceOfWarehouseInfo.Collection(i)
            If SelectedWarehouse.Visible Then
                Dim Column_Quantity As New DataGridViewTextBoxColumn() With {
                .Name = "Warehouse" + SelectedWarehouse.Id.ToString,
                .HeaderText = SelectedWarehouse.Name,
                .MaxInputLength = 10,
                .MinimumWidth = .MaxInputLength * (MultiplierPixel - 3),
                .Resizable = DataGridViewTriState.True}
                collection.Add(Column_Quantity)
            End If
        Next
        Dim Column_Unit As New DataGridViewTextBoxColumn With {
            .Name = "Unit",
            .HeaderText = "Birim",
            .MaxInputLength = 4,
            .MinimumWidth = .MaxInputLength * (MultiplierPixel - 3),
            .Resizable = DataGridViewTriState.True}
        collection.Add(Column_Unit)
        Dim Column_LocalCurrencyPrice As New DataGridViewTextBoxColumn With {
            .Name = "LocalCurrencyPrice",
            .HeaderText = "Döviz",
            .MaxInputLength = 10,
            .MinimumWidth = .MaxInputLength * (MultiplierPixel - 3),
            .Resizable = DataGridViewTriState.True}
        collection.Add(Column_LocalCurrencyPrice)
        Dim Column_ShortName As New DataGridViewTextBoxColumn With {
            .Name = "ShortName",
            .HeaderText = "Kısa İsim",
            .MaxInputLength = 25,
            .MinimumWidth = .MaxInputLength * (MultiplierPixel - 3),
            .Resizable = DataGridViewTriState.True,
            .Visible = False}
        collection.Add(Column_ShortName)
        Dim Column_PriceChangeDate As New DataGridViewTextBoxColumn With {
            .Name = "PriceChangeDate",
            .HeaderText = "Fiyat Değişiklik Tarihi",
            .MaxInputLength = 10,
            .MinimumWidth = .MaxInputLength * (MultiplierPixel - 3),
            .Resizable = DataGridViewTriState.True,
            .Visible = False}
        collection.Add(Column_PriceChangeDate)
        Dim Column_PriceChangeUser As New DataGridViewTextBoxColumn With {
            .Name = "PriceChangeUser",
            .HeaderText = "Fiyat Değişiklik Kullanıcısı",
            .MaxInputLength = 8,
            .MinimumWidth = .MaxInputLength * (MultiplierPixel - 3),
            .Resizable = DataGridViewTriState.True,
            .Visible = False}
        collection.Add(Column_PriceChangeUser)
        Return collection
    End Function
End Module

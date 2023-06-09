Module DefaultValues
    ReadOnly Property Default_Client_0901 As New Client With {
        .Id = "0901",
        .Name = "Aydın",
        .Url = "https://api.yilmazyapimarket.com",
        .ApiKey = "1905kalem",
        .Username = "rasim",
        .Password = "yilmaz"
        }
    ReadOnly Property Default_Client_3501 As New Client With {
        .Id = "3501",
        .Name = "İzmir",
        .Url = "https://api.yilmazpark.com",
        .ApiKey = "1905kalem",
        .Username = "rasim",
        .Password = "yilmaz"
        }
    ReadOnly Property ApplicationName As String = "Rasim"
    ReadOnly Property AuthorName As String = "Rasim Yılmaz"
    ReadOnly Property AuthorFolderName As String = "rasimyilmaz"
    ReadOnly Property ClientsJsonFilename As String = "clients.json"
    ReadOnly Property OptionsFilename As String = "options.ini"
    ReadOnly Property LastStateFilename As String = "last_state"
    ReadOnly Property FirstUseFilename As String = "first_use"
End Module

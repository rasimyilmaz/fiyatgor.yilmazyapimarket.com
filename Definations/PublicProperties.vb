Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports RestSharp

Module PublicProperties
    Public Event ClientsChanged()
    Public Property RestClients As List(Of KeyValuePair(Of String, RestClient))
    Public Property WarehouseInfos As List(Of KeyValuePair(Of String, WarehouseInfo))
    Public Property Options As List(Of KeyValuePair(Of String, String))
    Public Property Clients As List(Of Client)
    Public Property ReferenceOfClient As Client
    Public Property ReferenceOfRestClient As RestClient
    Public Property ReferenceOfWarehouseInfo As WarehouseInfo
    Public Property ProcessorId As String
    Public Property ReferenceOfColumns As List(Of DataGridViewTextBoxColumn)
    Public Property DataGridColumns As List(Of KeyValuePair(Of String, List(Of DataGridViewTextBoxColumn)))
End Module

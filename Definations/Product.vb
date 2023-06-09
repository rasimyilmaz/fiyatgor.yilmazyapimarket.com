Imports Newtonsoft.Json

Public Class Product
    <JsonProperty(PropertyName:="code")>
    Public Property Code As String

    <JsonProperty(PropertyName:="barcode")>
    Public Property Barcode As String

    <JsonProperty(PropertyName:="name")>
    Public Property Name As String

    <JsonProperty(PropertyName:="short_name")>
    Public Property ShortName As String

    <JsonProperty(PropertyName:="price")>
    Public Property Price As String

    <JsonProperty(PropertyName:="currency")>
    Public Property CurrencyName As String

    <JsonProperty(PropertyName:="warehouse_quantity_collection")>
    Public Property WarehouseQuantity_Collection As WarehouseQuantityPair()

    <JsonProperty(PropertyName:="unit")>
    Public Property Unit As String

    <JsonProperty(PropertyName:="price_change_date")>
    Public Property PriceChangeDate As String

    <JsonProperty(PropertyName:="price_change_user")>
    Public Property PriceChangeUser As String

    <JsonProperty(PropertyName:="local_currency_price")>
    Public Property LocalCurrencyPrice As String

    Public Function GetQuantity(ByVal id As String) As Decimal
        Dim value As Decimal
        Dim SelectedWarehouseQuantityPair As WarehouseQuantityPair
        For i As Integer = WarehouseQuantity_Collection.Count - 1 To 0
            SelectedWarehouseQuantityPair = WarehouseQuantity_Collection(i)
            If SelectedWarehouseQuantityPair.Id = id Then
                value = SelectedWarehouseQuantityPair.Quantity
            End If
        Next
        Return value
    End Function
End Class

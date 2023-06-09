Imports System.Text.Json.Serialization
Imports Newtonsoft.Json
Public Class ProductCriteria
    <JsonProperty(PropertyName:="product_code", Order:=1)>
    Public Property ProductCode As String
    <JsonProperty(PropertyName:="product_barcode", Order:=2)>
    Public Property ProductBarcode As String
    <JsonProperty(PropertyName:="product_name", Order:=3)>
    Public Property ProductName As String
    <JsonProperty(PropertyName:="product_price", Order:=4)>
    Public Property ProductPrice As Decimal
    <JsonProperty(PropertyName:="is_code_filled", Order:=5)>
    Public Property IsCodeFilled As Boolean
    <JsonProperty(PropertyName:="is_barcode_filled", Order:=6)>
    Public Property IsBarcodeFilled As Boolean
    <JsonProperty(PropertyName:="is_name_filled", Order:=7)>
    Public Property IsNameFilled As Boolean
    <JsonProperty(PropertyName:="is_price_filled", Order:=8)>
    Public Property IsPriceFilled As Boolean
    Sub New()
        ProductCode = ""
        ProductBarcode = ""
        ProductName = ""
        ProductPrice = 0
    End Sub
End Class

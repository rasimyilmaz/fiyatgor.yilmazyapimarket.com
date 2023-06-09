Imports Newtonsoft.Json

Public Class WarehouseQuantityPair
    <JsonProperty(PropertyName:="id")>
    Public Property Id As String

    <JsonProperty(PropertyName:="quantity")>
    Public Property Quantity As Decimal

End Class

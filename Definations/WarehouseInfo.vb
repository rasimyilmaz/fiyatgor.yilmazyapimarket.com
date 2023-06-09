Imports Newtonsoft.Json

Public Class WarehouseInfo
    Inherits BaseResponse
    <JsonProperty(PropertyName:="revision")>
    Public Property Revision As Integer

    <JsonProperty(PropertyName:="collection")>
    Public Property Collection As Warehouse()

End Class


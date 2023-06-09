Imports Newtonsoft.Json

Public Class WarehouseInfoResponse
    'https://www.tutlane.com/tutorial/visual-basic/vb-inheritance
    Inherits BaseResponse
    <JsonProperty(PropertyName:="revision")>
    Public Property Revision As Integer

    <JsonProperty(PropertyName:="collection")>
    Public Property collection As Warehouse()

End Class

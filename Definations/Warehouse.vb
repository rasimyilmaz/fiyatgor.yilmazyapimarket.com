Imports Newtonsoft.Json

Public Class Warehouse
    <JsonProperty(PropertyName:="id")>
    Public Property Id As String

    <JsonProperty(PropertyName:="name")>
    Public Property Name As String

    <JsonProperty(PropertyName:="order")>
    Public Property Order As Integer

    <JsonProperty(PropertyName:="visible")>
    Public Property Visible As Boolean

End Class

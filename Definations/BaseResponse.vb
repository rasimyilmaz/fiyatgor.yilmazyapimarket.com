
Imports Newtonsoft.Json

Public Class BaseResponse
    <JsonProperty(PropertyName:="status")>
    Property Status As Integer

    <JsonProperty(PropertyName:="message")>
    Property Message As String()
End Class

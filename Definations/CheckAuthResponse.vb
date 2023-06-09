Imports System.Text.Json.Serialization
Imports Newtonsoft.Json

Public Class CheckAuthResponse
    Inherits BaseResponse
    <JsonProperty(PropertyName:="access_grant")>
    Public Property AccessGrant As Boolean

End Class

Imports System.Runtime.CompilerServices
Imports Newtonsoft.Json

Public Class Client
    Implements IComparable, IEquatable(Of Client)

    <JsonProperty(PropertyName:="apikey")>
    Property ApiKey As String

    <JsonProperty(PropertyName:="username")>
    Property Username As String

    <JsonProperty(PropertyName:="password")>
    Property Password As String

    <JsonProperty(PropertyName:="id")>
    Property Id As String

    <JsonProperty(PropertyName:="name")>
    Property Name As String

    <JsonProperty(PropertyName:="url")>
    Property Url As String
    Public Function CompareTo(obj As Object) As Integer Implements IComparable.CompareTo
        If obj Is Nothing Then
            Return 1
        End If
        Dim OtherClient As Client = TryCast(obj, Client)
        If OtherClient IsNot Nothing Then

            Return OtherClient.Id.CompareTo(Me.Id)
        Else
            Throw New ArgumentException("Object is not a Client")
        End If
    End Function

    Public Overloads Function Equals(other As Client) As Boolean Implements IEquatable(Of Client).Equals
        If other Is Nothing Then Return False

        If Me.Id = other.Id Then
            Return True
        Else
            Return False
        End If
    End Function
End Class

Imports System.Web.UI.WebControls

Class Depracated : Implements IDisposable

    Private disposedValue As Boolean
    '---------------------------------------------------------------------------------------
    ' Procedure : GetProcessorId
    ' Author    : Daniel Pineault, CARDA Consultants Inc.
    ' Website   : http://www.cardaconsultants.com--------------------------------------------------------------------------------

    ' TASK(OF T) EXAMPLE
    Async Function TaskOfT_MethodAsync() As Task(Of Integer)

        ' The body of an async method is expected to contain an awaited
        ' asynchronous call.
        ' Task.FromResult is a placeholder for actual work that returns a string.
        Dim today As String = Await Task.FromResult(Of String)(DateTime.Now.DayOfWeek.ToString())

        ' The method then can process the result in some way.
        Dim leisureHours As Integer
        If today.First() = "S" Then
            leisureHours = 16
        Else
            leisureHours = 5
        End If

        ' Because the return statement specifies an operand of type Integer, the
        ' method must have a return type of Task(Of Integer).
        Return leisureHours
    End Function

    ' TASK EXAMPLE
    Async Function Task_MethodAsync() As Task

        ' The body of an async method is expected to contain an awaited
        ' asynchronous call.
        ' Task.Delay is a placeholder for actual work.
        Await Task.Delay(0)
        'textBox1.Text &= vbCrLf & "Sorry for the delay. . . ." & vbCrLf

        ' This method has no return statement, so its return type is Task.
    End Function
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: yönetilen durumu (yönetilen nesneleri) atın
            End If

            ' TODO: yönetilmeyen kaynakları (yönetilmeyen nesneleri) serbest bırakın ve sonlandırıcıyı geçersiz kılın
            ' TODO: büyük alanları null olarak ayarlayın
            disposedValue = True
        End If
    End Sub

    ' ' TODO: sonlandırıcıyı yalnızca 'Dispose(disposing As Boolean)' içinde yönetilmeyen kaynakları serbest bırakacak kod varsa geçersiz kılın
    ' Protected Overrides Sub Finalize()
    '     ' Bu kodu değiştirmeyin. Temizleme kodunu 'Dispose(disposing As Boolean)' metodunun içine yerleştirin.
    '     Dispose(disposing:=False)
    '     MyBase.Finalize()
    ' End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Bu kodu değiştirmeyin. Temizleme kodunu 'Dispose(disposing As Boolean)' metodunun içine yerleştirin.
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub

End Class

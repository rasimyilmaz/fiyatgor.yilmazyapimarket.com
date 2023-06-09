Imports System.ComponentModel

Public Class BackGroundWorker3
    Public Event SearchFinished()
    Public WithEvents Worker As BackgroundWorker
    Public StopWatchForTasks As Stopwatch
    Public Result As String
    Public ExceptionMessage As String
    Public Successed As Boolean
    Public Progress As String
    Public TotalMilisecondsForAllTasks As Integer
    Public InstanceOfProductCriteria As ProductCriteria
    Public InstanceOfProductsResponse As ProductsResponse

    'Initialize Get Ready
    Sub New()
        Worker = New BackgroundWorker With {
        .WorkerReportsProgress = True,
        .WorkerSupportsCancellation = True
    }
        StopWatchForTasks = New Stopwatch()
        Result = ""
        Progress = ""
        TotalMilisecondsForAllTasks = 7000
    End Sub

    ' This event handler is where the time-consuming work is done.
    Public Sub DoWorkAsync(ByVal sender As System.Object, ByVal e As DoWorkEventArgs) Handles Worker.DoWork
        Successed = False
        ExceptionMessage = ""
        Dim worker As BackgroundWorker = CType(sender, BackgroundWorker)
        If (worker.CancellationPending = True) Then
            e.Cancel = True
        Else
            StopWatchForTasks.Start()
            Try
                InstanceOfProductsResponse = GetProducts(ReferenceOfClient, ReferenceOfRestClient, ProcessorId, InstanceOfProductCriteria).Result
                Successed = True
            Catch ex As Exception
                ExceptionMessage = ex.Message
            End Try
            ReportProgressByElapsedMilliseconds(StopWatchForTasks.ElapsedMilliseconds)
            ' Perform a time consuming operation and report progress.
            'System.Threading.Thread.Sleep(500)
            StopWatchForTasks.Reset()
        End If
    End Sub

    ' Start Procedure
    Public Sub Start()
        If Worker.IsBusy <> True Then
            ' Start the asynchronous operation.
            Worker.RunWorkerAsync()
        End If
    End Sub

    ' To Cancel
    Public Sub Cancel()
        If Worker.WorkerSupportsCancellation = True Then
            ' Cancel the asynchronous operation.
            Worker.CancelAsync()
        End If
    End Sub

    ' This event handler updates the progress.
    Private Sub ProgressChanged(ByVal sender As System.Object, ByVal e As ProgressChangedEventArgs) Handles Worker.ProgressChanged
        Progress = (e.ProgressPercentage.ToString() + "%")
    End Sub

    ' This event handler deals with the results of the background operation.
    Private Sub RunWorkerCompleted(ByVal sender As System.Object, ByVal e As RunWorkerCompletedEventArgs) Handles Worker.RunWorkerCompleted
        If e.Cancelled = True Then
            Result = "Canceled!"
        ElseIf e.Error IsNot Nothing Then
            Result = "Error: " & e.Error.Message
            RaiseEvent SearchFinished()
        Else
            RaiseEvent SearchFinished()
            Result = "Done!"
        End If

    End Sub

    Private Sub ReportProgressByElapsedMilliseconds(ByVal ElapsedMilliseconds As Integer)
        Worker.ReportProgress(Convert.ToInt16((ElapsedMilliseconds / TotalMilisecondsForAllTasks) * 100))
    End Sub
End Class

﻿Imports System.ComponentModel

Class BackGroundWorker1
    Public WithEvents Worker As BackgroundWorker
    Public Event ClientsReady()
    Public Event WarehousesReady()
    Public Event DataGridColumnsReady()
    Public Event RestClientsReady()
    Public StopWatchForTasks As Stopwatch
    Public Result As String
    Public Progress As String
    Public TotalMilisecondsForAllTasks As Integer
    Private Stepper As Integer

    'Initialize Get Ready
    Sub New()
        Worker = New BackgroundWorker With {
            .WorkerReportsProgress = True,
            .WorkerSupportsCancellation = True
        }
        StopWatchForTasks = New Stopwatch()
        Result = ""
        Progress = ""
        TotalMilisecondsForAllTasks = 3000
    End Sub

    ' This event handler is where the time-consuming work is done.
    Public Sub DoWorkAsync(ByVal sender As System.Object, ByVal e As DoWorkEventArgs) Handles Worker.DoWork
        Dim worker As BackgroundWorker = CType(sender, BackgroundWorker)
        If (worker.CancellationPending = True) Then
            e.Cancel = True
        Else
            StopWatchForTasks.Start()
            Stepper += 1
            ProcessorId = GetProcessorId().Result
            Clients = LoadClients.Result
            ReportProgressByElapsedMilliseconds(StopWatchForTasks.ElapsedMilliseconds)
            Stepper += 1
            RestClients = GetInstanceOfRestClients(Clients)
            ReportProgressByElapsedMilliseconds(StopWatchForTasks.ElapsedMilliseconds)
            Stepper += 1
            WarehouseInfos = LoadWarehouses(Clients, RestClients).Result
            ReportProgressByElapsedMilliseconds(StopWatchForTasks.ElapsedMilliseconds)
            Stepper += 1
            DataGridColumns = CreateDataGridColumns(WarehouseInfos)
            'RaiseEvent DataGridColumnsReady()
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
    Private Sub ProgressChanged(ByVal sender As System.Object,
    ByVal e As ProgressChangedEventArgs) Handles Worker.ProgressChanged
        Progress = (e.ProgressPercentage.ToString() + "%")
    End Sub

    ' This event handler deals with the results of the background operation.
    Private Sub RunWorkerCompleted(ByVal sender As System.Object,
    ByVal e As RunWorkerCompletedEventArgs) Handles Worker.RunWorkerCompleted
        If e.Cancelled = True Then
            Result = "Canceled!"
        ElseIf e.Error IsNot Nothing Then
            Result = "Error: " & e.Error.Message
        Else
            Result = "Done!"
        End If
        If Clients.Count Then
            RaiseEvent ClientsReady()
        End If
        If RestClients.Count Then
            RaiseEvent RestClientsReady()
        End If
        If WarehouseInfos.Count Then
            RaiseEvent WarehousesReady()
        End If
        If DataGridColumns.Count Then
            RaiseEvent DataGridColumnsReady()
        End If
    End Sub

    'This event
    Private Sub ReportProgressByElapsedMilliseconds(ByVal ElapsedMilliseconds As Integer)
        Worker.ReportProgress(Convert.ToInt16((ElapsedMilliseconds / TotalMilisecondsForAllTasks) * 100))
    End Sub
End Class


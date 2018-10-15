Imports System.IO
Public Class frmMain
    Private isRunning As Boolean
    Private requestCounter As Integer
    Private countMinutes As Integer
    Private request As System.Net.HttpWebRequest = Nothing
    Dim id As String = Guid.NewGuid().ToString("N")
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        isRunning = False
        pictureBox.Image = My.Resources.stopped
        requestCounter = 0
        tmrRequests.Interval = 480
    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        If isRunning Then
            tmrRequests.Enabled = False
            btnStart.Text = "Go!"
            pictureBox.Image = My.Resources.stopped
            isRunning = False
        Else
            requestCounter = 0
            btnStart.Text = "Stop!"
            pictureBox.Image = My.Resources.animated
            isRunning = True
            tmrRequests.Enabled = True
        End If
    End Sub

    Private Sub tmrRequests_Tick(sender As Object, e As EventArgs) Handles tmrRequests.Tick
        lblCounter.Text = requestCounter
        httpRequest()
    End Sub

    Private Sub httpRequest()
        tmrRequests.Enabled = False
        Try
            request = System.Net.HttpWebRequest.Create(My.Resources.bananaUrl + "?id=" + id)
            request.UserAgent = "KingKong Client 1.0"
            request.ServicePoint.ConnectionLeaseTimeout = 100
            request.ServicePoint.MaxIdleTime = 100
            request.Timeout = 1000
            request.KeepAlive = False
            requestCounter = requestCounter + 1
            countMinutes = countMinutes + 1

            Dim myStreamReader As New StreamReader(request.GetResponse.GetResponseStream)
            txtLog.AppendText(myStreamReader.ReadToEnd() + vbCrLf)
            request.ServicePoint.CloseConnectionGroup(request.ConnectionGroupName)
        Catch ex As Exception
            txtLog.AppendText(ex.ToString)
        End Try
        tmrRequests.Enabled = True
    End Sub

    Private Sub tmrStats_Tick(sender As Object, e As EventArgs) Handles tmrStats.Tick
        lblBananaSeconds.Text = Str(countMinutes * 6) + "b/m"
        countMinutes = 0
    End Sub
End Class

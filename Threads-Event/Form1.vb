Public Class Form1
    Dim r As New Random
    Dim maxint As Integer = 20
    Dim maxsleep As Integer = 80
    Dim blockedblack As Boolean
    Dim blockedgreen As Boolean
    Dim Xg As Integer
    Dim Xb As Integer
    Dim T1 As System.Threading.Thread
    Dim T2 As System.Threading.Thread


    Private Sub StartBT_Click(sender As Object, e As EventArgs) Handles StartBT.Click
        Dim G As Graphics
        Dim bg As Brush
        ' Clear All Graphics '
        G = Me.CreateGraphics
        bg = New SolidBrush(Me.BackColor)
        G.FillRectangle(bg, 0, 0, 1250, 500)
        ' Create Finish line '
        Dim p As Pen
        p = New Pen(Brushes.Black, 2)
        G.DrawLine(p, 1200, 80, 1200, 270)

        ' Create threading for run 2 loop same time '
        T1 = New System.Threading.Thread(AddressOf BlackCircle)
        T2 = New System.Threading.Thread(AddressOf GreenCircle)
        ' Threading start '
        T1.Start()
        T2.Start()
    End Sub

    Public Event Report_leader(leader_color As String)
    Public Event Race_over(winner_color As String)
    Public Delegate Sub xUpdatelabledelegate(vule As String)

    Sub xUdatelabel(winnercolor As String)
        Me.Label1.Text = "The winner color is " & winnercolor
    End Sub

    Public Sub Race_stop(winner_color As String) Handles Me.Race_over
        Dim oUpdatelabelDelegate As xUpdatelabledelegate
        oUpdatelabelDelegate = AddressOf xUdatelabel
        Me.Label1.Invoke(oUpdatelabelDelegate, winner_color)

        T1.Abort()
        T2.Abort()

        'MsgBox(" race over ")'

    End Sub
    Public Sub Reporter_leader(leader_color As String) Handles Me.Report_leader
        Dim G As Graphics
        G = Me.CreateGraphics
        SyncLock Me
            G.FillRectangle(New SolidBrush(Me.BackColor), 300, 10, 300, 50)
            G.DrawString(leader_color & " color is the leader", New Font("Arial", 18), New SolidBrush(Color.Black), 300, 10)
        End SyncLock

    End Sub

    Sub BlackCircle()
        Dim G As Graphics
        Dim black_Cir As Brush
        Dim bg As Brush
        Try
            Xb = 100
            G = Me.CreateGraphics
            black_Cir = New SolidBrush(Color.Black)
            bg = New SolidBrush(Me.BackColor)

            ' loop draw rectangle '
            Do While Xb < 1150
                G.FillRectangle(black_Cir, Xb, 100, 50, 50)
                Threading.Thread.Sleep(r.Next(maxsleep))
                G.FillRectangle(bg, Xb, 100, 50, 50)
                Xb += r.Next(maxint)
                If Xb > Xg And blockedblack = False Then
                    blockedblack = True
                    blockedblack = False
                    Dim treport_leader As New Threading.Thread(Sub() RaiseEvent Report_leader("Black"))
                    treport_leader.Start()

                End If
            Loop
            G.FillRectangle(black_Cir, Xb, 100, 50, 50)
            Dim trace_over As New Threading.Thread(Sub() RaiseEvent Race_over("Black"))
            trace_over.Start()
        Catch ex As Exception
            G.FillRectangle(black_Cir, Xb, 100, 50, 50)
        End Try

    End Sub

    Sub GreenCircle()
        Dim G As Graphics
        Dim Green_Cir As Brush
        Dim bg As Brush
        Try
            Xg = 100
            G = Me.CreateGraphics
            Green_Cir = New SolidBrush(Color.Green)
            bg = New SolidBrush(Me.BackColor)

            ' loop draw rectangle '
            Do While Xg < 1150
                G.FillRectangle(Green_Cir, Xg, 170, 50, 50)
                Threading.Thread.Sleep(r.Next(maxsleep))
                G.FillRectangle(bg, Xg, 170, 50, 50)
                Xg += r.Next(maxint)
                If Xg > Xb And blockedgreen = False Then
                    blockedgreen = True
                    blockedgreen = False
                    Dim treport_leader As New Threading.Thread(Sub() RaiseEvent Report_leader("Green"))
                    treport_leader.Start()
                End If
            Loop
            G.FillRectangle(Green_Cir, Xg, 170, 50, 50)
            Dim trace_over As New Threading.Thread(Sub() RaiseEvent Race_over("Green"))
            trace_over.Start()
        Catch ex As Exception
            G.FillRectangle(Green_Cir, Xg, 170, 50, 50)
        End Try

    End Sub

    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Dim G As Graphics
        Dim black_Cir As Brush
        Dim Green_Cir As Brush
        Dim p As Pen

        G = Me.CreateGraphics
        p = New Pen(Brushes.Black, 2)
        black_Cir = New SolidBrush(Color.Black)
        Green_Cir = New SolidBrush(Color.Green)
        G.FillRectangle(black_Cir, 100, 100, 50, 50)
        G.FillRectangle(Green_Cir, 100, 170, 50, 50)
        G.DrawLine(p, 1200, 80, 1200, 270)


    End Sub
End Class

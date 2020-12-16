Public Class minigame

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        Randomize()
        Rnd()

        Dim dir As Integer
        Dim dis As Integer
        Dim spd As Integer

        With Me.Button1

            dir = Int(Rnd() * 4)
            dis = Int(Rnd() * 30)
            spd = Int(Rnd() * 10)

            Select Case dir
                Case 1
                    .Left += dis
                Case 2
                    .Left -= dis
                Case 3
                    .Top += dis
                Case Else
                    .Top -= dis
            End Select

            If .Left <= 0 Then
                .Left = Me.Width - .Width - 5
            End If

            If .Left >= (Me.Width - .Width) Then .Left = 5
            If .Top <= 0 Then .Height = Me.Height - .Height - 5
            If .Top >= (Me.Height - .Height) Then .Height = 5

            Me.Timer1.Interval = spd * 10 + 1

        End With

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        If Me.Timer1.Enabled Then
            MsgBox("Damn you got me")
            Me.Button1.Text = "Again?"
            Timer1.Enabled = False
        Else
            Me.Button1.Text = "Your It"
            Timer1.Enabled = True
        End If


    End Sub

    Private Sub minigame_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.Button1.Text = "Your It"

    End Sub

End Class
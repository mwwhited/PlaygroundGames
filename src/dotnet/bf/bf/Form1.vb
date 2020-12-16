Public Class Form1

    Private bf As New bf_processor

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        bf.setProgram(Me.tbProgram.Text.Trim)
        bf.setInput(Me.tbInputs.Text.Trim)

        bf.runProgram()

        Me.tbOutput.Text = bf.getOutput

    End Sub

    Private Sub MenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem2.Click

        Dim fiDia As New SaveFileDialog
        fiDia.Filter = "BrainF*ck (*.bf)|*.bf|Text File (*.txt)|*.txt|All Files (*.*)|*.*"

        If fiDia.ShowDialog = DialogResult.OK Then
            Try
                Dim objWriter As New System.IO.StreamWriter(fiDia.FileName)
                objWriter.Write(Me.tbProgram.Text)
                objWriter.Close()
            Catch Ex As Exception
                MsgBox(Ex.Message)
            End Try
        End If

    End Sub

    Private Sub MenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem3.Click

        Dim fiDia As New OpenFileDialog
        fiDia.Filter = "BrainF*ck (*.bf)|*.bf|Text File (*.txt)|*.txt|All Files (*.*)|*.*"

        If fiDia.ShowDialog = DialogResult.OK Then
            Try
                Dim objReader As New System.IO.StreamReader(fiDia.FileName)
                Me.tbProgram.Text = objReader.ReadToEnd()
                objReader.Close()
            Catch Ex As Exception
                MsgBox(Ex.Message)
            End Try
        End If

    End Sub

    Private Sub MenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem4.Click

        Dim mesg As String
        mesg = "BrainF*ck Windows .Net 2.0" + vbCrLf + _
            "Matthew W. Whited" + vbCrLf + _
            "matt@whited.us"
        MsgBox(mesg)

    End Sub

End Class

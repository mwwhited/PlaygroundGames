Public Class intlist

    Private blocks(0 To 9) As Boolean

    Private Sub intlist_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim outString As String = ""
        Dim x As Integer

        For x = 1 To 9
            'If Int(Rnd() * 2) = 1 Then
            blocks(x) = False
            'Else
            'blocks(x) = True
            'End If
        Next

        outString += addends()
        
        TextBox1.Text = outString

    End Sub

    Private Function count_pips() As Integer
        Return 12
    End Function

#Region "UniqueInteger"

    Private Function uniqueInteger(ByVal inVar As String, ByVal newVar As String) As String

        Dim a As Array
        Dim b As String
        Dim outString As String = inVar + " "

        a = Split(newVar)

        For Each b In a
            If InStr(outString, b) <= 0 Then
                outString += b + " "
            End If
        Next

        outString = Trim(outString)
        a = Split(outString)

        Return outString

    End Function

    Public Function uniqueInteger(ByVal inVar As String) As String

        Dim a As Array = Split(inVar)
        Dim b As String
        Dim outstring As String

        For Each b In a
            If InStr(outstring, b) <= 0 Then
                outstring += b + " "
            End If
        Next

        Return outstring

    End Function

#End Region

#Region "Addends"

    Private Function addends() As String

        Dim start As String = count_pips.ToString
        Dim ok As Boolean = False
        Dim last As String = ""

        Do Until ok
            start = uniqueInteger(addends(start))
            If last = start Then ok = True
            last = start
        Loop

        Return start

    End Function

    Private Function addends(ByVal sum As Integer) As String
        Dim y, z As Integer
        Dim outstring As String = ""
        For y = 9 To 0 Step -1
            If Not blocks(y) Then
                For z = 9 To y + 1 Step -1
                    If Not blocks(z) Then
                        If y + z = sum Then
                            outstring += y.ToString + " " + z.ToString + " "
                        End If
                    End If
                Next
            End If
        Next

        Return outstring

    End Function

    Private Function addends(ByVal sumList As String) As String
        Dim b, outstring As String
        Dim a As Array
        Dim z As Integer

        a = Split(sumList)

        For Each b In a
            z = Val(b)
            outstring += addends(z)
        Next

        Return outstring

    End Function

#End Region

End Class
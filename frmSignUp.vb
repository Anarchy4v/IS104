Imports MySql.Data.MySqlClient

Public Class frmSignUp
    Private connectionString As String = "server=127.0.0.1;userid=root;password='';database=employees"

    Private formClosingByButton As Boolean = False
    Private formClosingBySystem As Boolean = False


    Private Sub btnSignUp_Click(sender As Object, e As EventArgs) Handles btnSignUp.Click
        Try
            ' existing shit out of me.
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Sub Clear()
        txtConfirm.Clear()
        txtPassword.Clear()
        txtEmail.Clear()
    End Sub

    Private Sub txtEmail_TextChanged(sender As Object, e As EventArgs) Handles txtEmail.TextChanged

    End Sub

    Private Sub txtPassword_TextChanged(sender As Object, e As EventArgs) Handles txtPassword.TextChanged

    End Sub

    Private Sub txtConfirm_TextChanged(sender As Object, e As EventArgs) Handles txtConfirm.TextChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        formClosingByButton = True
        Me.Close()
    End Sub

    Private Sub frmSignUp_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If formClosingByButton Then
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to cancel registration?", "Cancel Register", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            formClosingByButton = False

            e.Cancel = (result = DialogResult.No)
        ElseIf Not formClosingBySystem Then
            Dim result As DialogResult = MessageBox.Show("Are you sure to cancel process?", "Cancel Process", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            e.Cancel = (result = DialogResult.No)
        End If
    End Sub
End Class

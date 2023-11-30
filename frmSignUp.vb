Imports MySql.Data.MySqlClient

Public Class frmSignUp
    Private connectionString As String = "server=127.0.0.1;userid=root;password='';database=employees"
    Private cn As MySqlConnection
    Private cm As MySqlCommand

    Private Sub btnSignUp_Click(sender As Object, e As EventArgs) Handles btnSignUp.Click
        Try
            If String.IsNullOrEmpty(txtEmail.Text) Then Return
            If String.IsNullOrEmpty(txtPassword.Text) Then Return
            If String.IsNullOrEmpty(txtConfirm.Text) Then Return
            If txtPassword.Text <> txtConfirm.Text Then
                MsgBox("Confirm Password did not match!", vbCritical)
                Return
            ElseIf MsgBox("Are you sure you want to save this account?", vbYesNo + vbQuestion) = vbYes Then
                Using cn = New MySqlConnection(connectionString)
                    cn.Open()
                    Using cm = New MySqlCommand("INSERT INTO tbluser (username, password) VALUES (@username, @password)", cn)
                        cm.Parameters.AddWithValue("@username", txtEmail.Text)
                        cm.Parameters.AddWithValue("@password", txtPassword.Text)
                        cm.ExecuteNonQuery()
                    End Using
                End Using

                MsgBox("New account has been successfully created (DBCONNECTED).", vbInformation)
                Clear()
            End If

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
End Class

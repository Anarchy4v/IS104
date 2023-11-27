Imports MySql.Data.MySqlClient
Public Class frmSignUp
    Private cn As Object
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
                cn.Open()
                cm = New MySqlCommand("insert into tbluser (username, password) values(@username, @password)", cn)
                cm.Parameters.AddWithValue("@username", txtEmail.Text)
                cm.Parameters.AddWithValue("@password", txtPassword.Text)
                cm.ExecuteNonQuery()
                cn.Close()
                MsgBox("New account has been successfully created.", vbInformation)
                Clear()
            End If

        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Sub Clear()
        txtConfirm.Clear()
        txtPassword.Clear()
        txtEmail.Clear()
    End Sub
End Class
﻿Imports MySql.Data.MySqlClient

Public Class frmSignUp
    Private connectionString As String = "server=127.0.0.1;userid=root;password='';database=employees"

    Private formClosingByButton As Boolean = False
    Private formClosingBySystem As Boolean = False

    Private Function IsValidEmail(email As String) As Boolean
        Try
            Dim addr As New System.Net.Mail.MailAddress(email)
            Return addr.Address = email
        Catch
            Return False
        End Try
    End Function

    Private Sub btnSignUp_Click(sender As Object, e As EventArgs) Handles btnSignUp.Click
        Try
            If Not IsValidEmail(txtEmail.Text) Then
                MsgBox("Please enter a valid email address.", vbExclamation)
                Return
            End If

            If txtPassword.Text <> txtConfirm.Text Then
                MsgBox("Passwords do not match. Please re-enter.", vbExclamation)
                Return
            End If

            Using connection As MySqlConnection = New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "INSERT INTO tbluser (username, password, email) VALUES (@username, @password, @email);"
                Using command As MySqlCommand = New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@username", "your_username") ' Replace with the actual username
                    command.Parameters.AddWithValue("@password", txtPassword.Text)
                    command.Parameters.AddWithValue("@email", txtEmail.Text)

                    command.ExecuteNonQuery()

                    MsgBox("Registration successful!", vbInformation)

                    ' Clear the form
                    Clear()
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Sub Clear()
        txtConfirm.Clear()
        txtPassword.Clear()
        txtEmail.Clear()
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

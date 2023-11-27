Imports System.Security.Policy
Imports MySql.Data.MySqlClient
Public Class Form1
    Private cn As Object
    Private cm As Object
    Private dr As Object
    Private strUser As String
    Private strPass As String
    Private ex As Object

    Private Sub btnAccount_Click(sender As Object, e As EventArgs) Handles btnAccount.Click
        With frmSignUp
            .ShowDialog()
        End With
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Try
            Dim found As Boolean = False
            If String.IsNullOrEmpty(txtUser.Text) Then Return
            If String.IsNullOrEmpty(txtPass.Text) Then Return
            cn.Open()
            cm = New MySqlCommand("select * from tbluser where username like '" & txtUser.Text & "' and password like '" & txtPass.Text & "'", cn)
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                strUser = dr.Item("username").ToString
                strPass = dr.Item("password").ToString
                found = True
            Else
                strPass = ""
                strUser = ""
                found = False
            End If
            dr.Close()
            cn.Close()
            If found = True Then
                txtPass.Clear()
                txtUser.Clear()
            Else
                MsgBox("Access Denied! Invalid username or password.", vbExclamation)
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Function MySqlCommand(v As String, cn As Object) As Object
        Throw New NotImplementedException()
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Connection()
    End Sub

    Private Sub Connection()
        Throw New NotImplementedException()
    End Sub
End Class

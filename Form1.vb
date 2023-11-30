Imports MySql.Data.MySqlClient

Public Class Form1
    Private connectionString As String = "server=127.0.0.1;userid=root;password='';database=employees"
    Private connection As MySqlConnection
    Private command As MySqlCommand
    Private reader As MySqlDataReader
    Private strUser As String
    Private strPass As String

    Private Sub btnAccount_Click(sender As Object, e As EventArgs) Handles btnAccount.Click
        Dim signUpForm As New frmSignUp()
        signUpForm.ShowDialog()
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Try
            Dim found As Boolean = False
            If String.IsNullOrEmpty(txtUser.Text) Then Return
            If String.IsNullOrEmpty(txtPass.Text) Then Return

            Using connection = New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT * FROM tbluser WHERE username = @username AND password = @password"
                Using command = New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@username", txtUser.Text)
                    command.Parameters.AddWithValue("@password", txtPass.Text)

                    Using reader = command.ExecuteReader()
                        If reader.Read() Then
                            strUser = reader("username").ToString()
                            strPass = reader("password").ToString()
                            found = True
                        Else
                            strPass = ""
                            strUser = ""
                            found = False
                        End If
                    End Using
                End Using
            End Using

            If found Then
                MsgBox("Access Granted!.", vbExclamation)
                ' access the fucking dashboard here
                Dim dashForm As New Dash()
                dashForm.Show()
                Me.Hide()
            Else
                MsgBox("Access Denied! Invalid username or password.", vbExclamation)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' No need to call Connection() separately; the connection is now created inside the btnLogin_Click method.
    End Sub
End Class

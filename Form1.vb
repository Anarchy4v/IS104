Imports MySql.Data.MySqlClient

Public Class Form1
    Private connectionString As String = "server=127.0.0.1;userid=root;password='';database=tgp_db"
    Private connection As MySqlConnection
    Private command As MySqlCommand
    Private reader As MySqlDataReader
    Private strEmail As String
    Private strPass As String

    Private Sub btnAccount_Click(sender As Object, e As EventArgs) Handles btnAccount.Click
        Dim signUpForm As New frmSignUp()
        signUpForm.Show()
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Try
            Dim found As Boolean = False
            If String.IsNullOrEmpty(txtUser.Text) Then Return
            If String.IsNullOrEmpty(txtPass.Text) Then Return

            Using connection = New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT * FROM usercredentials WHERE email = @email AND password = @password;"
                Using command = New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@email", txtUser.Text)
                    command.Parameters.AddWithValue("@password", txtPass.Text)

                    Using reader = command.ExecuteReader()
                        If reader.Read() Then
                            strEmail = reader("email").ToString()
                            strPass = reader("password").ToString()
                            found = True
                        Else
                            strPass = ""
                            strEmail = ""
                            found = False
                        End If
                    End Using
                End Using
            End Using

            If found Then
                MsgBox("Access Granted!.")
                My.Settings.Email = strEmail
                My.Settings.Password = strPass
                My.Settings.Save()

                Dim dashForm As New Dash()
                dashForm.UserEmailProperty = strEmail

                dashForm.Show()
                Me.Hide()
            Else
                MsgBox("Access Denied! Invalid email or password.", vbExclamation)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not String.IsNullOrEmpty(My.Settings.Email) Then
            txtUser.Text = My.Settings.Email
            txtPass.Text = My.Settings.Password
        End If
    End Sub

End Class

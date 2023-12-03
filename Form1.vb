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
        signUpForm.Show()
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Try
            Dim found As Boolean = False
            If String.IsNullOrEmpty(txtUser.Text) Then Return
            If String.IsNullOrEmpty(txtPass.Text) Then Return

            Using connection = New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT * FROM tbluser WHERE (username = @username AND password = @password) OR (email = @email AND password = @password);"
                Using command = New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@username", txtUser.Text)
                    command.Parameters.AddWithValue("@email", txtUser.Text)
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
                MsgBox("Access Granted!.")
                ' Save credentials if the checkbox is checked
                If CheckBox1.Checked Then
                    My.Settings.Username = strUser
                    My.Settings.Password = strPass
                    My.Settings.Save()
                End If

                ' Access the dashboard here
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
        ' Load saved credentials
        If Not String.IsNullOrEmpty(My.Settings.Username) Then
            txtUser.Text = My.Settings.Username
            txtPass.Text = My.Settings.Password
            CheckBox1.Checked = True
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs)
        ' Save credentials when the checkbox is checked
        If CheckBox1.Checked Then
            My.Settings.Username = txtUser.Text
            My.Settings.Password = txtPass.Text
        Else
            ' Clear saved credentials when the checkbox is unchecked
            My.Settings.Username = ""
            My.Settings.Password = ""
        End If

        ' Save changes to My.Settings
        My.Settings.Save()
    End Sub
End Class

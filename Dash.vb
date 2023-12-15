Imports MySql.Data.MySqlClient
Imports Mysqlx

Public Class Dash
    Private connectionString As String = "server=127.0.0.1;userid=root;password='';database=tgp_db"
    Dim random As New Random()
    Private formClosingByButton As Boolean = False
    Private formClosingBySystem As Boolean = False

    Private Sub LoadStockAlert()
        Try
            Using connection As MySqlConnection = New MySqlConnection(connectionString)
                connection.Open()

                ' Modify the query to retrieve rows where @item_qty is less than or equal to 50
                Dim query As String = "SELECT item_name, item_qty, item_dosage, category, item_price FROM Inventory WHERE item_qty <= 50"

                Using command As MySqlCommand = New MySqlCommand(query, connection)
                    Using reader As MySqlDataReader = command.ExecuteReader()
                        Dim dataTable As New DataTable()
                        dataTable.Load(reader)

                        If dataTable.Rows.Count > 0 Then
                            DataGridView1.DataSource = dataTable
                        Else
                            DataGridView1.DataSource = Nothing
                        End If
                    End Using
                End Using

                Dim userEmail As String = GetUserEmail(connection)
                Label2.Text = userEmail
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading medicines: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadBestSellers()
        Try
            Using connection As MySqlConnection = New MySqlConnection(connectionString)
                connection.Open()
                Dim query As String = "SELECT item_name, item_qty, item_dosage, category, item_price FROM Inventory WHERE item_qty <= 400"

                Using command As MySqlCommand = New MySqlCommand(query, connection)
                    Using reader As MySqlDataReader = command.ExecuteReader()
                        Dim dataTable As New DataTable()
                        dataTable.Load(reader)

                        If dataTable.Rows.Count > 0 Then
                            DataGridView2.DataSource = dataTable
                        Else
                            DataGridView2.DataSource = Nothing
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading medicines: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Dash_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
        LoadStockAlert()
        LoadBestSellers()
        Me.AcceptButton = Nothing
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'dashboard active
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'point of sales
        Dim sales As New Sales()
        sales.Show()
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'inventory
        Dim inv As New Inventory()
        inv.Show()
        Me.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim SalesReport As New SalesReport()
        SalesReport.Show()
        Me.Close()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)
        'settings
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to log out?", "Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            Me.Close()
            Form1.Show()
        End If
    End Sub

    Private Function GetOrderDetails() As DataTable
        ' Replace this with the actual code to retrieve order details from your database or another source
        Try
            Dim connectionString As String = "server=127.0.0.1;userid=root;password='';database=tgp_db"

            Using connection As MySqlConnection = New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT * FROM OrderDetails"

                Using command As MySqlCommand = New MySqlCommand(query, connection)
                    Using reader As MySqlDataReader = command.ExecuteReader()
                        Dim dataTable As New DataTable()
                        dataTable.Load(reader)

                        Return dataTable
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error retrieving order details: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' Generate syempre live count haha
        Dim randomNumber As Integer = random.Next(1, 301)
        Dim randomNumber1 As Integer = random.Next(1, 401)
        Dim randomNumber2 As Integer = random.Next(1, 501)
        Dim randomNumber3 As Integer = random.Next(1, 201)

        Label7.Text = randomNumber.ToString()
        Label8.Text = randomNumber1.ToString()
        Label9.Text = randomNumber2.ToString()
        Label10.Text = randomNumber3.ToString()
    End Sub

    Private Function GetUserEmail(connection As MySqlConnection) As String
        ' Fetch the email from the usercredentials table
        Dim userEmail As String = String.Empty

        Dim query As String = "SELECT email FROM usercredentials WHERE user_id = @userId"
        Using command As MySqlCommand = New MySqlCommand(query, connection)
            ' Assuming you have a user_id associated with the current user
            ' Replace 1 with the actual user_id or parameterize it as needed
            command.Parameters.AddWithValue("@userId", 1)

            Using reader As MySqlDataReader = command.ExecuteReader()
                If reader.Read() Then
                    userEmail = reader("email").ToString()
                End If
            End Using
        End Using

        Return userEmail
    End Function

End Class